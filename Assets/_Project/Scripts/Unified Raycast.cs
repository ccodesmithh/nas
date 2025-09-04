using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UnifiedRaycast : MonoBehaviour
{
    public float rayLength = 5; // Panjang raycast
    public LayerMask layerMaskInteract; // Layer mask untuk objek yang bisa diinteraksi
    public string excludeLayerName = null; // Nama layer yang akan dikecualikan

    public KeyCode interactionKey = KeyCode.F; // Tombol untuk melakukan interaksi

    public Rigidbody playerRigidbody; // Referensi ke Rigidbody player
    public float movementThreshold = 0.1f; // Batas kecepatan untuk dianggap berhenti

    public float normalCrosshairScale = 1f; // Skala default crosshair
    public float enlargedCrosshairScale = 1.5f; // Skala crosshair saat membesar
    public float crosshairChangeSpeed = 5f; // Kecepatan perubahan skala crosshair
    public float crosshairTransitionSpeed = 5f; // Kecepatan transisi skala crosshair

    public Image crosshair; // Referensi ke UI crosshair
    public GameObject interactionText; // Referensi ke teks interaksi

    public LevelLoader levelLoader;

    private bool isCrosshairActive; // Apakah crosshair sedang aktif
    private bool doOnce; // Mencegah logika dijalankan berulang kali saat tidak perlu
    private const string interactableTag1 = "InteractiveObject"; // Tag untuk objek interaktif tipe 1
    //private const string interactableTag2 = "InteractiveObject2"; // Tag untuk objek interaktif tipe 2

    private Vector3 currentScaleVelocity = Vector3.zero; // Kecepatan transisi untuk SmoothDamp
    private float targetScale; // Target skala crosshair
    private Coroutine currentCrosshairCoroutine; // Menyimpan coroutine yang sedang berjalan


    private void Update()
    {
        #region Raycast dan Interaksi
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward); // Arah raycast

        // Buat layer mask untuk raycast
        int mask = 1 << LayerMask.NameToLayer(excludeLayerName) | layerMaskInteract.value;

        // Periksa apakah raycast mengenai objek yang valid
        if (Physics.Raycast(transform.position, fwd, out hit, rayLength, mask))
        {
            GameObject hitObject = hit.collider.gameObject;

            // Cek tag objek yang terkena raycast dan tangani interaksinya
            if (hitObject.CompareTag("InteractiveObject"))
            {
                HandleInteraction(hitObject.GetComponent<DoorController>(), hitObject);
            }
            /*else if (hitObject.CompareTag("InteractiveObject2"))
            {
                HandleInteraction(hitObject.GetComponent<DoorController2>(), hitObject);
            }*/
            else if (hitObject.GetComponent<letter>())
            {
                HandleInteraction(hitObject.GetComponent<letter>(), hitObject);
            }
            else if (hitObject.CompareTag("InteractiveObjectGate"))
            {
                Debug.Log("Ray hit Gate: " + hitObject.name);
                var loader = hitObject.GetComponent<LevelLoader>();
                if (loader == null) Debug.LogWarning("LevelLoader not found on " + hitObject.name);
                HandleInteraction(loader, hitObject);

            }
            else
            {
                ResetCrosshairAndText(); // Reset jika tidak ada objek valid
            }
        }
        else
        {
            ResetCrosshairAndText(); // Reset jika raycast tidak mengenai apapun
        }
        #endregion
    }
    #region Crosshair dan IntText

    // Periksa apakah player sudah berhenti?
    private bool IsPlayerStopped()
    {
        return playerRigidbody.linearVelocity.magnitude < movementThreshold;
    }

    // Tangani interaksi dengan objek
    private void HandleInteraction(MonoBehaviour component, GameObject hitObject)
    {

        

        if (component == null) return; // Jika tidak ada komponen, keluar

        // Jika objek adalah surat dan player sedang bergerak, reset crosshair
        if (component is letter && !IsPlayerStopped())
        {
            ResetCrosshairAndText();
            return;
        }

        // Aktifkan crosshair dan teks interaksi jika belum dilakukan
        if (!doOnce)
        {
            CrosshairChange(true);
            interactionText.SetActive(true);
        }

        isCrosshairActive = true;
        doOnce = true;

        // Tangani input untuk interaksi
        if (Input.GetKeyDown(interactionKey))
        {
            if (component is LevelLoader gateController)
            {
                gateController.LoadNextLevel();
            }
            if (component is DoorController doorController)
                {
                    doorController.PlayAnimation();
                }
                else if (component is DoorController2 doorController2)
                {
                    doorController2.PlayAnimation2();
                }
                else if (component is letter letterScript)
                {
                    letterScript.openCloseLetter();
                }
        }
    }
    #endregion

    #region Animasi Crosshair
    // Reset crosshair dan teks interaksi
    private void ResetCrosshairAndText()
    {
        if (isCrosshairActive) // Jika crosshair aktif
        {
            CrosshairChange(false);
            doOnce = false;
            interactionText.SetActive(false);
        }
    }

    // Ubah status crosshair (aktif/nonaktif)
    private void CrosshairChange(bool on)
    {
        crosshair.color = on ? Color.red : Color.white; // Ubah warna crosshair
        float targetScale = on ? enlargedCrosshairScale : normalCrosshairScale; // Tentukan skala target

        // Hentikan coroutine yang sedang berjalan
        if (currentCrosshairCoroutine != null)
        {
            StopCoroutine(currentCrosshairCoroutine);
        }

        // Mulai coroutine untuk animasi skala crosshair
        currentCrosshairCoroutine = StartCoroutine(AnimateCrosshairScale(targetScale));
        isCrosshairActive = on;
    }

    // Coroutine untuk animasi transisi skala crosshair
    private IEnumerator AnimateCrosshairScale(float targetScale)
    {
        Vector3 targetScaleVector = new Vector3(targetScale, targetScale, 1f); // Skala target dalam Vector3

        // Lakukan transisi hingga skala mendekati target
        while (Vector3.Distance(crosshair.rectTransform.localScale, targetScaleVector) > 0.01f)
        {
            crosshair.rectTransform.localScale = Vector3.SmoothDamp(
                crosshair.rectTransform.localScale,
                targetScaleVector,
                ref currentScaleVelocity,
                crosshairTransitionSpeed,
                Mathf.Infinity,
                Time.deltaTime
            );
            yield return null; // Tunggu satu frame
        }

        // Pastikan skala akhir sesuai target
        crosshair.rectTransform.localScale = targetScaleVector;
    }
    #endregion
}


#region Ndasku mumet.
#endregion
