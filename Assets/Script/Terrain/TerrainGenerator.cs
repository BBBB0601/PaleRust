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

        float[,] smoothedDune = new float[mapHeight, mapWidth];
        int smoothRadius = 4;

        for (int y = 0; y < mapHeight; ++y)
        {
            for (int x = 0; x < mapWidth; ++x)
            {
                float sum = 0f;
                int count = 0;

                for (int ky = -smoothRadius; ky <= smoothRadius; ++ky)
                {
                    for (int kx = -smoothRadius; kx <= smoothRadius; ++kx)
                    {
                        // 지형 경계를 벗어나지 않도록 함
                        int py = Mathf.Clamp(y + ky, 0, mapHeight - 1);
                        int px = Mathf.Clamp(x + kx, 0, mapWidth - 1);

                        sum += sandDune[py, px];
                        count++;
                    }
                }
                // 주변 영역의 평균값 대입
                smoothedDune[y, x] = sum / count;
            }
        }

        Vector3 currSize = terrainData.size;
        terrainData.size = new Vector3(currSize.x, _maxTerrainHeight, currSize.z);

        terrainData.SetHeights(0, 0, smoothedDune);
    }

    // private void OnApplicationQuit()
    // {
    //     if (_terrain != null)
    //     {
    //         TerrainData terrainData = _terrain.terrainData;
    //         int res = terrainData.heightmapResolution;
    //         terrainData.SetHeights(0, 0, new float[res, res]);
    //     }
    // }
}
