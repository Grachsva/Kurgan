using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LookAt : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}