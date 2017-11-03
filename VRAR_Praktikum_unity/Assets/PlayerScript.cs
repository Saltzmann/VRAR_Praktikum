using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class PlayerScript : MonoBehaviour {

    [Header("Ball-Attribute")]
    public float ballGeschwindigkeit;
    public float ballFrequenz;
    
    public GameObject BallPrefab;
 

    public void ThrowBall() {
        Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Debug.Log(myRay.origin);
        //Debug.Log(myRay.direction);

        GameObject newBall = Instantiate(BallPrefab, myRay.origin + 0.25f * myRay.direction, Quaternion.identity);
        if (newBall)
        {
            Rigidbody rb = newBall.GetComponent<Rigidbody>();
            rb.AddForce(myRay.direction * ballGeschwindigkeit);
        }
    }

	// Use this for initialization
	void Start () {
	    ballGeschwindigkeit = 50;
	    ballFrequenz = 2;
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetMouseButtonDown(0)) {
	        ThrowBall();
	    }
	}


}
