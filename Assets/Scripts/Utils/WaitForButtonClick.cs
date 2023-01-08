using FairyGUI;
using System.Linq;
using UnityEngine;

namespace plot
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
                    CommandSender.Instance.FindPredicate(ref CommandSender.Instance.i, CommandSender.Instance.mc, i);
                    CommandReceiver.Instance.decisions.Add(i);
                }
                buttons[i].onChanged.Remove(clicked);
            }
            buttonClicked = true;
        }
    }

}
