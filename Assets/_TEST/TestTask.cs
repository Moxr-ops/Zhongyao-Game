using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestTask : MonoBehaviour
{
    public string taskName = "";
    public bool _check = false;
    [SerializeField] public ObservableProperty<bool> check = new ObservableProperty<bool>(false);

    public class ObservableProperty<T>
    {
        private T value;
        public UnityAction<T> OnValueChanged;

        public T Value
        {
            get => value;
            set
            {
                if (!EqualityComparer<T>.Default.Equals(this.value, value))
                {
                    this.value = value;
                    OnValueChanged?.Invoke(value);
                }
            }
        }

        public ObservableProperty(T initialValue)
        {
            this.value = initialValue;
        }
    }

    void Start()
    {

        check.OnValueChanged += OnValueChange;
    }

    private void OnValueChange(bool newValue)
    {
        Debug.Log($"Value changed to: {newValue}");
    }

    public void CheckTasks()
    {
        TaskManager.Instance.CheckTaskProgress();
    }
}