using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Snapshots
{
    public class SnapShots : MonoBehaviour
    {
        public Camera configCamera; // ������ ��� ���������������� �����
        public Camera otherCamera;  // ������ ������
        public GameObject canvasConfig;
        //public Button makeShapshot;
        //public Button clearShapshot;
        //public GameObject speedSlider;
        //public GameObject speedText;
        public string saveFileName = "snapshots.json"; // ��� ����� ��� ����������

        public List<GameObject> SnapShotobjects = new List<GameObject>();

        private List<CameraData> savedSnapshots = new List<CameraData>();

        private void OnEnable()
        {
            LoadFromFile(); // �������� ������ �� ����� ��� ������
            LoadSnapshots(); // �������������� �������� �� ����������� ������
        }

        private void Start()
        {
            ButtonConfig.e_OnButtonConfig += EnableConfigScene;
        }

        public void EnableConfigScene()
        {
            // �������� ���������������� ������ � ��������� ������
            canvasConfig.gameObject.SetActive(!canvasConfig.gameObject.activeInHierarchy);

            configCamera.gameObject.SetActive(!configCamera.gameObject.activeInHierarchy);
            otherCamera.gameObject.SetActive(!otherCamera.gameObject.activeInHierarchy);
            //makeShapshot.gameObject.SetActive(!makeShapshot.gameObject.activeInHierarchy);
            //clearShapshot.gameObject.SetActive(!clearShapshot.gameObject.activeInHierarchy);

            //speedSlider.gameObject.SetActive(!speedSlider.gameObject.activeInHierarchy);
            //speedText.gameObject.SetActive(!speedText.gameObject.activeInHierarchy);

            for (int i = 0; i < SnapShotobjects.Count; i++)
            {
                SnapShotobjects[i].SetActive(!SnapShotobjects[i].activeInHierarchy);
            }
        }

        public void SaveCurrentCameraState()
        {
            CameraData snapshot = new CameraData
            {
                position = configCamera.transform.position,
                rotation = configCamera.transform.rotation.eulerAngles,
            };
            savedSnapshots.Add(snapshot);

            // ���������� � JSON ����
            SaveToFile();
            MakeSnapShotObj(snapshot);
        }

        private void MakeSnapShotObj(CameraData snapshot)
        {
            var snaphotPrefab = Resources.Load<GameObject>("Config/snapshot");
            GameObject snaphotObj = Instantiate(snaphotPrefab, snapshot.position, Quaternion.Euler(snapshot.rotation));
            // �������� ��������� Text � �������� Canvas � ������������� ���������� �����
            var textComponent = snaphotObj.GetComponentInChildren<Text>();
            if (textComponent != null)
            {
                textComponent.text = (SnapShotobjects.Count + 1).ToString();
            }

            SnapShotobjects.Add(snaphotObj);
        }

        public void ClearSnapshots()
        {
            savedSnapshots.Clear();
            SaveToFile(); // ��������� ������ ������ � ����
            Debug.Log("JSON ���� ������.");

            for (int i = 0; i < SnapShotobjects.Count; i++)
            {
                DestroyImmediate(SnapShotobjects[i], true);
            }
            SnapShotobjects.Clear(); // ������� ������ ��������
        }

        private void SaveToFile()
        {
            // ������������� ����� StreamingAssets
            string filePath = Path.Combine(Application.streamingAssetsPath, saveFileName);
            string json = JsonUtility.ToJson(new CameraDataList { snapshots = savedSnapshots }, true);
            File.WriteAllText(filePath, json);
            Debug.Log("������ ��������� � " + filePath);
        }

        public void LoadFromFile()
        {
            // ������������� ����� StreamingAssets
            string filePath = Path.Combine(Application.streamingAssetsPath, saveFileName);
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                CameraDataList dataList = JsonUtility.FromJson<CameraDataList>(json);
                savedSnapshots = dataList.snapshots;
            }
            else
            {
                Debug.LogWarning("���� �� ������: " + filePath);
            }
        }

        private void LoadSnapshots()
        {
            SnapShotobjects.Clear(); // ������� ������ �������� ����� ���������
            foreach (var snapshot in savedSnapshots)
            {
                MakeSnapShotObj(snapshot);
            }

            for (int i = 0; i < SnapShotobjects.Count; i++)
            {
                SnapShotobjects[i].SetActive(!SnapShotobjects[i].activeInHierarchy);
            }
        }

        [System.Serializable]
        public class CameraData
        {
            public Vector3 position;
            public Vector3 rotation;
        }

        [System.Serializable]
        public class CameraDataList
        {
            public List<CameraData> snapshots;
        }
    }
}