using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;

public class ClickToMove : MonoBehaviour
{
    public float raycastDistance = 100f;
    public GameObject collectiblePrefab;
    public int maxCollectibles = 10;

    private List<Vector3> collectiblePositions = new List<Vector3>();
    private PlayerController playerController;
    private int collectedCount = 0;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && collectedCount < maxCollectibles)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, raycastDistance))
            {
                Vector3 targetPosition = hitInfo.point;

                collectiblePositions.Add(targetPosition);

                Instantiate(collectiblePrefab, targetPosition, Quaternion.identity);

                if (collectiblePositions.Count == 1)
                {
                    MoveToPosition(targetPosition);
                }

                collectedCount++;

                if (collectedCount >= maxCollectibles)
                {
                    Debug.Log("Maximum number of collectibles reached!");
                }
            }
        }
    }

    void MoveToPosition(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;

        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);

        StartCoroutine(RotateCoroutine(toRotation));

        playerController.MoveToPosition(targetPosition, OnArrivedAtCollectible);
    }

    IEnumerator RotateCoroutine(Quaternion toRotation)
    {
        float duration = 0.5f;
        float elapsed = 0f;
        Quaternion startRotation = transform.rotation;

        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, toRotation, elapsed / duration);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.rotation = toRotation;
    }

    void OnArrivedAtCollectible()
    {
        collectiblePositions.RemoveAt(0);

        if (collectiblePositions.Count > 0)
        {
            MoveToPosition(collectiblePositions[0]);
        }
        else
        {
            playerController.MoveToHome();
        }
    }
}
