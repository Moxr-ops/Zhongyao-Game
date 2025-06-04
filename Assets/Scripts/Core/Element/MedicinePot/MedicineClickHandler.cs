using UnityEngine;
using UnityEngine.EventSystems;

public class MedicineClickHandler : MonoBehaviour, IPointerClickHandler
{
    public delegate void MedicineClicked(string nodeName);
    private MedicineClicked onMedicineClicked;
    private string nodeName;

    public void Initialize(string nodeName, MedicineClicked callback)
    {
        this.nodeName = nodeName;
        onMedicineClicked = callback;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onMedicineClicked?.Invoke(nodeName);
    }
}