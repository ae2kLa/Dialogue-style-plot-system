using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[JsonObject(MemberSerialization.OptOut)]
public class HEADER
{
    public string title { get; set; }
    public bool is_skippable { get; set; }
    public string fit_mode { get; set; }

    public void Execute()
    {
        UnityEngine.Debug.Log(title + " " + is_skippable + " " + fit_mode);

        if(is_skippable == true)
            UnityEngine.Debug.Log("bool convert success!");
    }

}
