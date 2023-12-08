using UnityEngine;

public class TerrainHeightProvider : MonoBehaviour
{
    public float GetTerrainHeight(Vector3 position)
    {
        return Terrain.activeTerrain.SampleHeight(position);
    }
}
