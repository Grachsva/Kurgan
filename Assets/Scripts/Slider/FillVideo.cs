using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UI.Pagination;
using TMPro; 

namespace SliderPages
{
    public class FillVideo : MonoBehaviour
    {
        private PagedRect _pagedRect;
        private PagesStream _streams;

        [SerializeField] private GameObject _page;
        [SerializeField] private GameObject verticalSlider;

        private void OnEnable()
        {
            PagesStream.e_streamsLoaded += FindAllNeeded;
        }

        private void OnDisable()
        {
            PagesStream.e_streamsLoaded -= FindAllNeeded;
        }

        private void FindAllNeeded()
        {
            _streams = FindObjectOfType<PagesStream>();
            _pagedRect = verticalSlider.GetComponentInChildren<PagedRect>();
            AddCountPages();
            _pagedRect.UpdatePages();
            _pagedRect.UpdatePagination();
            _pagedRect.UpdateDisplay();
            _pagedRect.SetCurrentPage(1);
        }

        private void AddCountPages()
        {
            for (int i = 0; i < _streams.VideoFiles.Count; i++)
            {
                if (_streams.VideoFiles.Count == 1)
                {
                    _pagedRect.ShowPagination = false;
                }
                Page newPage = _pagedRect.AddPageUsingTemplate();
                AddVideoInSlide(i, newPage);
            }
        }

        private void AddVideoInSlide(int numberSlide, Page page)
        {
            VideoPlayer videoPlayer = page.gameObject.GetComponentInChildren<VideoPlayer>();
            if (videoPlayer == null)
            {
                Debug.LogError("No VideoPlayer component found on the page.");
                return;
            }

            // Получаем путь к видеофайлу
            string videoPath = _streams.VideoFiles[numberSlide];
            // Извлекаем имя файла без расширения
            string videoFileName = Path.GetFileNameWithoutExtension(videoPath);

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
