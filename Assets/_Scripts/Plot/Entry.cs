using UnityEngine;
using plot_module;
using UnityEditor;

public class Entry : MonoBehaviour
{
    void Awake()
    {
        PlotModule.Instance.Init();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            PlotModule.Instance.SetPlotConfig("PlotConfigs/PlotCommandConfig");
            PlotModule.Instance.plotBegin.Invoke();
        }
    }




}
