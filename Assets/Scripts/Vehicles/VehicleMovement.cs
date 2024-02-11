using Cainos.PixelArtTopDown_Basic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    public float driftFactor = 0.95f;
    public float accelerationFactor = 30f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 20f;
    float accelerationInput = 0;
    float steeringInput = 0;
    float rotationAngle = 0;
    float velocityVsUp = 0;
    bool isActive = false;
    bool isEnterable = false;
    Rigidbody2D carRigidbody2D;

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject mainCam;

    void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
        carRigidbody2D.drag = 1000;
        carRigidbody2D.angularDrag = 1000;
        // rotationAngle = transform.rotation.eulerAngles.z;
    }

    void Update()
    {
        if (isEnterable && !isActive)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                EnterVehicle();
                return;
            }
        }
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ExitVehicle();
                return;
            }
        }
    }

    void FixedUpdate()
    {
        if (isActive)
        {
            Vector2 inputVector = Vector2.zero;
            inputVector.x = Input.GetAxis("Horizontal");
            inputVector.y = Input.GetAxis("Vertical");
            SetInputVector(inputVector);
            ApplyEngineForce();
            //remove to be "on ice"
            KillOrthogonalVelocity();
            ApplySteering();
        }
    }

    void EnterVehicle()
    {
        EnemyManager.instance.ChangeTarget(gameObject);
        GetComponent<Rigidbody2D>().drag = 0;
        GetComponent<Rigidbody2D>().angularDrag = 0.1f;
        player.SetActive(false);
        mainCam.GetComponent<CameraFollow>().target = transform;
        // player.GetComponent<SpriteRenderer>().enabled = false;
        isActive = true;
    }

    void ExitVehicle()
    {
        EnemyManager.instance.ChangeTarget(player);
        GetComponent<Rigidbody2D>().drag = 1000;
        GetComponent<Rigidbody2D>().angularDrag = 1000;
        player.SetActive(true);
        isActive = false;
        mainCam.GetComponent<CameraFollow>().target = player.transform;
        player.transform.position = transform.position;
    }

    void ApplyEngineForce()
    {
        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);
        if (velocityVsUp > maxSpeed && accelerationInput > 0)
        {
            return;
        }
        if (velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0)
        {
            return;
        }
        if (carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
        {
            return;
        }
        if (accelerationInput == 0)
        {
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 3.0f, Time.fixedDeltaTime * 3);
        }
        else
        {
            carRigidbody2D.drag = 0;
        }
        Vector2 engineForceVector = accelerationFactor * accelerationInput * transform.up;
        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        float minSpeedBeforeAllowTurningFactor = carRigidbody2D.velocity.magnitude / 8;
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);
        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;
        carRigidbody2D.MoveRotation(rotationAngle);
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

    void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity =
            transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);
        carRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            isEnterable = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            isEnterable = false;
        }
    }
}
