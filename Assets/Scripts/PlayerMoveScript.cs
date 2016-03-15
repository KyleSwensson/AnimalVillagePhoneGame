using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class PlayerMoveScript : MonoBehaviour {

    public static PlayerMoveScript instance = null;
    public bool isInteracting;
    public GameObject connectedGiftBox;
    public GameObject interactingNPC;
    Rigidbody2D rigidBod;
    public int interactState; // int that represents interaction state with an npc, defined as following 
    // 0 = no interaction, 1 = in option phase, 2 = being talked to, 3 = in gift phase
    public List<int> inventory; // a list containing all items in inv, max length = 12, 1 = a stick 2 = a statue
    const int maxInvSize = 12;
	public int menuState; // int that represents the current menu open in game, if 0 no menu is open, if 1 then options menu is open, if 2 then inventory is open

    void Awake()
    {
		menuState = 0;
        inventory = new List<int>();
        isInteracting = false;
        interactingNPC = null;
        connectedGiftBox = null;

        if (instance == null) // if no previous manager
        {
            instance = this;
        }
        else if (instance != this) // if already a manager dont make another
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject); // when a new scene is loaded normally all game objects are destroyed
        ;
    }

    public bool hasInvSpace()
    {
        if (inventory.Count < maxInvSize)
        {
            return true;
        } else
        {
            return false;
        }
    }


    // Use this for initialization
    void Start () {
        rigidBod = gameObject.GetComponent<Rigidbody2D>();
	}

    // Update is called once per frame
    void Update() {
        Touch myTouch;
        if (Input.touchCount > 0)
        {
            myTouch = Input.GetTouch(0);

            Vector2 touchPos = myTouch.position;
            touchPos.x *= ((18.5f / 500f));
            touchPos.x -= 9.25f;
            touchPos.y *= ((13.5f / 310f));
            touchPos.y -= 6.75f;

            if (myTouch.phase != TouchPhase.Ended)
            {
                if (touchPos.x > 0.5)
                {
                    rigidBod.AddForce(new Vector2(50, 0));
                }
                else if (touchPos.x < -0.5)
                {
                    rigidBod.AddForce(new Vector2(-50, 0));
                }
                else
                {
                    rigidBod.velocity = new Vector2(0, 0);
                }

                if (rigidBod.velocity.x > 5)
                {
                    rigidBod.velocity = new Vector2(5, rigidBod.velocity.y);
                }
                else if (rigidBod.velocity.x < -5)
                {
                    rigidBod.velocity = new Vector2(-5, rigidBod.velocity.y);
                }
            }
            else
            {
                rigidBod.velocity = new Vector2(0, 0);
            }
        }

	}
}
