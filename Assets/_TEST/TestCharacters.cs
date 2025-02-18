using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CHARACTERS;

namespace TESTING
{
    public class TestCharacters : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Character Elen = CharacterManager.instance.CreateCharacter("Elen");
            Character Stella = CharacterManager.instance.CreateCharacter("Stella");
            Character Stella2 = CharacterManager.instance.CreateCharacter("Stella");
            Character Adam = CharacterManager.instance.CreateCharacter("Adam");
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}