using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ClickToMove : MonoBehaviour
{
    public GameObject collectiblePrefab;
    public int maxCollectibles = 10;
    public float raycastDistance = 100f;
    public float rotationSpeed = 5f;

    public AudioClip pickupSound;

    private List<Vector3> collectiblePositions = new List<Vector3>();
    private List<GameObject> instantiatedCollectibles = new List<GameObject>();
    private PlayerController playerController;
    private int collectedCount = 0;

    public AudioSource audioSource;

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

                float terrainHeight = Terrain.activeTerrain.SampleHeight(targetPosition);
                Vector3 collectibleSpawnPosition = new Vector3(targetPosition.x, terrainHeight, targetPosition.z);

                GameObject newCollectible = Instantiate(collectiblePrefab, collectibleSpawnPosition, Quaternion.identity);
                instantiatedCollectibles.Add(newCollectible);

                if (collectedCount == 0)
                {
                    MoveAndRotateToPosition(collectibleSpawnPosition);
                }

                collectedCount++;

                if (collectedCount >= maxCollectibles)
                {                             
                    Destroy(collectiblePrefab);
                    SceneManager.LoadScene("Win");
                }

                PlayPickupSound();
            }
        }
    }
    
    public void PlayPickupSound()
    {
        if (pickupSound != null && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(pickupSound);
        }
    }

    void MoveAndRotateToPosition(Vector3 targetPosition)
    {
        StartCoroutine(MoveAndRotateCoroutine(targetPosition));
    }

    IEnumerator MoveAndRotateCoroutine(Vector3 targetPosition)
    {
        playerController.MoveToPosition(targetPosition, OnArrivedAtCollectible);

        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        float elapsed = 0f;
        while (elapsed < 1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, elapsed);
            elapsed += Time.deltaTime * rotationSpeed;

            yield return null;
        }

        transform.rotation = targetRotation;
    }

    void OnArrivedAtCollectible()
    {
        collectiblePositions.RemoveAt(0);

        if (collectiblePositions.Count > 0)
        {
            MoveAndRotateToPosition(collectiblePositions[0]);
        }
        else
        {
            playerController.MoveToHome();

            collectedCount = 0;
        }
    }
}
