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

    //void Update()
    //{
    //    // Проверяем нажатие кнопки "Запомнить" (например, клавиша Space)
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        SaveCurrentCameraState();
    //    }
    //}

    // Сохранение текущей позиции и вращения конфигурационной камеры
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
            SnapShotobjects.Remove(SnapShotobjects[i]);
        }
    }

    // Сохранение списка позиций и вращений в JSON файл
    private void SaveToFile()
    {
        string json = JsonUtility.ToJson(new CameraDataList { snapshots = savedSnapshots }, true);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, saveFileName), json);
        Debug.Log("Данные сохранены в " + Path.Combine(Application.persistentDataPath, saveFileName));
    }

    // Загрузка данных из JSON файла
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

    // Структура данных для сохранения позиции и вращения камеры
    [System.Serializable]
    public class CameraData
    {
        public Vector3 position;
        public Vector3 rotation;
    }

    // Список структур CameraData для сохранения в JSON
    [System.Serializable]
    public class CameraDataList
    {
        public List<CameraData> snapshots;
    }
}
