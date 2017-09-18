using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public struct UndoRedo
{
    public static int[] xPos = new int[1000];
    public static int[] yPos = new int[1000];
    public static int[] spriteNumber = new int[1000];
    public static int[] newSpriteNumber = new int[1000];
    public static float[] quaternionW = new float[1000];
    public static float[] quaternionX = new float[1000];
    public static float[] quaternionY = new float[1000];
    public static float[] quaternionZ = new float[1000];
    public static float[] newQuaternionW = new float[1000];
    public static float[] newQuaternionX = new float[1000];
    public static float[] newQuaternionY = new float[1000];
    public static float[] newQuaternionZ = new float[1000];
}

public class BlockScript : MonoBehaviour
{

    private GameObject instantiateObject;
    private Sprite tempSprite;
    private Vector3 dragPosition;
    public Camera _camera;
    public Sprite[] arraySprites;
    public static int undoI = -1;


    public int getIntOfImage(Sprite sprite1)
    {

        for (int i = 0; i < arraySprites.Length; i++)
        {
            if (arraySprites[i] == sprite1)
            {
                return i;
            }
        }
        return -1;
    }


    private void SaveData(int i)
    {
        UndoRedo.xPos[i] = (int)gameObject.transform.position.x;
        UndoRedo.yPos[i] = (int)gameObject.transform.position.y;
        UndoRedo.spriteNumber[i] = getIntOfImage(gameObject.GetComponent<SpriteRenderer>().sprite);
        UndoRedo.quaternionW[i] = gameObject.transform.rotation.w;
        UndoRedo.quaternionX[i] = gameObject.transform.rotation.x;
        UndoRedo.quaternionY[i] = gameObject.transform.rotation.y;
        UndoRedo.quaternionZ[i] = gameObject.transform.rotation.z;
    }


    private void SaveNewData(int i)
    {
        UndoRedo.newSpriteNumber[i] = getIntOfImage(gameObject.GetComponent<SpriteRenderer>().sprite);
        UndoRedo.newQuaternionW[i] = gameObject.transform.rotation.w;
        UndoRedo.newQuaternionX[i] = gameObject.transform.rotation.x;
        UndoRedo.newQuaternionY[i] = gameObject.transform.rotation.y;
        UndoRedo.newQuaternionZ[i] = gameObject.transform.rotation.z;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            undoI++;
            SaveData(undoI);
            RaycastHit2D hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            hit.transform.GetComponent<SpriteRenderer>().sprite = null;
            SaveNewData(undoI);
        }
    }

    void OnMouseDown()
    {
        if (BoardManager.rotation != 0)
        {
            undoI++;
            SaveData(undoI);
            transform.Rotate(new Vector3(0, 0, BoardManager.rotation));
            SaveNewData(undoI);
        }
        else
        if (BoardManager.toInstantiate != null)
            {
            undoI++;
            SaveData(undoI);
            gameObject.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 0));
            gameObject.GetComponent<SpriteRenderer>().sprite = BoardManager.toInstantiate.GetComponent<SpriteRenderer>().sprite;
            SaveNewData(undoI);
        }
    }



    private void OnMouseUp()
    {
        RaycastHit2D hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.transform != null)
        {
            
            tempSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            gameObject.GetComponent<SpriteRenderer>().sprite = null;
            hit.transform.GetComponent<SpriteRenderer>().sprite = tempSprite;
            hit.transform.SetPositionAndRotation(hit.transform.position, gameObject.transform.rotation);

        }
    }
}