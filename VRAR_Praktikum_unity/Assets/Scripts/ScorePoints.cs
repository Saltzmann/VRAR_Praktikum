using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePoints : MonoBehaviour
{
    private ScoreSystem scoreSys;

    [Header("Config")]
    public short Score;

    void Start() {
        scoreSys = gameObject.GetComponentInParent<ScoreSystem>();
        if (scoreSys) {
            //Debug.Log("Score System connected");
        }
    }

    void OnTriggerEnter(Collider col) {
        if (col.tag == "Plane") {
            scoreSys.AddPoints(this.Score);
        }
    }
}
