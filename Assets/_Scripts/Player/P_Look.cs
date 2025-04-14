using UnityEngine;
using UnityEngine.InputSystem;

public class P_Look : MonoBehaviour
{
    public Camera Camera;
    public float sensibilityX, sensibilityY;
    private float _xRotation, _yRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Aim(InputAction.CallbackContext context)
    {
        Vector2 mouseMove = context.ReadValue<Vector2>();

        _xRotation += mouseMove.x * Time.deltaTime * sensibilityX;
        _yRotation -= mouseMove.y * Time.deltaTime * sensibilityY;
        _yRotation = Mathf.Clamp(_yRotation, -90, 90);

        transform.rotation = Quaternion.Euler(0, _xRotation, 0);
        Camera.transform.localRotation = Quaternion.Euler(_yRotation, 0, 0);

    }
}
