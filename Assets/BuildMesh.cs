using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMesh : MonoBehaviour {

	public int size_x;
	public int size_z;
	public float max_height;
	int numVerts;
	int vsize_x;
	int vsize_z;
	float tileSize = 1.0f;
	// Use this for initialization
	void Start () {
		vsize_x = size_x + 1;
		vsize_z = size_z + 1;
		numVerts = vsize_x * vsize_z;
		BuildMyMesh ();
	}


	void Update() {
			CreateNewHeights ();
	}
	
	// Update is called once per frame
	void BuildMyMesh () {
		int numTiles = size_x * size_z;
		int numTris = numTiles * 2;


		Vector3[] verts = new Vector3[numVerts];
		Vector3[] normals = new Vector3[numVerts];
		Vector2[] uv = new Vector2[numVerts];
		int[] triangles = new int[numTris * 3];
		int x;
		int z;

		for(z=0; z < vsize_z; z++) {

			for (x = 0; x < vsize_x; x++) {
				verts [z * vsize_x + x] = new Vector3 (x * tileSize, 0, z * tileSize);
				normals[z * vsize_x + x]= Vector3.up;
				uv[z * vsize_x + x] = new Vector2((float)x/vsize_x, (float)z/vsize_z);
			}
		}
		for(z=0; z < size_z; z++) {

			for (x = 0; x < size_x; x++) {
				int squareIndex = z * size_x + x;
				int triIndex = squareIndex * 6;

				triangles [triIndex + 0] = z * vsize_x + x + 0;
				triangles [triIndex + 2] = z * vsize_x + x+ vsize_x + 1;
				triangles [triIndex + 1] = z * vsize_x + x+ vsize_x + 0;

				triangles [triIndex + 3] = z * vsize_x + x+ 0;
				triangles [triIndex + 5] = z * vsize_x + x+ 1;
				triangles[triIndex + 4]=z * vsize_x + x + vsize_x+1;

			}
		}


		Mesh mesh = new Mesh ();
		mesh.vertices = verts;
		mesh.triangles = triangles;
		mesh.normals = normals;
		mesh.uv = uv;


		MeshFilter mesh_filter = GetComponent<MeshFilter> ();
		MeshRenderer mesh_renderer = GetComponent<MeshRenderer> ();
		MeshCollider mesh_collider = GetComponent<MeshCollider> ();
		mesh_filter.mesh = mesh;

	}

	void CreateNewHeights() {
		Vector3[] heightverts = new Vector3[numVerts];
		float[] samples = this.GetComponent<ReadSoundData> ().CurrentSample ();
		int cur_sample = 0;
		for (int z = 0; z < vsize_z; z++) {
			float height = samples [z] * max_height;
			Debug.Log (height);
			heightverts [z * vsize_x + 0] = new Vector3 ((float)0 * tileSize, height, z * tileSize);
		}
		Mesh myMesh = this.GetComponent<Mesh> ();
		Vector2[] uv = myMesh.uv;
		int[] triangles = myMesh.triangles;
		myMesh.Clear ();
		myMesh.vertices = heightverts;
		myMesh.uv = uv;
		myMesh.triangles = triangles;
	}
}
