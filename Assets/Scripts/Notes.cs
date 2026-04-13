using UnityEngine;

public class Notes : MonoBehaviour
{
    public MeshRenderer[] meshRenderer;
    private Material[] originalMaterial;
    public Material highlightMaterial;

    private PlayerLook player;
    private Camera playerCamPosition;
    private float lookRange = 6f;
    private bool isLookedAt = false;

    void Start()
    {
        meshRenderer = GetComponentsInChildren<MeshRenderer>();
        originalMaterial = new Material[meshRenderer.Length];
        for (int i = 0; i <meshRenderer.Length; i++){
            originalMaterial[i] = meshRenderer[i].material;
        }
        player = FindAnyObjectByType<PlayerLook>();
        playerCamPosition = player.GetComponentInChildren<Camera>();
    }

    void Update()
    {
        if (playerCamPosition == null) return;

        Ray ray = new Ray(playerCamPosition.transform.position, playerCamPosition.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, lookRange)) {
            if (hit.collider.gameObject == this.gameObject) {
                isLookedAt = true;
                IsLookedAt(isLookedAt);
            }
            return;
        } else {
            isLookedAt = false;
            IsLookedAt(isLookedAt);
        }
    }

    public void IsLookedAt(bool isLookAt) {
        isLookedAt = isLookAt;
        if (isLookedAt) {
            foreach (MeshRenderer mr in meshRenderer) {
                mr.material = highlightMaterial;
            }
        } else {
            for (int i = 0; i < meshRenderer.Length; i++) {
                meshRenderer[i].material = originalMaterial[i];
            }
        }
    }
}
