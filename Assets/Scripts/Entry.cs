using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entry : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextParser textParser = new TextParser("Assets/Text/level01.txt");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
