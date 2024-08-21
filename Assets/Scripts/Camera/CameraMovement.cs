using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _speedRotate;
    [SerializeField] private float _speedMotion;
    [SerializeField] private Transform _startPos;

    [SerializeField] private Transform[] flybyPoints;

    private Camera _camera;

    private void OnEnable()
    {
        _camera = Camera.main;
        _camera.transform.position = _startPos.position;
    }
}