using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("플레이어 게임 오브젝트")]
    [SerializeField]
    private GameObject _player;

    [Header("움직임 설정")]
    [SerializeField]    private float moveSpeed = 5.0f;

    private InputSystem_Actions _inputActions;
    private Vector2 _moveInput;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        if(_player == null)     _player = gameObject;
    }

    private void OnEnable()
    {
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

    private void Update()
    {
        _moveInput = _inputActions.Player.Move.ReadValue<Vector2>();

        Vector3 moveDir = new Vector3(_moveInput.x, 0f, _moveInput.y);

        transform.Translate(moveDir * moveSpeed * Time.deltaTime);
    }
}
