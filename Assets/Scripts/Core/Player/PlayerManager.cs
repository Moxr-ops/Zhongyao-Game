using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance {  get; private set; }

    [SerializeField]
    private Player player;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateGameScriptIndex(int index)
    {
        player.gameScriptIndex = index;
    }

    public int GetScriptIndex()
    {
        return player.gameScriptIndex;
    }
}
