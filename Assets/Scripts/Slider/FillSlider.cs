using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UI.Pagination;

namespace SliderPages
{
    public class FillSlider : MonoBehaviour
    {
        private PagedRect _pagedRect;
        private PagesStream _streams;

        [SerializeField] private GameObject _page;
        [SerializeField] private GameObject verticalSlider;

        private void Start()
        {
            PagesStream.e_streamsLoaded += FindAllNeeded;
        }

        private void FindAllNeeded()
        {
            Debug.Log("HELLO");
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
            for (int i = 0; i < _streams.EntryWhiteMemorialSprites.Count; i++)
            {
                Page newPage = _pagedRect.AddPageUsingTemplate();
                AddImageInSlide(i, newPage);
            }
        }

        private void AddImageInSlide(int numberSlide, Page page)
        {
            Image imageInPage = page.gameObject.GetComponentInChildren<Image>();
            imageInPage.sprite = _streams.EntryWhiteMemorialSprites[numberSlide];
        }
    }
}