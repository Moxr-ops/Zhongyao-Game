using System;
using System.Collections;
using System.Collections.Generic;
using DIALOGUE;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

namespace COMMANDS
{
    public class CMD_DatabaseExtension_General : CMD_DatabaseExtension
    {
        private static string[] PARAM_ENABLE => new string[] { "e", "enable" };
        private static string[] PARAM_IMMEDIATE => new string[] { "i", "immediate" };
        private static string[] PARAM_SPEED => new string[] { "spd", "speed" };
        private static string[] PARAM_SMOOTH => new string[] { "sm", "smooth" };
        private static string[] PARAM_FILE => new string[] { "f", "file" };
        private static string PARAM_XPOS => "x";
        private static string PARAM_YPOS => "y";

        new public static void Extend(CommandDatabase database)
        {
            database.AddCommand("wait", new Func<string, IEnumerator>(Wait));
            database.AddCommand("startdialogue", new Action<string[]>(StartDialogue));
            database.AddCommand("openloader", new Action<string[]>(OpenDialogueLoader));
            database.AddCommand("closeloader", new Action<string[]>(CloseDialogueLoader));
            database.AddCommand("resetloader", new Action<string[]>(ResetLoader));
        }

        private static IEnumerator Wait(string data)
        {
            if (float.TryParse(data, out float time))
            {
                yield return new WaitForSeconds(time);
            }
        }

        private static void StartDialogue(string[] data)
        {
            string fileName;

            var parameters = ConvertDataToParameters(data);
            parameters.TryGetValue(PARAM_FILE, out fileName, defaultValue: "testFile");

            DialogueManager.instance.SetFileToRead(fileName);

            if (int.TryParse(fileName, out int gameScriptIndex))
            {
                PlayerManager.instance.UpdateGameScriptIndex(gameScriptIndex);
            }

            DialogueManager.instance.StartDialogue();
        }

        private static void OpenDialogueLoader(string[] data)
        {
            float speed = 1f;

            var parameters = ConvertDataToParameters(data);

            parameters.TryGetValue(PARAM_SPEED, out speed, defaultValue: 1f);

            DialogueLoaderManager.instance.Open(speed);
        }
        
        private static void CloseDialogueLoader(string[] data)
        {
            float speed = 1f;

            var parameters = ConvertDataToParameters(data);

            parameters.TryGetValue(PARAM_SPEED, out speed, defaultValue: 1f);

            DialogueLoaderManager.instance.Close(speed);
        }
        private static void ResetLoader(string[] data)
        {
            DialogueLoaderManager.instance.ResetDialogueLoader();
        }
    }
}