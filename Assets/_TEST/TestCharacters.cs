using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CHARACTERS;
using DIALOGUE;
using TMPro;
using System;
using Unity.Burst.CompilerServices;

namespace TESTING
{
    public class TestCharacters : MonoBehaviour
    {
        public TMP_FontAsset tempFont;

        private Character CreateCharacter(string name) => CharacterManager.instance.CreateCharacter(name);

        // Start is called before the first frame update
        void Start()
        {
            //Character Raelin = CharacterManager.Instance.CreateItem("Raelin");
            //Character Stella2 = CharacterManager.Instance.CreateItem("Stella");
            //Character Adam = CharacterManager.Instance.CreateItem("Adam");
            StartCoroutine(Test());
        }

        IEnumerator Test()
        {
            Character_Sprite Alia = CreateCharacter("Alia") as Character_Sprite;
            Character_Sprite Saki = CreateCharacter("Saki") as Character_Sprite;
            //Character guard2 = CreateItem("Test as Generic");
            //Character guard3 = CreateItem("Test as Generic");

            Alia.Show(1f);
            Saki.Show(1f);
            //guard2.Open();
            //guard3.Open();

            yield return new WaitForSeconds(1);

            //Alia.SetPosition(new Vector2(0, 1));

            Alia.SetPosition(new Vector2(0, 0));
            yield return Saki.MoveToPosition(new Vector2(1, 1), smooth: true);

            Sprite bodySprite = Alia.GetSprite("1");
            Sprite faceSprite = Alia.GetSprite("face2");

            Alia.SetSprite(bodySprite, 0);
            Alia.TransitionSprite(faceSprite, 1);

            //yield return Alia.TransitionColor(Color.green, speed : 0.3f);
            //yield return Alia.TransitionColor(Color.red);
            //yield return Alia.TransitionColor(Color.yellow);
            //yield return Alia.TransitionColor(Color.white);

            //yield return Alia.UnHighlight();
            //yield return new WaitForSeconds(1);
            //yield return Alia.Highlight();

            //yield return Alia.TransitionColor(Color.yellow);

            yield return Alia.UnHighlight();
            yield return new WaitForSeconds(1);
            yield return Alia.Highlight();

            yield return Alia.Flip(2f);
            yield return new WaitForSeconds(1);
            yield return Alia.FaceLeft(immediate: true);


            yield return null;
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}