using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class OreInteraction : MonoBehaviour
{
    public float interactionDistance = 2f; // Adjust this distance to your preference
    public UnityEvent OnMine;
    private bool isMining = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isMining)
            {
                return;
            }
            Collider2D[] colliders = Physics2D.OverlapCircleAll(
                transform.position,
                interactionDistance
            );

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Ore"))
                {
                    StartCoroutine(MineCoroutine());
                    break;
                }
            }
        }
        if (
            Input.GetKey(KeyCode.W)
            || Input.GetKey(KeyCode.A)
            || Input.GetKey(KeyCode.S)
            || Input.GetKey(KeyCode.D)
        )
        {
            isMining = false;
            StopAllCoroutines();
        }
    }

    IEnumerator MineCoroutine()
    {
        isMining = true;
        while (isMining)
        {
            yield return new WaitForSeconds(3f);
            OnMine.Invoke();
        }
        // isMining = false;
    }
    // IEnumerator MineCoroutine()
    // {
    //     isMining = true;
    //     float miningTime = 3f;
    //     float elapsedTime = 0f;

    //     while (elapsedTime < miningTime && isMining)
    //     {
    //         elapsedTime += Time.deltaTime;
    //         yield return null;
    //     }

    //     if (isMining)
    //     {
    //         OnMine.Invoke();
    //     }

    //     isMining = false;
    // }
}
