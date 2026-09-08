using UnityEngine;
using UnityEngine.InputSystem;

public class FlyCamera : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float lookSensitivity = 0.12f;

    float yaw;
    float pitch;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    void Update()
    {
        if (Keyboard.current == null || Mouse.current == null)
            return;

        if (Mouse.current.rightButton.wasPressedThisFrame)
            Cursor.lockState = CursorLockMode.Locked;

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
            Cursor.lockState = CursorLockMode.None;

        if (Cursor.lockState != CursorLockMode.Locked)
            return;

        float x = (Keyboard.current.dKey.isPressed ? 1f : 0f)
                - (Keyboard.current.aKey.isPressed ? 1f : 0f);
        float z = (Keyboard.current.wKey.isPressed ? 1f : 0f)
                - (Keyboard.current.sKey.isPressed ? 1f : 0f);
        float y = (Keyboard.current.eKey.isPressed ? 1f : 0f)
                - (Keyboard.current.qKey.isPressed ? 1f : 0f);

        float speed = Keyboard.current.leftShiftKey.isPressed
            ? moveSpeed * 3f
            : moveSpeed;

        Vector3 direction =
            transform.right * x +
            transform.forward * z +
            Vector3.up * y;

        transform.position += direction.normalized * speed * Time.deltaTime;

        Vector2 mouseDelta = Mouse.current.delta.ReadValue() * lookSensitivity;
        yaw += mouseDelta.x;
        pitch = Mathf.Clamp(pitch - mouseDelta.y, -80f, 80f);
        transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }
}