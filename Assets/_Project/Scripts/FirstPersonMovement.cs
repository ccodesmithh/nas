using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    [Header("Headbob Settings")]
    public Jump jumpScript; // Drag and drop Jump script component in the Inspector
    public GroundCheck groundCheck; // Drag and drop the GroundCheck component in the Inspector


    public Transform cameraTransform; // Referensi ke kamera
    public float walkBobSpeed = 14f;    // Kecepatan headbob saat berjalan
    public float runBobSpeed = 18f; // Kecepatan headbob saat berlari
    public float bobAmount = 0.05f; // Intensitas headbob saat bergerak
    public float idleBobSpeed = 0.5f;   // Kecepatan headbob saat diam
    public float idleBobAmount = 0.02f; // Intensitas headbob saat diam
    public float transitionSpeed = 5f; // Kecepatan transisi antara status
    private float defaultYPos;  // Posisi default kamera
    private float timer;  // Timer untuk headbob
    private float currentBobSpeed = 0f; // Kecepatan headbob saat bergerak
    private float currentBobIntensity = 0f; // Intensitas headbob saat bergerak


    Rigidbody rigidbody;
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>(); // List untuk menentukan kecepatan berjalan

    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();

        // Simpan posisi awal kamera
        if (cameraTransform != null)
        {
            defaultYPos = cameraTransform.localPosition.y;
        }
    }

    void FixedUpdate()
    {
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        rigidbody.linearVelocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.linearVelocity.y, targetVelocity.y);

        // Apply headbob effect.
        ApplyHeadbob(targetVelocity.magnitude > 0.1f);
    }

    private void ApplyHeadbob(bool isMoving)
    {
        // Mematikan headbob saat player tidak di ground
        if (cameraTransform == null || (groundCheck != null && !groundCheck.isGrounded))
            return;

        // Tentukan target kecepatan dan intensitas headbob berdasarkan status pemain.
        float targetBobSpeed = isMoving ? (IsRunning ? runBobSpeed : walkBobSpeed) : idleBobSpeed;
        float targetBobIntensity = isMoving ? bobAmount : idleBobAmount;

        // Lerp kecepatan dan intensitas agar transisinya mulus.
        currentBobSpeed = Mathf.Lerp(currentBobSpeed, targetBobSpeed, Time.deltaTime * transitionSpeed);
        currentBobIntensity = Mathf.Lerp(currentBobIntensity, targetBobIntensity, Time.deltaTime * transitionSpeed);

        // Tingkatkan timer berdasarkan kecepatan bobbing.
        timer += Time.deltaTime * currentBobSpeed;

        // Goyangkan posisi Y kamera berdasarkan sinusoidal.
        float newY = defaultYPos + Mathf.Sin(timer) * currentBobIntensity;
        cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, newY, cameraTransform.localPosition.z);
    }

}
