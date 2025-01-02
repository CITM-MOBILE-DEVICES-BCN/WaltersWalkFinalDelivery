using UnityEngine;

namespace WalterWalk
{
    public class GameDataComponent : MonoBehaviour
    {
        private GameDataManager gameDataManager;

        private void Start()
        {
            // Inicializamos el gestor de datos
            gameDataManager = new GameDataManager();

            // Agregamos monedas para pruebas iniciales
            gameDataManager.AddCurrency(200);
            Debug.Log($"Monedas iniciales: {gameDataManager.GetCurrency()}");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                TryBuyItem("Tobacco", 50);

            if (Input.GetKeyDown(KeyCode.W))
                TryBuyItem("LSD", 60);

            if (Input.GetKeyDown(KeyCode.E))
                TryBuyItem("BubbleGum", 70);

            if (Input.GetKeyDown(KeyCode.R))
                TryBuyItem("SportShoes", 80);

            if (Input.GetKeyDown(KeyCode.T))
                TryBuyItem("AirPlaneMode", 90);
        }

        private void TryBuyItem(string itemName, int price)
        {
            Debug.Log($"Intentando comprar {itemName} por {price} monedas...");
            bool success = gameDataManager.BuyItem(itemName, price);

            if (success)
                Debug.Log($"Compra exitosa: {itemName}. Monedas restantes: {gameDataManager.GetCurrency()}");
            else
                Debug.Log($"No se pudo comprar {itemName}. Monedas actuales: {gameDataManager.GetCurrency()}");
        }
    }
}
