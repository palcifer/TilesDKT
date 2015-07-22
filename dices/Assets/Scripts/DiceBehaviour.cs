using UnityEngine;
using System.Collections;

public class DiceBehaviour : MonoBehaviour {

	public int TimeStep;
	private int counter;

	enum Direction
	{
		up, 
		down, 
		right, 
		left
	}
	// Use this for initialization
	void Start () {
		counter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		counter++;
		if (counter > TimeStep) {
			Direction direction = getRandomDirection();
			rotation(direction);
			counter = 0;
		}
	}

	private void rotation(Direction dir){
//		if (dir.Equals (Direction.up))
//			this.transform.Rotate(Vector3.right, 90f);
//		else if (dir.Equals (Direction.right))
//			this.transform.Rotate(Vector3.up, 90f);
//		else if (dir.Equals (Direction.left))
//			this.transform.Rotate(Vector3.up, -90f);
//		else if (dir.Equals (Direction.down))
//			this.transform.Rotate(Vector3.right, -90f);


		switch (dir) {
		case Direction.up:
			this.transform.Rotate(Vector3.right, 90f);
			break;
		case Direction.right:
			this.transform.Rotate(Vector3.up, 90f);
			break;
		case Direction.left:
			this.transform.Rotate(Vector3.up, -90f);
			break;
		default:
			this.transform.Rotate(Vector3.right, -90f);
			break;
		}
	}

	private Direction getRandomDirection(){
		int number = Random.Range (0, 4);
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
		default:
			dir = Direction.right;
			break;
		}
		return dir;
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
