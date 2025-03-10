using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ITEMS;

[CreateAssetMenu(menuName = "TaskSystem/Conditions/Collecting Medicine")]
public class MedicinePreparingCondition : TaskCondition
{
    [Tooltip("Medicine needed")]
    public string[] requiredMedicine;

    public bool medicineIsMet = false;

    public override string ConditionDescription =>
        $"Need {requiredMedicine}";

    public void CheckMedicine()
    {
        foreach (var item in requiredMedicine)
        {
            if (!ItemWarehouse.Instance.HasItem(item))
            {
                medicineIsMet = false;
                return;
            }
        }
        medicineIsMet = true;
        return;
    }

    public override bool IsMet()
    {
        CheckMedicine();
        Debug.Log(medicineIsMet);
        return medicineIsMet;
    }

    public override void RegisterListeners()
    {
        //EventCenter.Instance.OnMedicineBrewed += CheckMedicine;
        //EventCenter.Instance.OnMedicineBrewed += TaskManager.Instance.CheckTaskProgress;
    }

    public override void UnregisterListeners()
    {
        //EventCenter.Instance.OnMedicineBrewed -= CheckMedicine;
        //EventCenter.Instance.OnMedicineBrewed -= TaskManager.Instance.CheckTaskProgress;
    }

    public override void ResetCondition()
    {
        medicineIsMet = false;
    }

    public override void OnCompletion()
    {
        Debug.Log("medicine over");
    }
}
