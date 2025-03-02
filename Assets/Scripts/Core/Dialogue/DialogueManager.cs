using UnityEngine;
using System.Collections;
using CHARACTERS;
using System.Collections.Generic;

namespace DIALOGUE
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private TextAsset fileToRead = null;
        public static DialogueManager instance { get; private set; }

        private DialogueLoaderManager dialogueLoaderManager => DialogueLoaderManager.instance;

        private void Awake()
        {
            instance = this;
        }

        public void StartDialogue()
        {
            dialogueLoaderManager.Open();

            List<string> lines = FileManager.ReadTextAsset(fileToRead);

            DialogueSystem.instance.Say(lines);
        }

        public void EndDialogue()
        {
            dialogueLoaderManager.Close();
        }
        
    }
}