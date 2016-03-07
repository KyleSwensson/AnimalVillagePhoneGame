using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    public MapManager mapScript;

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
        // but obv we dont want this, ddol makes it so it stays when new scene is loaded
        mapScript = GetComponent<MapManager>();
        InitGame();
    }

    void InitGame()
    {
        mapScript.SetupScene();
    }


    // Update is called once per frame
    void Update()
    {

    }
}
