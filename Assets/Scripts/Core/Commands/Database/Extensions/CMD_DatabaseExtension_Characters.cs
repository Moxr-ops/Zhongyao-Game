using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CHARACTERS;
using DIALOGUE;
using TMPro;
using UnityEngine;

namespace COMMANDS
{
    public class CMD_DatabaseExtension_Characters : CMD_DatabaseExtension
    {
        private static string[] PARAM_ENABLE => new string[] { "e", "enable" };
        private static string[] PARAM_IMMEDIATE => new string[] { "i", "immediate" };
        private static string[] PARAM_SPEED => new string[] { "spd", "speed" };
        private static string[] PARAM_SMOOTH => new string[] { "sm", "smooth" };
        private static string[] PARAM_SPRITE => new string[] { "sprite", "sp" };
        private static string[] PARAM_LAYER => new string[] { "layer", "l" };
        private static string PARAM_XPOS => "x";
        private static string PARAM_YPOS => "y";

        new public static void Extend(CommandDatabase database) // All commands are here
        {
            database.AddCommand("createcharacter", new Action<string[]>(CreateCharacter));
            database.AddCommand("movecharacter", new Func<string[], IEnumerator>(MoveCharacter));
            database.AddCommand("show", new Func<string[], IEnumerator>(ShowAll));
            database.AddCommand("hide", new Func<string[], IEnumerator>(HideAll));
            database.AddCommand("faceleft", new Func<string[], IEnumerator>(FaceLeft));
            database.AddCommand("faceright", new Func<string[], IEnumerator>(FaceRight));
            database.AddCommand("setsprite", new Action<string[]>(SetSprite));
            database.AddCommand("tssprite", new Func<string[], IEnumerator>(TransitionSprite));

        }

        public static void CreateCharacter(string[] data) // Only one character can be created at a time. e.p. CreateItem(Alia), CreateItem(Saki -e true -i false)
        {
            string characterName = data[0];
            bool enable = false;
            bool immediate = false;
            float speed = 1f;

            var parameters = ConvertDataToParameters(data);
            parameters.TryGetValue(PARAM_ENABLE, out enable, defaultValue: false);
            parameters.TryGetValue(PARAM_IMMEDIATE, out immediate, defaultValue: false);
            parameters.TryGetValue(PARAM_SPEED, out speed, defaultValue: 1f);

            Character character = CharacterManager.instance.CreateCharacter(characterName);

            if (!enable)
            {
                UnityEngine.Debug.Log(enable);
                return;
            }

            if (immediate)
            {
                character.isVisible = true;
                character.Show(20); // 其实应该用showorhideimmediately，但这里偷懒了（
            }

            else
            {
                character.Show(speed);
            }
        }

        private static IEnumerator MoveCharacter(string[] data) // e.p. MoveCharacter(Saki -x 0 -spd 0.75 -sm true)
        {
            string characterName = data[0];
            Character character = CharacterManager.instance.GetCharacter(characterName);

            if (character == null)
            {
                UnityEngine.Debug.LogError($"Cannot find {character.name}");
                yield break;
            }

            float x = 0, y = 0;
            float speed = 1;
            bool smooth = false;
            bool immediate = false;

            var parameters = ConvertDataToParameters(data);

            // Try to get the x axis position
            parameters.TryGetValue(PARAM_XPOS, out x);

            // Try to get the y axis position
            parameters.TryGetValue(PARAM_YPOS, out y);

            // Try to get the speed
            parameters.TryGetValue(PARAM_SPEED, out speed, defaultValue: 1);

            // Try to get the smoothing
            parameters.TryGetValue(PARAM_SMOOTH, out smooth, defaultValue: false);

            // Try to get immediate setting of position
            parameters.TryGetValue(PARAM_IMMEDIATE, out immediate, defaultValue: false);

            Vector2 position = new Vector2(x, y);

            if (immediate)
                character.SetPosition(position);
            else
            {
                CommandManager.instance.AddTerminationActionToCurrentProcess(() => { character?.SetPosition(position); });
                yield return character.MoveToPosition(position, speed, smooth);
            }
        }

        public static IEnumerator ShowAll(string[] data) // e.p. Open(Alia Saki -immediate false)
        {
            List<Character> characters = new List<Character>();
            bool immediate = false;
            float speed = 1f;

            foreach (string s in data)
            {
                Character character = CharacterManager.instance.GetCharacter(s, createIfDoesNotExist: false);
                if (character != null)
                    characters.Add(character);
            }

            if (characters.Count == 0)
                yield break;

            // Convert the data array to a parameter container
            var parameters = ConvertDataToParameters(data);

            // Try to get the immediate parameter value
            parameters.TryGetValue(PARAM_IMMEDIATE, out immediate, defaultValue: false);
            parameters.TryGetValue(PARAM_SPEED, out speed, defaultValue: 1f);

            UnityEngine.Debug.Log($"speed = {speed}");

            // Call the logic on all the characters
            foreach (Character character in characters)
            {
                if (immediate)
                {
                    character.isVisible = true;
                    character.ShowOrHideImmediately(true);
                }
                else
                    character.Show(speed);
            }

            if (!immediate)
            {
                CommandManager.instance.AddTerminationActionToCurrentProcess(() =>
                {
                    foreach (Character character in characters)
                    {
                        //character.isVisible = true;
                        character.ShowOrHideImmediately(true);
                    }
                });

                while (characters.Any(c => c.isRevealing))
                    yield return null;
            }
        }

