using UnityEngine;

public class CollectibleObject : MonoBehaviour
{
    public int points = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddPoints(points);

            Destroy(gameObject);
        }
    }
}
