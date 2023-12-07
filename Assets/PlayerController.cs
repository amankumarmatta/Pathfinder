using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Vector3 homePosition;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        homePosition = transform.position;
    }

    public void MoveToPosition(Vector3 targetPosition, System.Action onArrivedCallback = null)
    {
        navMeshAgent.destination = targetPosition;
        StartCoroutine(WaitForArrival(onArrivedCallback));
    }

    IEnumerator WaitForArrival(System.Action onArrivedCallback)
    {
        while (navMeshAgent.pathPending || navMeshAgent.remainingDistance > 0.1f)
        {
            yield return null;
        }

        onArrivedCallback?.Invoke();
    }

    public void MoveToHome()
    {
        MoveToPosition(homePosition, null);
    }
}
