using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GravityGun _gravityGun;
    private Rigidbody _rigidBody;
    private CameraLook _cameraLook;

    //[SerializeField] private Transform _respawnTransform;
    [SerializeField] private Checkpoint _activeCheckpoint;

    [Header("Movement")]
    private Vector2 _inputAxes;
    private Vector3 _movementVector;
    [SerializeField] private float _movementSpeed = 0.25f;
    [SerializeField] private float _jumpHeight = 10;

    private bool _jumpPressed;



    // ~~~ START AND UPDATE ~~~
    void Start()
    {
        _gravityGun = GetComponentInChildren<GravityGun>();
        _rigidBody = GetComponent<Rigidbody>();
        _cameraLook = GetComponentInChildren<CameraLook>();
    }
    void Update()
    {
        //Check for movement input
        UpdateMovementVector();
        
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
            _jumpPressed = true;

        //Check for other input
        if (Input.GetKeyDown(KeyCode.R))
            Respawn();

    }

    void FixedUpdate()
    {
        //update rotation based on camera look
        gameObject.transform.rotation = Quaternion.Euler(0f, _cameraLook.GetYAxisRotation(), 0f);

        //move player
        gameObject.transform.Translate(_movementVector);

        //check for jump
        if (_jumpPressed)
        {
            _rigidBody.AddForce(_jumpHeight * Vector3.up, ForceMode.Impulse);
            _jumpPressed = false;
        }
    }


    // ~~~ MOVEMENT CONTROLS ~~~
    private void UpdateMovementVector()
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

    }
    private bool IsGrounded()
    {
        //Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.5f, Color.red);
        return Physics.Raycast(transform.position, Vector3.down, 1.5f);
    }

    // ~~~ RESPAWNS ~~~
    public void SetActiveCheckpoint(Checkpoint checkpoint)
    {
        _activeCheckpoint = checkpoint;
    }

    public void Respawn()
    {
        _gravityGun.DropObject();
        gameObject.transform.SetPositionAndRotation(
            _activeCheckpoint.GetRespawnPoint().position, 
            _activeCheckpoint.GetRespawnPoint().rotation);

        _activeCheckpoint._onRespawnEvent.Invoke();     //do whatever other specified custom events
    }

    public GravityGun GetGravityGun()
    {
        return _gravityGun;
    }
}
