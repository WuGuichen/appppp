using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInput : IUserInputer
{
    [Header("===== Joystick Setting =====")]
    public string axisX = "axisX";
    public string axisY = "axisY";
    public string axisJright = "axis4th";
    public string axisJup = "axis5th";
    public string btnA = "btn0";
    public string btnB = "btn1";
    public string btnC = "btn2";
    public string btnD = "btn3";
    public string btnAL = "btn8";
    public string btnAR = "btn9";
    public string btnLT = "LT";
    public string btnLB = "LB";
    public string btnRB = "RB";


    public MyButton buttonA = new MyButton();
    public MyButton buttonB = new MyButton();
    public MyButton buttonC = new MyButton();
    public MyButton buttonD = new MyButton();
    public MyButton buttonLB = new MyButton();
    public MyButton buttonLT = new MyButton();
    public MyButton buttonRB = new MyButton();
    public MyButton buttonAL = new MyButton();
    public MyButton buttonAR = new MyButton();



    void Update()
    {
        buttonA.Tick(Input.GetButton(btnA));
        buttonB.Tick(Input.GetButton(btnB));
        buttonC.Tick(Input.GetButton(btnC));
        buttonD.Tick(Input.GetButton(btnD));
        buttonLB.Tick(Input.GetButton(btnLB));
        buttonLT.Tick(Input.GetButton(btnLT));
        buttonRB.Tick(Input.GetButton(btnRB));
        buttonAL.Tick(Input.GetButton(btnAL));
        buttonAR.Tick(Input.GetButton(btnAR));


        Jup = Input.GetAxis(axisJup);
        Jright = Input.GetAxis(axisJright);
        

        targetDup = Input.GetAxis(axisY);
        targetDright = Input.GetAxis(axisX);

        if (!inputEnable)
        {
            targetDright = 0;
            targetDup = 0;
        }

        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);

        Vector2 tempDAxis = SquareToCircle(new Vector2(Dright, Dup));
        float Dright2 = tempDAxis.x;
        float Dup2 = tempDAxis.y;


        Dmag = Mathf.Sqrt(Dup2 * Dup2 + Dright2 * Dright2);
        Dvec = Dright2 * transform.right + Dup2 * transform.forward;
        
        run = (buttonA.isPressing && !buttonA.IsDelaying);
        jump = run && buttonAL.onPressed;
        roll = buttonA.OnReleased && buttonA.IsDelaying;
        //print(roll);
        attack = buttonAL.onPressed && !defense;
        defense = buttonAR.isPressing && inputEnable;
        lockon = buttonAR.onPressed;

    }

    //private Vector2 SquareToCircle(Vector2 input)
    //{
    //    Vector2 output = Vector2.zero;

    //    output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
    //    output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);

    //    return output;
    //}
}
