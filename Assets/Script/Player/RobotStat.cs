using UnityEngine;

public class RobotStat : MonoBehaviour
{
    [SerializeField]   private float _maxHp;

    private float _currentHp;

    void Start()
    {
        _maxHp = 100;
        _currentHp = _maxHp;
    }

    private void Update()
    {
        // test code
        Debug.Log("현재 hp = " + _currentHp.ToString());    
    }


    public void TakeDamage(float amount) => _currentHp = Mathf.Clamp(_currentHp - amount, 0, _maxHp);

    public void GetRepair(float amount) => _currentHp = Mathf.Clamp(_currentHp + amount, 0, _maxHp);
}