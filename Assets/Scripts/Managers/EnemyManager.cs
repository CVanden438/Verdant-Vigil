using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public List<GameObject> enemies;

    private void Awake()
    {
        instance = this;
    }

    void Start() { }

    void Update() { }

    public void ChangeTarget(GameObject newTarget)
    {
        foreach (GameObject enemy in enemies)
        {
            // enemy.GetComponent<EnemyMovement>().target = newTarget.transform;
        }
    }
}
