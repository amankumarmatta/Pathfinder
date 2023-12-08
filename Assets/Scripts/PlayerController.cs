using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Vector3 homePosition;
    private Quaternion originalRotation;
    private Animator animator;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        homePosition = transform.position;
        originalRotation = transform.rotation;

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        float speed = navMeshAgent.velocity.magnitude;

        animator.SetBool("IsMoving", speed > 0.1f);
    }

    public void MoveToPosition(Vector3 targetPosition, System.Action onArrivedCallback = null)
    {
        navMeshAgent.destination = targetPosition;

        StartCoroutine(WaitForArrival(targetPosition, onArrivedCallback));
    }

    IEnumerator WaitForArrival(Vector3 targetPosition, System.Action onArrivedCallback)
    {
        while (navMeshAgent.pathPending || navMeshAgent.remainingDistance > 0.1f)
        {
            yield return null;
        }

        transform.position = targetPosition;

        navMeshAgent.isStopped = true;

        transform.rotation = originalRotation;

        navMeshAgent.isStopped = false;

        onArrivedCallback?.Invoke();
    }

    public void MoveToHome()
    {
        MoveToPosition(homePosition, null);
    }
}
