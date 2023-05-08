
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClickHandler : MonoBehaviour
{
    
    int CurrentPlayerId =  1;
    //GameObject[] Tiles;
    Cube[] Cubes;

    List<String> TileStates;

    bool GameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        Cubes = FindObjectsOfType<Cube>();
        //print(Cubes.Length);

        TileStates = new List<string>();
        
        foreach (Cube cube in Cubes)
        {
            //print(cube.TileState.ToString());
            TileStates.Add(cube.TileState.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Click the cubes
        if (Input.GetMouseButtonDown(0) && GameOver == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // the object identified by hit.transform was clicked
                if (hit.collider.GetComponentInParent<IClickable>() != null)
                {
                    hit.collider.GetComponentInParent<Cube>().Clicked(CurrentPlayerId);
                }
            }
        }
    }
    
    public string GetCurrentPlayerID()
    {
        return "Player " + CurrentPlayerId + "'s Turn";
    }

    public void BoardCheck()
    {
        // Check if the current player has won
        CheckWinConditions();

        // If all of the tiles are selected, then it is a tie
        CheckForTie();

        // If there is no winner, pass the turn
        if (GameOver) return;

        PassTurn();
    }

    private void CheckForTie()
    {
        foreach (Cube cube in Cubes)
        {
            if (cube.TileState == Cube.TileStates.Open)
            {
                return;
            }
        }

        GameOver = true;
        print("It's a tie");
    }

    void PassTurn()
    {
        // Switch Players
        if (CurrentPlayerId == 1) {CurrentPlayerId = 2;}else{CurrentPlayerId = 1;}
        
        //print(GetCurrentPlayerID());
        
    }

    void CheckWinConditions()
    {
        // Top Row
        TileChecks("Top Left" , "Top Middle" , "Top Right");

        // Middle Row
        TileChecks("Middle Left", "Middle", "Middle Right");

        // Bottom Row
        TileChecks("Bottom Left", "Bottom Middle" , "Bottom Right");

        // Left Column
        TileChecks("Top Left", "Middle Left" , "Bottom Left");

        // Middle Column
        TileChecks("Top Middle" , "Middle" , "Bottom Middle");

        // Right Column
        TileChecks("Top Right" , "Middle Right" , "Bottom Right");

        // Left Diagonal
        TileChecks("Top Left" , "Middle" , "Bottom Right");

        // Right Diagonal
        TileChecks("Top Right" , "Middle" , "Bottom Left");
    }

    void TileChecks(string Cube1, string Cube2, string Cube3)
    {
        // Check if the first string is Open
        foreach (Cube cube1 in Cubes)
        {
            if (cube1.TileName == Cube1 && cube1.TileState != Cube.TileStates.Open )
            {
                // Check if the second string is Open
                foreach (Cube cube2 in Cubes)
                {
                    if (cube2.TileName == Cube2 && cube2.TileState != Cube.TileStates.Open)
                    {
                        // Check if the third string is open
                        foreach (Cube cube3 in Cubes)
                        {
                            if (cube3.TileName == Cube3 && cube3.TileState != Cube.TileStates.Open)
                            {
                                if (cube1.TileState == cube2.TileState && cube2.TileState == cube3.TileState)
                                {
                                    // If all three cubes have the same symbol, then the player wins
                                    print("Player " + CurrentPlayerId + " Wins!");
                                    GameOver = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
