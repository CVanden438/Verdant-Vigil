using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MeleeSwing : MonoBehaviour
{
    bool rotating = false;
    public GameObject objectToRotate;
    public GameObject referenceRotation;
    public GameObject referenceWeapon;
    public GameObject meleeWeapon;

    void Start() { }

    public void SwingAnimation(WeaponSO weaponData)
    {
        meleeWeapon.GetComponent<SpriteRenderer>().sprite = weaponData.icon;
        StartCoroutine(SwingWeapon(objectToRotate, weaponData.meleeCooldown));
    }

    public void StabAnimation(WeaponSO weaponData)
    {
        meleeWeapon.GetComponent<SpriteRenderer>().sprite = weaponData.icon;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - referenceWeapon.transform.position).normalized;
        StartCoroutine(StabWeapon(objectToRotate, weaponData.meleeCooldown, direction));
    }

    IEnumerator SwingWeapon(GameObject gameObjectToMove, float duration)
    {
        meleeWeapon.SetActive(true);
        Quaternion rotation2 = Quaternion.Euler(
            new Vector3(0, 0, referenceRotation.transform.eulerAngles.z - 45)
        );
        if (rotating)
        {
            yield break;
        }
        rotating = true;

        // Quaternion currentRot = gameObjectToMove.transform.rotation;
        Quaternion currentRot = Quaternion.Euler(
            new Vector3(0, 0, referenceRotation.transform.eulerAngles.z + 45)
        );

        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            gameObjectToMove.transform.rotation = Quaternion.Lerp(
                currentRot,
                rotation2,
                counter / duration
            );
            yield return null;
        }
        rotating = false;
        meleeWeapon.SetActive(false);
    }

    IEnumerator StabWeapon(GameObject gameObjectToMove, float duration, Vector2 direction)
    {
        meleeWeapon.SetActive(true);
        Quaternion currentRot = Quaternion.Euler(
            new Vector3(0, 0, referenceRotation.transform.eulerAngles.z)
        );
        objectToRotate.transform.rotation = currentRot;
        // Vector2 currentPos = referenceWeapon.transform.position;
        var endPos = referenceRotation.transform.localPosition + new Vector3(1, 0);
        if (rotating)
        {
            yield break;
        }
        rotating = true;

        var startPos = referenceRotation.transform.localPosition;
        // Quaternion currentRot = gameObjectToMove.transform.rotation;
        // meleeWeapon.transform.position = currentPos;
        float counter = 0;
        while (counter < duration / 4)
        {
            counter += Time.deltaTime;
            meleeWeapon.transform.localPosition = Vector2.Lerp(
                // referenceWeapon.transform.position,
                // referenceWeapon.transform.position + new Vector3(direction.x * 5, direction.y * 5),
                startPos,
                endPos,
                // meleeWeapon.transform.position,
                // meleeWeapon.transform.position + new Vector3(direction.x, direction.y),
                counter / (duration / 4)
            );
            yield return null;
        }
        counter = 0;
        while (counter < duration / 4)
        {
            counter += Time.deltaTime;
            meleeWeapon.transform.localPosition = Vector2.Lerp(
                // referenceWeapon.transform.position,
                // referenceWeapon.transform.position + new Vector3(direction.x * 5, direction.y * 5),
                endPos,
                startPos,
                // meleeWeapon.transform.position,
                // meleeWeapon.transform.position + new Vector3(direction.x, direction.y),
                counter / (duration / 4)
            );
            yield return null;
        }
        rotating = false;
        meleeWeapon.SetActive(false);
    }
}
