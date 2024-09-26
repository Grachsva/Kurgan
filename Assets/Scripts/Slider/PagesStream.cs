using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace SliderPages
{
    public class PagesStream : MonoBehaviour
    {
        public delegate void StreamsLoadedHandler();
        public static event StreamsLoadedHandler e_streamsLoaded;

        private string pathToEntryWhiteMemorial = "1.EntryWhiteMemorial";
        private string pathToEternalFlame = "2.EternalFlame";
        private string pathToFiveBlackMemorials = "3.FiveBlackMemorials";
        private string pathToTwoWhiteSculptures = "4.TwoWhiteSculptures";
        private string pathToBlackMemorialBehindWhiteSculpture = "5.BlackMemorialBehindWhiteSculpture";
        private string pathTo76mmDivisionalGun = "6.76mmDivisionalGun";
        private string pathToMainManument = "7.MainManument";
        private string pathToSecondManument = "8.SecondManument";

        private string pathToVideoMainManument = "7.MainManument";
        private string pathToVideoStartSquare = "9.StartSquare";
        private string pathToVideoMainButton = "Video";

        [SerializeField] public List<Sprite> EntryWhiteMemorialSprites = new List<Sprite>();
        [SerializeField] public List<Sprite> EternalFlame = new List<Sprite>();
        [SerializeField] public List<Sprite> FiveBlackMemorials = new List<Sprite>();
        [SerializeField] public List<Sprite> TwoWhiteSculptures = new List<Sprite>();
        [SerializeField] public List<Sprite> BlackMemorialBehindWhiteSculpture = new List<Sprite>();
        [SerializeField] public List<Sprite> DivisionalGun = new List<Sprite>();
        [SerializeField] public List<Sprite> MainManument = new List<Sprite>(); // замена на видео
        [SerializeField] public List<Sprite> SecondManument = new List<Sprite>();

        [SerializeField] public List<string> VideoFileMainManument = new List<string>();
        [SerializeField] public List<string> VideoFileStartSquare = new List<string>();
        [SerializeField] public List<string> VideoFileMainButton = new List<string>();


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

            GetVideoFileLocation(pathToVideoMainManument, VideoFileMainManument);
            GetVideoFileLocation(pathToVideoStartSquare, VideoFileStartSquare);
            GetVideoFileLocation(pathToVideoMainButton, VideoFileMainButton);

            e_streamsLoaded?.Invoke();
        }

        public void GetFileLocation(string pathToTexturesForMerkersSlider, List<Sprite> listSprites)
        {
            // Получаем путь к директории в StreamingAssets
            string fullPath = Path.Combine(Application.streamingAssetsPath, pathToTexturesForMerkersSlider);
            DirectoryInfo directoryInfo = new DirectoryInfo(fullPath);

            // Проверяем, существует ли директория
            if (!directoryInfo.Exists)
            {
                //Debug.LogError("Directory not found: " + fullPath);
                return;
            }

            // Получаем все файлы .png, .jpg, и .jpeg в директории
            FileInfo[] allFiles = directoryInfo.GetFiles("*.*")
                                               .Where(file => file.Extension.Equals(".png", StringComparison.OrdinalIgnoreCase) ||
                                                              file.Extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) ||
                                                              file.Extension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase))
                                               .ToArray();

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

            //Debug.Log("Loaded " + listSprites.Count + " sprites.");
        }

        public void GetVideoFileLocation(string pathToVideos, List<string> videoList)
        {
            // Получаем путь к директории в StreamingAssets
            string fullPath = Path.Combine(Application.streamingAssetsPath, pathToVideos);
            DirectoryInfo directoryInfo = new DirectoryInfo(fullPath);

            // Проверяем, существует ли директория
            if (!directoryInfo.Exists)
            {
                //Debug.LogError("Directory not found: " + fullPath);
                return;
            }

            // Получаем все файлы .mp4 в директории
            FileInfo[] allFiles = directoryInfo.GetFiles("*.mp4", SearchOption.TopDirectoryOnly);

            // Добавляем пути к файлам в список
            foreach (FileInfo file in allFiles)
            {
                string filePath = file.FullName;
                videoList.Add(filePath);
            }

            //Debug.Log("Loaded " + videoList.Count + " video files.");
        }
    }
}
