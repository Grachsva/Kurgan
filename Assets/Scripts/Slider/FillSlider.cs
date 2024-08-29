using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI.Pagination;
using TMPro;
using System;

namespace SliderPages
{
    [System.Serializable]
    public class PageTextData
    {
        public string EntryWhiteMemorialSprites;
        public string EternalFlame;
        public string FiveBlackMemorials;
        public string TwoWhiteSculptures;
        public string BlackMemorialBehindWhiteSculpture;
        public string DivisionalGun;
        public string MainManument;
        public string SecondManument;
    }


    public class FillSlider : MonoBehaviour
    {
        private PagesStream _streams;

        //[SerializeField] private GameObject _page;
        [SerializeField] private GameObject[] verticalSliders; // Массив слайдеров

        private Dictionary<int, List<Sprite>> _spriteDictionary; // Словарь для хранения списков спрайтов

        private void OnEnable()
        {
            PagesStream.e_streamsLoaded += FindAllNeeded;
        }

        private void OnDisable()
        {
            PagesStream.e_streamsLoaded -= FindAllNeeded;
        }

        private PageTextData LoadTextData()
        {
            string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "SlidersText.json");

            // Чтение содержимого файла
            string json = System.IO.File.ReadAllText(filePath);

            // Десериализация JSON в объект
            return JsonUtility.FromJson<PageTextData>(json);
        }


        private void FindAllNeeded()
        {
            _streams = FindObjectOfType<PagesStream>();

            // Инициализация словаря с соответствующими списками спрайтов
            _spriteDictionary = new Dictionary<int, List<Sprite>>
            {
                { 0, _streams.EntryWhiteMemorialSprites },
                { 1, _streams.EternalFlame },
                { 2, _streams.FiveBlackMemorials },
                { 3, _streams.TwoWhiteSculptures },
                { 4, _streams.BlackMemorialBehindWhiteSculpture },
                { 5, _streams.DivisionalGun },
                { 6, _streams.MainManument },
                { 7, _streams.SecondManument },
            };

            // Загружаем текст для страниц
            PageTextData textData = LoadTextData();

            // Создаем словарь, который будет связывать индексы с текстами
            Dictionary<int, string> textDictionary = new Dictionary<int, string>
            {
                { 0, textData.EntryWhiteMemorialSprites },
                { 1, textData.EternalFlame },
                { 2, textData.FiveBlackMemorials },
                { 3, textData.TwoWhiteSculptures },
                { 4, textData.BlackMemorialBehindWhiteSculpture },
                { 5, textData.DivisionalGun },
                { 6, textData.MainManument },
                { 7, textData.SecondManument }
            };

            // Заполнение слайдеров
            for (int i = 0; i < verticalSliders.Length; i++)
            {
                if (_spriteDictionary.TryGetValue(i, out List<Sprite> spriteList)) // Проверка наличия списка спрайтов
                {
                    var pagedRect = verticalSliders[i].GetComponentInChildren<PagedRect>();
                    AddCountPages(pagedRect, spriteList, textDictionary.ContainsKey(i) ? textDictionary[i] : ""); // Используем текст из словаря
                    UpdatePagedRects(pagedRect);
                }
            }
        }

        private void AddCountPages(PagedRect pagedRect, List<Sprite> spriteList, string pageText)
        {
            for (int i = 0; i < spriteList.Count; i++)
            {
                Page newPage = pagedRect.AddPageUsingTemplate();
                AddImageInSlide(i, newPage, spriteList, pageText);

                if (spriteList.Count <= 1)
                {
                    pagedRect.ShowPagination = false;
                }
            }
        }

        private void AddImageInSlide(int numberSlide, Page page, List<Sprite> spriteList, string pageText)
        {
            Image imageInPage = page.gameObject.GetComponentInChildren<Image>();
            imageInPage.sprite = spriteList[numberSlide];

            // Добавляем текст на страницу
            TextMeshProUGUI textInPage = page.gameObject.GetComponentInChildren<TextMeshProUGUI>(true);
            if (textInPage != null)
            {
                string spriteName = imageInPage.sprite.name;
                if (spriteName.Contains("Frame"))
                {
                    if (pageText != null)
                    {
                        textInPage.text = pageText;
                        textInPage.gameObject.SetActive(true);
                    }
                }
                else
                {
                    spriteName = System.Text.RegularExpressions.Regex.Replace(spriteName, @"\(\d+\)", ""); // @"\(\d+\)", "");
                    spriteName = spriteName.Replace('\'', '\"');
                    textInPage.text = spriteName;
                    textInPage.gameObject.SetActive(true);
                }
            }
            else
            {
                Debug.Log("Текст не найден");
            }

            // Получаем RectTransform для изменения его свойств
            RectTransform rectTransform = imageInPage.GetComponent<RectTransform>();

            // Получаем размеры спрайта
            float spriteWidth = imageInPage.sprite.bounds.size.x;
            float spriteHeight = imageInPage.sprite.bounds.size.y;

            // Проверяем, шире ли изображение, чем оно высоко
            if (spriteWidth > spriteHeight)
            {
                rectTransform.SetAnchor(AnchorPresets.TopCenter);
                rectTransform.SetAnchor(AnchorPresets.TopCenter, 0, 0);
                rectTransform.SetPivot(PivotPresets.TopCenter);
            }
            else
            {
                rectTransform.SetAnchor(AnchorPresets.MiddleCenter);
                rectTransform.SetAnchor(AnchorPresets.MiddleCenter, 0, 0);
                rectTransform.SetPivot(PivotPresets.MiddleCenter);
            }
            // Обновляем layout
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        }

