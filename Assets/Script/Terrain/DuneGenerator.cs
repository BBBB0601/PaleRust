using UnityEngine;

public class DuneGenerator : MonoBehaviour
{
    [SerializeField] private Terrain terrain;

    [Header("사구 설정")]
    [Range(0f, 1f)]
    [SerializeField] private float noiseScale = 0.01f;      // 낮을수록 부드럽고 거대해짐
    [Range(0f, 1f)]
    [SerializeField] private float heightMultiplier = 0.05f;

    void Start()
    {
        if(terrain == null) terrain = GetComponent<Terrain>();
        GenerateDune();
    }

    void OnApplicationQuit()
    {
        TerrainData terrainData = terrain.terrainData;
        int width = terrainData.heightmapResolution;
        int height = terrainData.heightmapResolution;

        float[,] flatHeights = new float[width, height];
        terrainData.SetHeights(0, 0, flatHeights);
    }

    void GenerateDune()
    {
        TerrainData terrainData = terrain.terrainData;
        int width = terrainData.heightmapResolution;
        int height = terrainData.heightmapResolution;

        float[,] heights = new float[width, height];

        for(int x = 0; x < width; x++)
        {
            for(int z = 0; z < height; z++)
            {
                // scale을 곱해서 격자 간격을 늘림 => 사구 곡선이 부드러워짐
                float xCoord = x * noiseScale;
                float zCoord = z * noiseScale;

                float noise = Mathf.PerlinNoise(xCoord, zCoord);

                heights[x, z] = noise * heightMultiplier;
            }

            terrainData.SetHeights(0, 0, heights);
        }
    }
}
