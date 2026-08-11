using UnityEngine;

[CreateAssetMenu(fileName = "MineralData", menuName = "SO/MineralData")]
public class MineralData : ScriptableObject
{
    [SerializeField]    private int _id;
    [SerializeField]    private string _name;
    [SerializeField, Tooltip("광물 1개 판정을 내기위한 무게")]    private int _weight;

    public int ID => _id;
    public string MineralName => _name;
    public int MineralWeight => _weight;
}