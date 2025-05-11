using System;
using System.Collections.Generic;
using System.IO;
using ITEMS;
using UnityEngine;

namespace ARCHIVE
{
    public class ArchivingManager : MonoBehaviour
    {
        public static ArchivingManager Instance { get; private set; }

        [SerializeField] private GameObject playerObject;
        private string saveFileName = "archive.txt";
        private string savePath => FilePaths.GetPathToResource(FilePaths.resources_archivefile, saveFileName);

        private void Awake()
        {
            Instance = this;
        }

        public void Save()
        {
            try
            {
                string jsonToSave = GetDataToArchive();
                File.WriteAllText(savePath, jsonToSave);
                Debug.Log("¥Êµµ≥…π¶£°");
            }
            catch (Exception ex)
            {
                Debug.LogError($"¥Êµµ ß∞‹: {ex.Message}");
            }
        }

        public void Load()
        {
            PlayerData playerData = GetDataToLoad();
            Player player = playerObject.GetComponent<Player>();

            int scene = playerData.playerScene;
            List<string> items = playerData.items;
            int timesPlayedGame = playerData.timesPlayerGame;
            List<string> tasks = playerData.tasks;

            player.scene = scene;
            player.timesPlayedGame = timesPlayedGame + 1;

            foreach (var itme in items)
            {
                ItemWarehouse.Instance.AddItem(itme);
            }

            foreach (var task in tasks)
            {
                TaskManager.Instance.AddTaskByName(task);
            }
        }

        public bool HaveArchive()
        {
            if (File.Exists(savePath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private PlayerData GetDataToLoad()
        {
            try
            {
                if (!File.Exists(savePath)) return null;

                string jsonData = File.ReadAllText(savePath);
                return JsonUtility.FromJson<PlayerData>(jsonData);
            }
            catch (Exception ex)
            {
                Debug.LogError($"∂¡»°¥Êµµ ß∞‹: {ex.Message}");
                return null;
            }
        }

        private string GetDataToArchive()
        {
            if (playerObject == null)
            {
                Debug.LogError("Player∂‘œÛŒ¥∑÷≈‰£°");
                return "{}";
            }

            Player player = playerObject.GetComponent<Player>();
            if (player == null)
            {
                Debug.LogError("Player◊Èº˛»± ß£°");
                return "{}";
            }

            List<string> itemList = ItemWarehouse.Instance.GetAllItems();
            List<string> taskList = TaskManager.Instance.GetActiveTaskIDs();

            PlayerData data = new PlayerData
            {
                playerScene = player.scene,
                items = itemList,
                timesPlayerGame = player.timesPlayedGame,
                tasks = taskList
            };

            return JsonUtility.ToJson(data, true);
        }

        [Serializable]
        private class PlayerData
        {
            public int playerScene;
            public List<string> items;
            public int timesPlayerGame;
            public List<string> tasks;
        }
    }
}
