
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ClickHandler : MonoBehaviour
{
    
    int CurrentPlayerId =  1;
    public Text WinningPlayer;
    public Canvas WinMenu;
    public Canvas TieMenu;

    Cube[] Cubes;

    List<String> TileStates;

    bool GameOver = true;



    // Start is called before the first frame update
    void Start()
    {
        // Set Up Menus
        WinMenu.enabled = false;
        TieMenu.enabled = false;
        GameOver = false;
        
        Cubes = FindObjectsOfType<Cube>();

        TileStates = new List<string>();
        
        foreach (Cube cube in Cubes)
        {
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
    
    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void BoardCheck()
    {
        // Check if the current player has won
        CheckWinConditions();
        if (GameOver) return;

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
        TieMenu.enabled = true;
    }

    void PassTurn()
    {
        // Switch Players
        if (CurrentPlayerId == 1) {CurrentPlayerId = 2;}else{CurrentPlayerId = 1;}      
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
                                    WinningPlayer.text = "Player " + CurrentPlayerId + " Wins!";
                                    WinMenu.enabled = true;
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
