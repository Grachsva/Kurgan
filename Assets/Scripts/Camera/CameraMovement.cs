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

                // Режим активации при зажатии клавиши
                if (Input.GetKey(KeyCode.H) || Input.GetKey(KeyCode.J))
                {
                    if (!_continuousMode)
                    {
                        _continuousMode = true; // Включаем непрерывный режим
                        UpdateNextPosition(Input.GetKey(KeyCode.H) ? 1 : -1);
                        _isMoving = true;
                    }
                }

                // Режим активации при одиночном нажатии клавиши
                if (Input.GetKeyDown(KeyCode.H))
                {
                    if (_continuousMode)
                    {
                        _continuousMode = false; // Выключаем непрерывный режим
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
                        _continuousMode = false; // Выключаем непрерывный режим
                    }
                    else
                    {
                        UpdateNextPosition(-1);
                        _isMoving = true;
                    }
                }

                // Выполняем перемещение, если оно активно
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

                // Если в режиме непрерывного движения, продолжаем к следующей точке
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
