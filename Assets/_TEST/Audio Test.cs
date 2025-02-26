using System.Collections;
using System.Collections.Generic;
using CHARACTERS;
using UnityEngine;

namespace TESTING
{
    public class AudioTesting : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(Running());
        }

        Character CreateCharacter(string name) => CharacterManager.instance.CreateCharacter(name);

        IEnumerator Running()
        {
            Character_Sprite Alia = CreateCharacter("Alia") as Character_Sprite;
            Alia.Show(1f);
            yield return new WaitForSeconds(0.5f);

            AudioManager.instance.PlaySoundEffect("Audio/SFX/Birdsong");

            yield return new WaitForSeconds(1f);
        }
    }
}