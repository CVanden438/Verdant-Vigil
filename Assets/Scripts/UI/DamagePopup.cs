using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private float duration = 3;
    private float speed = 1f;
    private float DOTspeed = 0.5f;
    public bool isDOT;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        switch (isDOT)
        {
            case true:
                transform.position += new Vector3(0, DOTspeed) * Time.deltaTime;
                GetComponent<TextMeshPro>().alpha -= Time.deltaTime / duration;
                GetComponent<TextMeshPro>().fontSize = 3;
                duration -= Time.deltaTime;
                if (duration < 0)
                {
                    Destroy(gameObject);
                }
                break;
            case false:
                transform.position += new Vector3(0, speed) * Time.deltaTime;
                GetComponent<TextMeshPro>().alpha -= Time.deltaTime / duration;
                GetComponent<TextMeshPro>().fontSize = 6;
                duration -= Time.deltaTime;
                if (duration < 0)
                {
                    Destroy(gameObject);
                }
                break;
        }
    }
}
