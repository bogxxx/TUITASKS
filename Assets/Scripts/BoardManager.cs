using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField]
    public int columns = 4;
    [SerializeField]
    public int rows = 1;

    public GameObject floorTiles;
    public GameObject backgroundTiles;
    public static GameObject toInstantiate = null;
    public static int rotation = 0;
    public static GameObject[,] board;

  void Start()
    {
        board = new GameObject[columns, rows];
        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Instantiate(backgroundTiles, new Vector3(x, y, 5f), Quaternion.Euler(0, 0, 0));
                board[x, y] = floorTiles;
                Instantiate(board[x, y], new Vector3(x, y, 0f), Quaternion.Euler(0,0,0));

                
            }
        }
    }

}
