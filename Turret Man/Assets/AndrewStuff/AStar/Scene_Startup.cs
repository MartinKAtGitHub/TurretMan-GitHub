using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Startup : MonoBehaviour {

	public float SizeOfMapDiameter = 0;
	public TriangleMaker TheTriangleMaker;

	List<Triangles> test;
	Triangles testing = new Triangles();

 public Vector2[] points = new Vector2[2];

	TriangleMaker test2;

	void Awake() {

		TheTriangleMaker.SetStartTriangles(SizeOfMapDiameter);
		test = TheTriangleMaker.GetTriangles();
	//	TheTriangleMaker.GetTheLine(points);

		testing.A = new Vector2(-1, 1);
		testing.B = new Vector2(1, -1);
		testing.C = new Vector2(-1, -1);


	}



	public bool DrawTriangles = false;

	public bool updateCheck = false;

	private void Update() {
		if (updateCheck == true) {
			TheTriangleMaker.Ceckup(testing, points[0], points[1]);
		}
	}



	private void OnDrawGizmos() {

		if (DrawTriangles == true) {

			for (int i = 0; i < test.Count; i++) {
				Gizmos.color = test[i].TriangleColor;
				Gizmos.DrawLine(test[i].A, test[i].B);
				Gizmos.DrawLine(test[i].C, test[i].B);
				Gizmos.DrawLine(test[i].A, test[i].C);
			}


			Gizmos.color = Color.white;
			Gizmos.DrawLine(points[0], points[1]);




		}


	}

}
