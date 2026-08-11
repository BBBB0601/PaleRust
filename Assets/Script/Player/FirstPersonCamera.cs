using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField]    private Transform player;

    [Header("마우스 민감도")]
    [SerializeField]    private float mouseSensitivity = 100f;

    private float _xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -85f, 85f);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        player.Rotate(Vector3.up * mouseX);
    }
}
