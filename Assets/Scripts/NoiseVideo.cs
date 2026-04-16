using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class NoiseVideo : MonoBehaviour
{
    public RawImage noiseImage;
    public Slender slender;
    public VideoPlayer noiseVideoPlayer;
    private Transform slenderTransform;
    private Camera mainCamera;

    private float maxNoiseAlpha = 0.45f;
    private float increaseRate = 0.4f;
    private float decreaseRate = 0.8f;
    private float viewOfCamera = 0.85f;
    private float currentNoiseAlpha = 0f;

    void Start()
    {
        mainCamera = Camera.main;
        if (noiseImage == null || noiseVideoPlayer == null || slender == null) {
            enabled = false;
            return;
        }
        slenderTransform = slender.transform;
        Color color = noiseImage.color;
        color.a = 0f;
        noiseImage.color = color;
    }

    void Update()
    {
        if (slenderTransform == null || mainCamera == null || !slender.isEnabled()) return;
        if (CheckIfLookingAtSlender()) {
            currentNoiseAlpha += increaseRate * Time.deltaTime;
        } else {
            currentNoiseAlpha -= decreaseRate * Time.deltaTime;
        }
        currentNoiseAlpha = Mathf.Clamp01(currentNoiseAlpha);
        ApplyAlphaFilter();
    }

    void ApplyAlphaFilter() {
        float finalAlpha = currentNoiseAlpha * maxNoiseAlpha;
        Color color = noiseImage.color;
        color.a = finalAlpha;
        noiseImage.color = color;
    }

    bool CheckIfLookingAtSlender() {
        Vector3 directionToSlender = (slenderTransform.position - mainCamera.transform.position).normalized;
        Vector3 cameraForward = mainCamera.transform.forward;
        float dotProduct = Vector3.Dot(cameraForward, directionToSlender);
        return dotProduct > viewOfCamera;
    }
}
