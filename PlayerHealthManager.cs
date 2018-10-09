using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour {


    public int health;
    public Text text;
    public PickuptwoD player;

    private bool knock;
   
    // Use this for initialization
	void Start () {
        player = FindObjectOfType<PickuptwoD>();
        knock = player.knocked;
        health = 3;
        
	}
	
	// Update is called once per frame
	void Update () {
       text.text = "Health: " + health;

       knock = GetComponent<PickuptwoD>().knocked;

       if(knock)
        {
            health--;
        }
       if(health == 0)
        {
            health = 0;
            GetComponent<LevelManager>().LoadLevel("KapooStartScreen");
        }
	}
}
