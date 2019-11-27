﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaftArmAnimFix : MonoBehaviour
{
    private Animator anim;

    public Vector3 a;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnAnimatorIK()
    {
        if (!anim.GetBool("defense"))
        {
            Transform leftLowerArm = anim.GetBoneTransform(HumanBodyBones.LeftLowerArm);
            leftLowerArm.localEulerAngles += a;
            anim.SetBoneLocalRotation(HumanBodyBones.LeftLowerArm, Quaternion.Euler(leftLowerArm.localEulerAngles));
        }
    }
}
