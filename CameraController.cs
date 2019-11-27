using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //public PlayerInput pi;
    public IUserInputer pi;
    public float horizontalSpeed = 20.0f;
    public float verticalSpeed = 20f;
    public float cameraDampVelue = 0.5f;

    private GameObject playerHandle;
    private GameObject cameraHandle;
    private GameObject modle;
    private GameObject _camera;
    private float tempEulerX;

    private Vector3 cameraDampVelocity;

    void Awake()
    {
        IUserInputer[] inputs = GetComponentsInParent<IUserInputer>();
        foreach (var input in inputs)
        {
            if (input.enabled == true)
            {
                pi = input;
                //print(pi);
                break;
            }
        }
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        Actor_Controller ac = playerHandle.GetComponent<Actor_Controller>();
        modle = ac.modle;
        
        print("camera" + pi);


        _camera = Camera.main.gameObject;
        tempEulerX = 20;


        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 tempModelEuler = modle.transform.eulerAngles;
        playerHandle.transform.Rotate(Vector3.up, pi.Jright * horizontalSpeed * Time.fixedDeltaTime);
        //cameraHandle.transform.Rotate(Vector3.right, pi.Jup * -verticalSpeed * Time.deltaTime);
        //print("camera" + pi.Jup);

        tempEulerX -= pi.Jup * -verticalSpeed * Time.fixedDeltaTime;
        tempEulerX = Mathf.Clamp(tempEulerX, -70, 70);

        cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);

        modle.transform.eulerAngles = tempModelEuler;

        _camera.transform.position = Vector3.SmoothDamp(_camera.transform.position, transform.position, ref cameraDampVelocity, cameraDampVelue);
        //camera.transform.eulerAngles = transform.eulerAngles;
        _camera.transform.LookAt(cameraHandle.transform);
    }
}
