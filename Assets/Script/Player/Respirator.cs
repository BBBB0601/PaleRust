using UnityEngine;

public class Respirator : MonoBehaviour
{
    [SerializeField]    private RobotInventory _robotInventory;
    [SerializeField]    private InputReader _inputReader;
    [SerializeField]    private float _gatherInterval;
    [SerializeField, Tooltip("흡입기가 수집 가능하게 할 자원 목록")]    private MineralData[] _mineralDatas;

    private bool _isRespiratorOn;
    private float _currentInterval;

    private void Awake()
    {
        if(_robotInventory == null) _robotInventory = GetComponent<RobotInventory>();
        if(_inputReader == null)    _inputReader = GetComponent<InputReader>();
        _isRespiratorOn = false;
        _currentInterval = 0f;
    }

    public (int mineral, int weight) GatherRandomMineral()
    {
        int mineral = Random.Range(0, _mineralDatas.Length);
        int weight = Random.Range(0, 18) + 10;      // [10 ~ 100]g 사이만큼 얻음

        return (mineral, weight);
    }

    private void OnEnable()
    {
        _inputReader.OnGatherPressed += HandleLeftPressed;
    }

    private void OnDisable()
    {
        _inputReader.OnGatherPressed -= HandleLeftPressed;
    }

    private void HandleLeftPressed(bool isPressed)
    {
        _isRespiratorOn = isPressed;
    }

    private void Update()
    {
        _currentInterval = Mathf.Clamp(_currentInterval - Time.deltaTime, 0f, _gatherInterval);

        if(_currentInterval <= 0 && _isRespiratorOn)
        {
            var (mineral, weight) = GatherRandomMineral();

            if(!_robotInventory.GatherMineral(mineral, weight))
            {
                Debug.Log("인벤토리가 가득 참");
            }
            else
            {
                // Debug.Log("수집한 자원: " + mineral.MineralName.ToString()
                //         + "\n 수집량(g): " + mineral.MineralWeight.ToString());
            }

            // 수집 주기 초기화, _currentInterval이 0이 될 때까지 수집 불가
            _currentInterval = _gatherInterval;
        }
    }
}