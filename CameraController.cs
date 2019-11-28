using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    //public PlayerInput pi;
    public IUserInputer pi;
    public float horizontalSpeed = 20.0f;
    public float verticalSpeed = 20f;
    public float cameraDampVelue = 0.5f;
    public Image lockDot;          //用image做指示
    public bool lockState;

    private GameObject playerHandle;
    private GameObject cameraHandle;
    private GameObject model;
    private GameObject _camera;
    
    private float tempEulerX;
    [SerializeField]
    private GameObject lockTarget;

    private Vector3 cameraDampVelocity;

    int i = 0;
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
        model = ac.model;
        
        //print("camera" + pi);


        _camera = Camera.main.gameObject;
        tempEulerX = 20;


        Cursor.lockState = CursorLockMode.Locked;
        lockDot.enabled = false;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (lockTarget == null)
        {
            Vector3 tempModelEuler = model.transform.eulerAngles;
            playerHandle.transform.Rotate(Vector3.up, pi.Jright * horizontalSpeed * Time.fixedDeltaTime);
            //cameraHandle.transform.Rotate(Vector3.right, pi.Jup * -verticalSpeed * Time.deltaTime);
            //print("camera" + pi.Jup);

            tempEulerX -= -pi.Jup * -verticalSpeed * Time.fixedDeltaTime;
            tempEulerX = Mathf.Clamp(tempEulerX, -70, 70);

            cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);

            model.transform.eulerAngles = tempModelEuler;
        }
        else
        {
            Vector3 tempForward = lockTarget.transform.position - model.transform.position;
            tempForward.y = 0;
            playerHandle.transform.forward = tempForward;
        }

        _camera.transform.position = Vector3.SmoothDamp(_camera.transform.position, transform.position, ref cameraDampVelocity, cameraDampVelue);
        //camera.transform.eulerAngles = transform.eulerAngles;
        _camera.transform.LookAt(cameraHandle.transform);
    }

    public void LockUnlock()
    {
        //print("Lockunlock");
        //try to lock
        Vector3 modelOrigin1 = this.model.transform.position;
        Vector3 modelOrigin2 = modelOrigin1 + new Vector3(0, 1, 0);  //从脚下origin到中点
        Vector3 boxCenter = modelOrigin2 + model.transform.forward * 5.0f;
        Collider[] cols = Physics.OverlapBox(boxCenter, new Vector3(0.5f, 0.5f, 5.0f), model.transform.rotation, LayerMask.GetMask("Enemy"));
        //print("lenth"+cols.Length);
        
        if (cols.Length == 0)
        {
            lockTarget = null;
            lockDot.enabled = false;
            lockState = false;
            i = 0;
        }
        else
        {
            lockDot.enabled = true;
            lockState = true;
            //print("cols[0]"+cols[0]);
            //print("i="+i);
            if (i < cols.Length && cols[0] != lockTarget)
            {
                lockTarget = cols[i].gameObject;
                i += 1;
                //print(lockTarget);
                if (i >= cols.Length)
                {
                    i = 0;
                    //lockTarget = cols[i].gameObject;
                    print("replay");
                }
            }
            else
            {
                i = 0;
                lockTarget = cols[0].gameObject;
            }
            

        }
        
        
        
    }
}
