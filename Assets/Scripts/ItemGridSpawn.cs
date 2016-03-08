using UnityEngine;
using System.Collections;

public class ItemGridSpawn : MonoBehaviour {
    public GameObject backButton;
    public GameObject contButton;
	// Use this for initialization
	void Start () {
        Vector3 backButtonVector = new Vector3(transform.position.x + .25f, transform.position.y + 2.26f);
        GameObject bInstance = Instantiate(backButton, backButtonVector, Quaternion.identity) as GameObject;
        bInstance.transform.SetParent(transform);

        Vector3 contButtonVector = new Vector3(transform.position.x + 3.25f , transform.position.y + 2.26f);
        GameObject cInstance = Instantiate(contButton, contButtonVector, Quaternion.identity) as GameObject;
        cInstance.transform.SetParent(transform);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
