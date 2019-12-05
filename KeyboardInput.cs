using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : IUserInputer
{
    [Header("===== Key Setting =====")]
    public string keyUp = "w";
    public string keyDown = "s";
    public string keyLeft = "a";
    public string keyRight = "d";

    public string keyA;
    public string keyB;
    public string keyC;
    public string keyD;
    public string keyE;
    public string keyF;
    public string keyG;
    public string keyH;
    public string keyI;



    public string keyJRight;
    public string keyJLeft;
    public string keyJUp;
    public string keyJDown;
    [Header("===== Mouse Setting =====")]
    public bool mouseEnable = false;
    public float mouseSensitivityX = 1.0f;
    public float mouseSensitivityY = 1.0f;


    public MyButton KeyUp = new MyButton();
    public MyButton KeyDown = new MyButton();
    public MyButton KeyLeft = new MyButton();
    public MyButton KeyRight = new MyButton();
    public MyButton KeyA = new MyButton();
    public MyButton KeyB = new MyButton();
    public MyButton KeyC = new MyButton();
    public MyButton KeyD = new MyButton();
    public MyButton KeyE = new MyButton();
    public MyButton KeyF = new MyButton();
    public MyButton KeyG = new MyButton();
    public MyButton KeyH = new MyButton();
    public MyButton KeyI = new MyButton();



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        KeyUp.Tick(Input.GetButton(keyUp));
        KeyDown.Tick(Input.GetButton(keyDown));
        KeyLeft.Tick(Input.GetButton(keyLeft));
        KeyRight.Tick(Input.GetButton(keyRight));

        KeyA.Tick(Input.GetButton(keyA));
        KeyB.Tick(Input.GetButton(keyB));
        KeyC.Tick(Input.GetButton(keyC));
        KeyD.Tick(Input.GetButton(keyD));
        KeyE.Tick(Input.GetButton(keyE));
        KeyF.Tick(Input.GetButton(keyF));
        KeyG.Tick(Input.GetButton(keyG));
        KeyH.Tick(Input.GetButton(keyH));
        KeyI.Tick(Input.GetButton(keyI));



        if (mouseEnable)
        {
            Jup = Input.GetAxis("Mouse Y") * mouseSensitivityY *3f;
            Jright = Input.GetAxis("Mouse X")*mouseSensitivityX*2.5f;
        }
        else
        {
            Jup = (Input.GetKey(keyJUp) ? 1.0f : 0) - (Input.GetKey(keyJDown) ? 1.0f : 0);
            Jright = (Input.GetKey(keyJRight) ? 1.0f : 0) - (Input.GetKey(keyJLeft) ? 1.0f : 0);
        }
        //print(Jup);

        targetDup = (Input.GetKey(keyUp) ? 1f : 0) - (Input.GetKey(keyDown) ? 1f : 0);
        targetDright = (Input.GetKey(keyRight) ? 1f : 0) - (Input.GetKey(keyLeft) ? 1f : 0);

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

        run = (KeyA.isPressing && !KeyA.IsDelaying) || (run&&KeyA.IsExtending);
        jump = run && KeyA.onPressed;
        roll = KeyA.OnReleased && KeyA.IsDelaying;
        //print(roll);
        //attack = KeyC.onPressed && !defense;
        rb = KeyF.onPressed;
        rt = KeyG.onPressed;
        lb = KeyH.onPressed;
        lt = KeyI.onPressed;
        defense = KeyD.isPressing && inputEnable;
        lockon = KeyE.onPressed;
    }

   
}
