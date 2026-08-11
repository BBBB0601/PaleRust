using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ShelterZone : MonoBehaviour
{
    public static ShelterZone Instance;

    [SerializeField]    private BoxCollider _shelterZone;
    [SerializeField, Tooltip("피해를 입는 주기")]    private float _damageInterval;
    [SerializeField, Tooltip("피해량")]    private float _damageAmount;

    private Coroutine _damageCouroutine;

    private Dictionary<int, int> _shelterStorage = new Dictionary<int, int>();

    public System.Action OnStorageChanged;
    public IReadOnlyDictionary<int, int> ShelterStorage => _shelterStorage;

    private void Start()
    {
        if(Instance == null)    Instance = this;
        else                    Destroy(this);
        if(_shelterZone == null)    _shelterZone = GetComponent<BoxCollider>();
        if(_damageAmount <= 0)      _damageAmount = 10;
        if(_damageInterval <= 0)    _damageInterval = 1;
    }
    
    public bool ConsumeMineral(int mdID, int amount)
    {
        if(_shelterStorage.TryGetValue(mdID, out var mdAmount))
        {
            if(mdAmount < amount)   return false;

            _shelterStorage[mdID] -= amount;
            return true;
        }

        return false;
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
            bool changed = false;
            foreach(var (md, weight) in robotInventory.Inventory)
            {
                if(_shelterStorage.TryGetValue(md, out int cw))
                {
                    _shelterStorage[md] = cw + weight;
                }
                else
                {
                    _shelterStorage[md] = weight;
                }
                changed = true;
            }
            robotInventory.ResetInventory();

            if (changed)
            {
                OnStorageChanged?.Invoke();
            }
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
