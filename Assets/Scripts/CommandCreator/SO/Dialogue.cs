using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace plot
{
    [Serializable]
    public class Dialogue : ScriptableObject
    {
        public new string name = "";
        public string text;
    }
}