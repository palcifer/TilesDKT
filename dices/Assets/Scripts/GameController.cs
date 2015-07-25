using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	private int points;
	private string pointsString;
	private int tries;
	private string triesString;

	public InputField numOfPoints;
	public InputField numOfTries;
	public Button StartOfGame;

	public Text finalNumOfPoints;
	public Text finalNumOfTries;

	enum State 
	{
		beforeGame,
		game,
		afterGame
	}

	private State gameState;

	// Use this for initialization
	void Start () {
		gameState = State.beforeGame;
		numOfPoints.contentType = InputField.ContentType.IntegerNumber;
		numOfTries.contentType = InputField.ContentType.IntegerNumber;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake()
	{

	}

	public void AllAtrAreSet(){
		if (gameState == State.beforeGame) {

			bool isNumeric = int.TryParse(numOfPoints.text, out points);
			isNumeric = isNumeric && int.TryParse(numOfTries.text, out tries);
			if(isNumeric){
				//Debug.Log("lalala");
				gameState = State.game;
				numOfPoints.gameObject.SetActive(false);
				numOfTries.gameObject.SetActive(false);
				StartOfGame.gameObject.SetActive(false);

				pointsString = "Aktuálny počet bodov: " + points;
				finalNumOfPoints.text = pointsString;

				triesString = "Zostáva pokusov: " + tries;
				finalNumOfTries.text = triesString;
			}
		}
	}
}
