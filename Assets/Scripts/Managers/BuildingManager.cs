using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Unity.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager instance;

    [SerializeField]
    private TowerSO[] towers;

    [SerializeField]
    private WallSO[] walls;
    private BuildingSO selectedBuilding;
    public GameObject highlightedBuilding;

    [SerializeField]
    private ResourceManager rm;

    [SerializeField]
    private GameObject highlight;
    public bool isBuilding = false;
    private bool isUpgrading = false;

    private void Awake()
    {
        instance = this;
    }

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
                PlaceStructure(point, selectedBuilding);
                return;
            }
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GetXY(mouse, out float x, out float y);
            highlight.transform.position = new Vector3(x, y);
            var col = CheckCollision(
                x - 0.5f,
                y - 0.5f,
                selectedBuilding.width,
                selectedBuilding.height
            );
            if (col)
            {
                highlight.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else
            {
                highlight.GetComponent<SpriteRenderer>().color = Color.green;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
        }
    }

    private GameObject PlaceStructure(Vector3 point, BuildingSO building)
    {
        GetXY(point, out float x, out float y);
        if (!isUpgrading)
        {
            if (CheckCollision(x - 0.5f, y - 0.5f, building.width, building.height))
            {
                return null;
            }
        }
        GameObject obj = Instantiate(building.prefab, new Vector3(x, y), Quaternion.identity);
        selectedBuilding = null;
        isBuilding = false;
        highlight.SetActive(false);

        //for adding building to obstacles
        if (obj != null)
        {
            //rescan AStar graph
            // Bounds bounds = obj.GetComponent<BoxCollider2D>().bounds;
            // var guo = new GraphUpdateObject(bounds);
            // guo.updatePhysics = true;
            // AstarPath.active.UpdateGraphs(guo);
        }
        return obj;
    }

    private void GetXY(Vector3 worldPosition, out float x, out float y)
    {
        x = Mathf.Floor(worldPosition.x) + 0.5f;
        y = Mathf.Floor(worldPosition.y) + 0.5f;
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
        isBuilding = true;
        highlight.SetActive(true);
        highlight.GetComponent<SpriteRenderer>().sprite = walls[index].sprite;
    }

    public void SelectBuilding(BuildingSO building)
    {
        // chosenTower = towers[index];
        selectedBuilding = building;
        isBuilding = true;
        highlight.SetActive(true);
        highlight.GetComponent<SpriteRenderer>().sprite = building.sprite;
    }

    public void UpgradeBuilding()
    {
        TowerSO upgrade = highlightedBuilding.GetComponent<TowerController>().GetData().upgrade;
        Vector3 pos = highlightedBuilding.transform.position;
        Destroy(highlightedBuilding);
        isUpgrading = true;
        var newBuilding = PlaceStructure(pos, upgrade);
        isUpgrading = false;
        // UIManager.instance.buildingName.text = upgrade.buildingName;
        // if (upgrade.tier == 3)
        // {
        //     UIManager.instance.upgradeButton.SetActive(false);
        //     UIManager.instance.maxUpgradeButtons.SetActive(true);
        // }
        highlightedBuilding = newBuilding;
        UIManager.instance.ShowTowerPanel(upgrade);
        // highlightedBuilding = null;
    }

    public void MaxUpgrade(int index)
    {
        TowerSO upgrade = highlightedBuilding.GetComponent<TowerController>().GetData().maxUpgrades[
            index
        ];
        Vector3 pos = highlightedBuilding.transform.position;
        Destroy(highlightedBuilding);
        isUpgrading = true;
        var newBuilding = PlaceStructure(pos, upgrade);
        isUpgrading = false;
        highlightedBuilding = newBuilding;
        UIManager.instance.ShowTowerPanel(upgrade);
        //need this to hide max upgrade tooltip as onmouseleave cant be called
        TooltipManager.instance.HideTowerTooltip();
    }
}
