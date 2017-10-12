using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineHelper : MonoBehaviour {

	private BallController ball;
	private LineRenderer helper;

	// Use this for initialization
	void Start () {
		ball = GameObject.Find ("Ball").GetComponent<BallController> ();;
		helper = ball.GetComponent<LineRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
			Helper ();
		}else {
			helper.SetPosition (0, ball.transform.position);
			helper.SetPosition (1, ball.transform.position);
		}

		if (ball.score == 5)
			Destroy (this);
	}

	void Helper(){
		Vector2 catapultToMouse = Camera.main.ScreenToWorldPoint (Input.mousePosition) - this.transform.position;

		catapultToMouse = catapultToMouse.sqrMagnitude > 1 ? -1 * catapultToMouse.normalized : -1 * catapultToMouse;

		helper.SetPosition (0, ball.transform.position);
		helper.SetPosition (1, (catapultToMouse * 750));

	}

}
