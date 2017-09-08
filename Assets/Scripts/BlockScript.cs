using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour
{

    private GameObject instantiateObject;
    public Sprite defaultSprite;
    private Sprite tempSprite;
    private Vector3 dragPosition;
    public Camera _camera;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            hit.transform.GetComponent<SpriteRenderer>().sprite = defaultSprite;
        }
    }

    void OnMouseDown()
    {

                if (BoardManager.rotation != 0)
                {
                    transform.Rotate(new Vector3(0, 0, BoardManager.rotation));
                }
                else
                if (BoardManager.toInstantiate != null)
                {
                    gameObject.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, 0));
                    gameObject.GetComponent<SpriteRenderer>().sprite = BoardManager.toInstantiate.GetComponent<SpriteRenderer>().sprite;
                }
    }



    private void OnMouseUp()
    {
        RaycastHit2D hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.transform != null)
        {
            
            tempSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            gameObject.GetComponent<SpriteRenderer>().sprite = defaultSprite;
            hit.transform.GetComponent<SpriteRenderer>().sprite = tempSprite;
            hit.transform.SetPositionAndRotation(hit.transform.position, gameObject.transform.rotation);

        }
    }
}