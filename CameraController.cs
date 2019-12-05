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
    public bool isAI = false;

    private GameObject playerHandle;
    private GameObject cameraHandle;
    private GameObject model;
    private GameObject _camera;
    
    private float tempEulerX;
    [SerializeField]
    private LockTarget lockTarget;

    private Vector3 cameraDampVelocity;

    int i = 0;
    void Start()
    {
        
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        Actor_Controller ac = playerHandle.GetComponent<Actor_Controller>();
        model = ac.model;
        tempEulerX = 20;
        pi = ac.pi;

        if (!isAI)
        {
            _camera = Camera.main.gameObject;
            lockDot.enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        
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
            Vector3 tempForward = lockTarget.obj.transform.position - model.transform.position;
            tempForward.y = 0;
            playerHandle.transform.forward = tempForward;
            cameraHandle.transform.LookAt(lockTarget.obj.transform);
        }

        if (!isAI)   //摄像机跟随
        {
            _camera.transform.position = Vector3.SmoothDamp(_camera.transform.position, transform.position, ref cameraDampVelocity, cameraDampVelue);
            //camera.transform.eulerAngles = transform.eulerAngles;
            _camera.transform.LookAt(cameraHandle.transform);
        }
    }

    private void Update()
    {
        if (lockTarget != null)
        {
            if (!isAI)
            {
                lockDot.rectTransform.position = Camera.main.WorldToScreenPoint(lockTarget.obj.transform.position + new Vector3(0, lockTarget.halfHeight, 0));
            }
                if (Vector3.Distance(model.transform.position, lockTarget.obj.transform.position) > 10.0f)
            {
                LockProcessA(null, false, false, isAI);
            }
              
        }
    }

    private void LockProcessA(LockTarget _locktaget, bool _lockDotEnable, bool _lockState, bool _isAI)
    {
        lockTarget = _locktaget;
        if(!_isAI)
            lockDot.enabled = _lockDotEnable;
        lockState = _lockState;
    }

    public void LockUnlock()
    {
        //print("Lockunlock");
        //try to lock
        Vector3 modelOrigin1 = this.model.transform.position;
        Vector3 modelOrigin2 = modelOrigin1 + new Vector3(0, 1, 0);  //从脚下origin到中点
        Vector3 boxCenter = modelOrigin2 + model.transform.forward * 5.0f;
        Collider[] cols = Physics.OverlapBox(boxCenter, new Vector3(0.5f, 0.5f, 5.0f), model.transform.rotation, LayerMask.GetMask(isAI?"Player":"Enemy"));
        //print("lenth"+cols.Length);
        
        if (cols.Length == 0)
        {
            LockProcessA(null, false, false, isAI);
            i = 0;
        }
        else
        {
            foreach (var col in cols)
            {
                if (lockTarget != null && lockTarget.obj == col.gameObject)
                {
                    LockProcessA(null, false, false, isAI);
                    break;
                }
                
                LockProcessA(new LockTarget(col.gameObject, col.bounds.extents.y), true, true, isAI);
                break;
            }
            
            
            
            

        }
        
        
        
    }

    private class LockTarget
    {
        public GameObject obj;
        public float halfHeight;

        public LockTarget(GameObject _obj, float _halfHeight)
        {
            obj = _obj;
            halfHeight = _halfHeight;
        }

    }
        

}
