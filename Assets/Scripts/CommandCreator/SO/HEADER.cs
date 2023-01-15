using System;
using UnityEngine;

namespace plot
{

    [Serializable]
    public class HEADER : ScriptableObject
    {
        public string title;
        public bool is_skippable;
        public string fit_mode;
    }
}