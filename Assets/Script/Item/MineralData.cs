using UnityEngine;

[CreateAssetMenu(fileName = "MineralData", menuName = "SO/MineralData")]
public class MineralData : ScriptableObject
{
    [SerializeField]    private string _name;
    [SerializeField, Tooltip("광물 1개 판정을 내기위한 무게")]    private int _weight;
}