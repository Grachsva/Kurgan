using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SnapShots : MonoBehaviour
{
    public Camera configCamera; // Камера для конфигурационной сцены
    public Camera otherCamera;  // Другая камера
    public Button makeShapshot;
    public Button clearShapshot;
    public string saveFileName = "snapshots.json"; // Имя файла для сохранения

    public List<GameObject> SnapShotobjects = new List<GameObject>();

    private List<CameraData> savedSnapshots = new List<CameraData>();

    private void OnEnable()
    {
        LoadFromFile(); // Загрузка данных из файла при старте
        LoadSnapshots(); // Восстановление объектов из загруженных данных
    }

    public void EnableConfigScene()
    {
        // Включаем конфигурационную камеру и выключаем другую
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

        // Сохранение в JSON файл
        SaveToFile();
        MakeSnapShotObj(snapshot);
    }

    private void MakeSnapShotObj(CameraData snapshot)
    {
        var snaphotPrefab = Resources.Load<GameObject>("Config/snapshot");
        GameObject snaphotObj = Instantiate(snaphotPrefab, snapshot.position, Quaternion.Euler(snapshot.rotation));
        // Получаем компонент Text в дочернем Canvas и устанавливаем порядковый номер
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
        SaveToFile(); // Сохраняем пустой список в файл
        Debug.Log("JSON файл очищен.");

        for (int i = 0; i < SnapShotobjects.Count; i++)
        {
            DestroyImmediate(SnapShotobjects[i], true);
        }
        SnapShotobjects.Clear(); // Очищаем список объектов
    }

    private void SaveToFile()
    {
        // Использование папки StreamingAssets
        string filePath = Path.Combine(Application.streamingAssetsPath, saveFileName);
        string json = JsonUtility.ToJson(new CameraDataList { snapshots = savedSnapshots }, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Данные сохранены в " + filePath);
    }

    public void LoadFromFile()
    {
        // Использование папки StreamingAssets
        string filePath = Path.Combine(Application.streamingAssetsPath, saveFileName);
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            CameraDataList dataList = JsonUtility.FromJson<CameraDataList>(json);
            savedSnapshots = dataList.snapshots;
        }
        else
        {
            Debug.LogWarning("Файл не найден: " + filePath);
        }
    }

    private void LoadSnapshots()
    {
        SnapShotobjects.Clear(); // Очищаем список объектов перед загрузкой
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
