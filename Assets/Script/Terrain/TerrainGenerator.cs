using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [Header("연동할 지형")]
    [SerializeField] private Terrain _terrain;

    [Header("사막 지형 최대 높이")]
    [SerializeField] private float _maxTerrainHeight = 15f;

    [Header("노이즈 데이터")]
    [SerializeField] private NoiseData _noiseData;

    private void Start()
    {
        if(_terrain == null)    _terrain = GetComponent<Terrain>();
    }

    [ContextMenu("지형 생성하기")]
    public void GenerateMap()
    {
        if(_terrain == null || _noiseData == null)
        {
            Debug.LogError("Terrain 또는 NoiseData가 세팅되지 않음");
            return;
        }

        TerrainData terrainData = _terrain.terrainData;

        float[,] rawNoiseMap = NoiseMapGenerator.NoiseGenerate(_noiseData);

        int mapWidth = _noiseData.Width;
        int mapHeight = _noiseData.Height;

        float[,] sandDune = new float[mapHeight, mapWidth];
        for(int y = 0; y < mapHeight; ++y)
        {
            for(int x = 0; x < mapWidth; ++x)
            {
                float noise = rawNoiseMap[y, x];

                float sharpDune = 1f - Mathf.Abs(noise * 2f - 1f);

                sandDune[y, x] = sharpDune * sharpDune;
            }
        }

        terrainData.heightmapResolution = mapWidth + 1;

        Vector3 currSize = terrainData.size;
        terrainData.size = new Vector3(currSize.x, _maxTerrainHeight, currSize.z);

        terrainData.SetHeights(0, 0, sandDune);
    }

    private void OnApplicationQuit()
    {
        if (_terrain != null)
        {
            TerrainData terrainData = _terrain.terrainData;
            int res = terrainData.heightmapResolution;
            terrainData.SetHeights(0, 0, new float[res, res]);
        }
    }
}
