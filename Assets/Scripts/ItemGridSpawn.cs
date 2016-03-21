﻿using UnityEngine;
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
	Vector3[] gridPos = new Vector3[12];
	// Use this for initialization
	void Start () {
		gridPos[0] = new Vector3 (0, 0);
		gridPos[1] = new Vector3 (1, 0);
		gridPos[2] = new Vector3 (2, 0);
		gridPos[3] = new Vector3 (0, 1);
		gridPos[4] = new Vector3 (1, 1);
		gridPos[5] = new Vector3 (2, 1);
		gridPos[6] = new Vector3 (3, 1);
		gridPos[7] = new Vector3 (4, 1);
		gridPos[8] = new Vector3 (0, 2);
		gridPos[9] = new Vector3 (1,2);
		gridPos[10] = new Vector3 (2, 2);
		gridPos[11] = new Vector3 (3, 2);


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
