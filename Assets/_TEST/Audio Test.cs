using System.Collections;
using UnityEngine;
using CHARACTERS;
using DIALOGUE;

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
            yield return new WaitForSeconds(1);

            Character_Sprite Alia = CreateCharacter("Alia") as Character_Sprite;
            Alia.Show();

            yield return DialogueSystem.instance.Say("Narrator", "Can we see your ship?");

            AudioManager.instance.PlayTrack("Audio/SFX/Birdsong", volumeCap: 1f, pitch: 0.1f);
            // AudioManager.instance.PlayVoice("Audio/Voices/Stella/Yeah_Laugh");
        }
    }
}