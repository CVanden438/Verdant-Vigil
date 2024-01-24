using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopupHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject popupObject;
    private GameObject damageObject;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void DisplayDamage(int damageAmount)
    {
        damageObject = Instantiate(popupObject, transform);
        damageObject.GetComponent<TextMeshPro>().SetText(damageAmount.ToString());
    }
}
