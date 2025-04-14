using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class P_Movement : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    [Range(-30, 0)]public float GravityForce;

    private Rigidbody _rb;
    private Vector3 _velocity;
    public bool _isGrounded;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {        
        _rb.linearVelocity = ((_velocity.z * transform.forward) + (_velocity.y * transform.up) + (_velocity.x * transform.right)).normalized * Speed * Time.deltaTime;
        Gravity();
    }

    public void OnInputMove(InputAction.CallbackContext context)
    {
        Vector2 dir = context.ReadValue<Vector2>();
        Vector3 finalDir = new Vector3(dir.x, 0, dir.y);

        _velocity = finalDir;
        //_velocity = finalDir.normalized * Speed;
    }

    public void OnInputJump(InputAction.CallbackContext context)
    {
        if(_isGrounded && context.started)
        {
            _isGrounded = false;
            _rb.AddForce(transform.up * JumpForce, ForceMode.Impulse);
        }        
    }

    public void Gravity()
    {
        if (Physics.Raycast(transform.position, -transform.up, 1.01f))
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }

        if(!_isGrounded) 
        {
            _velocity.y += GravityForce * Time.deltaTime;
        }
        else
        {
            _velocity.y = 0;
        }
    }
}
