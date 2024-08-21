using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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
            FindSnapshots();
            _camera = Camera.main;
            _camera.transform.position = _startPos.position;
        }

        private void FindSnapshots()
        {
            _flybyPoints.Clear();
            // ������� ��� ������� ���� Snapshot, ������� ����������
            var snapshotObjects = GameObject.FindObjectsOfType<Snapshot>(true);

            // ������� ������� ������ � ��������� ��� ������������ ��������� ��������
            _flybyPoints.Clear();
            //foreach (var snapshot in snapshotObjects)
            //{
            //    _flybyPoints.Add(snapshot.transform);
            //}
            // ��������� ������ � �������� �������
            for (int i = snapshotObjects.Length - 1; i >= 0; i--)
            {
                _flybyPoints.Add(snapshotObjects[i].transform);
            }
            _startPos = _flybyPoints[0];
        }

        private void Update()
        {
            if (_isMoving)
            {
                MoveCamera();
            }
        }

        public void MoveToNextPoint()
        {
            if (_isMoving) return;

            _currentPos = (_currentPos + 1) % _flybyPoints.Count;
            _isMoving = true;
        }

        public void MoveToPreviousPoint()
        {
            if (_isMoving) return;

            _currentPos = (_currentPos - 1 + _flybyPoints.Count) % _flybyPoints.Count;
            _isMoving = true;
        }

        private void MoveCamera()
        {
            Transform target = _flybyPoints[_currentPos];
            float step = _speedMotion * Time.deltaTime;

            _camera.transform.position = Vector3.Slerp(_camera.transform.position, target.position, step);
            _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, target.rotation, step);

            if (Vector3.Distance(_camera.transform.position, target.position) < 0.01f &&
                Quaternion.Angle(_camera.transform.rotation, target.rotation) < 0.01f)
            {
                _isMoving = false;
            }
        }
    }
}