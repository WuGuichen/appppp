using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyIUserInput : IUserInputer
{
    
    IEnumerator Start()
    {
        while (true)
        {
            
            rb = true;
            yield return 0;


        }
    }

    
    void Update()
    {
        UpdateDmagDvec(Dup, Dright);
    }
}
