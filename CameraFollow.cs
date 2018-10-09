using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Movement1 player;
    public LevelEditor levelEditor;

    private float boundX;
    private float boundY;
    private Vector3 bounds;
    private bool isFollowing;

    // Use this for initialization
    void Start()
    {
        levelEditor = FindObjectOfType<LevelEditor>();
        player = FindObjectOfType<Movement1>();
        isFollowing = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.SetParent(levelEditor.transform);

        bounds = levelEditor.GetComponent<CompositeCollider2D>().bounds.size;

        Debug.Log(bounds);

        if (player == null)
        {
            player = FindObjectOfType<Movement1>();
        }

        if (isFollowing)
        {
            boundX = Mathf.Clamp(player.transform.position.x, 6, bounds.x);
            boundY = Mathf.Clamp(player.transform.position.y, 3, bounds.y);

            transform.position = new Vector3(boundX, boundY, -10f);
        }
    }
}