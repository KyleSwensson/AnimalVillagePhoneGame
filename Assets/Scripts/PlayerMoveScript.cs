using UnityEngine;
using System.Collections;

public class PlayerMoveScript : MonoBehaviour {

    public static PlayerMoveScript instance = null;

    Rigidbody2D rigidBod;

    void Awake()
    {
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
