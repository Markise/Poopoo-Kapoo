using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public float chickTime;
    public float chickCamSpeed;
    public float plrCamSpeed;
    public Movement1 player;

    private GameObject Chick;
    private GameObject PlayersHead;
    public bool isFollowing;
    private bool watching4Throw;
    private Transform cameraPos;
    

	// Use this for initialization
	void Start () {
       player =  FindObjectOfType<Movement1>();
       isFollowing = true;
       watching4Throw = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        if (player == null)
        {
            player = FindObjectOfType<Movement1>();
        }

        if (isFollowing)
        {
            transform.position = new Vector3( player.transform.position.x,player.transform.position.y,-10f);
        }

        if(player.GetComponent<PickuptwoD>().chickOnHead == true)
        {
           Chick = player.transform.GetChild(2).gameObject;
           PlayersHead = player.transform.GetChild(0).gameObject;
            watching4Throw = true;
        }
        if (watching4Throw)
        {
            if (Chick.transform.position.x != PlayersHead.transform.position.x)
            {
                Vector3 chickPos = new Vector3(Chick.transform.position.x, Chick.transform.position.y,-10f);
                isFollowing = false;
                transform.position = Vector3.MoveTowards(transform.position, chickPos, chickCamSpeed * Time.deltaTime);
                StartCoroutine(FollowChickTime());
            }
        }
	}

    private  IEnumerator FollowChickTime()
    {
        yield return new WaitForSeconds(chickTime);
        Chick = null;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, chickCamSpeed * Time.deltaTime);
        isFollowing = true;
        watching4Throw = false;
    }
}
