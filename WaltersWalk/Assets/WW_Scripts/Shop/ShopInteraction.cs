using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

namespace WalterWalk
{
    public class ShopInteraction : MonoBehaviour
    {
        public GameObject itemPanel;
        public TextMeshProUGUI itemNameText;
        public TextMeshProUGUI itemPriceText;
        public Button buyButton;

        private GameDataManager gameDataManager;
        private Transform hoveredItem;
        private Vector3 originalScale; 

        private void Start()
        {
            gameDataManager = new GameDataManager();
            itemPanel.SetActive(false); 
        }

        private void Update()
        {
            HandleItemHover();
        }

        private void HandleItemHover()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                ItemInfo itemInfo = hit.transform.GetComponent<ItemInfo>();

                if (itemInfo != null)
                {
                    if (hoveredItem == hit.transform)
                        return;

                    ResetHoveredItem();
                    hoveredItem = hit.transform;
                    originalScale = hoveredItem.localScale;

                    hoveredItem.DOScale(originalScale * 1.2f, 0.2f);
                    ShowItemPanel(itemInfo);
                }
                else
                {
                    ResetHoveredItem();
                }
            }
            else
            {
                ResetHoveredItem();
            }
        }

        private void ShowItemPanel(ItemInfo itemInfo)
        {
            itemPanel.SetActive(true);
            itemNameText.text = $"x1 {itemInfo.itemName}";
            itemPriceText.text = $"${itemInfo.price}";
        }

        private void ResetHoveredItem()
        {
            if (hoveredItem != null)
            {
                hoveredItem.DOScale(originalScale, 0.2f);
                hoveredItem = null;
            }

            itemPanel.SetActive(false);
        }

        public void OnBuyButtonClicked()
        {
            if (hoveredItem != null)
            {
                ItemInfo itemInfo = hoveredItem.GetComponent<ItemInfo>();
                if (itemInfo != null)
                {
                    string result = gameDataManager.TryBuyItem(itemInfo.itemName, itemInfo.price);
                    Debug.Log(result);
                }
            }
        }
    }
}
