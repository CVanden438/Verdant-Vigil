using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBar : MonoBehaviour
{
    public List<GameObject> buttons;
    public List<GameObject> lists;

    public void Toggle(int index)
    {
        for (int i = 0; i < lists.Count; i++)
        {
            if (i != index)
            {
                lists[i].SetActive(false);
            }
            else
            {
                lists[index].SetActive(!lists[index].activeInHierarchy);
            }
        }
    }
}
