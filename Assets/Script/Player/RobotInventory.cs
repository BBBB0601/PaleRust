using System.Collections.Generic;
using UnityEngine;

public class RobotInventory : MonoBehaviour
{
    [SerializeField, Tooltip("최대 수용력, 단위 g")] private int _maxWeight;

    /// <summary>
    /// 인벤토리는 현재 어떤 광물을 얼마나 많이 갖고 있는지 기록해야 함
    /// </summary>
    private Dictionary<MineralData, int> _inventory;

    private int _currentWeight = 0;

    /// <summary>
    /// 현재 인벤토리가 갖고 있는 자원의 총량 반환
    /// </summary>
    /// <returns>자원의 총량 반환, 단위 g</returns>
    private int GetTotalWeight()
    {
        int totalWeight = 0;
        foreach(var (key, val) in _inventory)   totalWeight += val;
        return totalWeight;
    }

    /// <summary>
    /// 봇 인벤토리에 자원을 추가함
    /// </summary>
    /// <param name="md">추가할 미네랄 데이터</param>
    /// <param name="weight">추가할 미네랄 데이터의 그램 수</param>
    /// <returns></returns>
    private bool GatherMineral(MineralData md, int weight)
    {
        if(_currentWeight + weight > _maxWeight)    return false;
        else {
            _inventory[md] += weight;
            return true;
        }
    }
}
