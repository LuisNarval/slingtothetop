using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMoveAndShoot : MonoBehaviour {

	public float maxStrech = 1.0f;
	public Rigidbody2D rg2d;
	public bool thisIsValid;
	public float force;

	private Ray rayToMouse;
	private BoxCollider2D boxCol;
	private float maxStrechSqr;
	private bool validTouch;

	void Awake (){
		maxStrechSqr = maxStrech * maxStrech;
		boxCol = GetComponent<BoxCollider2D> ();
	}

	void Start(){
		if (!thisIsValid) transform.position = new Vector3 (Random.Range(-2.0f, 2.0f), transform.position.y, 0.0f);
		rayToMouse = new Ray (this.transform.position, Vector3.zero);
	}

	void Update(){
	
		// TOUCH CONTROLS
		///*
		if (Input.touchCount > 0 && thisIsValid) {
			
			Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector2 catapultToMouse = mouseWorldPoint - this.transform.position;

			if (catapultToMouse.sqrMagnitude < maxStrechSqr) {
				validTouch = true;
			}

			if (validTouch) {
				if (Input.GetTouch (0).phase == TouchPhase.Moved || Input.GetTouch (0).phase == TouchPhase.Stationary) {
					rg2d.velocity = Vector2.zero;
					Dragging ();
				} 

				if (Input.GetTouch(0).phase == TouchPhase.Ended) {
					Launch ();
				}
			}
				
		}
		//*/
		// MOUSE CONTROLS
		/*
		if (thisIsValid) {
			Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector2 catapultToMouse = mouseWorldPoint - this.transform.position;

			if (catapultToMouse.sqrMagnitude < maxStrechSqr) {
				validTouch = true;
			}

			if (validTouch) {
				if (Input.GetMouseButton (0)) {
					rg2d.velocity = Vector2.zero;
					spring.enabled = false;
					Dragging ();
				} 

				if (Input.GetMouseButtonUp (0)) {
					spring.enabled = true;
					rg2d.isKinematic = false;
					validTouch = false;
					thisIsValid = false;	
				}
			}
		}
		*/

	}

	public void setUp(){
		rayToMouse = new Ray (this.transform.position, Vector3.zero);
		boxCol.enabled = true;
	}

	void Dragging(){

		Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Vector2 catapultToMouse = mouseWorldPoint - this.transform.position;

		rayToMouse.direction = catapultToMouse;

		if (catapultToMouse.sqrMagnitude > maxStrechSqr) {
			mouseWorldPoint = rayToMouse.GetPoint (maxStrech);
		}
		mouseWorldPoint.z = 0.0f;

		rg2d.transform.position = mouseWorldPoint;
	}

	void Launch(){
		Vector2 catapultToMouse = Camera.main.ScreenToWorldPoint (Input.mousePosition) - this.transform.position;

		rg2d.isKinematic = false;
		validTouch = false;
		thisIsValid = false;

		catapultToMouse = catapultToMouse.sqrMagnitude > maxStrechSqr ? -1 * catapultToMouse.normalized : -1 * catapultToMouse;

		rg2d.AddForce (catapultToMouse * force);

	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Ball") {
			boxCol.enabled = false;
			thisIsValid = true;
			rg2d.velocity = Vector2.zero;
			rg2d.isKinematic = true;
		}
	}

}
