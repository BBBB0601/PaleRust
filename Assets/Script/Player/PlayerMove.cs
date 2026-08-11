using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("플레이어 게임 오브젝트")]
    [SerializeField]    private GameObject _player;
    [SerializeField]    private CharacterController _characterController;
    [SerializeField]    private InputReader _inputReader;

    [Header("움직임 설정")]
    [SerializeField]    private float moveSpeed = 5.0f;
    [SerializeField]    private float gravity = 1.0f;

    private Vector2 _moveInput;
    private float _verticalVelocity;

    private const float GRAVITY_FORCE = -9.8f;

    private void Awake()
    {
        if(_player == null)     _player = gameObject;
        if(_characterController == null)    _characterController = GetComponent<CharacterController>();
        if(_inputReader == null)    _inputReader = GetComponent<InputReader>();
    }

    private void OnEnable()
    {
        _inputReader.OnMove += HandleMove;
    }

    private void OnDisable()
    {
        _inputReader.OnMove -= HandleMove;
    }

    private void HandleMove(Vector2 input) => _moveInput = input;

    private void Update()
    {
        Vector3 move = new Vector3(_moveInput.x, 0, _moveInput.y);
        move = transform.TransformDirection(move) * moveSpeed;

        // 땅에 접했더라도 미세하게 떨어지게 함
        if(_characterController.isGrounded) _verticalVelocity = -0.5f;
        else                                _verticalVelocity += gravity * GRAVITY_FORCE * Time.deltaTime;

        move.y = _verticalVelocity;

        _characterController.Move(move * Time.deltaTime);
    }
}
