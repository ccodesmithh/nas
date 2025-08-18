using UnityEngine;

[ExecuteInEditMode]
public class Zoom : MonoBehaviour
{
    Camera camera;
    public float defaultFOV = 60;
    public float maxZoomFOV = 15;
    public float zoomSpeed = 10; // Kecepatan transisi zoom
    private float targetFOV;
    private bool isZooming;

    void Awake()
    {
        // Get the camera on this gameObject and the defaultZoom.
        camera = GetComponent<Camera>();
        if (camera)
        {
            defaultFOV = camera.fieldOfView;
        }
        targetFOV = defaultFOV;
    }

    void Update()
    {
        // Deteksi klik kanan mouse
        if (Input.GetMouseButtonDown(1)) // Klik kanan ditekan
        {
            isZooming = true;
            targetFOV = maxZoomFOV;
        }
        else if (Input.GetMouseButtonUp(1)) // Klik kanan dilepaskan
        {
            isZooming = false;
            targetFOV = defaultFOV;
        }

        // Lerp untuk transisi FOV yang halus
        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
    }
}
