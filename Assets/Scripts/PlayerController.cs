using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private CameraLook _cameraLook;
    
    [Header("Movement")]
    private Vector2 _inputAxes;
    private Vector3 _movementVector;
    [SerializeField] private float _movementSpeed = 0.25f;
    [SerializeField] private float _jumpHeight = 10;
    
    
    
    
    
    // ~~~ START AND UPDATE ~~~
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _cameraLook = GetComponentInChildren<CameraLook>();
    }

    void FixedUpdate()
    {
        //update rotation based on camera look
        gameObject.transform.rotation = Quaternion.Euler(0f, _cameraLook.GetYAxisRotation(), 0f);

        //Check for movement input
        CheckMovement();
        CheckJump();


    }


    //~~~ MOVEMENT CONTROLS ~~~
    private void CheckMovement()
    {
        _inputAxes.x = Input.GetAxis("Horizontal");
        _inputAxes.y = Input.GetAxis("Vertical");

        _movementVector = Vector3.zero;

        if (_inputAxes.y > 0f)          //move forwards
        {
            _movementVector += Vector3.forward * _movementSpeed;
        } 
        else if (_inputAxes.y < 0f)     //move backwards
        {
            _movementVector += Vector3.back * _movementSpeed;
        }

        if (_inputAxes.x > 0f)          //move right
        {
            _movementVector += Vector3.right * _movementSpeed;
        }
        else if (_inputAxes.x < 0f)     //move left
        {
            _movementVector += Vector3.left * _movementSpeed;
        }

        gameObject.transform.Translate(_movementVector);
    }
    private void CheckJump()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            _rigidBody.AddForce(_jumpHeight * Vector3.up, ForceMode.Impulse);
        }
    }
    private bool IsGrounded()
    {
        //Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.5f, Color.red);
        return Physics.Raycast(transform.position, Vector3.down, 1.5f);
    }

}
