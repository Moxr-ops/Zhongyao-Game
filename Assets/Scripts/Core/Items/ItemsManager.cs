using System.Collections;
using System.Collections.Generic;
using CHARACTERS;
using DIALOGUE;
using UnityEngine;

namespace ITEMS
{
    public class ItemsManager : MonoBehaviour
    {
        [SerializeField] private ItemConfigSO config;
        [SerializeField] private Transform _itemPanel = null;
        [SerializeField] private ItemWarehouse warehouse = null;
        public Transform itemPanel => _itemPanel;

        public static ItemsManager instance { get; private set; }
        private Dictionary<string, Item> items = new Dictionary<string, Item>();

        private const string ITEM_NAME_ID = "<itemname>";

        public string itemRootPathFormat => $"Items/{ITEM_NAME_ID}";
        public string itemPrefabNameFormat => $"Character - [{ITEM_NAME_ID}]";
        public string itemPrefabPathFormat => $"{itemRootPathFormat}/{itemPrefabNameFormat}";

        private void Awake()
        {
            instance = this;
        }

        public ItemConfigData GetItemConfig(string itemName)
        {
            return config.GetConfig(itemName);
        }

        public Item GetItem(string itemName, bool createIfDoesNotExist = false)
        {
            if (items.ContainsKey(itemName.ToLower()))
                return items[itemName.ToLower()];
            else if (createIfDoesNotExist)
                return CreateItem(itemName);
            return null;
        }

        public Item CreateItem(string itemName, bool revealAfterCreation = false)
        {
            ITEM_INFO info = GetItemInfo(itemName);
            Item item = CreateItemFromInfo(info);
            items.Add(info.name.ToLower(), item);

            if (revealAfterCreation)
            {
                item.Show();
            }

            return item;
        }

        private ITEM_INFO GetItemInfo(string itemName)
        {
            ITEM_INFO result = new ITEM_INFO();

            result.config = config.GetConfig(itemName);
            result.prefab = GetPrefabForItem(itemName);
            result.rootItemFolder = FormatItemPath(itemRootPathFormat, itemName);

            return result;
        }

        private GameObject GetPrefabForItem(string itemName)
        {
            string prefabPath = FormatItemPath(itemPrefabPathFormat, itemName);

            return Resources.Load<GameObject>(prefabPath);
        }

        public string FormatItemPath(string path, string itemName) => path.Replace(ITEM_NAME_ID, itemName);

        private Item CreateItemFromInfo(ITEM_INFO info)
        {
            ItemConfigData config = info.config;

            return new Item(config.name, config, info.prefab);
        }

        public void AddToWarehouse(string name)
        {
            ITEM_INFO info = GetItemInfo(name);

            if (info.config.name != "")
            {
                warehouse.AddItem(name);
                Debug.Log($"Added {name} to the warehouse");
            }
            else
            {
                Debug.Log($"Cannot add {name} to the warehouse");
            }
        }

        private class ITEM_INFO
        {
            public string name => config.name;
            public string rootItemFolder = "";

            public ItemConfigData config = null;
            public GameObject prefab = null;
        }
    }
}