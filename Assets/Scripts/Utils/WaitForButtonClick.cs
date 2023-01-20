using FairyGUI;
using UnityEngine;

namespace plot_command_executor
{
    public class WaitForButtonClick : CustomYieldInstruction
    {
        private bool buttonClicked = false;
        private GButton[] buttons;

        public WaitForButtonClick(GButton[] buttons)
        {
            this.buttons = buttons;

            for(int i = 0; i < this.buttons.Length; i++)
            {
                this.buttons[i].onClick.Add(clicked);
            }
        }

        public override bool keepWaiting
        {
            get
            {
                return !buttonClicked;
            }
        }

        private void clicked()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i].selected == true)
                {
                    //CommandSender.Instance.FindPredicate(i);
                    //CommandReceiver.Instance.decisions.Add(i);
                    buttons[i].enabled = false;
                }
                else
                {
                    buttons[i].touchable = false;
                }
            }
            buttonClicked = true;
        }
    }

}
