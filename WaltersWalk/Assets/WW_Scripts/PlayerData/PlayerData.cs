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
            new Item("Cigarette", false),
            new Item("LSD", false),
            new Item("BubbleGum", false),
            new Item("SportShoes", false),
            new Item("AirPlaneMode", false)
        };
    }
}