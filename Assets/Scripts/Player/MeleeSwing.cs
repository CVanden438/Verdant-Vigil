using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSwing : MonoBehaviour
{
    bool rotating = false;
    public GameObject objectToRotate;
    public GameObject referenceRotation;
    public GameObject meleeWeapon;

    void Start() { }

    public void SwingWeapon()
    {
        StartCoroutine(RotateObject(objectToRotate, 0.3f));
    }

    IEnumerator RotateObject(GameObject gameObjectToMove, float duration)
    {
        // meleeWeapon.SetActive(true);
        Quaternion rotation2 = Quaternion.Euler(
            new Vector3(0, 0, referenceRotation.transform.eulerAngles.z - 180)
        );
        if (rotating)
        {
            yield break;
        }
        rotating = true;

        // Quaternion currentRot = gameObjectToMove.transform.rotation;
        Quaternion currentRot = Quaternion.Euler(
            new Vector3(0, 0, referenceRotation.transform.eulerAngles.z)
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
        // meleeWeapon.SetActive(false);
    }
}
