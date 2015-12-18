using UnityEngine;
using System.Collections;

public class youwin : MonoBehaviour {

    public GUISkin skin3;
    //updates whenever it needs to draw GUI elements
    void OnGUI()
    {
        //skin is our variable, we're making a variable location to put the GUI skin
        GUI.skin = skin3;

        //new rect creates struct instance, so it draws it?
       
      
        if (GUI.Button(new Rect(270, 200, 400, 100), "You've won! Click here to quit."))
        {
            print("You've won the game!");
            Application.Quit();
        }
        

    }
}

