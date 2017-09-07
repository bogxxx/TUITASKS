using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightRotationScript : MonoBehaviour {

    private void OnMouseDown()
    {
        BoardManager.rotation = 90;
    }
}
