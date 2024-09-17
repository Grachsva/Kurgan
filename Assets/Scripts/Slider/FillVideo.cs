using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UI.Pagination;
using TMPro;
using UnityEngine.Rendering;

namespace SliderPages
{
    public class FillVideo : MonoBehaviour
    {
        private PagedRect _pagedRect;
        private PagesStream _streams;

        [SerializeField] private GameObject _pageMainButtonVideo;
        [SerializeField] private GameObject verticalSliderMainbuttonVideo;

        [SerializeField] private GameObject _pageMainManumentVideo;
        [SerializeField] private GameObject verticalSliderMainManumentVideo;

        [SerializeField] private GameObject _pageStartSquare;
        [SerializeField] private GameObject verticalSliderStartSquare;

        private void OnEnable()
        {
            PagesStream.e_streamsLoaded += CountOnAllVideoSliders;
        }

        private void OnDisable()
        {
            PagesStream.e_streamsLoaded -= CountOnAllVideoSliders;
        }

        private void CountOnAllVideoSliders()
        {
            FindAllNeeded(verticalSliderMainbuttonVideo, 0);
            FindAllNeeded(verticalSliderMainManumentVideo, 7);
            FindAllNeeded(verticalSliderStartSquare, 9);
        }

        private void FindAllNeeded(GameObject catchVideoSlider, int numberFolder)
        {
            _streams = FindObjectOfType<PagesStream>();
            _pagedRect = catchVideoSlider.GetComponentInChildren<PagedRect>();
            AddCountPages(numberFolder);
            _pagedRect.UpdatePages();
            _pagedRect.UpdatePagination();
            _pagedRect.UpdateDisplay();
            _pagedRect.SetCurrentPage(1);
        }

        private void AddCountPages(int numberFolder)
        {
            switch (numberFolder)
            {
                case 0:
                    for (int i = 0; i < _streams.VideoFileMainButton.Count; i++)
                    {
                        if (_streams.VideoFileMainButton.Count == 1)
                        {
                            _pagedRect.ShowPagination = false;
                        }
                        Page newPage = _pagedRect.AddPageUsingTemplate();
                        AddVideoInSlide(i, newPage, numberFolder);
                    }
                    break;
                case 7:
                    for (int i = 0; i < _streams.VideoFileMainManument.Count; i++)
                    {
                        if (_streams.VideoFileMainManument.Count == 1)
                        {
                            _pagedRect.ShowPagination = false;
                        }
                        Page newPage = _pagedRect.AddPageUsingTemplate();
                        AddVideoInSlide(i, newPage, numberFolder);
                    }
                    break;
                case 9:
                    for (int i = 0; i < _streams.VideoFileStartSquare.Count; i++)
                    {
                        if (_streams.VideoFileStartSquare.Count == 1)
                        {
                            _pagedRect.ShowPagination = false;
                        }
                        Page newPage = _pagedRect.AddPageUsingTemplate();
                        AddVideoInSlide(i, newPage, numberFolder);
                    }
                    break;
            }
        }

        private void AddVideoInSlide(int numberSlide, Page page, int numberFolder)
        {
            VideoPlayer videoPlayer = page.gameObject.GetComponentInChildren<VideoPlayer>();
            if (videoPlayer == null)
            {
                Debug.LogError("No VideoPlayer component found on the page.");
                return;
            }

            string videoPath;
            string videoFileName;
            switch (numberFolder)
            {
                case 0:
                    // Получаем путь к видеофайлу
                    videoPath = _streams.VideoFileMainButton[numberSlide];
                    Debug.Log("Путь к видеофайлу + " + videoPath + " под номером: " + numberFolder);
                    // Извлекаем имя файла без расширения
                    videoFileName = Path.GetFileNameWithoutExtension(videoPath);
                    break;
                case 7:
                    // Получаем путь к видеофайлу
                    videoPath = _streams.VideoFileMainManument[numberSlide];
                    Debug.Log("Путь к видеофайлу + " + videoPath + " под номером: " + numberFolder);
                    // Извлекаем имя файла без расширения
                    videoFileName = Path.GetFileNameWithoutExtension(videoPath);
                    break;
                case 9:
                    // Получаем путь к видеофайлу
                    videoPath = _streams.VideoFileStartSquare[numberSlide];
                    Debug.Log("Путь к видеофайлу + " + videoPath + " под номером: " + numberFolder);
                    // Извлекаем имя файла без расширения
                    videoFileName = Path.GetFileNameWithoutExtension(videoPath);
                    break;
                default:
                    Debug.LogError("Ни одного не найдено");
                    videoPath = null;
                    videoFileName = null;
                    break;
            }

            // Устанавливаем URL для VideoPlayer
            videoPlayer.url = videoPath;
            videoPlayer.Play();

            // Ищем компонент TextMeshPro на странице
            TextMeshProUGUI textMeshPro = page.gameObject.GetComponentInChildren<TextMeshProUGUI>();
            if (textMeshPro != null)
            {
                // Устанавливаем текст как имя видеофайла
                textMeshPro.text = videoFileName;
            }
            else
            {
                Debug.LogError("No TextMeshPro component found on the page.");
            }
        }
    }
}
