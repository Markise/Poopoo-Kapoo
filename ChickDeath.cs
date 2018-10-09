using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChickDeath : MonoBehaviour {

  
    public GameObject Markier;
    public PickuptwoD player;
    public float deathTime;

    private GameObject minimap;
    private GameObject minimapRender;

	// Use this for initialization
	void Start () {

        player = FindObjectOfType<PickuptwoD>();
        minimap = player.minimap.gameObject;
        minimapRender = player.minimap.gameObject;
	}

	// Update is called once per frame
	void Update () {
		
        if(player.okToFollow && gameObject == player.ThrowChick)
        {
            if (GetComponent<Rigidbody2D>().velocity.x == 0f && player.chickCamSpawn)
            {
                Debug.Log("destroying");
                Destroy(player.Cam.gameObject);
                Color temp = minimap.GetComponent<Image>().color;
                temp.a = 0;
                minimap.GetComponent<Image>().color = temp;

                Color temp2 = minimapRender.GetComponent<Image>().color;
                temp2.a = 0;
                minimapRender.GetComponent<Image>().color = temp2;
            }
        }

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Destroy(gameObject);

            if (player.okToFollow)
            {
                Destroy(player.Cam.gameObject);
                player.okToFollow = false;

            }
        }
        if(collision.tag == "Water")
        {
            StartCoroutine(DeathWait());
        }
    }
    public IEnumerator DeathWait()
    {
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
        if (player.okToFollow)
        {
            Destroy(player.Cam.gameObject);
            player.okToFollow = false;
        }
    }
}
