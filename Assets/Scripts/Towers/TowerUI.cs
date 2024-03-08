using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUI : MonoBehaviour
{
    public GameObject highlight;

    void OnMouseDown()
    {
        highlight.SetActive(true);
    }
}
