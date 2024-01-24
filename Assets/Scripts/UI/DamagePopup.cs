using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private float duration = 3;
    private float speed = 1f;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, speed) * Time.deltaTime;
        GetComponent<TextMeshPro>().alpha -= Time.deltaTime / duration;
        duration -= Time.deltaTime;
        if (duration < 0)
        {
            Destroy(gameObject);
        }
    }
}
