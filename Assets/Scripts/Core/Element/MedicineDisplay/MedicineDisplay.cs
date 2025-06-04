using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MedicineDisplay : MonoBehaviour
{
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image medicineImage;
    [SerializeField] private TMP_Text medicineNameText;
    [SerializeField] private TMP_Text introductionText;
    [SerializeField] private Button closeButton;

    private string medicineName;
    private string medicineDescription;
    private Sprite medicineSprite;

    public void Initialize(string name, string description, Sprite image)
    {
        medicineName = name;
        medicineDescription = description;
        medicineSprite = image;

        if (medicineImage != null)
            medicineImage.sprite = medicineSprite;

        if (medicineNameText != null)
            medicineNameText.text = medicineName;

        if (introductionText != null)
            introductionText.text = medicineDescription;

        if (closeButton != null)
            closeButton.onClick.AddListener(Hide);
    }

    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);
    public void CloseAndDestroy()
    {
        Hide();
        Destroy(gameObject);
    }
}