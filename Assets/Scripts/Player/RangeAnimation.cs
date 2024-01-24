using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAnimation : MonoBehaviour
{
    bool rotating = false;
    public GameObject objectToRotate;
    public GameObject referenceRotation;
    public GameObject rangeWeapon;
    public GameObject referenceWeapon;

    void Start() { }

    public void SwingWeapon()
    {
        StartCoroutine(RecoilObject(objectToRotate, 1f));
    }

    IEnumerator RecoilObject(GameObject gameObjectToMove, float duration)
    {
        rangeWeapon.SetActive(true);
        Quaternion currentRot = Quaternion.Euler(
            new Vector3(0, 0, referenceRotation.transform.eulerAngles.z)
        );
        if (currentRot.eulerAngles.z > 90 && currentRot.eulerAngles.z < 270)
        {
            rangeWeapon.GetComponent<SpriteRenderer>().flipY = true;
        }
        else
        {
            rangeWeapon.GetComponent<SpriteRenderer>().flipY = false;
        }
        // rangeWeapon.transform.position = referenceWeapon.transform.position;
        objectToRotate.transform.rotation = currentRot;
        Vector3 position2 = new Vector3(
            rangeWeapon.transform.position.x - 0.2f,
            rangeWeapon.transform.position.y,
            0
        );
        if (rotating)
        {
            yield break;
        }
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            // rangeWeapon.transform.position = Vector3.Lerp(
            //     rangeWeapon.transform.position,
            //     position2,
            //     counter / duration
            // );
            yield return null;
        }
        rotating = false;
        rangeWeapon.SetActive(false);
    }
}
