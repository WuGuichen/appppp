using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Actor_Controller : MonoBehaviour
{
    public GameObject model;
    public GameObject playerDir;
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
    //private float lerpTarget;   //0, 1
    private Vector3 deltaPos;

    public bool leftIsShield = true;
    
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
                
                planarVec1 = pi.Dvec.normalized * walkSpeed * rollVelocity;

            }
            //print("M" + model.transform.forward);
            //print("D"+planarVec1);
            //print("pi.Vec"+pi.Dvec.normalized);
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

        if ((pi.rb || pi.lb) && (CheckState("ground") || CheckStateTag("attackR") || CheckStateTag("attackL")) && canAttack)
        {
            if (pi.rb)
            {
                anim.SetBool("R0L1", false);
                anim.SetTrigger("attack");
            }
            else if (pi.lb && !leftIsShield)
            {
                anim.SetBool("R0L1", true);
                anim.SetTrigger("attack");
            }
            
        }

        // counterBack
        if (pi.rt || pi.lt && (CheckState("ground") || CheckStateTag("attackR") || CheckStateTag("attackL")) && canAttack)
        {
            if (pi.rt)
            {

            }
            else
            {
                if(!leftIsShield)
                {

                }
                else
                {
                    anim.SetTrigger("counterBack");
                }
            }
        }


        if (leftIsShield)
        {
            if (CheckState("ground") || CheckState("blocked"))
            {
                anim.SetBool("defense", pi.defense);
                anim.SetLayerWeight(anim.GetLayerIndex("defense"), 1);

            }
            else
            {
                anim.SetBool("defense", false);
                anim.SetLayerWeight(anim.GetLayerIndex("defense"), 0);

            }
        }
        else
        {
            anim.SetLayerWeight(anim.GetLayerIndex("defense"), 0);
        }

        if (!camcon.lockState)
        {
            if (pi.Dmag > 0.1f)
            {
                model.transform.forward = Vector3.Slerp(model.transform.forward, pi.Dvec, 0.5f);
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

        rigid.position += deltaPos;
        //rigid.position += movingVec * Time.fixedDeltaTime * walkSpeed;  //速度乘时间
        if (!isRoll)
            rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrustVec;
        else
            rigid.velocity = new Vector3(planarVec1.x, rigid.velocity.y, planarVec1.z) + thrustVec;
        //print(rigid.velocity);
        thrustVec = Vector3.zero;
        deltaPos = Vector3.zero;
    }
    private void FixedUpdate()
    {
        

    }

    public bool CheckState(string stateName, string layerName = "Base Layer")
    {
        return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsName(stateName);
    }

    public bool CheckStateTag(string tagName, string layerName = "Base Layer")
    {
        return anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex(layerName)).IsTag(tagName);
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
        thrustVec = new Vector3(0, jumpVelocity/2, 0);
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
        //planarVec = Vector3.zero;
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

    

    public void OnAttack1hAEnter()
    {
        pi.inputEnable = false;
        //lockPlanar = true;
        //lerpTarget = 1.0f;
    }

    public void OnAttack1hAUpdate()
    {
        thrustVec = model.transform.forward * anim.GetFloat("attack1hAVelocity");
        //float currentWeight = anim.GetLayerWeight(anim.GetLayerIndex("attack"));
        //currentWeight = Mathf.Lerp(currentWeight, lerpTarget, 0.3f);
        //anim.SetLayerWeight(anim.GetLayerIndex("attack"), currentWeight);
    }

    public void OnUpdateRM(object _deltaPos) //_ 说明是区域变量
    {
        //print(_deltaPos);
        if(CheckState("attack1hC"))
            deltaPos += (0.2f*deltaPos + 0.8f*(Vector3)_deltaPos)/1.0f;
    }

    public void OnHitEnter()
    {
        pi.inputEnable = false;
        planarVec = Vector3.zero;
        model.SendMessage("WeaponDisable");
    }

    public void Issuetrigger(string triggerName)
    {
        anim.SetTrigger(triggerName);
    }

    public void OnAttackExit()
    {
        model.SendMessage("WeaponDisable");
    }

    public void OnBlockEnter()
    {
        pi.inputEnable = false;
    }

    public void OnDieEnter()
    {
        pi.inputEnable = false;
        planarVec = Vector3.zero;
        model.SendMessage("WeaponDisable");
    }

    public void OnStunnedEnter()
    {
        pi.inputEnable = false;
        planarVec = Vector3.zero;
    }

    public void OnCounterBackEnter()
    {
        pi.inputEnable = false;
        planarVec = Vector3.zero;
    }

    public void OnCounterBackExit()
    {
        model.SendMessage("CounterBackDisable");
    }
}
