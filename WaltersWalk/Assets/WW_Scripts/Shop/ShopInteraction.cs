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
        private string currentItemName;
        private int currentItemPrice;

        private Transform lastHoveredItem;
        private Vector3 originalScale;

        private void Start()
        {

            gameDataManager = new GameDataManager();

            itemPanel.SetActive(false);

        }

        private void Update()
        {
            HandleItemHover();

            if (Input.GetKeyDown(KeyCode.P))
            {
                gameDataManager.AddCurrency(100);
                Debug.Log($"Dinero añadido: {gameDataManager.GetCurrency()} $");
            }
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

                    itemPanel.SetActive(true);
                    currentItemName = itemInfo.itemName;
                    currentItemPrice = itemInfo.price;
                    itemNameText.text = "x1 " + currentItemName;
                    itemPriceText.text = "$" + currentItemPrice;


                    if (lastHoveredItem != hit.transform)
                    {
                        ResetLastHoveredItem(); 
                        lastHoveredItem = hit.transform;
                        originalScale = lastHoveredItem.localScale;
                        lastHoveredItem.DOScale(originalScale * 1.2f, 0.2f); 
                    }
                }
                else
                {
                    ResetLastHoveredItem(); 
                    itemPanel.SetActive(false);
                }
            }
            else
            {
                ResetLastHoveredItem(); 
                itemPanel.SetActive(false);
            }
        }

        private void ResetLastHoveredItem()
        {
            if (lastHoveredItem != null)
            {
                lastHoveredItem.DOScale(originalScale, 0.2f); 
                lastHoveredItem = null;
            }
        }

        public void OnBuyButtonClicked()
        {
            string result = gameDataManager.TryBuyItem(currentItemName, currentItemPrice);
            Debug.Log(result);
        }
    }
}