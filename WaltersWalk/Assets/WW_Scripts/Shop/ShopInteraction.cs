using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

        private void Start()
        {
            gameDataManager = new GameDataManager();

            itemPanel.SetActive(false);

            buyButton.onClick.AddListener(OnBuyButtonClicked);

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
                    // Show the UI panel
                    itemPanel.SetActive(true);
                    currentItemName = itemInfo.itemName;
                    currentItemPrice = itemInfo.price;

                    itemNameText.text = currentItemName;
                    itemPriceText.text = "$" + currentItemPrice;
                }
                else
                {
                    // Hide the panel 
                    itemPanel.SetActive(false);
                }
            }
            else
            {
                // Hide the panel if no object is detected
                itemPanel.SetActive(false);
            }
        }

        public void OnBuyButtonClicked()
        {
            // Attempt to purchase the current item
            bool success = gameDataManager.BuyItem(currentItemName, currentItemPrice);
        }

    }



    
}
