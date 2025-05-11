using System.Collections;
using System.Collections.Generic;
using DIALOGUE;
using UnityEngine;

public class TestLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CommandManager.instance.Execute("startdialogue", "-f", "testFile");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
