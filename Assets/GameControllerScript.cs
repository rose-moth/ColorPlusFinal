using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour
{
    
    private GameObject[,] allCubes;
    private GameObject randomCube;
    public GameObject CubePrefab;
    //aClicked Cube is a specific cube
    //ClickedCube is the general definition of a cube that can eb clicked
    public CubeBehavior clickedCube;
    public CubeBehavior oldCube;
    public CubeBehavior newCube;
    int numCubesWide = 5;
    int numCubesTall = 8;
    float timeToAct = 0.0f;
    float turnLength = 2.0f;
    public Text countCargo;
    public Text countPoints;
    private int amountOfCargo;
    public int points ;
    bool hasBeenPressed;
    Color[] colors = { Color.yellow, Color.blue, Color.green, Color.red, Color.magenta };
    int sameColoredPlusPoints = 10;
    int diffColoredPlusPoints = 10;
    public int currentx;
    public int currenty;
    public bool failedToPressANumber;
    public bool foundWhiteCube;
   // public int timer = 60; 
    //tracks whether a cube has been clicke

    //the 60 second marker
    public float startTime;
    //start time - a second each second
    private string currentTime;
   
    //applies things to the gui 
    public GUISkin skin;

    public Rect timerRect;
    public Rect timerLabelRect;

    

 

    

    void Start()
    {
        failedToPressANumber = false;

        clickedCube = new CubeBehavior();
        //startTime -= Time.deltaTime;

        
        //allCubes[0, 1].GetComponent<Renderer>().material.color = Color.gray;

        
        points = 0;
        SetCountText();

       
      
        allCubes = new GameObject[numCubesWide, numCubesTall];

        randomCube = new GameObject();
        randomCube = (GameObject)Instantiate(CubePrefab, new Vector3(29, 18, 0), Quaternion.identity);
        randomCube.GetComponent<Renderer>().material.color = colors[Random.Range(0, colors.Length)];

        for (int x = 0; x < numCubesWide; x++)
        {
            for (int y = 0; y < numCubesTall; y++)
            {
                //where the array is built
                allCubes[x, y] = (GameObject)Instantiate(CubePrefab, new Vector3(x * 3, y * 3, 0), Quaternion.identity);
                //allCubes[x,y] references the exact place of the cube we just used, GetComponent gets the info on which script, and .x says the x value of that component     
                allCubes[x, y].GetComponent<CubeBehavior>().x = x;
                allCubes[x, y].GetComponent<CubeBehavior>().y = y;


            }
        }

        //the starting cube is allCubes[0, 4].GetComponent<Renderer>().material.color = Color.blue;


        //make a timer
        //this timer takes place every turn ( 2 seconds)
        timeToAct = turnLength;


    }



   

    
  



   
    public bool AdjacentToClickedCube(GameObject clickedCube, int x, int y)
    {


        if ( //diagonal to the cube, in the upper left direction
           clickedCube.GetComponent<CubeBehavior>().x - 1 == this.clickedCube.x && clickedCube.GetComponent<CubeBehavior>().y + 1 == this.clickedCube.y ||
            //the one directly right of it, with the same y coordinate 
           clickedCube.GetComponent<CubeBehavior>().x + 1 == this.clickedCube.x && clickedCube.GetComponent<CubeBehavior>().y == this.clickedCube.y ||
            //the one directly below it, with the same x coordinate
           clickedCube.GetComponent<CubeBehavior>().y - 1 == this.clickedCube.y && clickedCube.GetComponent<CubeBehavior>().x == this.clickedCube.x ||
            //the one directly left of it, with the same y coordinate
           clickedCube.GetComponent<CubeBehavior>().x - 1 == this.clickedCube.x && clickedCube.GetComponent<CubeBehavior>().y == this.clickedCube.y ||
            //diagonal to the cube, in the lower right direction
           clickedCube.GetComponent<CubeBehavior>().x + 1 == this.clickedCube.x && clickedCube.GetComponent<CubeBehavior>().y - 1 == this.clickedCube.y ||

           //diagonal to the cube, in the upper right direction
           clickedCube.GetComponent<CubeBehavior>().x + 1 == this.clickedCube.x && clickedCube.GetComponent<CubeBehavior>().y + 1 == this.clickedCube.y ||

           //diagonal to the cube, in the lower left direction
           clickedCube.GetComponent<CubeBehavior>().x - 1 == this.clickedCube.x && clickedCube.GetComponent<CubeBehavior>().y - 1 == this.clickedCube.y ||
            //the one directly above it, with the same x coordinate
           clickedCube.GetComponent<CubeBehavior>().y + 1 == this.clickedCube.y && clickedCube.GetComponent<CubeBehavior>().x == this.clickedCube.x)



        {
            

            return true;
        }
        else
        {
            
            return false;
            
        }



    }

    public void ProcessClickedCube(GameObject aRandomClickedCube, int x, int y)
    {
        if (aRandomClickedCube.GetComponent<Renderer>().material.color != Color.white && aRandomClickedCube.GetComponent<Renderer>().material.color != Color.black)
        {

            if (clickedCube != null && clickedCube.active && x == clickedCube.x && y == clickedCube.y)
            {
                


                print("this should shrink...");

                clickedCube.transform.localScale = new Vector3(1, 1, 1);
                clickedCube.active = false;
                clickedCube = null;
                     


            }
            else
            {


                if (clickedCube != null)
                {//setting it back to small first
                    clickedCube.transform.localScale = new Vector3(1, 1, 1);
                    clickedCube.active = false;

                }
                //selecting the cube clicked 
                clickedCube = aRandomClickedCube.GetComponent<CubeBehavior>();
                //making it grow
                clickedCube.transform.localScale = new Vector3(2, 2, 2);
                clickedCube.didGrow = true;
                clickedCube.active = true;


            }
        }

       /* else if (aRandomClickedCube.GetComponent<Renderer>().material.color != Color.white && aRandomClickedCube.GetComponent<Renderer>().material.color != Color.black)
        {


            if (clickedCube.active && x == clickedCube.x && y == clickedCube.y)
            {



                print("this should shrink...");

                clickedCube.transform.localScale = new Vector3(1, 1, 1);
                clickedCube.active = false;


            }

        }
        */


        else if (clickedCube != null && AdjacentToClickedCube(aRandomClickedCube, x, y) && aRandomClickedCube.GetComponent<Renderer>().material.color == Color.white)
        {



            clickedCube.active = false;
            clickedCube.transform.localScale = new Vector3(1, 1, 1);



            aRandomClickedCube.GetComponent<Renderer>().material.color = clickedCube.gameObject.GetComponent<Renderer>().material.color;
            print("color set to" + clickedCube.gameObject.GetComponent<Renderer>().material.color);
            clickedCube.GetComponent<Renderer>().material.color = Color.white;
            //updates aRandom, keeps clickedCube up to date
            clickedCube = aRandomClickedCube.GetComponent<CubeBehavior>();
            clickedCube.transform.localScale = new Vector3(2, 2, 2);
            clickedCube.active = true;

        }

       
        

    }

    public void CheckKeyboardInput()
                    {
                        if (hasBeenPressed == true)
                        {
                            return;
                        }
                        
                        if (Input.GetKeyDown("1"))
                        {
                            print("1 is pressed");
                           // int number = Random.Range(0, 8);
                            if (CheckColumn(0) == true)
                            {
                                ReturnWhiteCubeInRow(0).GetComponent<Renderer>().material.color = randomCube.GetComponent<Renderer>().material.color;
                                randomCube.SetActive(false);
                                hasBeenPressed = true;
                                failedToPressANumber = false;
                                
                            }
                            else
                            {
                                Lose();
                            }


                        }

                        if (Input.GetKeyDown("2"))
                        {
                            print("2 is pressed");
                         //   int number = Random.Range(0, 8);
                            if (CheckColumn(1) == true)
                            {
                                ReturnWhiteCubeInRow(1).GetComponent<Renderer>().material.color = randomCube.GetComponent<Renderer>().material.color;
                                randomCube.SetActive(false);
                                hasBeenPressed = true;
                                failedToPressANumber = false;
                                
                            }
                            else
                            {
                                Lose();
                            }


                        }


                        if (Input.GetKeyDown("3"))
                        {
                            print("3 is pressed");
                          //  int number = Random.Range(0, 8);
                            if (CheckColumn(2) == true)
                            {
                                ReturnWhiteCubeInRow(2).GetComponent<Renderer>().material.color = randomCube.GetComponent<Renderer>().material.color;
                                randomCube.SetActive(false);
                                hasBeenPressed = true;
                                failedToPressANumber = false;
                                
                            }
                            else
                            {
                                Lose();
                            }


                        }


                        if (Input.GetKeyDown("4"))
                        {
                            print("4 is pressed");
                          //  int number = Random.Range(0, 8);
                            if (CheckColumn(3) == true)
                            {
                                ReturnWhiteCubeInRow(3).GetComponent<Renderer>().material.color = randomCube.GetComponent<Renderer>().material.color;
                                randomCube.SetActive(false);
                                hasBeenPressed = true;
                                failedToPressANumber = false;
                                
                            }
                            else
                            {
                                Lose();
                            }


                        }


                        if (Input.GetKeyDown("5"))
                        {
                            print("5 is pressed");
                          //  int number = Random.Range(0, 8);
                            if (CheckColumn(4) == true)
                            {
                                ReturnWhiteCubeInRow(4).GetComponent<Renderer>().material.color = randomCube.GetComponent<Renderer>().material.color;
                                randomCube.SetActive(false);
                                hasBeenPressed = true;
                                failedToPressANumber = false;
                                
                            }
                            else
                            {
                                Lose();
                            }


                        }


                    }



    //tells if there's a white cube
    bool checkAll()
                {

                    //bool foundWhiteCube = false;

                    for (int x = 0; x < numCubesWide; x++)
                    {
                        for (int y = 0; y < numCubesTall; y++)
                        {

                            if (allCubes[x, y].GetComponent<Renderer>().material.color == Color.white)
                            {
                                return foundWhiteCube = true;

                            }

                        }


                    }
                    return false;
                }


    bool CheckColumn(int row)
            {
                for (int y = 0; y < numCubesTall; y++)
                {

                    if (allCubes[row, y].GetComponent<Renderer>().material.color == Color.white)
                    {
                        return true;

                    }

                }
                return false;
            }



    GameObject ReturnWhiteCube()
            {

                bool foundWhiteCube = checkAll();
                if (foundWhiteCube == true)
                {


                    GameObject whiteCube = allCubes[(Random.Range(0, 5)), (Random.Range(0, 8))];

                    while (whiteCube.GetComponent<Renderer>().material.color != Color.white)
                    {
                        whiteCube = allCubes[(Random.Range(0, 5)), (Random.Range(0, 8))];
                

                    }

                    return whiteCube;
                }
                else
                {
                    return null;
                }


            }

    GameObject ReturnWhiteCubeInRow(int row)
                {

                    bool foundWhiteCube = CheckColumn(row);
                    if (foundWhiteCube == true)
                    {


                        GameObject whiteCube = allCubes[row, (Random.Range(0, 8))];

                        while (whiteCube.GetComponent<Renderer>().material.color != Color.white)
                        {
                            whiteCube = allCubes[row, (Random.Range(0, 8))];


                        }

                        return whiteCube;
                    }
                    else
                    {
                        Lose();
                        return null;
                        
                    }


                }

    void Lose() {
                    print("You lose!");
                    Application.LoadLevel("gameover");
                }




    void Win()
    {
        print("You win!");
        Application.LoadLevel("you_win");
    }





    void SetCountText()
        {
            
            countPoints.text = "Points = " + points.ToString();
        }



    
    
    bool CheckForSamePlus(int x, int y) {

        //holds the color information of the center cube 
        Color targetColor = allCubes[x,y].GetComponent<Renderer>().material.color;  
       
        //this determines if it's an actual colored cube and not white or black
        if (targetColor == Color.white || targetColor == Color.black) {
            return false;
        }
        if (
            //the above color is the same as the center targetColor
            targetColor == allCubes[x, y + 1].GetComponent<Renderer>().material.color &&
            //below
            targetColor == allCubes[x, y - 1].GetComponent<Renderer>().material.color &&
            //right
            targetColor == allCubes[x + 1, y].GetComponent<Renderer>().material.color &&
            //left
            targetColor == allCubes[x - 1, y].GetComponent<Renderer>().material.color)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    


 

    //give a color, it gives you a number
 
    int ColorValue(Color myColor) {

        if (myColor == Color.red) {
            return 1;
        }
        if (myColor == Color.blue) {
            return 10;
        }
        if (myColor == Color.green) {
            return 100;
        }
        if (myColor == Color.yellow) {
            return 1000;
        }
        if (myColor == Color.magenta) {
            return 10000;
        }
        else  return 0;
        

    }





    bool CheckForDiffPlus(int x, int y) {
        int myColorTotal = 0;
        Color targetColor = allCubes[x, y].GetComponent<Renderer>().material.color;  
        if (targetColor == Color.white || targetColor == Color.black)
        {
            return false;
        }
        //call method, give it the color in the center of the plus

        myColorTotal += ColorValue (allCubes[x,y].GetComponent<Renderer>().material.color);

        //call method, give it the color in the left of the center of the plus
        myColorTotal += ColorValue (allCubes[x,y+1].GetComponent<Renderer>().material.color);
        myColorTotal += ColorValue (allCubes[x,y-1].GetComponent<Renderer>().material.color);
        myColorTotal += ColorValue (allCubes[x+1,y].GetComponent<Renderer>().material.color);
        myColorTotal += ColorValue (allCubes[x-1,y].GetComponent<Renderer>().material.color);

        if (myColorTotal == 11111) 
            {
                return true; 
            }

        return false;

    }

    void MakePlusBlack(int x, int y) {
       
        
        {
            //the middle
            allCubes[x, y].GetComponent<Renderer>().material.color = Color.black;
            //above
            allCubes[x, y + 1].GetComponent<Renderer>().material.color = Color.black;
            //right
            allCubes[x + 1, y].GetComponent<Renderer>().material.color  = Color.black;
            //left
            allCubes[x - 1, y].GetComponent<Renderer>().material.color = Color.black;
            //below
            allCubes[x, y - 1].GetComponent<Renderer>().material.color = Color.black;


        }
    
    }



    



 
    //gives the score
    void ScoreCheck()
    {
        SetCountText();

        //chek for 5 cubes of the SAME color in a + config\
        //go through all inner groups of pluses
        for (int x = 1; x <numCubesWide -1; x++) {
            for (int y = 1; y < numCubesTall-1; y++) {
               if (CheckForSamePlus(x,y)) {
                    points += sameColoredPlusPoints;
                    //turn cubes black
                    print("Found a same plus!");
                    MakePlusBlack(x,y);
                    
                    

                }
               
                  if (  CheckForDiffPlus(x,y)) {
                      points += diffColoredPlusPoints;
                      print("Found a different plus!");
                      MakePlusBlack(x, y);

                  }

                if (failedToPressANumber && points > 0)
                {
                        //points = points - 1;
                        print("You lost a point.");
                        points -= 1;
                        failedToPressANumber = false;
                        
                  
                 }

            }
        }
        
        //check for 5 cubes of dif colors in +
    }


    void OnGUI()
    {
        GUI.skin = skin;
        GUI.Label(timerRect, currentTime + " seconds remaining", skin.GetStyle("Timer"));
        //GUI.Label(timerLabelRect,skin.GetStyle("Timer") + " seconds remaining");
        if (startTime < 10f)
        {
            skin.GetStyle("Timer").normal.textColor = Color.red;

        }
    }



    void Update()
                {
                    ScoreCheck();
                    CheckKeyboardInput();
                    //delta time allow something to move at a constant rate, a rhythm 
                    startTime -= Time.deltaTime;
                    // converts the int value of startTime to a string value
                    //currentTime = startTime.ToString();

                    //converts the int of startTime to a string, formats it to be 0:0
                    //currentTime holds all the updating information, so we want to make that the GUI
                    currentTime = string.Format("{0:0}",startTime);


               
                   



                    if (startTime <= 0 && points <= 0)
                    {
                        startTime = 0;
                        print("You're out of time...");
                        Application.LoadLevel("gameover");
                    }

                    if (startTime <= 0 && points > 0)
                    {
                        startTime = 0;
                        print("you win!");
                        Win();
                    }

                   
                    
                   
                    //when the time is greater than 1.5 seconds
                    if (Time.time > timeToAct)
                    { //add more time
                        timeToAct += turnLength;

            
                        //turn the cube back on
                        randomCube.SetActive(true);
                        //re-randomize randomCube's color
                        randomCube.GetComponent<Renderer>().material.color = colors[Random.Range(0, colors.Length)];
                        


                        if (hasBeenPressed == false && checkAll() == true)
                        {
                            ReturnWhiteCube().GetComponent<Renderer>().material.color = Color.black;
                            failedToPressANumber = true;
                
                        }
                        hasBeenPressed = false;
                        

                        //ScoreCheck();










                      
                    }
                }

    
}