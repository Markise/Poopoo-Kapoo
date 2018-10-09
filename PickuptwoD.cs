using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PickuptwoD: MonoBehaviour
{
    public GameObject ThrowChick;
    public GameObject Cam;
    public GameObject minimap;
    public GameObject minimapRender;
    public PhysicsMaterial2D ball;
    public Transform head;
    public GameObject pwrMeter;
    public GameObject ChickCam;
    public float velocitydiff;
    public float throwDrag;
    public float powerCap;
    public float throwPower;
    public float throwForce;
    public float pushBack;
    public float chickFollowTime;
    public bool chickOnHead;
    public bool throwing;
    public bool knocked;
    public bool okToFollow;
    public bool chickCamSpawn;

    private int actionPress;
    private float holdThrow;
    private bool followingChick;
    private Vector3 camPos;
    private Canvas canvas;
    private GameObject chick;
    private GameObject Meter;

    // Use this for initialization
    void Start()
    {
        chickOnHead = false;
        actionPress = 0;

        canvas = FindObjectOfType<Canvas>();
        minimap = canvas.transform.GetChild(0).gameObject;
        minimapRender = canvas.transform.GetChild(1).gameObject;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K) || Input.GetKeyUp(KeyCode.K))
        {
            actionPress++;
            if(actionPress == 4)
            {
                actionPress = 0;
            }
        }

        if (okToFollow == false)
        {
            Color temp = minimap.GetComponent<Image>().color;
            temp.a = 0;
            minimap.GetComponent<Image>().color = temp;

            Color temp2 = minimapRender.GetComponent<Image>().color;
            temp2.a = 0;
            minimapRender.GetComponent<Image>().color = temp2;
        }
    }

    void FixedUpdate()
    {

        if (chickOnHead == true)
        {

            if (actionPress == 2)
            {
                if (Input.GetKeyDown(KeyCode.K))
                {

                    holdThrow = Time.time;
                    Meter = Instantiate(pwrMeter, head.position, transform.rotation);
                    Meter.transform.SetParent(head);
                }
            }
            if(actionPress ==3)
            { 
                if (Input.GetKeyUp(KeyCode.K))
                {
                    Destroy(Meter);
                    throwPower = 0f;
                    throwForce = 2.5f * (Time.time - holdThrow);
                    
                    if (throwForce <= .5f)
                    {
                        throwForce = .5f;
                    }
                    else if (throwForce >= powerCap)
                    {
                        throwForce = powerCap;
                    }

                    throwPower = throwForce * 10f;

                    Vector3 throwDirection = new Vector3(1f, throwForce / 5f, 0f);
                    if (transform.localScale.x == -1f)
                    {
                        throwDirection.x = -1f;
                    }

                    ThrowChick = chick.gameObject;
                    ThrowChick.transform.parent = null;
                    ThrowChick.AddComponent<Rigidbody2D>();
                    ThrowChick.GetComponent<Rigidbody2D>().sharedMaterial = ball;
                    ThrowChick.GetComponent<Rigidbody2D>().drag = throwDrag;

                    if (GetComponent<Rigidbody2D>().velocity.x > 0f)
                    {
                        throwPower += velocitydiff;
                    }
                    if(GetComponent<Rigidbody2D>().velocity.x < 0f)
                    {
                        throwPower += velocitydiff;
                    }
                    ThrowChick.GetComponent<Rigidbody2D>().AddForce(throwDirection * throwPower, ForceMode2D.Impulse);
                    ThrowChick.GetComponent<CircleCollider2D>().enabled = true;
                    chickOnHead = false;
                    okToFollow = true;
                    actionPress = 0;

                }
              
            }
        }

        if (okToFollow)
        {
            if (!ThrowChick.GetComponent<SpriteRenderer>().isVisible)
            {
                camPos = new Vector3(ThrowChick.transform.position.x, ThrowChick.transform.position.y, -10f);
                if (chickCamSpawn == false)
                {
                    Cam = Instantiate(ChickCam, camPos, transform.rotation);
                    Color temp = minimap.GetComponent<Image>().color;
                    temp.a = 1;
                    minimap.GetComponent<Image>().color = temp;

                    Color temp2 = minimapRender.GetComponent<Image>().color;
                    temp2.a = 1;
                    minimapRender.GetComponent<Image>().color = temp2;

                    chickCamSpawn = true;
                    followingChick = true;
                }
                    StartCoroutine(Camfollow());                              
            }
            if (followingChick)
            {
                RectTransform uiCam = minimapRender.GetComponent<RectTransform>();
                RectTransform uiCam2 = minimap.GetComponent<RectTransform>();
                RectTransform CanvasRect = canvas.GetComponent<RectTransform>();

                Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, ThrowChick.transform.position);
                screenPoint.x = Mathf.Clamp(screenPoint.x, 100f, 1472f);
                screenPoint.y = Mathf.Clamp(screenPoint.y, 100f, 782f);

                uiCam.anchoredPosition = screenPoint - CanvasRect.sizeDelta;
                uiCam2.anchoredPosition = screenPoint - CanvasRect.sizeDelta;;

                camPos = new Vector3(ThrowChick.transform.position.x, ThrowChick.transform.position.y, -10f);
                Cam.transform.position = camPos;
            }
        }
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "bird")
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), other.GetComponent<CircleCollider2D>());
        }

        if(other.tag == "Enemy" || other.tag == "Water")
        {
            if (chickOnHead == false)
            {
                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), other.GetComponent<CapsuleCollider2D>());
            }
            else
            {
                actionPress = 0;
                knocked = true;
                Destroy(Meter);
                chick.transform.parent = null;
                chick.AddComponent<Rigidbody2D>();
                chick.GetComponent<Rigidbody2D>().sharedMaterial = ball;
                chick.GetComponent<Rigidbody2D>().drag = throwDrag;
                chick.GetComponent<Rigidbody2D>().AddForce(transform.right*pushBack, ForceMode2D.Impulse);
                chick.GetComponent<CircleCollider2D>().enabled = true;
                chickOnHead = false;
                knocked = false;
            }
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "bird")
        {
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), other.GetComponent<CircleCollider2D>());

            if (chickOnHead == false)
            {
                if (Input.GetKeyDown(KeyCode.K))
                {
                    chick = other.gameObject;
                    Destroy(chick.GetComponent<Rigidbody2D>());
                    chick.GetComponent<CircleCollider2D>().enabled = false;
                    chick.GetComponent<CircleCollider2D>().enabled = false;
                    chick.transform.position = head.transform.position;
                    chick.transform.SetParent(transform);
                    chickOnHead = true;
                    actionPress = 0;
                }
                
            }
        }

        if(other.tag == "Enemy" || other.tag == "Water")
        {
            if (chickOnHead == false)
            {
                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), other.GetComponent<CapsuleCollider2D>());
            }
            else
            {
                actionPress = 0;
                knocked = true;
                Destroy(Meter);
                chick.transform.parent = null;
                chick.AddComponent<Rigidbody2D>();
                chick.GetComponent<Rigidbody2D>().sharedMaterial = ball;
                chick.GetComponent<Rigidbody2D>().drag = throwDrag;
                chick.GetComponent<Rigidbody2D>().AddForce( transform.right * pushBack, ForceMode2D.Impulse);
                chick.GetComponent<CircleCollider2D>().enabled = true;
                chickOnHead = false;
                knocked = false;
            }
        }
    }

    public IEnumerator Camfollow()
    {
        yield return new WaitForSeconds(chickFollowTime);
        Destroy(Cam);
        okToFollow = false;
        chickCamSpawn = false;
        followingChick = false;
        Color temp = minimap.GetComponent<Image>().color;
        temp.a = 0;
        minimap.GetComponent<Image>().color = temp;

        Color temp2 = minimapRender.GetComponent<Image>().color;
        temp2.a = 0;
        minimapRender.GetComponent<Image>().color = temp2;

    }
}
