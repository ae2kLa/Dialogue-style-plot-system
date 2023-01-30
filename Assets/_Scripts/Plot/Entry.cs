using UnityEngine;
using plot_command_executor;


public class Entry : MonoBehaviour
{
    public GameObject dialogueUIPrefab;

    private GameObject dialogueUI;

    void Start()
    {
        dialogueUI = GameObject.Instantiate(dialogueUIPrefab);
        PlotUISettings.Instance.dialogueRoot.visible = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            PlotEventContainer.Instance.plotBegin.Invoke();
        }
    }




}
