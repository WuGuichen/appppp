using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBones : MonoBehaviour
{
    public SkinnedMeshRenderer srcMeshRenderer;
    public SkinnedMeshRenderer tgtMeshRenderer;

    void Start()
    {
        tgtMeshRenderer.bones = srcMeshRenderer.bones;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
