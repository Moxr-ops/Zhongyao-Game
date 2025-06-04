using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using COMMANDS;
using COUNTER;
using DIALOGUE;
using ITEMS;
using UnityEngine;

namespace MEDICINE
{
    public class CMD_DatabaseExtension_Medicine : CMD_DatabaseExtension
    {
        private static string[] PARAM_COUNT => new string[] { "c", "count" };
        private static string[] PARAM_NECESSARY => new string[] { "n", "necessary" };
        private static string[] PARAM_ITEMS => new string[] { "i", "items" };
        private static string[] PARAM_TARGETS => new string[] { "t", "targets" };
        private static string[] PARAM_IMAGES => new string[] { "img", "images" };
        private static string[] PARAM_POSITION => new string[] { "p", "position" };
        private static string[] PARAM_FILE => new string[] { "f", "file" };

        new public static void Extend(CommandDatabase database)
        {
            database.AddCommand("putmedicineoncounter", new Action<string[]>(PutMedicineOnCounter));
            database.AddCommand("addmedicinetowarehouse", new Action<string[]>(AddMedicineToWarehouse));
            database.AddCommand("createmedicinepot", new Action<string[]>(CreateMedicinePot));
        }

        private static void CreateMedicinePot(string[] data)
        {
            CommandParameters parameters = new CommandParameters(data);

            if (!parameters.TryGetValue(PARAM_TARGETS, out string targetsStr, ""))
            {
                Debug.LogError("CreateMedicinePot 需要 -targets 参数");
                return;
            }

            string[] targetMeds = ParseStringArray(targetsStr);

            if (!parameters.TryGetValue(PARAM_COUNT, out int totalCount, 0))
            {
                Debug.LogError("CreateMedicinePot 需要 -count 参数");
                return;
            }

            Vector3 position = new Vector3(-1, 0, -4);
            if (parameters.TryGetValue(PARAM_POSITION, out string posStr, ""))
            {
                Debug.Log("Got Positon");
                string[] posParts = posStr.Split(',');
                if (posParts.Length == 3)
                {
                    float x, y, z;
                    if (float.TryParse(posParts[0], out x) &&
                        float.TryParse(posParts[1], out y) &&
                        float.TryParse(posParts[2], out z))
                    {
                        position = new Vector3(x, y, z);
                    }
                }
            }

            if (!parameters.TryGetValue(PARAM_FILE, out string file))
            {
                Debug.Log("创建药罐需要设置回调脚本 -f");
                return;
            }

            string[] randomOtherMedicine = ItemWarehouse.Instance.GetRandomItemsExcluding(totalCount, targetMeds);
            Debug.Log(randomOtherMedicine.Length);

            var(targetIndex, allMedicineToPut) = MergeAndShuffle(targetMeds, randomOtherMedicine);

            if (MedicinePotManager.Instance != null)
            {
                Debug.Log("Creating MedicinePot");
                Debug.Log(allMedicineToPut.Length);
                MedicinePotManager.Instance.CreateMedicinePot(targetIndex, allMedicineToPut, position, file);
            }
            else
            {
                Debug.LogError("MedicinePotManager 实例未创建");
            }
        }

        private static (string[] IndicesFormatted, string[] ShuffledArray) MergeAndShuffle(string[] a, string[] b)
        {
            var random = new System.Random();
            var combined = a.Concat(b).ToArray();
            var indices = Enumerable.Range(0, combined.Length).ToList();
            var shuffledIndices = indices.OrderBy(_ => random.Next()).ToList();
            var shuffledArray = shuffledIndices.Select(i => combined[i]).ToArray();

            string[] result = new string[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                int indexInShuffled = Array.IndexOf(shuffledArray, a[i]);
                result[i] = (indexInShuffled + 1).ToString("D2");
            }

            return (result, shuffledArray);
        }


        private static string[] ParseStringArray(string input)
        {
            return input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static void AddMedicineToWarehouse(string[] data)
        {
            CommandParameters parameters = new CommandParameters(data);

            if (!parameters.TryGetValue(PARAM_ITEMS, out string itemsStr, ""))
            {
                Debug.LogError("AddMedicineToWarehouse 命令需要 -items 参数");
                return;
            }

            string[] items = itemsStr.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (ItemWarehouse.Instance == null)
            {
                Debug.LogError("ItemWarehouse 实例未找到");
                return;
            }

            foreach (string item in items)
            {
                string trimmedItem = item.Trim();
                if (!string.IsNullOrEmpty(trimmedItem))
                {
                    bool success = ItemWarehouse.Instance.AddItem(trimmedItem);
                    if (!success)
                        Debug.LogWarning($"药品 '{trimmedItem}' 已存在，添加失败");
                }
            }
        }

        private static void PutMedicineOnCounter(string[] data)
        {
            CommandParameters parameters = new CommandParameters(data);

            if (!parameters.TryGetValue(PARAM_COUNT, out int count, 0))
            {
                Debug.LogError("PutMedicineOnCounter 命令需要 -count 参数");
                return;
            }

            List<string> necessaryMeds = new List<string>();
            if (parameters.TryGetValue(PARAM_NECESSARY, out string medsStr, ""))
            {
                string[] medArray = medsStr.Split(' ');
                foreach (string med in medArray)
                {
                    if (!string.IsNullOrEmpty(med))
                        necessaryMeds.Add(med.Trim());
                }
            }

            if (CounterManager.instance != null)
                CounterManager.instance.PlaceAllMedicine(count, necessaryMeds);
            else
                Debug.LogError("CounterManager 实例未找到");
        }
    }
}
