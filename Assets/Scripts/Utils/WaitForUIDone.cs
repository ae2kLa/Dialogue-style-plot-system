using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace plot
{
    public class WaitForUIDone : CustomYieldInstruction
    {
        public bool wait = true;
        public override bool keepWaiting => CheckKeepWaiting();

        private bool CheckKeepWaiting()
        {
            Debug.Log(wait);
            return wait;
        }
    }

}
