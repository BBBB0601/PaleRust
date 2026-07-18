using UnityEngine;

[System.Serializable]
public class NoiseData
{
    [SerializeField] private int _width = 512;
    [SerializeField] private int _height = 512;
    [SerializeField] private float _scale = 50f;
    [SerializeField] private int _octave = 4;
    [Range(1f, 10f), SerializeField] private float _lacunarity = 2f;
    [Range(0.1f, 1f), SerializeField] private float _persistence = 0.5f;
    [SerializeField] private int _seed = 601;

    public int Width => _width;
    public int Height => _height;
    public float Scale => _scale;
    public int Octave => _octave;
    public float Lacunarity => _lacunarity;
    public float Persistence => _persistence;
    public int Seed => _seed;
}
