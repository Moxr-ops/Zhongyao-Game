using UnityEngine;

public abstract class TaskCondition : ScriptableObject
{
    public abstract string ConditionDescription { get; }
    public abstract bool IsMet();
    public abstract void RegisterListeners();
    public abstract void UnregisterListeners();

    public abstract void OnCompletion();

    public virtual void ResetCondition() { }
}