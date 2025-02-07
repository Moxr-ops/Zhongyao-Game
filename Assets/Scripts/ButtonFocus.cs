using UnityEngine;
using UnityEngine.EventSystems; // ���ڴ����¼�ϵͳ

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

    // �����ͣʱ����
    public void OnPointerEnter()
    {
        if (animator != null)
        {
            animator.SetBool("ButtonFocus", true);
            Debug.Log("Mouse entered the button area.");
        }
    }

    // ����뿪ʱ����
    public void OnPointerExit()
    {
        if (animator != null)
        {
            animator.SetBool("ButtonFocus", false);
            Debug.Log("Mouse exited the button area.");
        }
    }
}
