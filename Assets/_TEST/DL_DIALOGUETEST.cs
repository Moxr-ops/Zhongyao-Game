using TMPro;
using UnityEngine;

namespace TESTING
{
    public class DL_DIALOGUETEST : MonoBehaviour
    {
        public string testTxt;
        public TextMeshProUGUI dialogueText;

        private TextArchitect architect;

        void Start()
        {
            // 确保 dialogueText 不为 null
            if (dialogueText != null)
            {
                architect = new TextArchitect(dialogueText);
                architect.Append(testTxt);
            }
            else
            {
                Debug.LogError("dialogueText not assigned! Please assign a TextMeshProUGUI component in the Inspector.");
            }
        }
    }
}