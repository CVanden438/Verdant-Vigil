using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] towers;
    private GameObject chosenTower;

    [SerializeField]
    private GameObject[] walls;
    private GameObject chosenWall;

    [SerializeField]
    private ResourceManager rm;

    [SerializeField]
    private GameObject highlight;
    private bool isBuilding = false;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            PlaceStructure(point);
        }
        if (isBuilding)
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GetXY(mouse, out float x, out float y);
            highlight.transform.position = new Vector3(x, y);
        }
    }

    private void PlaceStructure(Vector3 point)
    {
        GetXY(point, out float x, out float y);
        if (CheckCollision(x, y))
        {
            return;
        }
        if (chosenTower != null)
        {
            TowerController buildingController = chosenTower.GetComponent<TowerController>();
            if (rm.crystals >= buildingController.data.crystalCost)
            {
                Instantiate(chosenTower, new Vector3(x, y), Quaternion.identity);
                rm.RemoveCrystals(buildingController.data.crystalCost);
            }
            chosenTower = null;
            isBuilding = false;
            highlight.SetActive(false);
        }
        if (chosenWall != null)
        {
            WallController wallController = chosenWall.GetComponent<WallController>();
            if (rm.crystals >= wallController.crystalCost)
            {
                Instantiate(chosenWall, new Vector3(x, y), Quaternion.identity);
                rm.RemoveCrystals(wallController.crystalCost);
            }
            highlight.SetActive(false);
            chosenWall = null;
            isBuilding = false;
        }
    }

    private void GetXY(Vector3 worldPosition, out float x, out float y)
    {
        x = Mathf.Floor(worldPosition.x);
        y = Mathf.Floor(worldPosition.y);
    }

    private bool CheckCollision(float x, float y)
    {
        Collider2D collisions = Physics2D.OverlapArea(
            new Vector2(x + 0.1f, y + 0.9f),
            new Vector2(x + 0.9f, y + 0.1f),
            layerMask: Physics.DefaultRaycastLayers,
            minDepth: 0,
            maxDepth: 100
        );
        if (collisions)
        {
            if (collisions.isTrigger)
            {
                return false;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    // private Collider2D GetCollider(GameObject obj){
    //     return obj.GetComponent<Collider2D>();
    // }
    public void SelectWall()
    {
        chosenWall = walls[0];
        chosenTower = null;
        isBuilding = true;
        highlight.SetActive(true);
    }

    public void SelectTower()
    {
        chosenTower = towers[0];
        chosenWall = null;
        isBuilding = true;
        highlight.SetActive(true);
    }
}
