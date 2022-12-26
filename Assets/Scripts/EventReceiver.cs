using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace plot
{
    public class EventReceiver : MonoSingleton<EventReceiver>
    {

        protected override void OnStart()
        {
            base.OnStart();

            Debug.Log("EventReciver Instance Done");
        }

        public void HEADER(string title, bool is_skippable, string fit_mode)
        {
            Debug.Log("HEADER Done!");
        }

        public void Background(string image, float fadetime, string screenadapt)
        {
            Debug.Log("Background Done!");
        }

        public void Dialogue(string name , string text)
        {
            Debug.Log("Dialogue Done!");
        }

        public void Delay(float delaytime)
        {
            Debug.Log("Delay Done!");
        }

        public void CameraShake(double Duration, int XStrength, int YStrength, int Vibrato, int Randomness, bool Fadeout, bool Block)
        {
            Debug.Log("CameraShake Done!");
        }



    }

}
