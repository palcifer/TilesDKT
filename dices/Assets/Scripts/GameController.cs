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

	public DiceBehaviour leftDice;
	public DiceBehaviour rightDice;

	public UnityEngine.UI.Text vyhral;
	public UnityEngine.UI.Text prehral;
	public UnityEngine.UI.Text zaver;

	enum State 
	{
		beforeGame,
		game,
		pausedGame,
		afterGame
	}

	private State gameState;

	// Use this for initialization
	void Start () {
		gameState = State.beforeGame;
		points = 100;
		tries = 100;
		numOfPoints.contentType = InputField.ContentType.IntegerNumber;
		numOfTries.contentType = InputField.ContentType.IntegerNumber;

		vyhral.gameObject.SetActive(false);
		prehral.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (gameState);
		if (Input.GetKey (KeyCode.Space)) {

			if(gameState == State.game)
				{
				rightDice.GetComponent<DiceBehaviour>().StopRolling();
				//rightDice.GetComponent<DiceBehaviour>().SetOriginalSpeed();
				leftDice.GetComponent<DiceBehaviour>().StopRolling();
				//leftDice.GetComponent<DiceBehaviour>().SetOriginalSpeed();
				tries--;
				updateTries(tries);

				RaycastHit hitLeft;
				RaycastHit hitRight;

				Physics.Raycast(new Vector3(-1f, 0, -5), Vector3.forward, out hitLeft);
				Physics.Raycast(new Vector3(1f, 0, -5), Vector3.forward, out hitRight);

				Debug.Log("left: " + hitLeft.collider.tag);
				Debug.Log("right: " + hitRight.collider.tag);
				if(hitLeft.collider.tag == hitRight.collider.tag){
					Debug.Log("trafil");
					vyhral.gameObject.SetActive(true);
					points = 2 * points;
				} else {
					Debug.Log("netrafil");
					prehral.gameObject.SetActive(true);
					float tmp = (float)points;
					tmp = (0.5f * tmp);
					points = Mathf.FloorToInt(tmp);
				}
				updatePoints(points);

				gameState = State.pausedGame;

			} else if (false) {
				rightDice.GetComponent<DiceBehaviour>().StartRolling();
				leftDice.GetComponent<DiceBehaviour>().StartRolling();

				gameState = State.game;
			}
		}

		if(Input.GetKey(KeyCode.RightShift)) {
			if (gameState == State.pausedGame) {
				rightDice.GetComponent<DiceBehaviour>().StartRolling();
				leftDice.GetComponent<DiceBehaviour>().StartRolling();

				rightDice.GetComponent<DiceBehaviour>().SetOriginalSpeed();
				leftDice.GetComponent<DiceBehaviour>().SetOriginalSpeed();
				gameState = State.game;

				rightDice.WriteSpeed();
				leftDice.WriteSpeed();

				vyhral.gameObject.SetActive(false);
				prehral.gameObject.SetActive(false);
			}
		}

		if(Input.GetKey(KeyCode.R)) {
			Application.LoadLevel("dicesBeta");
		}


		if (tries < 1) {
			gameState = State.afterGame;
			vyhral.gameObject.SetActive(false);
			prehral.gameObject.SetActive(false);
			string str = "Výsledný počet dekátov: " + points;
			zaver.gameObject.SetActive(true);
			zaver.text = str;
		}
	}

	private void updatePoints(int points){
		pointsString = "Aktuálny počet bodov: " + points;
		finalNumOfPoints.text = pointsString;
	}

	private void updateTries(int tries){
		triesString = "Zostáva pokusov: " + tries;
		finalNumOfTries.text = triesString;
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

				updatePoints(points);
				updateTries(tries);

				rightDice.GetComponent<DiceBehaviour>().DefineOriginalSpeed();
				leftDice.GetComponent<DiceBehaviour>().DefineOriginalSpeed();
				
				leftDice.GetComponent<DiceBehaviour>().StartRolling();
				rightDice.GetComponent<DiceBehaviour>().StartRolling();
			}
		}
	}
}
