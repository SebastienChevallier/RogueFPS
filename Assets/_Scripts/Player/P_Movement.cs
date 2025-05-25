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
    public float descreaseFactor = 0.1f;
    public Vector3 _dir;
    public bool _isGrounded;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 FinalVelocity = Vector3.zero;
        
        FinalVelocity += _dir.x * transform.right * Speed * airSpeedMulti;
        FinalVelocity += _dir.z * transform.forward * Speed * airSpeedMulti;
        FinalVelocity += _dir.y * transform.up;

        FinalVelocity += _velocity;

        //_velocity = Vector3.Lerp(_velocity, Vector3.zero, Time.deltaTime);
        _velocity.x = Mathf.Lerp(_velocity.x, 0, Time.deltaTime * descreaseFactor);
        _velocity.y = Mathf.Lerp(_velocity.y, 0, Time.deltaTime * descreaseFactor);
        _velocity.z = Mathf.Lerp(_velocity.z, 0, Time.deltaTime * descreaseFactor);

        _rb.linearVelocity = FinalVelocity;
        Gravity();
    }

    private void Update()
    {

        if (Physics.Raycast(transform.position, -transform.up, 1.1f))
        {
            if(!_isGrounded) 
            {
                _isGrounded = true;
                airSpeedMulti = 1;
                //_dir.y = 0;
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
            _velocity = Vector3.zero; // Reset velocity on ground movement start
        }

        if (_isGrounded)
        {            
            _velocity = Vector3.zero; // Reset velocity on ground movement start
        }
    }

    public void OnInputJump(InputAction.CallbackContext context)
    {
        if(_isGrounded && context.started)
        {
            _isGrounded = false;
            _dir.y = 0;            
            _dir.y = JumpForce;
        }        
    }

    public void Impulse(Vector3 direction)
    {
        //_velocity = Vector3.zero;
        _dir.y = 0;        
        _velocity = direction;        
    }

    public void Gravity()
    {        
        if(!_isGrounded) 
        {
            _dir.y += GravityForce * Mathf.Exp(Time.fixedDeltaTime);
        }
        else
        {
            _velocity = Vector3.Lerp(_velocity, Vector3.zero, Time.deltaTime * descreaseFactor * 100);
        }           
    }
}
