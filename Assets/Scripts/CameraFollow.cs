using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [Range(0f, 1f)][SerializeField] private float smoothness;
    [SerializeField] private Vector3 offset;

    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothness);
        transform.position = smoothPosition;
    }

    private void OnValidate()
    {
        transform.position = target.position + offset;
    }
}
