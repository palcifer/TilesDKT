using UnityEngine;
using System.Collections;

public class DiceBehaviour : MonoBehaviour {

	public int TimeStop;
	private int counter;
	
	private Direction direction;
	private Vector3 directionVector;

	private float[] arr;

	private float startTime;

	enum Direction
	{
		up, 
		down, 
		right, 
		left,
		clockwise,
		anticlockwise
	}
	// Use this for initialization
	void Start () {
		counter = 0;
		direction = getRandomDirection();
		startTime = Time.time;
		arr = new float[TimeStop];
		arr = NonUniformDistributionsGaussian (0, TimeStop); //musi byt neparne cislo!
	}
	
	// Update is called once per frame
	void Update () {
		counter++;
//		if (counter > TimeStep) {
//			Direction direction = getRandomDirection();
//			Vector3 newPosition = rotationVector(direction);
//			counter = 0;
//		}

		if (counter >= TimeStop) {
			counter = 0;
			direction = getRandomDirection();
			directionVector = rotationVector(direction);
			startTime = Time.time;
		} else {
			//Debug.Log(counter);
			//float distCovered = (Time.time - startTime) * TimeStop;
			//transform.rotation = Quaternion.Lerp (startMarker, endMarker, distCovered);
			this.transform.RotateAround(transform.position, directionVector, arr[counter-1]*90);
		}
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
