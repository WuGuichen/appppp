using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaftArmAnimFix : MonoBehaviour
{
    private Animator anim;
    private Actor_Controller ac;
    public Vector3 a;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        ac = GetComponentInParent<Actor_Controller>();
    }

    private void OnAnimatorIK()
    {
        if (ac.leftIsShield)
        {
            if (!anim.GetBool("defense"))
            {
                Transform leftLowerArm = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
                leftLowerArm.localEulerAngles += a;
                anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, Quaternion.Euler(leftLowerArm.localEulerAngles));
            }
        }
    }
}
