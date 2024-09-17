using UnityEngine;
using System.IO;
using System.Collections;

public class FileManager : MonoBehaviour
{
    private string filePath;
    private Coroutine checkCoroutine;

    //private void Start()
    //{
    //    // Определите путь к файлу check.json в папке StreamingAssets
    //    filePath = Path.Combine(Application.streamingAssetsPath, "check.json");

    //    // Запустите корутину для проверки состояния
    //    checkCoroutine = StartCoroutine(CheckWindowState());
    //}

    //void Update()
    //{
    //    // Проверка на наличие касаний
    //    if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
    //    {
    //        // Если произошло касание, сбросьте таймер
    //        ResetTimer();
    //    }
    //}

    // Метод для сброса таймера
    private void ResetTimer()
    {
        if (checkCoroutine != null)
        {
            // Остановите текущую корутину
            StopCoroutine(checkCoroutine);
        }

        // Перезапустите корутину
        checkCoroutine = StartCoroutine(CheckWindowState());
    }

    private IEnumerator CheckWindowState()
    {
        // Обновите значение в check.json на false (например, делаем окно неактивным)
        //UpdateCheckFile(true);

        // Ожидание 120 секунд (или нужное вам время)
        yield return new WaitForSeconds(180);

        // Обновите значение в check.json на false (через 120 секунд)
        //UpdateCheckFile(false);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            print("OnApplicationFocus");
            //UpdateCheckFile(focus);
        }
    }

    //private void UpdateCheckFile(bool isActive)
    //{
    //    string json = JsonUtility.ToJson(new CheckData { isActive = isActive });
    //    File.WriteAllText(filePath, json);
    //}

    [System.Serializable]
    public class CheckData
    {
        public bool isActive;
    }
}
