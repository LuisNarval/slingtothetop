using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarsController : MonoBehaviour {

	[SerializeField]
	private LineRenderer lineLeft;
	[SerializeField]
	private LineRenderer lineRight;

	public  BallMoveAndShoot setUpExp;
	public Transform gameBall;
	public float offSetLine;
	public float offSetLine2;
	public bool readyToGoUp;


	void Awake(){
		setUpExp = GetComponent<BallMoveAndShoot> ();
	}

	// Use this for initialization
	void Start () {
		gameBall = GameObject.Find ("Ball").GetComponent<Transform> ();
		BigBrotherControler.Bars.Add (this);
	}
	
	// Update is called once per frame
	void Update () {

		lineLeft.SetPosition (0, new Vector3 (lineLeft.transform.position.x + offSetLine, lineLeft.transform.position.y, 1.0f));
		lineRight.SetPosition (0, new Vector3 (lineRight.transform.position.x - offSetLine, lineRight.transform.position.y, 1.0f));

		if (setUpExp.thisIsValid) {
			lineLeft.SetPosition (1, new Vector3 (gameBall.position.x, gameBall.position.y, 1.0f));
			lineRight.SetPosition (1, new Vector3 (gameBall.position.x, gameBall.position.y, 1.0f));
		} else {
			lineLeft.SetPosition (1, new Vector3 (lineLeft.transform.position.x + offSetLine2, lineLeft.transform.position.y, 1.0f));
			lineRight.SetPosition (1, new Vector3 (lineRight.transform.position.x - offSetLine2, lineRight.transform.position.y, 1.0f));
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Finish") {
			readyToGoUp = true;
		}
	}
}
