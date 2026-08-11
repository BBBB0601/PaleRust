using System.Collections.Generic;
using UnityEngine;

public class RobotRepair : MonoBehaviour
{
    [SerializeField]
    private RobotStat rs;

    private void Start()
    {
        if(rs == null)  rs = GetComponent<RobotStat>();
    }

    public void Repair()
    {
        IReadOnlyDictionary<int, int> shelterStorage = ShelterZone.Instance.ShelterStorage;

        // 1 = 구리
        // 2 = 철
        // 3 = 마그네슘
        if(shelterStorage[1] >= 4 &&
           shelterStorage[2] >= 5 &&
           shelterStorage[3] >= 2)
        {
            ShelterZone.Instance.ConsumeMineral(1, 4);
            ShelterZone.Instance.ConsumeMineral(2, 5);
            ShelterZone.Instance.ConsumeMineral(3, 2);

            rs.GetRepair(20);
        }
    }
}
