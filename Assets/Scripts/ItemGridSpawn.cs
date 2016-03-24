using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemGridSpawn : MonoBehaviour {
    public GameObject backButton;
    public GameObject contButton;
	public GameObject stickIcon;
	public GameObject statueIcon;
	public List<int> invList; // this is passed into the thing by player when it is created so it knows what to display
    public Sprite contButtonActive;
    public Sprite contButtonInactive;
    public int selectedSlot = 0; // this changes to the id of the slot that the thing is in when an item is selected
    public GameObject selectedItem; // this changes to the selected object when obj selected, null otherwise.
    public GameObject thisContButton;
    public GameObject thisBackButton;
	Vector3[] gridPos = new Vector3[10];
	// Use this for initialization
	void Start () {
		gridPos[0] = new Vector3 (transform.position.x + 1.23f, transform.position.y + 2.25f);
		gridPos[1] = new Vector3 (transform.position.x + 2.23f,transform.position.y +  2.25f);
		gridPos[2] = new Vector3 (transform.position.x + .23f,transform.position.y +  1.25f);
		gridPos[3] = new Vector3 (transform.position.x + 1.23f,transform.position.y +  1.25f);
		gridPos[4] = new Vector3 (transform.position.x + 2.23f,transform.position.y +  1.25f);
		gridPos[5] = new Vector3 (transform.position.x + 3.23f,transform.position.y +  1.25f);
		gridPos[6] = new Vector3 (transform.position.x + 0.23f,transform.position.y +  2.25f);
		gridPos[7] = new Vector3 (transform.position.x + 1.23f,transform.position.y +  2.25f);
		gridPos[8] = new Vector3 (transform.position.x + 2.23f,transform.position.y +  2.25f);
		gridPos[9] = new Vector3 (transform.position.x + 3.23f,transform.position.y + 2.25f);
	


		int accum = 0;
		foreach (int i in invList) {
			GameObject itemToInst;
			Vector3 newItemVector = gridPos [accum];
			accum++;
			switch (i) {
			case 1:
				itemToInst = stickIcon;
				break;
			case 2: 
				itemToInst = statueIcon;
				break;
			default: 
				itemToInst = null;
				break;
			}
			GameObject newIcon = Instantiate (itemToInst, newItemVector, Quaternion.identity) as GameObject;
			newIcon.transform.SetParent (transform);


				
		}

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
