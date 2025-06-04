using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPrefebPos : MonoBehaviour
{

    public Transform parentObject; // ÄãµÄ¿ÕÎïÌå
    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        GameObject instance = Instantiate(prefab, parentObject.position, Quaternion.identity, parentObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
