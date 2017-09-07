using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightRotationScript : MonoBehaviour {

    private Color color;
    private Color color1;

    private void OnMouseDown()
    {
        BoardManager.rotation = 90;
    }


    void Update()
    {
        if (BoardManager.rotation == 90)
        {
            color = new Vector4(0, 1, 0, 1);
            gameObject.GetComponent<SpriteRenderer>().color = color;
        }
        if (BoardManager.rotation != 90 || BoardManager.toInstantiate != null)
        {
            color1 = new Vector4(1, 1, 1, 1);
            gameObject.GetComponent<SpriteRenderer>().color = color1;
        }
    }


}
