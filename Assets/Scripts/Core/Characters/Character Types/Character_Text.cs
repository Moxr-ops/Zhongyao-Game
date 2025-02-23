using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CHARACTERS
{
    public class Character_Text : Character
    {
        public Character_Text(string name, CharacterConfigData config, GameObject prefab = null) : base(name, config, prefab)
        {
            Debug.Log($"Created Text Character: '{name}'");
        }
    }
}