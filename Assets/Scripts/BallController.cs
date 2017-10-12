using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

	public int score;
	public bool isFalling;
	public bool finish;
	private Rigidbody2D rbBall;

	// Use this for initialization
	void Awake(){
		rbBall = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate(){

		isFalling = rbBall.velocity.y < -0.1 ? true : false;

	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Bar") {
			score += 1;
		}
		if (col.tag == "Finish") {
			finish = true;
		}
	}

}
