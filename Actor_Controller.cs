using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Actor_Controller : MonoBehaviour
{
    public GameObject model;
    public CameraController camcon;
    public IUserInputer pi;
    
    public float walkSpeed;
    public float runMultiplier = 2.0f;
    public float jumpVelocity = 3.0f;
    public float rollVelocity = 1.0f;
    public float jabVelocity = 3.0f;
    

    [Space(10)]
    [Header("===== Friction Setting =====")]
    public PhysicMaterial frictionOne;
    public PhysicMaterial frictionZero;

    
    private Animator anim;
    private Rigidbody rigid;
    private Vector3 planarVec;
    private Vector3 planarVec1; 
    private Vector3 thrustVec;
    private bool canAttack;
    private CapsuleCollider col;
    private float lerpTarget;   //0, 1
    private Vector3 deltaPos;

    
    private bool lockPlanar = false;
    public bool isRoll = false;
    public bool trackDirection = false;

    void Awake()
    {
        
        anim = model.GetComponent<Animator>();
        IUserInputer[] inputs = GetComponents<IUserInputer>();
        foreach (var input in inputs)
        {
            if(input.enabled == true)
            {
                pi = input;
                //print(pi);
                break;
            }
        }
        rigid = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pi.lockon)
        {
            camcon.LockUnlock();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(camcon.lockState == false)
        {
            //anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"),((pi.run) ? runMultiplier : 1.0f), 0.1f));
            anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), ((pi.run) ? runMultiplier : 1.0f), 0.5f));
            anim.SetFloat("right", 0f);
            anim.SetFloat("Vec", anim.GetFloat("forward"));
        }
        else
        {
            Vector3 localDVecz = transform.InverseTransformVector(pi.Dvec);
            //anim.SetFloat("forward", pi.Dvec.z * ((pi.run) ? runMultiplier : 1.0f));
            anim.SetFloat("forward", pi.Dvec.z * ((pi.run) ? runMultiplier : 1.0f));
            anim.SetFloat("right", pi.Dvec.x * ((pi.run) ? runMultiplier : 1.0f));
            anim.SetFloat("Vec", rigid.velocity.magnitude);
            //print(pi.Dvec.z);
            //print((pi.run) ? runMultiplier : 1.0f);
        }

        
        anim.SetBool("defense", pi.defense);
        //anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"),((pi.run) ? runMultiplier : 1.0f), 0.1f));
        //anim.SetFloat("forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("forward"), ((pi.run) ? runMultiplier : 1.0f), 0.5f));
        anim.SetBool("defense", pi.defense);
        if(pi.roll)
        {
            if (pi.Dmag <= 0.1f)
                planarVec1 = model.transform.forward * walkSpeed * rollVelocity;
            else
            {
                //float x = rigid.velocity.x / (Mathf.Sqrt((rigid.velocity.x * rigid.velocity.x) + (rigid.velocity.z * rigid.velocity.z)));
                //float z = rigid.velocity.z / (Mathf.Sqrt((rigid.velocity.x * rigid.velocity.x) + (rigid.velocity.z * rigid.velocity.z)));
                //planarVec1 = new Vector3(x,0,z) * walkSpeed * rollVelocity;
                planarVec1 = rigid.velocity.normalized * walkSpeed * rollVelocity;

            }
            anim.SetTrigger("roll");
            //print(pi.roll);
            canAttack = false;
            //pi.inputEnable = false;
            
            
        }

        if (pi.jump)
        {
            anim.SetTrigger("jump");
            canAttack = false;
        }

        if (pi.attack && CheckState("ground") && canAttack)
            anim.SetTrigger("attack");
        if (!camcon.lockState)
        {
            if (pi.Dmag > 0.1f)
            {
                model.transform.forward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.3f); ;
            }
            if (!lockPlanar)
            {
                planarVec = pi.Dmag * model.transform.forward * walkSpeed * ((pi.run) ? runMultiplier : 1.0f);
            }
        }
        else
        {
            //print(model.transform.forward);
            if (trackDirection == false)    //不让追踪，面向前
                model.transform.forward = transform.forward;
            else
                model.transform.forward = planarVec.normalized;  //单位化而不改变
            if (!lockPlanar)
            {
                planarVec = pi.Dvec * walkSpeed * ((pi.run) ? runMultiplier : 1.0f);
            }

        }
        
        
        

    }
    private void FixedUpdate()
    {
        rigid.position += deltaPos;
        //rigid.position += movingVec * Time.fixedDeltaTime * walkSpeed;  //速度乘时间
        if(!isRoll)
            rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrustVec;
        else
            rigid.velocity = new Vector3(planarVec1.x, rigid.velocity.y, planarVec1.z);
        //print(rigid.velocity);
        thrustVec = Vector3.zero;
        deltaPos = Vector3.zero;

    }

    public bool CheckState(string stateName, string layerName = "Base Layer")
    {
        return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsName(stateName);
    }
    /// <summary>
    /// Message Processing Block
    /// </summary>

    private void OnJumpEnter()
    {
        //print("On Jump");
        pi.inputEnable = false;
        lockPlanar = true;
        thrustVec = new Vector3(0, jumpVelocity, 0);
        trackDirection = true;
    }

    private void OnJumpExit()
    {
        //print("Exit");
        
    }

    public void IsGround()
    {
        anim.SetBool("isGround", true);
    }

    public void IsNotGround()
    {
        anim.SetBool("isGround", false);
    }

    public void OnGroundEnter()
    {
        pi.inputEnable = true;
        lockPlanar = false;
        canAttack = true;
        col.material = frictionOne;
        trackDirection = false;
        
    }

    public void OnGroungExit()
    {
        col.material = frictionZero;
    }

    public void OnFallEnter()
    {
        anim.SetBool("isGround", false);
        lockPlanar = true;
    }

    public void OnRollEnter()
    {
        isRoll = true;
        pi.inputEnable = false;
        trackDirection = true;
        //float x = rigid.velocity.x / (Mathf.Sqrt(rigid.velocity.x * rigid.velocity.x + rigid.velocity.z * rigid.velocity.z * rigid.velocity.z));
        //float z = rigid.velocity.z / (Mathf.Sqrt(rigid.velocity.x * rigid.velocity.x + rigid.velocity.z * rigid.velocity.z * rigid.velocity.z));
        //rigid.velocity *= 12f; 
        //pi.inputEnable = false;
        //print(rigid.velocity);
    }

    public void OnRollExit()
    {
        isRoll = false;
        
        pi.inputEnable = true;
        
    }

    public void OnJabEnter()
    {
        pi.inputEnable = false;
        lockPlanar = true;
        
    }

    public void OnJabUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("jabVelocity");
    }

    

    public void OnAttackIdleEnter()
    {
        pi.inputEnable = true;
        //lockPlanar = false;
        //anim.SetLayerWeight(anim.GetLayerIndex("attack"), 0f);
        lerpTarget = 0f;
    }
    public void OnAttackIdleUpdate()
    {
        float currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("attack"));
        currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.3f);
        anim.SetLayerWeight(anim.GetLayerIndex("attack"), currentWeight);
    }

    public void OnAttack1hAEnter()
    {
        pi.inputEnable = false;
        //lockPlanar = true;
        lerpTarget = 1.0f;
    }

    public void OnAttack1hAUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("attack1hAVelocity");
        float currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("attack"));
        currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.3f);
        anim.SetLayerWeight(anim.GetLayerIndex("attack"), currentWeight);
    }

    public void OnUpdateRM(object _deltaPos) //_ 说明是区域变量
    {
        //print(_deltaPos);
        if(CheckState("attack1hC", "attack"))
            deltaPos += (0.2f*deltaPos + 0.8f*(Vector3)_deltaPos)/1.0f;
    }


}
