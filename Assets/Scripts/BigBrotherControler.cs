using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BigBrotherControler : MonoBehaviour {

	public static List<BarsController> Bars = new List<BarsController> ();

	private Rigidbody2D mainCamera;
	private BarsController nextBar;
	private BallController ballContr;
	private AudioSource nextLevel;
	private int highscore;
	private int scoretargget;
	private int shopPoints;

	public float speedSizeTex;
	public float speedMoveTex;
	public Text scoreTex;
	public Text highscoreTex;
	public Text shopPointsTex;
	public Rigidbody2D instructions;
	public Rigidbody2D shop;
	public float nextPosition;
	public float speed;

	//PlayerPrefs.Save();
	// Use this for initialization
	void Start () {
		scoretargget = 3;
		mainCamera = Camera.main.GetComponent<Rigidbody2D>();
		nextLevel = Camera.main.GetComponent<AudioSource> ();
		
		ballContr = GameObject.FindGameObjectWithTag ("Ball").GetComponent<BallController> ();

		highscore = PlayerPrefs.GetInt ("highscore", highscore);
		shopPoints = PlayerPrefs.GetInt ("shoppoints", shopPoints);

		scoreTex.text = highscore.ToString();
		shopPointsTex.text = "$ " + shopPoints.ToString ();
	}

	void Update(){

		if (ballContr.score == scoretargget) {
			//NEW SPEED 
			speed += 0.2f;
			scoretargget += 3;
			//PLUS ONE ON SHOP MONEY
			PlayerPrefs.SetInt ("shoppoints", ++shopPoints);
			shopPointsTex.text = "$ " + shopPoints.ToString ();
			//SPEED LIMIT
			speed = speed > 3.0f ? 3.0f : speed; 
			nextLevel.Play ();
		}

		nextBar = Bars.FirstOrDefault(bar => 
			{
				try
				{
					return bar.readyToGoUp == true;
				}
				catch
				{
					return false;
				}
			});

		if (nextBar != null) {
			TeleportBar (nextBar);
			nextPosition += Random.Range(3.0f, 5.0f);
		}
	}

	void FixedUpdate(){
		if (ballContr.score > 0) {
			highscoreTex.enabled = false;
			scoreTex.text = "" + ballContr.score;
			mainCamera.velocity = Vector2.up * speed;
			reSizeText ();
		}

		if (ballContr.score > highscore) PlayerPrefs.SetInt ("highscore", ballContr.score); 

		if (ballContr.isFalling || ballContr.finish) StartCoroutine (EndGame());

	}

	//PASAR A UN NUEVO CODIGO PARA SER DESTRUIDO DESPUES DE SER EJECUTADO
	private void reSizeText(){
		if (scoreTex.rectTransform.localPosition.x > -300.0f)
			scoreTex.rectTransform.localPosition = new Vector3 (
				scoreTex.rectTransform.localPosition.x - speedMoveTex, 
				scoreTex.rectTransform.localPosition.y, 
				scoreTex.rectTransform.localPosition.z
			);
		if (scoreTex.rectTransform.localPosition.y < 700.0f)
			scoreTex.rectTransform.localPosition = new Vector3 (
				scoreTex.rectTransform.localPosition.x, 
				scoreTex.rectTransform.localPosition.y + speedMoveTex, 
				scoreTex.rectTransform.localPosition.z
			);

		if (scoreTex.rectTransform.localScale.x > 0.55f)
			scoreTex.rectTransform.localScale = new Vector3 (
				scoreTex.rectTransform.localScale.x - speedSizeTex, 
				scoreTex.rectTransform.localScale.y - speedSizeTex, 
				scoreTex.rectTransform.localScale.z
			);

		if (instructions.position.x > -1200.0f) instructions.velocity = Vector2.down * speed;

		if (shop.position.x > -1200.0f) shop.velocity = Vector2.down * speed;

	}

	private IEnumerator EndGame(){
		yield return new WaitForSeconds(2.0f);
		SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
	}

	private void TeleportBar(BarsController bar)
	{	
		bar.readyToGoUp = false;
		bar.transform.position = new Vector3 (Random.Range(-1.8f, 1.8f), nextPosition, 0.0f);
		bar.setUpExp.setUp ();
	}

	public void Replay(){
		Time.timeScale = 1;
		SceneManager.LoadScene("MainGame", LoadSceneMode.Single);
	}

}
