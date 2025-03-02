using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ITEMS
{
    [CreateAssetMenu(fileName = "Item Configuration Asset", menuName = "Item System/Item Configuration Asset")]
    public class ItemConfigSO : ScriptableObject
    {
        public ItemConfigData[] items;

        public ItemConfigData GetConfig(string itemName)
        {
            itemName = itemName.ToLower();

            for (int i = 0; i < items.Length; i++)
            {
                ItemConfigData data = items[i];
                if (string.Equals(itemName, data.name.ToLower()))
                    return data.Copy();
            }

            return ItemConfigData.Default;
        }
    }
}