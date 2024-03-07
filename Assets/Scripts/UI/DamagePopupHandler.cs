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
    void Start()
    {
        if (TryGetComponent<HealthController>(out var health))
        {
            health.OnDamaged += DisplayDamage;
            health.OnDOT += DisplayDOT;
        }
    }

    // Update is called once per frame
    void Update() { }

    public void DisplayDamage(float damageAmount)
    {
        damageObject = Instantiate(popupObject, transform);
        damageObject.GetComponent<TextMeshPro>().SetText(damageAmount.ToString());
    }

    public void DisplayDOT(float damageAmount)
    {
        damageObject = Instantiate(popupObject, transform);
        damageObject.GetComponent<DamagePopup>().isDOT = true;
        damageObject.GetComponent<TextMeshPro>().SetText(damageAmount.ToString());
    }
}
