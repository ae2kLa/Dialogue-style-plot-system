using UnityEngine;
using plot_command_executor;


public class Entry : MonoBehaviour
{
    public GameObject dialogueUIPrefab;

    void Awake()
    {
        PlotEventContainer.Instance.PlotInit(dialogueUIPrefab);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            PlotEventContainer.Instance.plotBegin.Invoke();
        }
    }




}
