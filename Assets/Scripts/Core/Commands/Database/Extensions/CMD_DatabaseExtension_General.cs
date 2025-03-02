using System;
using System.Collections;
using System.Collections.Generic;
using DIALOGUE;
using UnityEngine;

namespace COMMANDS
{
    public class CMD_DatabaseExtension_General : CMD_DatabaseExtension
    {
        private static string[] PARAM_ENABLE => new string[] { "e", "enable" };
        private static string[] PARAM_IMMEDIATE => new string[] { "i", "immediate" };
        private static string[] PARAM_SPEED => new string[] { "spd", "speed" };
        private static string[] PARAM_SMOOTH => new string[] { "sm", "smooth" };
        private static string PARAM_XPOS => "x";
        private static string PARAM_YPOS => "y";

        new public static void Extend(CommandDatabase database)
        {
            database.AddCommand("wait", new Func<string, IEnumerator>(Wait));
            database.AddCommand("start", new Action(StartDialogue));
            database.AddCommand("openloader", new Action<string[]>(OpenDialogueLoader));
            database.AddCommand("closeloader", new Action<string[]>(CloseDialogueLoader));
        }

        private static IEnumerator Wait(string data)
        {
            if (float.TryParse(data, out float time))
            {
                yield return new WaitForSeconds(time);
            }
        }

        private static void StartDialogue()
        {
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
    }
}