using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesScript : MonoBehaviour {

    private void OnMouseDown()
    {
        BoardManager.toInstantiate = gameObject;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
