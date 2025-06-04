using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarksManager : MonoBehaviour
{
    public static MarksManager instance {  get; private set; }

    private PlayerManager playerManager => PlayerManager.instance;

    [SerializeField]
    private List<GameObject> marks = new List<GameObject>();
    [SerializeField]
    private List<int> ints = new List<int>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CheckMarksState();
    }

    private void CheckMarksState()
    {
        int playerScriptIndex = playerManager.GetScriptIndex();
        Dictionary<GameObject, int> markIndexPair = new Dictionary<GameObject, int>();

        for (int i = 0; i < marks.Count; i++)
        {
            markIndexPair.Add(marks[i], ints[i]);
        }

        foreach (var pair in markIndexPair)
        {
            if (pair.Value <= playerScriptIndex)
            {
                pair.Key.SetActive(true);
            }
            else
            {
                pair.Key.SetActive(false);
            }
        }
    }
}
