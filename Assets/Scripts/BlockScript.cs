using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{

    private GameObject instantiateObject;

    void OnMouseDown()
    {
        if (BoardManager.rotation != 0)
        {
            transform.Rotate(new Vector3(0, 0, BoardManager.rotation));
        }
        else
        if (BoardManager.toInstantiate != null)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = BoardManager.toInstantiate.GetComponent<SpriteRenderer>().sprite;
        }
    }
}