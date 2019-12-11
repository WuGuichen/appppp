using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUserInputer : MonoBehaviour
{
    [Header("===== Output Setting =====")]
    public float Dup;
    public float Dright;
    public float Dmag;
    public Vector3 Dvec;
    public float Jup;
    public float Jright;

    // 1. pressing signal
    public bool run;
    // 2.trigger once signal
    public bool action;
    public bool jump;
    protected bool lastJump;
    //public bool attack;
    protected bool lastAttack;
    public bool defense;
    public bool roll;
    public bool lockon;
    public bool lb;
    public bool lt;
    public bool rb;
    public bool rt;
    // 3.double triger

    [Header("===== Others =====")]

    public bool inputEnable = true;

    protected float targetDup;
    protected float targetDright;
    protected float velocityDup;
    protected float velocityDright;

    protected Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;

        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);

        return output;
    }

    protected void UpdateDmagDvec(float Dup, float Dright)
    {
        Dmag = Mathf.Sqrt(Dup * Dup + Dright* Dright);
        Dvec = Dright * transform.right + Dup * transform.forward;
    }
}
