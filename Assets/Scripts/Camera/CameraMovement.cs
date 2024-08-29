using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Buttons
{
    public class CameraMovement : MonoBehaviour
    {
        //[SerializeField] private float _speedMotion;
        [SerializeField] private Transform _startPos;
        [SerializeField] private List<Transform> _flybyPoints = new List<Transform>();
        [SerializeField] private int _currentPos;

        [SerializeField] private GameObject leftButton;
        [SerializeField] private GameObject righttButton;

        private Camera _camera;
        private Transform _nextPos;
        public bool _isMoving;
        private bool _continuousMode;

        private void Start()
        {
            FindSnapshots();
            _camera = Camera.main;
            _camera.transform.position = _startPos.position;
            _camera.transform.rotation = _startPos.rotation;
        }

        private void FindSnapshots()
        {
            _flybyPoints.Clear();
            // Находим все объекты типа Snapshot, включая неактивные
            var snapshotObjects = GameObject.FindObjectsOfType<Snapshot>(true);

            // Очищаем текущий список и заполняем его трансформами найденных объектов
            _flybyPoints.Clear();
            //foreach (var snapshot in snapshotObjects)
            //{
            //    _flybyPoints.Add(snapshot.transform);
            //}
            // Заполняем список в обратном порядке
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
                leftButton.SetActive(false);
                righttButton.SetActive(false);
            }
            else
            {
                leftButton.SetActive(true);
                righttButton.SetActive(true);
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
            float step = PlayerPrefs.GetInt("CameraSpeed") * Time.deltaTime;

            _camera.transform.position = Vector3.Slerp(_camera.transform.position, target.position, step);
            _camera.transform.rotation = Quaternion.Slerp(_camera.transform.rotation, target.rotation, step);

            if (Vector3.Distance(_camera.transform.position, target.position) < 0.1 &&
                Quaternion.Angle(_camera.transform.rotation, target.rotation) < 0.1f)
            {
                _isMoving = false;
            }
        }
    }
}