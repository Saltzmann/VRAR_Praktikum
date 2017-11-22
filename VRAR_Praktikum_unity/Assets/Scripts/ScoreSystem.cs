using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    
    public short CurrentScore;
    public short HighScore;
    public Text CurrentScoreText;
    public Text HighScoreText;
    public Text PlanesLeftText;



    [Header("Game Options")]
    public int MaxNumberOfPlanes;
    private int PlanesLeft;
    private bool noMessagePosted;

    [Header("Notepad")]
    public GameObject NotePad;

    public void AddPoints(short Score)
    {
        CurrentScore += Score;
        Debug.LogWarning("Score! Current Score: " + CurrentScore);
    }

    public void ResetScore()
    {
        Debug.Log("Score was: " + CurrentScore + "\n" + "Score is now reset to 0");
        PlanesLeft = MaxNumberOfPlanes;
        if (CurrentScore > HighScore) {
            Debug.Log("new Highscore");
            HighScore = CurrentScore;
            HighScoreText.text = "Highscore: " + HighScore.ToString();
        }
        CurrentScore = 0;
        noMessagePosted = true;
        GameObject[] Planes = GameObject.FindGameObjectsWithTag("Plane");
        foreach (var Plane in Planes) {
            Destroy(Plane.gameObject);
        }
    }

    void FixedUpdate() { 
        PlanesLeft = MaxNumberOfPlanes - GameObject.FindGameObjectsWithTag("Plane").Length;
        //Debug.LogError(PlanesLeft + "  Planes left to play with");
    }

    public bool GetPlanesLeft()
    {
        bool isPlaneLeft = PlanesLeft > 0;
        if (!isPlaneLeft && noMessagePosted) {
            Debug.LogError("Sorry, no Planes left...to throw ;)");
            noMessagePosted = false;
        }
        return isPlaneLeft;
    }

    private Collider notePadCollider;
    void Start() {
        noMessagePosted = true;
        CurrentScore = 0;
        HighScoreText.text = "No highscore yet";
        HighScore = 0;
        notePadCollider = NotePad.gameObject.GetComponent<BoxCollider>();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction, Color.red, 2000.0f);
            RaycastHit hit;
            if (notePadCollider.Raycast(ray, out hit, 100.0F)) {
                ResetScore();
            }
        }

        //Setup UI
        CurrentScoreText.text = "Score:" + CurrentScore.ToString();
        PlanesLeftText.text = PlanesLeft.ToString();
    }
}
