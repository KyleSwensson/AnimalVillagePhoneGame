using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random; 		//Tells Random to use the Unity Engine random number generator.

public class NPCInfoHolder : MonoBehaviour {
    public string talkText = "";
    public float happiness; // happiness is how happy the villager is, this goes from 0 to 30 with 0 being devastated and 30 being really happy
    public bool hasNews; // this determines whether or not the npc has something to tell to the player
    public bool canTalkToPlayer;
    public bool isTalkingToPlayer;
    public int moveDuration; // variable to tell how long current movement lasts
    public int moveType; // tells what type of move currently on, 0 = standing, 1 = moving left, 2 = moving right

    public float distToPlayer; // distance to player, if this is less than 2 you can talk to them
    Random random;
	// Use this for initialization
	void Start () {
        canTalkToPlayer = false;
        isTalkingToPlayer = false;
        happiness = Random.Range(0, 31);
        if (happiness < 0)
        {
            talkText = "There is a bug in my code!";
        } else if (happiness < 10)
        {
            talkText = "I'm sad!";
        } else if (happiness < 20)
        {
            talkText = "I'm mediocre i guess.";
        } else if (happiness < 30)
        {
            talkText = "I'm happy!";
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
