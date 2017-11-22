using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class PlayerScript : MonoBehaviour {

    [Header("Plane-Attribute")]
    public float PlaneGeschwindigkeit;
    public float PlaneFrequenz;
    public float wurfStaerkenMultiplikator;
    private bool werfend;

    private float startZeit;
    private short updateCount;
    
    public GameObject PlanePrefab;
    [Header("ScoreSystemLink")]
    public GameObject MyScoreSystem;
    private ScoreSystem scoreSystem;
 

    private void ThrowPlaneContinously() {
        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        GameObject newPlane = Instantiate(PlanePrefab, myRay.origin + 0.25f * myRay.direction, Quaternion.Euler(0, gameObject.transform.eulerAngles.y, 0));
        if (newPlane) {
            Rigidbody rb = newPlane.GetComponent<Rigidbody>();
            rb.AddForce(myRay.direction * PlaneGeschwindigkeit);
        }
    }

    private void ThrowPlaneIncreasingStrength()
    {
        werfend = false;
        float zeitdiff = Time.time - startZeit;
        //Debug.Log("Zeitdifferenz: " + zeitdiff);
        float wurfstaerke = zeitdiff * wurfStaerkenMultiplikator;
        //Debug.Log("Wurfstärke: " + wurfstaerke);
        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        GameObject newPlane = Instantiate(PlanePrefab, myRay.origin + 0.25f * myRay.direction, Quaternion.Euler(0, gameObject.transform.eulerAngles.y, 0));
        if (newPlane) {
            Rigidbody rb = newPlane.GetComponent<Rigidbody>();
            rb.AddForce(myRay.direction * wurfstaerke);
        }
    }

	// Use this for initialization
	void Start () {
	    scoreSystem = MyScoreSystem.GetComponent<ScoreSystem>();
	    //PlaneGeschwindigkeit = 50;
	    //PlaneFrequenz = 2;
	    wurfStaerkenMultiplikator = PlaneGeschwindigkeit * 1;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	    if (scoreSystem.GetPlanesLeft()) {
	        if (Input.GetMouseButton(1) && (updateCount % (int) (Mathf.Round(50.0f / PlaneFrequenz)) == 0)) {
	            ThrowPlaneContinously();
	        }
	        else if (!werfend && Input.GetMouseButtonDown(0)) {
	            werfend = true;
	            startZeit = Time.time;
	        }
	        else if (werfend && Input.GetMouseButtonUp(0)) {
	            ThrowPlaneIncreasingStrength();
	        }
	        updateCount++;
	    }
	}
}
 