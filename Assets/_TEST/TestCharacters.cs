using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CHARACTERS;
using DIALOGUE;

namespace TESTING
{
    public class TestCharacters : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            //Character Stella = CharacterManager.instance.CreateCharacter("Stella");
            //Character Stella2 = CharacterManager.instance.CreateCharacter("Stella2");
            //Character Adam = CharacterManager.instance.CreateCharacter("Adam");
            StartCoroutine(Test());
        }

        IEnumerator Test()
        {
            Character Elen = CharacterManager.instance.CreateCharacter("Elen");

            List<string> lines = new List<string>()
            {
                "\"Hi, there!\"",
                "\"My name is Elen.\"",
                "\"What's your name?\"",
                "\"Oh, {wa 1} that's very nice.\""
            };

            yield return Elen.Say(lines);

            //yield return DialogueSystem.instance.Say(lines);

            Debug.Log("Finished");
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}