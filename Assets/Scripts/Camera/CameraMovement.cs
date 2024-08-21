using System.Collections.Generic;
using UnityEngine;

namespace Buttons
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float _speedMotion;
        [SerializeField] private Transform _startPos;
        [SerializeField] private List<Transform> _flybyPoints = new List<Transform>();
        [SerializeField] private int _currentPos;

        private Camera _camera;
        private Transform _nextPos;
        private bool _isMoving;
        private bool _continuousMode;

        private void OnEnable()
        {
            _camera = Camera.main;
            _camera.transform.position = _startPos.position;
            SetNextPosition();
        }

        private void Update()
        {
            if (_camera != null && _flybyPoints.Count > 0)
            {
                float deltaSpeed = _speedMotion * Time.deltaTime;

                // ����� ��������� ��� ������� �������
                if (Input.GetKey(KeyCode.H) || Input.GetKey(KeyCode.J))
                {
                    if (!_continuousMode)
                    {
                        _continuousMode = true; // �������� ����������� �����
                        UpdateNextPosition(Input.GetKey(KeyCode.H) ? 1 : -1);
                        _isMoving = true;
                    }
                }

                // ����� ��������� ��� ��������� ������� �������
                if (Input.GetKeyDown(KeyCode.H))
                {
                    if (_continuousMode)
                    {
                        _continuousMode = false; // ��������� ����������� �����
                    }
                    else
                    {
                        UpdateNextPosition(1);
                        _isMoving = true;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.J))
                {
                    if (_continuousMode)
                    {
                        _continuousMode = false; // ��������� ����������� �����
                    }
                    else
                    {
                        UpdateNextPosition(-1);
                        _isMoving = true;
                    }
                }

                // ��������� �����������, ���� ��� �������
                if (_isMoving)
                {
                    MoveCamera(deltaSpeed);
                }
            }
        }

        private void MoveCamera(float deltaSpeed)
        {
            if (_nextPos == null) return;

            _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, _nextPos.position, deltaSpeed);

            if (Vector3.Distance(_camera.transform.position, _nextPos.position) < 0.1f)
            {
                _isMoving = false;
                SetNextPosition();

                // ���� � ������ ������������ ��������, ���������� � ��������� �����
                if (_continuousMode)
                {
                    _isMoving = true;
                    UpdateNextPosition(Input.GetKey(KeyCode.H) ? 1 : -1);
                }
            }
        }

        private void SetNextPosition()
        {
            _nextPos = _flybyPoints[_currentPos];
        }

        private void UpdateNextPosition(int direction)
        {
            _currentPos += direction;
            if (_currentPos >= _flybyPoints.Count)
            {
                _currentPos = 0;
            }
            else if (_currentPos < 0)
            {
                _currentPos = _flybyPoints.Count - 1;
            }
            SetNextPosition();
        }
    }
}
