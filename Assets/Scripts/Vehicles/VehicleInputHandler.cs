using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleInputHandler : MonoBehaviour
{
    VehicleMovement vehicleMovement;

    // Start is called before the first frame update
    void Awake()
    {
        vehicleMovement = GetComponent<VehicleMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");
        vehicleMovement.SetInputVector(inputVector);
    }
}
