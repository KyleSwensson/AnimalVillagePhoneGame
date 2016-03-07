using UnityEngine;
using System.Collections;

public class HouseScript : MonoBehaviour {
    public GameObject NPCInhabitant;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setInhabitant(GameObject npc)
    {
        NPCInhabitant = npc;
    }
}
