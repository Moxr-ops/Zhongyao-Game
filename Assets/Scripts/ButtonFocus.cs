using UnityEngine;
using UnityEngine.EventSystems; // 用于处理事件系统

public class ButtonFocus : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found on the object");
        }
    }

    // 鼠标悬停时调用
    public void OnPointerEnter()
    {
        if (animator != null)
        {
            animator.SetBool("ButtonFocus", true);
            Debug.Log("Mouse entered the button area.");
        }
    }

    // 鼠标离开时调用
    public void OnPointerExit()
    {
        if (animator != null)
        {
            animator.SetBool("ButtonFocus", false);
            Debug.Log("Mouse exited the button area.");
        }
    }
}
