using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerMove : MonoBehaviour
{
    [Header("플레이어 게임 오브젝트")]
    [SerializeField]    private GameObject _player;
    [SerializeField]    private CharacterController _characterController;

    [Header("움직임 설정")]
    [SerializeField]    private float moveSpeed = 5.0f;
    [SerializeField]    private float gravity = 1.0f;

    private InputSystem_Actions _inputActions;
    private Vector2 _moveInput;
    private float _verticalVelocity;

    private const float GRAVITY_FORCE = -9.8f;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        if(_player == null)     _player = gameObject;
        if(_characterController == null)    _characterController = GetComponent<CharacterController>();
    }

    private void OnEnable() =>  _inputActions.Enable();
    private void OnDisable() => _inputActions.Disable();

    private void Update()
    {
        _moveInput = _inputActions.Player.Move.ReadValue<Vector2>();

        Vector3 move = new Vector3(_moveInput.x, 0, _moveInput.y);
        move = transform.TransformDirection(move) * moveSpeed;

        // 바닥에 접지하더라도 미세하게 밑으로 떨어지도록 함
        if(_characterController.isGrounded) _verticalVelocity = -0.5f;
        else                                _verticalVelocity += gravity * GRAVITY_FORCE * Time.deltaTime;

        move.y = _verticalVelocity;

        _characterController.Move(move * Time.deltaTime);
    }
}
