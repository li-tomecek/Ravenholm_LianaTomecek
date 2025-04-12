using UnityEngine;

public class CameraLook : MonoBehaviour
{
    private Vector2 _lookRotation = Vector2.zero;
    [SerializeField] private float _lookSpeed = 3f;
    [SerializeField] private float _verticalAngleClamp = 30f;   //


    public void Start()
    {
        _lookRotation.x = transform.eulerAngles.x;
        _lookRotation.y = transform.eulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;
    }


    public void Update()
    {
        //update the look rotation values based on Mouse input. ~ horizontal look (rotation) in player controller

        _lookRotation.y += Input.GetAxis("Mouse X");        //rotate around Y axis based on X mouse position
        _lookRotation.x -= Input.GetAxis("Mouse Y");        //rotate around X axis based on Y mouse position

        //Clamp the vertical (x axis) rotation
        _lookRotation.x = Mathf.Clamp(_lookRotation.x, -_verticalAngleClamp, _verticalAngleClamp);

        //move the camera (vertical look)
        gameObject.transform.localRotation = Quaternion.Euler(_lookRotation.x * _lookSpeed, 0f, 0f);        
    }

    public Vector2 GetLookRotation()
    {
        return _lookRotation;
    }

    public float GetYAxisRotation()
    {
        return _lookRotation.y * _lookSpeed;
    }
}
