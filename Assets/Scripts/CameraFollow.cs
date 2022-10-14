using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private AllReferencesObjects referencesObjects;
    [Range(0f, 1f)][SerializeField] private float smoothness;
    [SerializeField] private Vector3 offset;

    private void FixedUpdate()
    {
        Vector3 desiredPosition = referencesObjects.player.transform.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothness);
        transform.position = smoothPosition;
    }
}
