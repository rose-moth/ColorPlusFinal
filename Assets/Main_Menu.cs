using UnityEngine;
using System.Collections;

public class Main_Menu : MonoBehaviour {
    public GUISkin skin;
    //updates whenever it needs to draw GUI elements
    void OnGUI()
    {
        //skin is our variable, we're making a variable location to put the GUI skin
        GUI.skin = skin;

        //new rect creates struct instance, so it draws it?

        if (GUI.Button(new Rect(300, 370, 250, 80), "Play!"))
        {
           
            Application.LoadLevel("Color_Plus");
        }
        if (GUI.Button(new Rect(300, 460, 250, 80), "Quit"))
        {
           Application.Quit();
        }

    }
}
