using UnityEngine;
using System.Collections;
using CHARACTERS;
using System.Collections.Generic;
using UnityEditor;

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
            //dialogueLoaderManager.Open();  // 如果说想要startDialogue方法调用后，对话框立即显现，则去掉注释
            List<string> lines = FileManager.ReadTextAsset(fileToRead);

            DialogueSystem.instance.Say(lines);
        }

        public void SetFileToRead(string filename)
        {
            //fileToRead = AssetDatabase.LoadAssetAtPath<TextAsset>(FilePaths.GetPathToResource(FilePaths.resources_gamescript, filename));
            fileToRead = Resources.Load<TextAsset>(FilePaths.GetPathToResource(FilePaths.resources_gamescript, filename));
            if (fileToRead == null)
            {
                Debug.LogError("No file found");
            }
        }

        public void EndDialogue()
        {
            dialogueLoaderManager.Close();
        }
        
    }
}