        public static IEnumerator HideAll(string[] data)
        {
            List<Character> characters = new List<Character>();
            bool immediate = false;
            float speed = 1f;

            foreach (string s in data)
            {
                Character character = CharacterManager.instance.GetCharacter(s, createIfDoesNotExist: false);
                if (character != null)
                    characters.Add(character);
            }

            if (characters.Count == 0)
                yield break;

            // Convert the data array to a parameter container
            var parameters = ConvertDataToParameters(data);

            // Try to get the immediate parameter value
            parameters.TryGetValue(PARAM_IMMEDIATE, out immediate, defaultValue: false);
            parameters.TryGetValue(PARAM_SPEED, out speed, defaultValue: 1f);

            // Call the logic on all the characters
            foreach (Character character in characters)
            {
                if (immediate)
                {
                    //character.isVisible = false;
                    character.ShowOrHideImmediately(false);
                }
                else
                    character.Hide(speed);
            }

            if (!immediate)
            {
                CommandManager.instance.AddTerminationActionToCurrentProcess(() =>
                {
                    foreach (Character character in characters)
                    {
                        character.isVisible = false;
                        character.ShowOrHideImmediately(false);
                    }
                });

                while (characters.Any(c => c.isHiding))
                    yield return null;
            }
        }

        private static IEnumerator FaceLeft(string[] data)
        {
            string characterName = data[0];
            Character character = CharacterManager.instance.GetCharacter(characterName);

            if (character == null || !(character is Character_Sprite))
            {
                UnityEngine.Debug.LogError($"Sprite character not found: {characterName}");
                yield break;
            }

            var parameters = ConvertDataToParameters(data);
            parameters.TryGetValue(PARAM_SPEED, out float speed, defaultValue: 1f);
            parameters.TryGetValue(PARAM_IMMEDIATE, out bool immediate, defaultValue: false);

            Character_Sprite spriteChar = (Character_Sprite)character;
            Coroutine flipCoroutine = spriteChar.FaceLeft(speed, immediate);

            CommandManager.instance.AddTerminationActionToCurrentProcess(() =>
            {
                spriteChar.FaceLeft(immediate: true);
                if (flipCoroutine != null)
                    CommandManager.instance.StopCoroutine(flipCoroutine);
            });

            yield return flipCoroutine;
        }

        private static IEnumerator FaceRight(string[] data)
        {
            string characterName = data[0];
            Character character = CharacterManager.instance.GetCharacter(characterName);

            if (character == null)
            {
                UnityEngine.Debug.LogError($"Character not found: {characterName}");
                yield break;
            }

            var parameters = ConvertDataToParameters(data);
            parameters.TryGetValue(PARAM_SPEED, out float speed, defaultValue: 1f);
            parameters.TryGetValue(PARAM_IMMEDIATE, out bool immediate, defaultValue: false);

            if (immediate)
                character.FaceRight(speed: 1, immediate: true);
            else
            {
                CommandManager.instance.AddTerminationActionToCurrentProcess(() => character.FaceRight(immediate: true));
                yield return character.FaceRight(speed, immediate);
            }
        }

        private static void SetSprite(string[] data)
        {
            string characterName = data[0];
            Character character = CharacterManager.instance.GetCharacter(characterName);

            if (character == null || !(character is Character_Sprite))
            {
                UnityEngine.Debug.LogError($"Sprite character not found: {characterName}");
                return;
            }

            var parameters = ConvertDataToParameters(data);

            parameters.TryGetValue(PARAM_SPRITE, out string spriteName, defaultValue: "");
            parameters.TryGetValue(PARAM_LAYER, out int layer, defaultValue: 0);

            Character_Sprite spriteChar = (Character_Sprite)character;
            Sprite sprite = spriteChar.GetSprite(spriteName);

            if (sprite == null)
            {
                UnityEngine.Debug.LogError($"Sprite '{spriteName}' not found for {characterName}");
                return;
            }

            spriteChar.SetSprite(sprite, layer);
        }

        private static IEnumerator TransitionSprite(string[] data)
        {
            string characterName = data[0];
            Character character = CharacterManager.instance.GetCharacter(characterName);

            if (character == null || !(character is Character_Sprite))
            {
                UnityEngine.Debug.LogError($"Sprite character not found: {characterName}");
                yield break;
            }

            var parameters = ConvertDataToParameters(data);
            parameters.TryGetValue(PARAM_SPRITE, out string spriteName, defaultValue: "");
            parameters.TryGetValue(PARAM_LAYER, out int layer, defaultValue: 0);
            parameters.TryGetValue(PARAM_SPEED, out float speed, defaultValue: 1f);

            Character_Sprite spriteChar = (Character_Sprite)character;
            UnityEngine.Debug.Log(spriteChar == null);
            Sprite sprite = spriteChar.GetSprite(spriteName);

            if (sprite == null)
            {
                UnityEngine.Debug.LogError($"Sprite '{spriteName}' not found for {characterName}");
                yield break;
            }

            Coroutine transition = spriteChar.TransitionSprite(sprite, layer, speed);

            CommandManager.instance.AddTerminationActionToCurrentProcess(() =>
            {
                if (transition != null)
                    CommandManager.instance.StopCoroutine(transition);
                spriteChar.SetSprite(sprite, layer);
            });

            yield return transition;
        }
    }
}
