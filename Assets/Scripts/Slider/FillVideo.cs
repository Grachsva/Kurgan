using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UI.Pagination;

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

            string videoPath = _streams.VideoFiles[numberSlide];
            videoPlayer.url = videoPath;
            videoPlayer.Play();
        }
    }
}
