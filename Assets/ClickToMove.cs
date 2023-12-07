using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

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
        // Check for mouse click to initiate movement towards a point.
        if (Input.GetMouseButtonDown(0) && collectedCount < maxCollectibles)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo, raycastDistance))
            {
                Vector3 targetPosition = hitInfo.point;

                // Add the clicked position to the list of collectible positions.
                collectiblePositions.Add(targetPosition);

                // Instantiate a collectible object at the clicked position.
                Instantiate(collectiblePrefab, targetPosition, Quaternion.identity);

                // If this is the first collectible, move the character to the clicked position.
                if (collectiblePositions.Count == 1)
                {
                    MoveToPosition(targetPosition);
                }

                // Increment the collected count.
                collectedCount++;

                // Check if the maximum number of collectibles has been reached.
                if (collectedCount >= maxCollectibles)
                {
                    Debug.Log("Maximum number of collectibles reached!");
                    // Optionally, you can add logic here to handle reaching the maximum.
                }
            }
        }
    }

    void MoveToPosition(Vector3 targetPosition)
    {
        playerController.MoveToPosition(targetPosition, OnArrivedAtCollectible);
    }

    void OnArrivedAtCollectible()
    {
        // Remove the current collectible position from the list.
        collectiblePositions.RemoveAt(0);

        // If there are more collectibles, move to the next one. Otherwise, move back home.
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
