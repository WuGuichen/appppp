using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyIUserInput : IUserInputer
{
    
    IEnumerator Start()
    {
        while (true)
        {
            Dup = 1.0f;
            Dright = 0f;
            Jright = 9.0f;
            Jup = 0;
            run = true;
            yield return new WaitForSeconds(2.0f);
            rb = true;
            yield return new WaitForSeconds(1.0f);


        }
    }

    
    void Update()
    {
        UpdateDmagDvec(Dup, Dright);
    }
}
