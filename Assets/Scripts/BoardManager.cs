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
    public static GameObject toInstantiate;
    public static int rotation = 0;
    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    void InitialiseList()
    {
        gridPositions.Clear();
        for (int x = -7; x < columns; x++)
        {
            for (int y = -2; y < rows; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0.0f));
            }
        }

    }

  void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;
        for (int x = -10; x < columns; x++)
        {
            for (int y = -2; y < rows; y++)
            {

                GameObject inctance = Instantiate(floorTiles, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                inctance.transform.SetParent(boardHolder);
            }
        }
    }


    public void SetupScene()
    {
        BoardSetup();
        InitialiseList();
    }
}
