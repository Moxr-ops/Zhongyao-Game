using UnityEngine;
using System.Collections;
using CHARACTERS;

namespace DIALOGUE
{
    public class DialogueLoaderManager : MonoBehaviour
    { 
        public static DialogueLoaderManager instance { get; private set; }

        public GameObject root;
        public GameObject dialogueLoader;
        public GameObject charactersContainer;
        public CanvasGroup rootCG => root.GetComponent<CanvasGroup>();

        protected Coroutine co_revealing;
        protected Coroutine co_closing;

        public bool isVisible => rootCG.alpha != 0;
        public bool isRevealing => co_revealing != null;
        public bool isClosing => co_closing != null;

        private void Awake()
        {
            instance = this;
        }

        public Coroutine Open(float speedMultiplier = 1f)
        {
            if (isRevealing)
                return co_revealing;

            if (isClosing)
                StopCoroutine(co_closing);

            co_revealing = StartCoroutine(OpeningOrClosing(true, speedMultiplier));
            return co_revealing;
        }

        public Coroutine Close(float speedMultiplier = 1f)
        {
            if (isClosing)
                return co_closing;

            if (isRevealing)
                StopCoroutine(co_revealing);

            co_closing = StartCoroutine(OpeningOrClosing(false, speedMultiplier));
            return co_closing;
        }

        public void ResetDialogueLoader()
        {
            Transform characters = charactersContainer.transform;

            if (characters != null)
            {
                foreach (Transform character in characters)
                {
                    Destroy(character);
                }
            }
        }

        private IEnumerator OpeningOrClosing(bool show, float speedMultiplier)
        {
            CanvasGroup self = rootCG;
            float targetAlpha = show ? 1f : 0f;

            if (show)
            {
                self.alpha = 0f;
                root.SetActive(true);
                self.interactable = true;
                self.blocksRaycasts = true;
            }

            while (!Mathf.Approximately(self.alpha, targetAlpha))
            {
                self.alpha = Mathf.MoveTowards(self.alpha, targetAlpha, 3f * Time.deltaTime * speedMultiplier);
                yield return null;
            }

            if (!show)
            {
                root.SetActive(false);
                self.interactable = false;
                self.blocksRaycasts = false;
            }

            co_revealing = co_closing = null;
        }
    }
}