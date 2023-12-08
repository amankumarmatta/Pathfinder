using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Collections;

public class ClickToMove : MonoBehaviour
{
    public GameObject collectiblePrefab;
    public int maxCollectibles = 10;
    public float raycastDistance = 100f;
    public float rotationSpeed = 5f;

    private List<Vector3> collectiblePositions = new List<Vector3>();
    private PlayerController playerController;
    private int collectedCount = 0;

    private TerrainHeightProvider terrainHeightProvider;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        terrainHeightProvider = GetComponent<TerrainHeightProvider>();
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

                // Get terrain height at the specified position.
                float terrainHeight = Terrain.activeTerrain.SampleHeight(targetPosition);

                // Adjust the y-coordinate to place collectibles at terrain height.
                Vector3 collectibleSpawnPosition = new Vector3(targetPosition.x, terrainHeight, targetPosition.z);

                Instantiate(collectiblePrefab, collectibleSpawnPosition, Quaternion.identity);

                if (collectedCount == 0)
                {
                    MoveToPosition(collectibleSpawnPosition);
                }

                RotateTowards(targetPosition);

                collectedCount++;

                if (collectedCount >= maxCollectibles)
                {
                    Debug.Log("Maximum number of collectibles reached!");
                }
            }
        }
    }

    void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void MoveToPosition(Vector3 targetPosition)
    {
        // Ignore the vertical component of the target position for rotation.
        Vector3 horizontalTargetPosition = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);

        // Calculate the direction to the target position.
        Vector3 direction = (horizontalTargetPosition - transform.position).normalized;

        // Use Quaternion.LookRotation to smoothly rotate the character towards the target direction.
        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);

        // Use Quaternion.Lerp for smooth interpolation between current rotation and the target rotation.
        StartCoroutine(RotateCoroutine(toRotation));

        // Move the player to the target position.
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
