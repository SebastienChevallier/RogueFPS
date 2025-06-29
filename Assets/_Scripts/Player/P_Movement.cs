using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class P_Movement : MonoBehaviour
{
    public float Speed;
    public float airSpeedMulti;
    public float JumpForce;
    [Range(-30, 0)]public float GravityForce;
    

    public Rigidbody _rb;
    public Vector3 _velocity;
    public Vector3 _dir;
    public bool _isGrounded;
    [Header("Ground Check Settings")]
    public float GroundCheckDistance = 1.1f;
    public float GroundCheckRadius = 0.25f;
    public LayerMask GroundMask = ~0;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // FixedUpdate is called every physics step
    void FixedUpdate()
    {
        CheckGround();
        Vector3 FinalVelocity = Vector3.zero;
        
        FinalVelocity += _dir.x * transform.right * Speed * airSpeedMulti;
        FinalVelocity += _dir.z * transform.forward * Speed * airSpeedMulti;
        FinalVelocity += _dir.y * transform.up;

        FinalVelocity += _velocity;

        //_velocity = Vector3.Lerp(_velocity, Vector3.zero, Time.deltaTime);
        _velocity.x = Mathf.Lerp(_velocity.x, 0, Time.deltaTime);
        _velocity.y = Mathf.Lerp(_velocity.y, 0, Time.deltaTime);
        _velocity.z = Mathf.Lerp(_velocity.z, 0, Time.deltaTime);

        _rb.linearVelocity = FinalVelocity;
        Gravity();
    }

    private void CheckGround()
    {
        Vector3 origin = transform.position - transform.up * (GroundCheckDistance - GroundCheckRadius);
        bool grounded = Physics.CheckSphere(origin, GroundCheckRadius, GroundMask);

        if (grounded)
        {
            if (!_isGrounded)
            {
                _isGrounded = true;
                airSpeedMulti = 1;
            }
        }
        else
        {
            _isGrounded = false;
            airSpeedMulti = 0.65f;
        }
    }

    public void OnInputMove(InputAction.CallbackContext context)
    {
        Vector2 dir = context.ReadValue<Vector2>();
        Vector3 finalDir = new Vector3(dir.x, _dir.y, dir.y);

        _dir = finalDir;

        if (_isGrounded && context.started)
        {            
            _dir.y = 0;
        }
    }

    public void OnInputJump(InputAction.CallbackContext context)
    {
        if(_isGrounded && context.started)
        {
            _isGrounded = false;
            _dir.y = 0;
            //_rb.AddForce(JumpForce * transform.up, ForceMode.Impulse);
            _dir.y = JumpForce;
        }        
    }

    public void Impulse(Vector3 direction)
    {
        _velocity = Vector3.zero;
        _dir.y = 0;
        //_rb.AddForce(direction, forceMode);
        _velocity = direction;
    }

    public void Gravity()
    {        
        if(!_isGrounded) 
        {
            _dir.y += GravityForce * Time.fixedDeltaTime;
        }
            
    }
}
