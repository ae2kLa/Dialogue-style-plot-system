using UnityEngine;
using plot_module;

public class Entry : MonoBehaviour
{
    public GameObject dialogueUIPrefab;

    void Awake()
    {
        PlotModule.Instance.PlotInit(dialogueUIPrefab);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            PlotModule.Instance.plotBegin.Invoke();
        }
    }




}
