using System;
using System.Collections;
using System.Collections.Generic;
using ITEMS;
using UnityEditor.Rendering;
using UnityEngine;

public class MedicineDisplayManager : MonoBehaviour
{
    public static MedicineDisplayManager instance {  get; private set; }

    [SerializeField]
    private GameObject MedicineDisplayPrefab;
    [SerializeField]
    private Transform spawnParent;

    private void Awake()
    {
        instance = this;
    }

    public void InitializeMedicineDisplay(string medicineName)
    {
        if (string.IsNullOrEmpty(medicineName) || MedicineDisplayPrefab == null)
        {
            Debug.LogError("Invalid medicine name or missing prefab reference");
            return;
        }

        var medicineInfo = ItemsManager.instance.GetItemInfo(medicineName);
        if (medicineInfo == null || medicineInfo.config == null)
        {
            Debug.LogError($"Failed to get medicine info for: {medicineName}");
            return;
        }

        string name = medicineInfo.config.name;
        string description = medicineInfo.config.characteristic;
        Sprite image = null;

        Sprite[] images = Resources.LoadAll<Sprite>(ItemsManager.instance.GetItemImagePath(medicineName));
        if (images != null && images.Length > 0)
        {
            image = Array.Find(images, img => img.name == medicineName);
        }

        GameObject newMedicineDisplay = Instantiate(MedicineDisplayPrefab, spawnParent);
        MedicineDisplay displayer = newMedicineDisplay.GetComponent<MedicineDisplay>();

        if (displayer != null)
        {
            displayer.Initialize(name, description, image);
            displayer.Show();
        }
        else
        {
            Debug.LogError("MedicineDisplay component missing on prefab");
            Destroy(newMedicineDisplay);
        }
    }

}
