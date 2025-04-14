using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class P_Movement : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    [Range(-30, 0)]public float GravityForce;
    public ForceMode forceMode;

    public Rigidbody _rb;
    public Vector3 _velocity;
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
        
        FinalVelocity += _dir.x * transform.right * Speed;
        FinalVelocity += _dir.z * transform.forward * Speed;
        FinalVelocity += _dir.y * transform.up;

        FinalVelocity += _velocity;

        //_velocity = Vector3.Lerp(_velocity, Vector3.zero, Time.deltaTime);
        _velocity.x = Mathf.Lerp(_velocity.x, 0, Time.deltaTime);
        _velocity.y = Mathf.Lerp(_velocity.y, 0, Time.deltaTime);
        _velocity.z = Mathf.Lerp(_velocity.z, 0, Time.deltaTime);

        _rb.linearVelocity = FinalVelocity;
        Gravity();
    }

    private void Update()
    {

        if (Physics.Raycast(transform.position, -transform.up, 1.1f))
        {
            _isGrounded = true;            
        }
        else
        {
            _isGrounded = false;
        }
    }

    public void OnInputMove(InputAction.CallbackContext context)
    {
        Vector2 dir = context.ReadValue<Vector2>();
        Vector3 finalDir = new Vector3(dir.x, _dir.y, dir.y);

        _dir = finalDir;
        //_velocity = finalDir.normalized * Speed;
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
