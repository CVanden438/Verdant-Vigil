using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTowerVisual : MonoBehaviour, ITowerVisual
{
    public LineRenderer[] lineRenderers;
    public List<GameObject> enemyTransforms;
    public float laserWidth = 0.1f;

    public void VisualEffect(List<GameObject> targets)
    {
        enemyTransforms = targets;
    }

    // Start is called before the first frame update
    void Start()
    {
        lineRenderers = GetComponentsInChildren<LineRenderer>();
        foreach (var line in lineRenderers)
        {
            line.startWidth = laserWidth;
            line.endWidth = laserWidth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if (enemyTransforms == null)
        // {
        foreach (var line in lineRenderers)
        {
            line.enabled = false;
        }
        // }
        if (enemyTransforms != null)
        {
            // enemyTransforms.RemoveAll(item => item == null);
            for (int i = 0; i < enemyTransforms.Count; i++)
            {
                if (enemyTransforms[i] != null)
                {
                    Debug.Log("ENEMIES" + " " + enemyTransforms.Count);
                    lineRenderers[i].enabled = true;
                    lineRenderers[i].SetPosition(0, transform.position);
                    lineRenderers[i].SetPosition(1, enemyTransforms[i].transform.position);
                }
                else
                {
                    lineRenderers[i].enabled = false;
                }
            }
        }
    }
}
