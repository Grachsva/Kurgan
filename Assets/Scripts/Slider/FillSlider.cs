using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI.Pagination;

namespace SliderPages
{
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
                //{ 8, _streams.SpriteList9 },
                //{ 9, _streams.SpriteList10 }
            };

            // Заполнение слайдеров
            for (int i = 0; i < verticalSliders.Length; i++)
            {
                if (_spriteDictionary.TryGetValue(i, out List<Sprite> spriteList)) // Проверка наличия списка спрайтов
                {
                    var pagedRect = verticalSliders[i].GetComponentInChildren<PagedRect>();
                    AddCountPages(pagedRect, spriteList);
                    UpdatePagedRects(pagedRect);
                }
            }
        }

        private void AddCountPages(PagedRect pagedRect, List<Sprite> spriteList)
        {
            for (int i = 0; i < spriteList.Count; i++)
            {
                Page newPage = pagedRect.AddPageUsingTemplate();
                AddImageInSlide(i, newPage, spriteList);
            }
        }

        private void AddImageInSlide(int numberSlide, Page page, List<Sprite> spriteList)
        {
            Image imageInPage = page.gameObject.GetComponentInChildren<Image>();
            imageInPage.sprite = spriteList[numberSlide];
            imageInPage.SetNativeSize();
        }

        private void UpdatePagedRects(PagedRect pagedRect)
        {
            pagedRect.UpdatePages();
            pagedRect.UpdatePagination();
            pagedRect.UpdateDisplay();
            pagedRect.SetCurrentPage(1);
        }
    }
}
