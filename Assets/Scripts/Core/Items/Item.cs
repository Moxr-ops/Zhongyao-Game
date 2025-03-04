using System.Collections;
using System.Collections.Generic;
using CHARACTERS;
using UnityEngine;
using UnityEngine.UI;

namespace ITEMS
{
    public class Item
    {
        private const float UNHIGHLIGHTED_DARKEN_STRENGTH = 0.65f;

        public string name = "";
        public bool unlocked = false;
        public RectTransform root = null;
        public ItemConfigData config;
        public Animator animator;
        private CanvasGroup rootCG => root.GetComponent<CanvasGroup>();
        public Color color { get; protected set; } = Color.white;
        protected Color displayColor => highlighted ? highlightedColor : unhighlightedColor;
        protected Color highlightedColor => color;
        protected Color unhighlightedColor => new Color(color.r * UNHIGHLIGHTED_DARKEN_STRENGTH, color.g * UNHIGHLIGHTED_DARKEN_STRENGTH, color.b * UNHIGHLIGHTED_DARKEN_STRENGTH, color.a);
        public bool highlighted { get; protected set; } = true;

        protected Coroutine co_revealing, co_hiding;
        protected Coroutine co_moving;
        protected Coroutine co_changingColor;
        protected Coroutine co_highlighting;
        protected Coroutine co_flipping;

        public bool isRevealing => co_revealing != null;
        public bool isHiding => co_hiding != null;
        public bool isMoving => co_moving != null;
        public bool isChangingColor => co_changingColor != null;
        public bool isHighlighting => (highlighted && co_highlighting != null);
        public bool isUnHighlighting => (!highlighted && co_highlighting != null);
        public virtual bool isVisible { get; set; }
        public bool isFlipping => co_flipping != null;
        protected ItemsManager itemManager => ItemsManager.instance;

        public Item(string name, ItemConfigData config, GameObject prefab)
        {
            this.name = name;
            this.config = config;

            if (prefab != null)
            {
                GameObject ob = Object.Instantiate(prefab, itemManager.itemPanel);
            }
        }

        public Coroutine Show(float speedMultiplier = 1f)
        {
            if (isRevealing)
                return co_revealing;

            if (isHiding)
                itemManager.StopCoroutine(co_hiding);

            co_revealing = itemManager.StartCoroutine(ShowingOrHiding(true, speedMultiplier));
            return co_revealing;
        }

        public Coroutine Hide(float speedMultiplier = 1f)
        {
            if (isHiding)
                return co_hiding;

            if (isRevealing)
                itemManager.StopCoroutine(co_revealing);

            co_hiding = itemManager.StartCoroutine(ShowingOrHiding(false, speedMultiplier));
            return co_hiding;
        }

        public IEnumerator ShowingOrHiding(bool show, float speedMultiplier)
        {
            float targetAlpha = show ? 1f : 0;
            CanvasGroup self = rootCG;

            while (self.alpha != targetAlpha)
            {
                self.alpha = Mathf.MoveTowards(self.alpha, targetAlpha, 3f * Time.deltaTime * speedMultiplier);
                yield return null;
            }

            co_revealing = null;
            co_hiding = null;
        }
    }
}
