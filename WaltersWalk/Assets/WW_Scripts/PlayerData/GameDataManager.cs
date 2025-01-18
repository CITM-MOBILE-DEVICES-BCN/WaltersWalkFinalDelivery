using System.IO;
using UnityEngine;

namespace WalterWalk
{
    public class GameDataManager
    {
        private static string dataPath = Path.Combine(Application.persistentDataPath, "PlayerData.json");
        private PlayerData playerData;

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

        public bool BuyItem(string itemName, int price)
        {
            if (!CanBuyItem(itemName, price)) return false;

            var item = playerData.itemsOwned.Find(i => i.itemName == itemName);
            playerData.currency -= price;
            item.isOwned = true;
            SavePlayerData();
            return true;
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

        private void SavePlayerData()
        {
            string json = JsonUtility.ToJson(playerData, true);
            File.WriteAllText(dataPath, json);
        }
    }
}