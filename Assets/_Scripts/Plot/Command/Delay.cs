using System;
using UnityEngine;

namespace plot_command
{
    [Serializable]
    public class Delay : CommandBase
    {
        [field: SerializeField]
        public float time;

        private float startTime;

        public override void Execute()
        {
            startTime = Time.time;
        }

        public override void OnUpdate()
        {

        }

        public override bool IsFinished()
        {
            return Time.time > startTime + time;
        }
    }
}