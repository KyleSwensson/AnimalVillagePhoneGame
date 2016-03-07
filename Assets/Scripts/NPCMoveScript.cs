using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator.

public class NPCMoveScript : MonoBehaviour {
    public GameObject interestBubble;
    public GameObject questionBubble;
    public GameObject happyBubble;
    public GameObject sadBubble;
    public GameObject mediumBubble;
    public GameObject genericNPCHouse;
    public GameObject optionBubble;
    public GameObject textOption;
    public GameObject giftOption;
    bool isTalkingToPlayer; // true when they player presses on this guy
    int moveDuration; // variable to tell how long current movement lasts
    int moveType; // tells what type of move currently on, 0 = standing, 1 = moving left, 2 = moving right
    Rigidbody2D rigidBod;
    float distToPlayer; // distance to player, if this is less than 2 you can talk to them
    bool canTalkToPlayer;
    GameObject thisNPCsHouse;
    float happiness = 0; // happiness is how happy the villager is, this goes from 0 to 30 with 0 being devastated and 30 being really happy
    bool hasNews; // this determines whether or not the npc has something to tell to the player

    // Use this for initialization
    void Start () {
        rigidBod = gameObject.GetComponent<Rigidbody2D>();
        Vector3 housePos = new Vector3(transform.position.x, transform.position.y);
        thisNPCsHouse = Instantiate(genericNPCHouse, housePos, Quaternion.identity) as GameObject;
        happiness = Random.Range(0, 31);
        isTalkingToPlayer = false;

	}

    private void decideNextMove()
    {
        moveDuration = Random.Range(10, 60); // makes random move duration between 30 and 120 frames
        moveType = Random.Range(0, 3);
        if (moveType == 0)
        {
            moveDuration += 15;
        }

    }

    // Update is called once per frame
    void Update () {
        if (!isTalkingToPlayer)
        {
            if (moveDuration <= 0)
            {
                decideNextMove();
            }
            else
            {
                moveDuration--;
                if (moveType == 0)
                {
                    rigidBod.velocity = new Vector2(0, 0);
                }
                else if (moveType == 1)
                {
                    rigidBod.AddForce(new Vector2(40, 0));
                }
                else if (moveType == 2)
                {
                    rigidBod.AddForce(new Vector2(-40, 0));
                }
                else
                {
                    //TODO: throw exception here
                }

                if (rigidBod.velocity.x > 2)
                {
                    rigidBod.velocity = new Vector2(2, rigidBod.velocity.y);
                }
                else if (rigidBod.velocity.x < -2)
                {
                    rigidBod.velocity = new Vector2(-2, rigidBod.velocity.y);
                }
            }
        } else
        {
            rigidBod.velocity = new Vector2(0, 0);
        }


        Vector3 playerPos = PlayerMoveScript.instance.transform.position;
        distToPlayer = Mathf.Abs(transform.position.x - playerPos.x);
        if (distToPlayer < 2)
        {
            if (!canTalkToPlayer)
            {
                GameObject toInstantiate;
                if (hasNews)
                {
                    toInstantiate = interestBubble;
                } else if (happiness > 20)
                {
                    toInstantiate = happyBubble;
                } else if (happiness > 10)
                {
                    toInstantiate = mediumBubble;
                } else
                {
                    toInstantiate = sadBubble;
                }
                Vector3 bubbleVector = new Vector3(transform.position.x, transform.position.y + 3);
                GameObject instanceF = Instantiate(toInstantiate, bubbleVector, Quaternion.identity) as GameObject;
                instanceF.transform.SetParent(transform);
                canTalkToPlayer = true;
            }
            
        
        } else
        {
            if (canTalkToPlayer)
            {
                foreach (Transform child in transform)
                {
                    Destroy(child.gameObject);
                }
                canTalkToPlayer = false;
                isTalkingToPlayer = false;
            }
        }


        int i = 0;
        while (i < Input.touchCount)
        {

            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                //Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), -Vector2.up);

                Debug.Log("TAP !");

                if (hit.collider != null)
                {

                    if (hit.collider.tag == "NPC")
                    {
                        if (canTalkToPlayer && !isTalkingToPlayer)
                        {
                            isTalkingToPlayer = true;
                            foreach (Transform child in transform)
                            {
                                Destroy(child.gameObject);
                            }

                            Vector3 bubbleVector = new Vector3(transform.position.x, transform.position.y + 3);
                            GameObject instance = Instantiate(optionBubble, bubbleVector, Quaternion.identity) as GameObject;
                            instance.transform.SetParent(transform);

                            Vector3 giftChoiceVector = new Vector3(transform.position.x, transform.position.y + 3);
                            GameObject choice1Instance = Instantiate(giftOption, giftChoiceVector, Quaternion.identity) as GameObject;
                            choice1Instance.transform.SetParent(transform);

                            Vector3 textChoiceVector = new Vector3(bubbleVector.x, bubbleVector.y);
                            GameObject choice2Instance = Instantiate(textOption, giftChoiceVector, Quaternion.identity) as GameObject;
                            choice2Instance.transform.SetParent(transform);

                            moveType = 0;
                            moveDuration = 50;


                        }
                    }

                }
            }
            ++i;
        }

    }
}
