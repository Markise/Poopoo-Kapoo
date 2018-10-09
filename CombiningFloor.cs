using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombiningFloor : MonoBehaviour {

    GameObject[] Floors;
    GameObject[] Waters;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        Floors = GameObject.FindGameObjectsWithTag("Tile");
        Waters = GameObject.FindGameObjectsWithTag("Water");

        for (int i = 1; i < Floors.Length; i++)
        {
            Floors[i].transform.SetParent(Floors[0].transform);
        }
        for (int i = 1; i < Waters.Length; i++)
        {
            Waters[i].transform.SetParent(Waters[0].transform);
        }



    }
}
