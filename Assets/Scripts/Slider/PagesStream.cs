using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    public class PagesStream : MonoBehaviour
    {
        private string pathToEntryWhiteMemorial = "1.EntryWhiteMemorial";
        private string pathToEternalFlame = "2.EternalFlame";
        private string pathToFiveBlackMemorials = "3.FiveBlackMemorials";
        private string pathToTwoWhiteSculptures = "4.TwoWhiteSculptures";
        private string pathToBlackMemorialBehindWhiteSculpture = "5.BlackMemorialBehindWhiteSculpture";
        private string pathTo76mmDivisionalGun = "6.76mmDivisionalGun";
        private string pathToMainManument = "7.MainManument";
        private string pathToSecondManument = "8.SecondManument";

        [SerializeField] private List<Sprite> EntryWhiteMemorialSprites = new List<Sprite>();
        [SerializeField] private List<Sprite> EternalFlame = new List<Sprite>();
        [SerializeField] private List<Sprite> FiveBlackMemorials = new List<Sprite>();
        [SerializeField] private List<Sprite> TwoWhiteSculptures = new List<Sprite>();
        [SerializeField] private List<Sprite> BlackMemorialBehindWhiteSculpture = new List<Sprite>();
        [SerializeField] private List<Sprite> DivisionalGun = new List<Sprite>();
        [SerializeField] private List<Sprite> MainManument = new List<Sprite>();
        [SerializeField] private List<Sprite> SecondManument = new List<Sprite>();

        private void Start()
        {
            GetFileLocation(pathToEntryWhiteMemorial, EntryWhiteMemorialSprites);
            GetFileLocation(pathToEternalFlame, EternalFlame);
            GetFileLocation(pathToFiveBlackMemorials, FiveBlackMemorials);
            GetFileLocation(pathToTwoWhiteSculptures, TwoWhiteSculptures);
            GetFileLocation(pathToBlackMemorialBehindWhiteSculpture, BlackMemorialBehindWhiteSculpture);
            GetFileLocation(pathTo76mmDivisionalGun, DivisionalGun);
            GetFileLocation(pathToMainManument, MainManument);
            GetFileLocation(pathToSecondManument, SecondManument);
        }

        public void GetFileLocation(string pathToTexturesForMerkersSlider, List<Sprite> listSprites)
        {
            // Получаем путь к директории в StreamingAssets
            string fullPath = Path.Combine(Application.streamingAssetsPath, pathToTexturesForMerkersSlider);
            DirectoryInfo directoryInfo = new DirectoryInfo(fullPath);

            // Проверяем, существует ли директория
            if (!directoryInfo.Exists)
            {
                Debug.LogError("Directory not found: " + fullPath);
                return;
            }

            // Получаем все файлы .png в директории
            FileInfo[] allFiles = directoryInfo.GetFiles("*.png");

            // Загружаем изображения и создаем спрайты
            foreach (FileInfo file in allFiles)
            {
                string filePath = file.FullName;

                // Загружаем текстуру из файла
                byte[] fileData = File.ReadAllBytes(filePath);
                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(fileData);

                // Создаем спрайт из текстуры
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                // Присваиваем спрайту имя файла (без расширения)
                sprite.name = Path.GetFileNameWithoutExtension(file.Name);
                // Добавляем спрайт в список
                listSprites.Add(sprite);
            }

            Debug.Log("Loaded " + listSprites.Count + " sprites.");
        }
    }
}
