
using UnityEngine;

public class ScreenResolutionAdjuster : MonoBehaviour
{

    public float targetAspectRatio = 16f / 9f;
    public float minOrthographicSize = 5f;
    public float maxOrthographicSize = 10f;

    void Start()
    {
        float targetAspect = targetAspectRatio;
        float currentAspect = (float)Screen.width / Screen.height;
        float ratio = targetAspect / currentAspect;

        Camera camera = GetComponent<Camera>();

        if (camera.orthographic)
        {
            float orthographicSize = camera.orthographicSize * ratio;
            camera.orthographicSize = Mathf.Clamp(orthographicSize, minOrthographicSize, maxOrthographicSize);
        }
        else
        {
            camera.fieldOfView *= ratio;
        }
    }
}
