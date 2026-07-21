using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private InputSystem_Actions _inputActions;

    public event Action<Vector2> OnMove;
    public event Action<bool> OnGatherPressed;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();

        // WASD 이동
        _inputActions.Player.Move.performed += OnMovePerformed;
        _inputActions.Player.Move.canceled += OnMoveCanceled;

        // 마우스 클릭
        _inputActions.Player.Attack.performed += OnAttackPerformed;
        _inputActions.Player.Attack.canceled += OnAttackCanceled;
    }

    private void OnEnable()
    {
        _inputActions.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Disable();
    }

    private void OnDestroy()
    {
        _inputActions.Player.Move.performed -= OnMovePerformed;
        _inputActions.Player.Move.canceled -= OnMoveCanceled;

        _inputActions.Player.Attack.performed -= OnAttackPerformed;
        _inputActions.Player.Attack.canceled -= OnAttackCanceled;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();

        OnMove?.Invoke(inputVector);
    }

    private void OnMoveCanceled(InputAction.CallbackContext context) => OnMove?.Invoke(Vector2.zero);

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        OnGatherPressed?.Invoke(true);
    }

    private void OnAttackCanceled(InputAction.CallbackContext context)
    {
        OnGatherPressed?.Invoke(false);
    }
}
