using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingController : MonoBehaviour
{
    [SerializeField]
    private TowerSO[] towers;
    private TowerSO chosenTower;

    [SerializeField]
    private WallSO[] walls;
    private WallSO chosenWall;
    private BuildingSO selectedBuilding;

    [SerializeField]
    private ResourceManager rm;

    [SerializeField]
    private GameObject highlight;
    private bool isBuilding = false;

    [SerializeField]
    // private AstarPath astarPath;

    private void Update()
    {
        if (isBuilding)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                PlaceStructure(point);
                return;
            }
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GetXY(mouse, out float x, out float y);
            highlight.transform.position = new Vector3(x, y);
            var col = CheckCollision(x, y, selectedBuilding.width, selectedBuilding.height);
            if (col)
            {
                highlight.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                highlight.GetComponent<SpriteRenderer>().color = Color.green;
            }
        }
    }

    private void PlaceStructure(Vector3 point)
    {
        GameObject obj = null;
        GetXY(point, out float x, out float y);
        if (CheckCollision(x, y, selectedBuilding.width, selectedBuilding.height))
        {
            return;
        }
        if (selectedBuilding != null)
        {
            // TowerController buildingController = chosenTower.GetComponent<TowerController>();
            // if (rm.crystals >= buildingController.data.crystalCost)
            // {
            obj = Instantiate(selectedBuilding.prefab, new Vector3(x, y), Quaternion.identity);
            // rm.RemoveCrystals(buildingController.data.crystalCost);
            // }
            selectedBuilding = null;
            isBuilding = false;
            highlight.SetActive(false);
        }
        if (obj != null)
        {
            //rescan AStar graph
            Bounds bounds = obj.GetComponent<BoxCollider2D>().bounds;
            var guo = new GraphUpdateObject(bounds);
            guo.updatePhysics = true;
            AstarPath.active.UpdateGraphs(guo);
        }
    }

    private void GetXY(Vector3 worldPosition, out float x, out float y)
    {
        x = Mathf.Floor(worldPosition.x);
        y = Mathf.Floor(worldPosition.y);
    }

    private bool CheckCollision(float x, float y, float width, float height)
    {
        Collider2D collisions = Physics2D.OverlapArea(
            new Vector2(x + 0.1f, y + height - 1 + 0.9f),
            new Vector2(x + width - 1 + 0.9f, y + 0.1f),
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
    public void SelectWall(int index)
    {
        // chosenWall = walls[index];
        selectedBuilding = walls[index];
        chosenTower = null;
        isBuilding = true;
        highlight.SetActive(true);
        highlight.GetComponent<SpriteRenderer>().sprite = walls[index].sprite;
    }

    public void SelectTower(int index)
    {
        // chosenTower = towers[index];
        selectedBuilding = towers[index];
        chosenWall = null;
        isBuilding = true;
        highlight.SetActive(true);
        highlight.GetComponent<SpriteRenderer>().sprite = towers[index].sprite;
    }
}
