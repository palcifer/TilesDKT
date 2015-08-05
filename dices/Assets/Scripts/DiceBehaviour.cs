using UnityEngine;
using System.Collections;

public class DiceBehaviour : MonoBehaviour {

	public UnityEngine.UI.Slider slider;

	public int Speed;
	private int counter;
	private int incrementSpeedCounter;
	
	private Direction direction;
	private Vector3 directionVector;

	private float[] arr;

	private State state;

	private bool rolling;
	private bool stopping;

	private int originalSpeed;
	private bool origialSpeedBool;

	private int changedSpeed;
	private bool changedSpeedBool;

	enum Direction
	{
		up, 
		down, 
		right, 
		left,
		clockwise,
		anticlockwise
	}

	enum State
	{
		rolling,
		stopping, 
		paused,
	}
	// Use this for initialization
	void Start () {
		counter = 0;
		incrementSpeedCounter = 0;
		direction = getRandomDirection();
		arr = new float[Speed];
		arr = NonUniformDistributionsGaussian (0, Speed); //musi byt neparne cislo!

		rolling = false;

		state = State.paused;

		//originalSpeed = 0;
		//origialSpeedBool = true;

		Debug.Log (Speed);
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) {

		case State.rolling :

			counter++;
			incrementSpeedCounter++;

			if(origialSpeedBool){
				originalSpeed = Speed;
				origialSpeedBool = false;
			}
			if (counter >= Speed) {
				if(changedSpeedBool){
					changedSpeedBool = false;
					Speed = changedSpeed;
					arr = NonUniformDistributionsGaussian (0, Speed);
				}
				counter = 0;
				direction = getRandomDirection ();
				directionVector = rotationVector (direction);
				if(incrementSpeedCounter > 120 && Speed > 20){
					incrementSpeedCounter = 0;
					Speed = Speed - 2;
					arr = NonUniformDistributionsGaussian(0, Speed);
				}
			} else {
				//Debug.Log(counter);
				//float distCovered = (Time.time - startTime) * TimeStop;
				//transform.rotation = Quaternion.Lerp (startMarker, endMarker, distCovered);
				this.transform.RotateAround (transform.position, directionVector, arr [counter-1] * 90);
			}

			break;

		case State.paused:

			break;

		case State.stopping:

			if(stopping){
				counter--;
				if(counter < 1){
					rolling = false;
				}
			} else {
				counter++;
				incrementSpeedCounter++;
			}

			if(counter >= Speed || counter < 1){

				counter = 0;
				state = State.paused;
				direction = getRandomDirection ();
				directionVector = rotationVector (direction);
				if(incrementSpeedCounter > 120 && Speed > 20){
					incrementSpeedCounter = 0;
					Speed = Speed - 2;
					arr = NonUniformDistributionsGaussian(0, Speed);
				}
			} else {
				if(stopping){
					this.transform.RotateAround (transform.position, directionVector, arr [counter] * -90);
				} else {
					this.transform.RotateAround (transform.position, directionVector, arr [counter - 1] * 90);
				}
			}

			break;
		}

//		if (state == State.rolling) {
//			//Debug.Log(Speed);
//
//
//
//
////		if (counter > TimeStep) {
////			Direction direction = getRandomDirection();
////			Vector3 newPosition = rotationVector(direction);
////			counter = 0;
////		}
//
//			if (counter >= Speed) {
//				if(changedSpeedBool){
//					changedSpeedBool = false;
//					Speed = changedSpeed;
//					arr = NonUniformDistributionsGaussian (0, Speed);
//				}
//				counter = 0;
//				direction = getRandomDirection ();
//				directionVector = rotationVector (direction);
//				if(incrementSpeedCounter > 120 && Speed > 20){
//					incrementSpeedCounter = 0;
//					Speed = Speed - 2;
//					arr = NonUniformDistributionsGaussian(0, Speed);
//				}
//			} else {
//				//Debug.Log(counter);
//				//float distCovered = (Time.time - startTime) * TimeStop;
//				//transform.rotation = Quaternion.Lerp (startMarker, endMarker, distCovered);
//				if(stopping){
//					this.transform.RotateAround (transform.position, directionVector, arr [counter] * -90);
//				} else {
//					this.transform.RotateAround (transform.position, directionVector, arr [counter - 1] * 90);
//				}
//			}
//		}
	}

	private Vector3 rotationVector(Direction dir){
//		if (dir.Equals (Direction.up))
//			this.transform.Rotate(Vector3.right, 90f);
//		else if (dir.Equals (Direction.right))
//			this.transform.Rotate(Vector3.up, 90f);
//		else if (dir.Equals (Direction.left))
//			this.transform.Rotate(Vector3.up, -90f);
//		else if (dir.Equals (Direction.down))
//			this.transform.Rotate(Vector3.right, -90f);

		Vector3 startPosition = Vector3.up;
		Vector3 endPosition = new Vector3();

		switch (dir) {
		case Direction.up:
			//endPosition = Quaternion.Euler(startPosition.x, startPosition.z * -1, startPosition.y);
			endPosition = new Vector3(startPosition.x, startPosition.z * -1, startPosition.y);
			//this.transform.Rotate(Vector3.right, 90f);
			break;
		case Direction.right:
			//endPosition = Quaternion.Euler(startPosition.z * -1, startPosition.y, startPosition.x);
			endPosition = new Vector3(startPosition.z * -1, startPosition.y, startPosition.x);
			//this.transform.Rotate(Vector3.up, 90f);
			break;
		case Direction.left:
			//endPosition = Quaternion.Euler(startPosition.z, startPosition.y, startPosition.x * -1);
			endPosition = new Vector3(startPosition.z, startPosition.y, startPosition.x * -1);
			//this.transform.Rotate(Vector3.up, -90f);
			break;
		case Direction.down:
			//endPosition = Quaternion.Euler(startPosition.x, startPosition.z, startPosition.y * -1);
			endPosition = new Vector3(startPosition.x, startPosition.z, startPosition.y * -1);
			break;
		case Direction.clockwise:
			//endPosition = Quaternion.Euler(startPosition.y, startPosition.x * -1, startPosition.z);
			endPosition = new Vector3(startPosition.y, startPosition.x * -1, startPosition.z);
			break;
		case Direction.anticlockwise:
			//endPosition = Quaternion.Euler(startPosition.y * -1, startPosition.x, startPosition.z);
			endPosition = new Vector3(startPosition.y * -1, startPosition.x, startPosition.z);
			break;
		default:
			//this.transform.Rotate(Vector3.right, -90f);
			break;
		}
		return endPosition;
	}

	private Direction getRandomDirection(){
		int number = Random.Range (0, 6);
		Direction dir;
		switch (number) {
		case 1:
			dir = Direction.up;
			break;
		case 2:
			dir = Direction.right;
			break;
		case 3:
			dir = Direction.left;
			break;
		case 4:
			dir = Direction.down;
			break;
		case 5:
			dir = Direction.clockwise;
			break;
		case 6:
			dir = Direction.anticlockwise;
			break;
		default:
			dir = Direction.right;
			break;
		}
		return dir;
	}

	private static float[] NonUniformDistributionsGaussian(float startNumber, int arraySize)
	{
		float totalNumber = 0;
		if (arraySize % 2 == 0)
			arraySize--;

		float [] arr = new float[arraySize];
		for (var i = 0; i < arraySize; i++)
		{
			
			if (i <= arraySize / 8)
			{
				startNumber = startNumber + 1;
			}
			else if (i <= arraySize/4)
			{
				startNumber = startNumber  + 2; 
			}
			else if (i <= 3*arraySize/8)
			{
				startNumber = startNumber  + 3; 
			}
			else if (i <= arraySize/2)
			{
				startNumber = startNumber  + 4; 
			}
			else if (i <= 5*arraySize/8)
			{
				startNumber = startNumber  - 4; 
			}
			else if (i <= 3*arraySize/4)
			{
				startNumber = startNumber  - 3; 
			}
			else if (i <= 7*arraySize/8)
			{
				startNumber = startNumber  - 2; 
			}
			else {
				startNumber = startNumber  - 1;
			}

			arr[i] = startNumber;
			totalNumber += startNumber;
		}
		for (int i = 0; i < arraySize; i++) {
			arr[i] = arr[i]/totalNumber;
		}
		return arr;
	}

	public void StartRolling(){
		Speed = 60 - 10 * (int)slider.value;
		state = State.rolling;
		rolling = true;
		stopping = false;
	}

	public void StopRolling(){
			state = State.stopping;
//			if (counter <= Speed / 2) {
//			stopping = true;
//		} else {
//				stopping = false;
//		}
			stopping = (counter <= Speed / 2) ? true : false;
	}

	public void SetSpeed(float i){
		changedSpeed = 60 - 10 * (int)i;
		changedSpeedBool = true;
	}

	public void SetOriginalSpeed(){
		Speed = originalSpeed;
		arr = NonUniformDistributionsGaussian (0, Speed);
	}

	public void DefineOriginalSpeed(){
		//Debug.Log (Speed);
		originalSpeed = Speed;
	}

	public void WriteSpeed(){
		Debug.Log (Speed);
	}

//	private Vector3 getVectorFromDirection(Direction dir){
//		if (dir.Equals (Direction.up))
//			return new Vector3 (Vector3.up);
//		else if (dir.Equals (Direction.right))
//			return new Vector3 (Vector3.right);
//		else if (dir.Equals (Direction.left))
//			return new Vector3 (Vector3.left);
//		else if (dir.Equals (Direction.down))
//			return new Vector3 (Vector3.down);
//	}
	
}
