using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KapooCount : MonoBehaviour {

    GameObject[] Kapoos;

    private int startingKapoos;
	// Use this for initialization
	void Start () {

        
    }
	
	// Update is called once per frame
	void Update () {

        Kapoos = GameObject.FindGameObjectsWithTag("bird");
        startingKapoos = Kapoos.Length;
        Debug.Log(Kapoos.Length);
        if (Kapoos.Length <= 0)
        {
            GetComponent<LevelManager>().LoadLevel("KapooStartScreen");
        }
	}
}
