using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOrga : MonoBehaviour {
    public ScoreSystem scSys;
    private bool endOfGame;

    public void LeaveGame() {
        Debug.Log("Spiel wird beendet");
        if (Application.isEditor)
        {
            EditorApplication.isPlaying = false;
        }
        else {
            Application.Quit();
        }
    }

    void OnTriggerStay(Collider col) {
        if (/*endOfGame && */col.tag == "Player") {
            LeaveGame();
        }
    }

    // Use this for initialization
	void Start () {
	    endOfGame = false;
	}
	
	// Update is called once per frame
	void Update () {
	    endOfGame = !scSys.GetPlanesLeft();
	}
}