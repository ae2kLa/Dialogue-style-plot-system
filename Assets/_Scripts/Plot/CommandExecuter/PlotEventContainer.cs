using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace plot_command_executor
{
    public class PlotEventContainer : Singleton<PlotEventContainer>
    {
        public UnityEvent plotBegin = new UnityEvent();

        public UnityEvent plotEnd = new UnityEvent();


    }

}
