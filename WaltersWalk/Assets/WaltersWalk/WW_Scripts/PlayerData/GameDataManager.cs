using System.IO;
using UnityEngine;

namespace WalterWalk
{
    public class GameDataManager
    {
        private static string dataPath = Path.Combine(Application.persistentDataPath, "PlayerData.json");
        public PlayerData playerData;

        public GameDataManager()
        {
            EnsureDataFileExists();
            LoadPlayerData();
        }

        public int GetCurrency() => playerData.currency;

        public void AddCurrency(int amount)
        {
            playerData.currency += amount;
            SavePlayerData();
        }

        public bool CanBuyItem(string itemName, int price)
        {
            var item = playerData.itemsOwned.Find(i => i.itemName == itemName);
            return item != null && !item.isOwned && playerData.currency >= price;
        }

        public string TryBuyItem(string itemName, int price)
        {
            var item = playerData.itemsOwned.Find(i => i.itemName == itemName);

            if (item == null)
                return $"El ítem {itemName} no existe.";

            if (item.isOwned)
                return $"Ya tienes un {itemName}.";

            if (playerData.currency < price)
                return "No tienes suficiente dinero.";

            // Compra exitosa
            playerData.currency -= price;
            item.isOwned = true;
            SavePlayerData();
            return $"Has comprado {itemName} por ${price}.";
        }

        private void EnsureDataFileExists()
        {
            if (!File.Exists(dataPath))
            {
                playerData = new PlayerData();
                SavePlayerData();
            }
        }

        private void LoadPlayerData()
        {
            string json = File.ReadAllText(dataPath);
            playerData = JsonUtility.FromJson<PlayerData>(json);
        }

        public void SavePlayerData()
        {
            string json = JsonUtility.ToJson(playerData, true);
            File.WriteAllText(dataPath, json);
        }
    }
}