using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ShelterZone : MonoBehaviour
{
    [SerializeField]    private BoxCollider _shelterZone;
    [SerializeField, Tooltip("피해를 입는 주기")]    private float _damageInterval;
    [SerializeField, Tooltip("피해량")]    private float _damageAmount;

    private Coroutine _damageCouroutine;

    private Dictionary<MineralData, int> _shelterStorage = new Dictionary<MineralData, int>();

    private void Start()
    {
        if(_shelterZone == null)    _shelterZone = GetComponent<BoxCollider>();
        if(_damageAmount <= 0)      _damageAmount = 10;
        if(_damageInterval <= 0)    _damageInterval = 1;
    }

    private void OnTriggerExit(Collider other)
    {
        if(_damageCouroutine == null)
            _damageCouroutine = StartCoroutine(ApplyDamage(other));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_damageCouroutine != null)
            StopCoroutine(_damageCouroutine);
        _damageCouroutine = null;

        var robotInventory = other.GetComponent<RobotInventory>();

        if(robotInventory != null) {
            foreach(var (val, key) in robotInventory.Inventory)
                _shelterStorage[val] += key;
            robotInventory.ResetInventory();
        }
    }

    private IEnumerator ApplyDamage(Collider robot)
    {
        var robotHp = robot.GetComponent<RobotStat>();

        while(true)
        {
            if(robotHp != null)
            {
                robotHp.TakeDamage(_damageAmount);
            }

            yield return new WaitForSeconds(_damageInterval);
        }
    }
}
