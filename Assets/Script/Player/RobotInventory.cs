using System.Collections.Generic;
using UnityEngine;

public class RobotInventory : MonoBehaviour
{
    [SerializeField, Tooltip("최대 수용력, 단위 g")] private int _maxWeight;
    [SerializeField]    private int _currentWeight = 0;

    /// <summary>
    /// 인벤토리는 현재 어떤 광물을 얼마나 많이 갖고 있는지 기록해야 함
    /// </summary>
    private Dictionary<MineralData, int> _inventory = new Dictionary<MineralData, int>();
    public Dictionary<MineralData, int> Inventory => _inventory;


    public void ResetInventory() 
    {
        _inventory.Clear();
        _currentWeight = 0;
    }

    /// <summary>
    /// 봇 인벤토리에 자원을 추가함
    /// </summary>
    /// <param name="md">추가할 미네랄 데이터</param>
    /// <param name="weight">추가할 미네랄 데이터의 그램 수</param>
    /// <returns></returns>
    public bool GatherMineral(MineralData md, int weight)
    {
        if(_currentWeight + weight > _maxWeight)    return false;
        
        // cw = currentWeight = 현재 딕셔너리[md]의 무게
        // map과 다르게 md가 key로써 존재하는지 1차 확인 필요
        if(_inventory.TryGetValue(md, out int cw))
        {
            _inventory[md] = cw + weight;
        }
        else
        {
            _inventory[md] = weight;
        }

        _currentWeight += weight;

        return true;
    }
}
