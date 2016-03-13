using UnityEngine;
using System.Collections;

public class ItemGridSpawn : MonoBehaviour {
    public GameObject backButton;
    public GameObject contButton;
    public Sprite contButtonActive;
    public Sprite contButtonInactive;
    public int selectedSlot = 0; // this changes to the id of the slot that the thing is in when an item is selected
    public GameObject selectedItem; // this changes to the selected object when obj selected, null otherwise.
    public GameObject thisContButton;
    public GameObject thisBackButton;
    
	// Use this for initialization
	void Start () {
        selectedItem = null;
        selectedSlot = 0;
        Vector3 backButtonVector = new Vector3(transform.position.x + .25f, transform.position.y + 2.26f);
        thisBackButton = Instantiate(backButton, backButtonVector, Quaternion.identity) as GameObject;
        thisBackButton.transform.SetParent(transform);

        Vector3 contButtonVector = new Vector3(transform.position.x + 3.25f , transform.position.y + 2.26f);
        thisContButton = Instantiate(contButton, contButtonVector, Quaternion.identity) as GameObject;
        thisContButton.transform.SetParent(transform);
    }
	
	// Update is called once per frame
	void Update () {
	    if (selectedSlot == 0)
        {
            thisContButton.GetComponent<SpriteRenderer>().sprite = contButtonInactive;
        } else
        {
            thisContButton.GetComponent<SpriteRenderer>().sprite = contButtonActive;
        }
	}
}
