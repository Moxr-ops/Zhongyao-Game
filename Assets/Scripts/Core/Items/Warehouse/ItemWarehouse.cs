using System.Collections.Generic;
using UnityEngine;

namespace ITEMS
{
    public class ItemWarehouse : MonoBehaviour
    {
        private static ItemWarehouse _instance;
        public static ItemWarehouse Instance => _instance;

        private HashSet<string> _obtainedItems = new HashSet<string>();
        public event System.Action<string> OnItemAdded;    // Item added event
        public event System.Action<string> OnItemRemoved;  // Item removed event

        void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        // Add an item (if it does not exist)
        public bool AddItem(string itemName)
        {
            if (_obtainedItems.Add(itemName))
            {
                OnItemAdded?.Invoke(itemName);
                return true;
            }
            return false;
        }

        // Add an Item object (using Item.name)
        public bool AddItem(Item item)
        {
            return AddItem(item.name);
        }

        public bool AddItem(ItemConfigData config)
        {
            return AddItemByConfig(config);
        }

        // Remove an item
        public bool RemoveItem(string itemName)
        {
            if (_obtainedItems.Remove(itemName))
            {
                OnItemRemoved?.Invoke(itemName);
                return true;
            }
            return false;
        }

        // Check if an item exists
        public bool HasItem(string itemName)
        {
            return _obtainedItems.Contains(itemName);
        }

        // Get a copy of all obtained items as a list
        public List<string> GetAllItems()
        {
            return new List<string>(_obtainedItems);
        }

        // Clear the warehouse
        public void ClearWarehouse()
        {
            _obtainedItems.Clear();
            Debug.Log("Warehouse has been cleared");
        }

        // Get the total number of items in the warehouse
        public int GetItemCount()
        {
            return _obtainedItems.Count;
        }

        // Add default items in bulk (for initialization)
        public void InitializeWithDefaultItems(params string[] defaultItems)
        {
            foreach (var item in defaultItems)
            {
                AddItem(item);
            }
        }

        // Add an item using ItemConfigData
        public bool AddItemByConfig(ItemConfigData config)
        {
            if (config != null)
            {
                return AddItem(config.name);
            }
            return false;
        }
    }
}