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
    public GameObject itemGrid;
    public GameObject backButton;
    public GameObject continueButton;
    
    Rigidbody2D rigidBod;
    float distToPlayer; // distance to player, if this is less than 2 you can talk to them
    GameObject thisNPCsHouse;
    
    NPCInfoHolder npcInfo;

    // Use this for initialization
    void Start () {
        rigidBod = gameObject.GetComponent<Rigidbody2D>();
        Vector3 housePos = new Vector3(transform.position.x, transform.position.y);
        thisNPCsHouse = Instantiate(genericNPCHouse, housePos, Quaternion.identity) as GameObject;
        npcInfo = gameObject.GetComponent<NPCInfoHolder>();
        npcInfo.isTalkingToPlayer = false;

	}

    private void decideNextMove()
    {
        npcInfo.moveDuration = Random.Range(10, 60); // makes random move duration between 30 and 120 frames
        npcInfo.moveType = Random.Range(0, 3);
        if (npcInfo.moveType == 0)
        {
            npcInfo.moveDuration += 15;
        }

    }

    // Update is called once per frame
    void Update () {
        if (!npcInfo.isTalkingToPlayer)
        {
            if (npcInfo.moveDuration <= 0)
            {
                decideNextMove();
            }
            else
            {
                npcInfo.moveDuration--;
                if (npcInfo.moveType == 0)
                {
                    rigidBod.velocity = new Vector2(0, 0);
                }
                else if (npcInfo.moveType == 1)
                {
                    rigidBod.AddForce(new Vector2(40, 0));
                }
                else if (npcInfo.moveType == 2)
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
            if (!npcInfo.canTalkToPlayer)
            {

                GameObject toInstantiate;
                if (npcInfo.hasNews)
                {
                    toInstantiate = interestBubble;
                }
                else if (npcInfo.happiness > 20)
                {
                    toInstantiate = happyBubble;
                }
                else if (npcInfo.happiness > 10)
                {
                    toInstantiate = mediumBubble;
                }
                else
                {
                    toInstantiate = sadBubble;
                }
                Vector3 bubbleVector = new Vector3(transform.position.x, transform.position.y + 3);
                GameObject instanceF = Instantiate(toInstantiate, bubbleVector, Quaternion.identity) as GameObject;
                instanceF.transform.SetParent(transform);
                npcInfo.canTalkToPlayer = true;
            }


        }
        else
        {
            if (npcInfo.canTalkToPlayer)
            {
                foreach (Transform child in transform)
                {
                    Destroy(child.gameObject);
                }
                npcInfo.canTalkToPlayer = false;
                npcInfo.isTalkingToPlayer = false;
            }
        }

    }
}
