using System.Collections;
using System.Collections.Generic;
using DIALOGUE;
using TMPro;
using UnityEngine;

namespace ITEMS
{
    [System.Serializable]
    public class ItemConfigData
    {
        public string name;

        public Color nameColor;

        public string characteristic;

        public TMP_FontAsset nameFont;

        public ItemConfigData Copy()
        {
            ItemConfigData result = new ItemConfigData();
            result.name = name;
            result.nameFont = nameFont;

            result.nameColor = new Color(nameColor.r, nameColor.g, nameColor.b, nameColor.a);

            return result;
        }

        private static Color defaultColor => DialogueSystem.instance.config.defaultTextColor;
        private static TMP_FontAsset defaultFont => DialogueSystem.instance.config.defaultFont;

        public static ItemConfigData Default
        {
            get
            {
                ItemConfigData result = new ItemConfigData();
                result.name = "";
                result.characteristic = "";

                result.nameFont = defaultFont;
                result.nameColor = new Color(defaultColor.r, defaultColor.g, defaultColor.b, defaultColor.a);

                return result;
            }
        }
    }
}