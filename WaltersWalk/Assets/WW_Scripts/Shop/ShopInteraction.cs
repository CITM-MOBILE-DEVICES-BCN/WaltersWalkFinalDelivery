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
                    itemNameText.text = currentItemName;
                    itemPriceText.text = "$" + currentItemPrice;
                }
                else
                {
                    itemPanel.SetActive(false);
                }
            }
            else
            {
                itemPanel.SetActive(false);
            }
        }

        public void OnBuyButtonClicked()
        {
            if (gameDataManager.BuyItem(currentItemName, currentItemPrice))
            {
                Debug.Log($"Has comprado {currentItemName} por ${currentItemPrice}.");
            }
            else
            {
                Debug.Log("No se pudo completar la compra.");
            }
        }
    }
}