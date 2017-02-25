using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLine : MonoBehaviour {
	//Publics
	public GameObject dot;
	public int width;
	public int length;
	public float cubeSize;
	public float maxHeight = 40.0f;
	int drumSection = 5;
	int melSection = 10;
	GameObject[,] alldots;
	int frameCount = 0;

	float damper = 0.3f;
	// Use this for initialization
	void Start () {
		
		int curIndex = 0;
		alldots = new GameObject[width, length];
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < length; j++) {
				GameObject cur_dot = (GameObject)Instantiate (dot, new Vector3 (j * (cubeSize + damper), 0, i * (cubeSize + damper)), Quaternion.identity);
				cur_dot.GetComponent<Renderer> ().material.color = Color.green;
				alldots [i,j] = cur_dot;
			}
		}
	}


	void Update() {
		for (int i = width-1; i >= 1; i--) {
			for (int j = length-1; j >= 0; j--) {
				alldots[i,j].transform.position = new Vector3 (alldots [i, j].transform.position.x, alldots [(i - 1), j].transform.position.y, alldots [i, j].transform.position.z);
			}
		}
		float[] heights = this.GetComponent<ReadSoundData> ().CurrentSample ();

		for (int i = 0; i < length; i++) {
				Vector3 myPos = alldots [0, i].transform.position;
			myPos.y = heights[i] * maxHeight;
				alldots [0, i].transform.position = myPos;

		}

			//for (int i = size-1; i > size-3; i--) {
			//	for (int j = size-1; j >= size-3; j--) {
			//		Vector3 myPos = alldots [i, j].transform.position;
			//		myPos.y = alldots [(i + 1), j].transform.position.y;
			//		alldots [i, j].transform.position = myPos;
			//	}
			//}
	}
}
		
