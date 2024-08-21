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

    private void OnEnable()
    {
        LoadFromFile(); // �������� ������ �� ����� ��� ������
        LoadSnapshots(); // �������������� �������� �� ����������� ������
    }

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
        string json = JsonUtility.ToJson(new CameraDataList { snapshots = savedSnapshots }, true);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, saveFileName), json);
        Debug.Log("������ ��������� � " + Path.Combine(Application.persistentDataPath, saveFileName));
    }

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
