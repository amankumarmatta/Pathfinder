using UnityEngine;

public class ThirdPersonFollowCamera : MonoBehaviour
{
    public Transform target;            // The target to follow
    public float distance = 5.0f;        // Distance from the target
    public float height = 2.0f;          // Height from the target
    public float rotationDamping = 5.0f; // Damping for smooth rotation
    public float positionDamping = 5.0f; // Damping for smooth position

    void Update()
    {
        if (target == null)
        {
            Debug.LogWarning("Target not set for the ThirdPersonFollowCamera script.");
            return;
        }

        // Calculate the desired camera position
        Vector3 desiredPosition = target.position - target.forward * distance + Vector3.up * height;

        // Smoothly interpolate between the current position and the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.fixedDeltaTime * positionDamping);

        // Smoothly interpolate between the current rotation and the desired rotation
        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.fixedDeltaTime * rotationDamping);
    }
}
