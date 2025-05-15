using System;
using Resources.Code.DataStructures.LiSa;
using UnityEngine;

namespace Resources.Code
{
    public class VerticalContainer: MonoBehaviour, IUIElement
    {
        [Header("Prefabs")] 
        [SerializeField] private GoodElement goodPrefab;

        private UISliding _sliding;

        void Start()
        {
            _sliding = GetComponent<UISliding>();
        }

        public void ShowGoods(String goods)
        {
            if (goods.Length == 0) return;

            Clear();

            foreach (var good in goods.Split(DataParsingExtension.AdditionalQuerySplitter))
            {
                GoodElement temp = Instantiate(goodPrefab, transform);
                temp.good.goodName = good.Split(DataParsingExtension.ValueSplitter)[0];
                temp.good.sellerName = good.Split(DataParsingExtension.ValueSplitter)[1];
                temp.good.gameName = good.Split(DataParsingExtension.ValueSplitter)[2];
                temp.good.description = good.Split(DataParsingExtension.ValueSplitter)[3];
                temp.good.paymentMethod = good.Split(DataParsingExtension.ValueSplitter)[4];
                temp.good.price = int.Parse(good.Split(DataParsingExtension.ValueSplitter)[5]);
                temp.good.stock = int.Parse(good.Split(DataParsingExtension.ValueSplitter)[6]);
                
                temp.ToggleEditMode(false);
                temp.UpdateTextValues();
            }
        }

        public void StartGoodsEdit(String goods)
        {
            ShowGoods(goods);

            ContinueGoodsEdit();
        }
        public void ContinueGoodsEdit()
        {
            GoodElement temp = Instantiate(goodPrefab, transform);
            temp.ToggleEditMode(true);
        }

        public void Show()
        {
            _sliding.StartAnimation(true);
            //gameObject.SetActive(true);
        }

        public void Hide()
        {
            _sliding.StartAnimation(false);
            //gameObject.SetActive(false);
        }

        public void Clear()
        {
            //
            foreach (var child in GetComponentsInChildren<UIScaling>())
                child.DIE();
        }

        public void Toggle(bool show)
        {
            _sliding.StartAnimation(show);
            //gameObject.SetActive(show);
        }
        public void Toggle()
        {
            _sliding.StartAnimation();
        }
    }
}