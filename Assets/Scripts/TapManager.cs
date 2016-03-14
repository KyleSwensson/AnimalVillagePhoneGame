using UnityEngine;
using System.Collections;

public class TapManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        int i = 0;
        while (i < Input.touchCount)
        {

            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                //A THING WAS TAPPED!

                //Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), -Vector2.up);


                if (hit.collider != null)
                {

                    if (hit.collider.tag == "NPC")
                    {

                        NPCMoveScript attachedNPC = hit.collider.transform.GetComponent<NPCMoveScript>();
                        NPCInfoHolder attachedNPCInfo = hit.collider.transform.GetComponent<NPCInfoHolder>();

                        if (attachedNPCInfo.canTalkToPlayer && !attachedNPCInfo.isTalkingToPlayer)
                        {
                            attachedNPCInfo.isTalkingToPlayer = true;
                            foreach (Transform child in transform)
                            {
                                Destroy(child.gameObject);
                            }

                            Vector3 bubbleVector = new Vector3(attachedNPC.transform.position.x, attachedNPC.transform.position.y + 3);
                            GameObject instance = Instantiate(attachedNPC.optionBubble, bubbleVector, Quaternion.identity) as GameObject;
                            instance.transform.SetParent(attachedNPC.transform);

                            Vector3 giftChoiceVector = new Vector3(attachedNPC.transform.position.x + 1.15f, attachedNPC.transform.position.y + 3.3f);
                            GameObject choice1Instance = Instantiate(attachedNPC.giftOption, giftChoiceVector, Quaternion.identity) as GameObject;
                            choice1Instance.transform.SetParent(attachedNPC.transform);

                            Vector3 textChoiceVector = new Vector3(attachedNPC.transform.position.x + 0.42f, attachedNPC.transform.position.y + 3.3f);
                            GameObject choice2Instance = Instantiate(attachedNPC.textOption, textChoiceVector, Quaternion.identity) as GameObject;
                            choice2Instance.transform.SetParent(attachedNPC.transform);

                            attachedNPCInfo.moveType = 0;
                            attachedNPCInfo.moveDuration = 50;
                            PlayerMoveScript.instance.interactState = 1;


                        }
                    }
                    else if (hit.collider.tag == "TextChoice")
                    {
                        NPCMoveScript attachedNPC = hit.collider.transform.parent.GetComponent<NPCMoveScript>();
                        NPCInfoHolder attachedNPCInfo = hit.collider.transform.parent.GetComponent<NPCInfoHolder>();

                        PlayerMoveScript.instance.interactState = 2;


                    }
                    else if (hit.collider.tag == "GiftChoice")
                    {

                        Debug.Log("HIT A THING");
                        NPCMoveScript attachedNPC = hit.collider.transform.parent.GetComponent<NPCMoveScript>();
                        NPCInfoHolder attachedNPCInfo = hit.collider.transform.parent.GetComponent<NPCInfoHolder>();

                        Vector3 itemBoxVector = new Vector3(attachedNPC.transform.position.x - 2f, attachedNPC.transform.position.y + 2.5f);
                        GameObject Instance = Instantiate(attachedNPC.itemGrid, itemBoxVector, Quaternion.identity) as GameObject;
                        Instance.transform.SetParent(attachedNPC.transform);
                        PlayerMoveScript.instance.connectedGiftBox = Instance;
                        PlayerMoveScript.instance.interactState = 3; // player in gift state now
                    }

                    else if (hit.collider.tag == "BackButton")
                    {
                        NPCMoveScript attachedNPC = hit.collider.transform.parent.GetComponent<NPCMoveScript>();
                        NPCInfoHolder attachedNPCInfo = hit.collider.transform.parent.GetComponent<NPCInfoHolder>();
                        if (PlayerMoveScript.instance.interactState == 3)
                        {
                            // delete the gift box thing without doing anything
                            Destroy(PlayerMoveScript.instance.connectedGiftBox);
                        }

                        PlayerMoveScript.instance.interactState = 0; // gos back to no interaction state
                        
                        
                    } else if (hit.collider.tag == "ContinueButton")
                    {
                        if (PlayerMoveScript.instance.interactState == 3)
                        {
                            // TODO: give NPC the selected item
                        }
                    } else if (hit.collider.tag == "PickupItem")
                    {
                        Debug.Log("hit pickup item");
                        Debug.Log(hit.collider.gameObject.name);
                        if (hit.collider.gameObject.name == "Stick(Clone)")
                        {

                            if (PlayerMoveScript.instance.hasInvSpace())
                            {
                                PlayerMoveScript.instance.inventory.Add(1);
                                Destroy(hit.collider.gameObject);
                            }
                        } else if (hit.collider.gameObject.name == "Statue(Clone)")
                        {

                            if (PlayerMoveScript.instance.hasInvSpace())
                            {
                                PlayerMoveScript.instance.inventory.Add(2);
                                Destroy(hit.collider.gameObject);
                            }
                        }



                    }

                }
            }
            ++i;
        }
    }
}
