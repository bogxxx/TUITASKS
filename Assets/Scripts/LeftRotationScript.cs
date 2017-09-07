using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRotationScript : MonoBehaviour {

    private void OnMouseDown()
    {
        BoardManager.rotation = -90;
        BoardManager.toInstantiate = null;
    }
}
