using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour, IClickable
{
    ClickHandler Board;
    
    public string TileName;
    public GameObject Mesh;

    public enum TileStates
    {
        Open,
        X,
        O
    }
    
    public TileStates TileState = TileStates.Open;

    // Start is called before the first frame update
    void Start()
    {
        Board = FindObjectOfType<ClickHandler>();
        //print(Board.GetCurrentPlayerID());
    }

    public void Clicked(int playerID)
    {
        FlipTile(playerID);
    }

    private void FlipTile(int playerID)
    {
        if (TileState != TileStates.Open)
        {
            print("Tile Already Selected");
            return;
        }

        Quaternion currentRotation = Mesh.transform.rotation;//Mesh.transform.rotation;
        if (playerID == 1)
        {
            TileState = TileStates.X;
            currentRotation = currentRotation * Quaternion.Euler(-90,0,0);
            Mesh.transform.rotation = Quaternion.Slerp(transform.rotation, currentRotation, 1f);
            //print("X gets the square");
            Board.BoardCheck();
            
        }
        else if (playerID == 2)
        {
            TileState = TileStates.O;
            currentRotation = currentRotation * Quaternion.Euler(90,0,0);
            Mesh.transform.rotation = Quaternion.Slerp(transform.rotation, currentRotation, 1f);
            //print("O gets the square");
            Board.BoardCheck();
        }
    }
}
