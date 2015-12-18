using UnityEngine;
using System.Collections;

public class Game_Over : MonoBehaviour {

    public GUISkin skin2;
    //updates whenever it needs to draw GUI elements
    void OnGUI()
    {
        //skin is our variable, we're making a variable location to put the GUI skin
        GUI.skin = skin2;

        //new rect creates struct instance, so it draws it?
       
        /* if (GUI.Button(new Rect(340, 400, 150, 50), "Replay?"))
        {
           
            Application.LoadLevel("Color_Plus");
        }
         * */

        if (GUI.Button(new Rect(270, 200, 400, 100), "You've lost!"))
        {
            print("You've quit the game");
            Application.Quit();
        }
        

    }
}
