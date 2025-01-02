using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace WalterWalk
{
    [System.Serializable]
    public class Item
    {
        public string itemName;
        public bool isOwned;

        public Item(string name, bool owned)
        {
            itemName = name;
            isOwned = owned;
        }
    }

    [System.Serializable]
    public class PlayerData
    {
        public int currency = 0;
        public List<Item> itemsOwned = new List<Item>
        {
            new Item("Tobacco", false),
            new Item("LSD", false),
            new Item("BubbleGum", false),
            new Item("SportShoes", false),
            new Item("AirPlaneMode", false)
        };
    }

    public class GameDataManager
    {
        private static string dataPath = Path.Combine(Application.persistentDataPath, "PlayerData.json");
        private PlayerData playerData;

        public GameDataManager()
        {
            EnsureDataFileExists();
            LoadPlayerData();
        }

        public int GetCurrency()
        {
            return playerData.currency;
        }

        public void AddCurrency(int amount)
        {
            playerData.currency += amount;
            SavePlayerData();
        }

        public bool BuyItem(string itemName, int price)
        {
            var item = playerData.itemsOwned.Find(i => i.itemName == itemName);
            if (item == null)
            {
                Debug.LogError($"El ítem {itemName} no existe.");
                return false;
            }

            if (playerData.currency >= price && !item.isOwned)
            {
                playerData.currency -= price;
                item.isOwned = true;
                SavePlayerData();
                Debug.Log($"Has comprado {itemName}.");
                return true;
            }
            else if (item.isOwned)
            {
                Debug.Log($"{itemName} ya lo tienes.");
                return false;
            }
            else
            {
                Debug.Log("No tienes suficiente moneda para comprar este ítem.");
                return false;
            }
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
