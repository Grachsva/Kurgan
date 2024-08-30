using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering;


public class FPS_limit : MonoBehaviour 
{
    private void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
        HDRenderPipeline refToPipeline = (HDRenderPipeline)RenderPipelineManager.currentPipeline;
        //Invoke("MyResetFunction", 1.0f / 300.0f);
        Debug.Log(Application.targetFrameRate);
    }

    private void MyResetFunction()
    {
        HDRenderPipeline refToPipeline = (HDRenderPipeline)RenderPipelineManager.currentPipeline;
    }
}