using UnityEngine;

public static class NoiseMapGenerator
{
    private static int _seed;
    private static float _scale;
    private static int _width;
    private static int _height;
    private static int _octave;
    private static float _lacunarity;
    private static float _persistence;

    private static void InitNoiseData(NoiseData data)
    {
        _seed = data.Seed;
        _scale = data.Scale;
        _width = data.Width;
        _height = data.Height;
        _octave = data.Octave;
        _lacunarity = data.Lacunarity;
        _persistence = data.Persistence;
    }

    public static float[,] NoiseGenerate(NoiseData data)
    {
        InitNoiseData(data);

        if(_scale <= 0) _scale = 0.0001f;

        var noiseMap = new float[_height, _width];

        // 옥타브 오프셋 벡터 생성
        var octaveOffset = new Vector2[_octave];
        var prng = new System.Random(_seed);

        for(int i = 0; i < _octave; ++i)
        {
            float xPos = prng.Next(-100000, 100000);
            float yPos = prng.Next(-100000, 100000);

            octaveOffset[i] = new Vector2(xPos, yPos);
        }

        float halfWidth = _width / 2f;
        float halfHeight = _height / 2f;
        float minHeight = float.MaxValue;
        float maxHeight = float.MinValue;

        for(int y = 0; y < _height; ++y)
        {
            for(int x = 0; x < _width; ++x)
            {
                // 주파수와 진폭
                float frequency = 1f;
                float amplitude = 1f;
                float noiseHeight = 0f;

                for(int i = 0; i < _octave; ++i)
                {
                    // 중앙 기준으로 정규화
                    var sampleX = (x - halfWidth) / _scale * frequency + octaveOffset[i].x;
                    var sampleY = (y - halfHeight) / _scale * frequency + octaveOffset[i].y;

                    float perlin = Mathf.PerlinNoise(sampleX, sampleY);
                    noiseHeight += perlin * amplitude;

                    frequency *= _lacunarity;
                    amplitude *= _persistence;
                }

                noiseMap[y, x] = noiseHeight;

                // 높이 정규화
                if(noiseHeight < minHeight) minHeight = noiseHeight;
                if(noiseHeight > maxHeight) maxHeight = noiseHeight;
            }
        }

        // 정규화
        for(int y = 0; y < _height; ++y)
            for(int x = 0; x < _width; ++x)
                noiseMap[y, x] = Mathf.InverseLerp(minHeight, maxHeight, noiseMap[y, x]);

        return noiseMap;
    }
}
