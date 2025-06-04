using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARCHIVE;
using DIALOGUE;

public class RunGameScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        CommandManager.instance.Execute("startdialogue", "-f", "1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