        private void UpdatePagedRects(PagedRect pagedRect)
        {
            pagedRect.UpdatePages();
            pagedRect.UpdatePagination();
            pagedRect.UpdateDisplay();
            pagedRect.SetCurrentPage(1);
        }
    }

    public enum AnchorPresets
    {
        TopLeft,
        TopCenter,
        TopRight,

        MiddleLeft,
        MiddleCenter,
        MiddleRight,

        BottomLeft,
        BottonCenter,
        BottomRight,
        BottomStretch,

        VertStretchLeft,
        VertStretchRight,
        VertStretchCenter,

        HorStretchTop,
        HorStretchMiddle,
        HorStretchBottom,

        StretchAll
    }

    public enum PivotPresets
    {
        TopLeft,
        TopCenter,
        TopRight,

        MiddleLeft,
        MiddleCenter,
        MiddleRight,

        BottomLeft,
        BottomCenter,
        BottomRight,
    }

    public static class RectTransformExtensions
    {
        public static void SetAnchor(this RectTransform source, AnchorPresets allign, int offsetX = 0, int offsetY = 0)
        {
            source.anchoredPosition = new Vector3(offsetX, offsetY, 0);

            switch (allign)
            {
                case (AnchorPresets.TopLeft):
                    {
                        source.anchorMin = new Vector2(0, 1);
                        source.anchorMax = new Vector2(0, 1);
                        break;
                    }
                case (AnchorPresets.TopCenter):
                    {
                        source.anchorMin = new Vector2(0.5f, 1);
                        source.anchorMax = new Vector2(0.5f, 1);
                        break;
                    }
                case (AnchorPresets.TopRight):
                    {
                        source.anchorMin = new Vector2(1, 1);
                        source.anchorMax = new Vector2(1, 1);
                        break;
                    }

                case (AnchorPresets.MiddleLeft):
                    {
                        source.anchorMin = new Vector2(0, 0.5f);
                        source.anchorMax = new Vector2(0, 0.5f);
                        break;
                    }
                case (AnchorPresets.MiddleCenter):
                    {
                        source.anchorMin = new Vector2(0.5f, 0.5f);
                        source.anchorMax = new Vector2(0.5f, 0.5f);
                        break;
                    }
                case (AnchorPresets.MiddleRight):
                    {
                        source.anchorMin = new Vector2(1, 0.5f);
                        source.anchorMax = new Vector2(1, 0.5f);
                        break;
                    }

                case (AnchorPresets.BottomLeft):
                    {
                        source.anchorMin = new Vector2(0, 0);
                        source.anchorMax = new Vector2(0, 0);
                        break;
                    }
                case (AnchorPresets.BottonCenter):
                    {
                        source.anchorMin = new Vector2(0.5f, 0);
                        source.anchorMax = new Vector2(0.5f, 0);
                        break;
                    }
                case (AnchorPresets.BottomRight):
                    {
                        source.anchorMin = new Vector2(1, 0);
                        source.anchorMax = new Vector2(1, 0);
                        break;
                    }

                case (AnchorPresets.HorStretchTop):
                    {
                        source.anchorMin = new Vector2(0, 1);
                        source.anchorMax = new Vector2(1, 1);
                        break;
                    }
                case (AnchorPresets.HorStretchMiddle):
                    {
                        source.anchorMin = new Vector2(0, 0.5f);
                        source.anchorMax = new Vector2(1, 0.5f);
                        break;
                    }
                case (AnchorPresets.HorStretchBottom):
                    {
                        source.anchorMin = new Vector2(0, 0);
                        source.anchorMax = new Vector2(1, 0);
                        break;
                    }

                case (AnchorPresets.VertStretchLeft):
                    {
                        source.anchorMin = new Vector2(0, 0);
                        source.anchorMax = new Vector2(0, 1);
                        break;
                    }
                case (AnchorPresets.VertStretchCenter):
                    {
                        source.anchorMin = new Vector2(0.5f, 0);
                        source.anchorMax = new Vector2(0.5f, 1);
                        break;
                    }
                case (AnchorPresets.VertStretchRight):
                    {
                        source.anchorMin = new Vector2(1, 0);
                        source.anchorMax = new Vector2(1, 1);
                        break;
                    }

                case (AnchorPresets.StretchAll):
                    {
                        source.anchorMin = new Vector2(0, 0);
                        source.anchorMax = new Vector2(1, 1);
                        break;
                    }
            }
        }

        public static void SetPivot(this RectTransform source, PivotPresets preset)
        {

            switch (preset)
            {
                case (PivotPresets.TopLeft):
                    {
                        source.pivot = new Vector2(0, 1);
                        break;
                    }
                case (PivotPresets.TopCenter):
                    {
                        source.pivot = new Vector2(0.5f, 1);
                        break;
                    }
                case (PivotPresets.TopRight):
                    {
                        source.pivot = new Vector2(1, 1);
                        break;
                    }

                case (PivotPresets.MiddleLeft):
                    {
                        source.pivot = new Vector2(0, 0.5f);
                        break;
                    }
                case (PivotPresets.MiddleCenter):
                    {
                        source.pivot = new Vector2(0.5f, 0.5f);
                        break;
                    }
                case (PivotPresets.MiddleRight):
                    {
                        source.pivot = new Vector2(1, 0.5f);
                        break;
                    }

                case (PivotPresets.BottomLeft):
                    {
                        source.pivot = new Vector2(0, 0);
                        break;
                    }
                case (PivotPresets.BottomCenter):
                    {
                        source.pivot = new Vector2(0.5f, 0);
                        break;
                    }
                case (PivotPresets.BottomRight):
                    {
                        source.pivot = new Vector2(1, 0);
                        break;
                    }
            }
        }
    }
}
