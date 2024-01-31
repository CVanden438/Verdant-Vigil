using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class DayNightCycleWithSunAndMoon : MonoBehaviour
{
    public float cycleDuration = 600.0f; // 600 seconds = 10 minutes (day + night)
    private float timeOfDay = 0.0f; // 0.0 = midnight, 0.5 = noon

    public RectTransform sunRectTransform; // Reference to the RectTransform of the sun UI element
    public RectTransform moonRectTransform; // Reference to the RectTransform of the moon UI element

    [SerializeField]
    private Light2D globalLight;

    // public TilemapCollider2D bounds;
    public bool IsDay
    {
        get { return timeOfDay < 0.5; }
    }

    void Update()
    {
        // Calculate the time of day
        timeOfDay += Time.deltaTime / cycleDuration;
        timeOfDay %= 1.0f;
        // globalLight.intensity = timeOfDay;

        // Set the anchored position of the sun/moon based on the time of day
        if (timeOfDay < 0.5f)
        {
            float xPos = Mathf.Lerp(20f, 350f, timeOfDay * 2); // Sun position (day)
            sunRectTransform.anchoredPosition = new Vector2(
                xPos,
                sunRectTransform.anchoredPosition.y
            );
            moonRectTransform.anchoredPosition = new Vector2(
                2000f,
                moonRectTransform.anchoredPosition.y
            ); // Move moon off-screen
            // bounds.enabled = false;
        }
        else
        {
            float xPos = Mathf.Lerp(20f, 350, (timeOfDay - 0.5f) * 2); // Moon position (night)
            moonRectTransform.anchoredPosition = new Vector2(
                xPos,
                moonRectTransform.anchoredPosition.y
            );
            sunRectTransform.anchoredPosition = new Vector2(
                2000f,
                sunRectTransform.anchoredPosition.y
            ); // Move sun off-screen
            // bounds.enabled = true;
        }
    }
}
