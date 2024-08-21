using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SnapShots : MonoBehaviour
{
    public Camera configCamera; // ������ ��� ���������������� �����
    public Camera otherCamera;  // ������ ������
    public Button makeShapshot;  
    public Button clearShapshot;  
    public string saveFileName = "snapshots.json"; // ��� ����� ��� ����������

    public List<GameObject> SnapShotobjects = new List<GameObject>();

    private List<CameraData> savedSnapshots = new List<CameraData>();

    public void EnableConfigScene()
    {
        // �������� ���������������� ������ � ��������� ������
        configCamera.gameObject.SetActive(!configCamera.gameObject.activeInHierarchy);
        otherCamera.gameObject.SetActive(!otherCamera.gameObject.activeInHierarchy);
        makeShapshot.gameObject.SetActive(!makeShapshot.gameObject.activeInHierarchy);
        clearShapshot.gameObject.SetActive(!clearShapshot.gameObject.activeInHierarchy);

        for (int i = 0; i < SnapShotobjects.Count; i++)
        {
            SnapShotobjects[i].SetActive(!SnapShotobjects[i].activeInHierarchy);
        }
    }

    //void Update()
    //{
    //    // ��������� ������� ������ "���������" (��������, ������� Space)
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        SaveCurrentCameraState();
    //    }
    //}

    // ���������� ������� ������� � �������� ���������������� ������
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
            SnapShotobjects.Remove(SnapShotobjects[i]);
        }
    }

    // ���������� ������ ������� � �������� � JSON ����
    private void SaveToFile()
    {
        string json = JsonUtility.ToJson(new CameraDataList { snapshots = savedSnapshots }, true);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, saveFileName), json);
        Debug.Log("������ ��������� � " + Path.Combine(Application.persistentDataPath, saveFileName));
    }

    // �������� ������ �� JSON �����
    public void LoadFromFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            CameraDataList dataList = JsonUtility.FromJson<CameraDataList>(json);
            savedSnapshots = dataList.snapshots;
        }
    }

    // ��������� ������ ��� ���������� ������� � �������� ������
    [System.Serializable]
    public class CameraData
    {
        public Vector3 position;
        public Vector3 rotation;
    }

    // ������ �������� CameraData ��� ���������� � JSON
    [System.Serializable]
    public class CameraDataList
    {
        public List<CameraData> snapshots;
    }
}
