using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene_Startup : MonoBehaviour {

	//----------TrianglePoints-------------------- 

	public float TrianglePointsDistance = 0.5f;
	public float TrianglePointsStartXPoint = -1;
	public float TrianglePointsStartYPoint = -1;
	public int TrianglePointsWidth = 5;
	public int TrianglePointsHeight = 5;

	//-------------------------------


	public TriangleMaker TheTriangleMaker;
	public Vector2[] points = new Vector2[2];




	List<Triangles> Triangles;
	PointsForTriangles[,] TrianglePoints;

	void Awake() {

		TheTriangleMaker.TriangleMakerSetup(this);
		Triangles = TheTriangleMaker.GetTriangles();
		TrianglePoints = TheTriangleMaker.GetGetpoin();
	}



	public bool DrawTriangles = false;
	public bool updateCheck = false;

	private void Update() {
		if (updateCheck == true) {

			//	TheTriangleMaker.LineCheckup(testing, points[0], points[1]);
			//	TheTriangleMaker.PointCheckup(testing, points[0]);

			for (int i = 0; i < 1000; i++) {
				test();
			}
			//TheTriangleMaker.CheckIfPointIsInside(points[0]);
			//TheTriangleMaker.LineCheckup(Triangles[0], points[0], points[1]);
			
			updateCheck = false;
		//	Debug.Log(TrianglePoints[0,0].triangleRef[0].A + " | " + TrianglePoints[0, 0].triangleRef[0].B + " | " + TrianglePoints[0, 0].triangleRef[0].C);
		//	Debug.Log(TrianglePoints[0,0].triangleRef[1].A + " | " + TrianglePoints[0, 0].triangleRef[1].B + " | " + TrianglePoints[0, 0].triangleRef[1].C);
		//	Debug.Log(TrianglePoints[0,0].triangleRef[2].A + " | " + TrianglePoints[0, 0].triangleRef[2].B + " | " + TrianglePoints[0, 0].triangleRef[2].C);

		}
	}

	void test() {
		if (TrianglePoints[0, 0].triangleRef.Count > 0) {
		}
	}



	private void OnDrawGizmos() {

		if (DrawTriangles == true) {

			for (int i = 0; i < Triangles.Count; i++) {
				Gizmos.color = Triangles[i].TriangleColor;
				Gizmos.DrawLine(Triangles[i].A, Triangles[i].B);
				Gizmos.DrawLine(Triangles[i].C, Triangles[i].B);
				Gizmos.DrawLine(Triangles[i].A, Triangles[i].C);
			}

			Gizmos.color = Color.white;
			Gizmos.DrawLine(points[0], points[1]);
			Gizmos.color = Color.black;

			
			for (int i = 0; i < TrianglePointsWidth; i++) {
				for (int j = 0; j < TrianglePointsHeight; j++) {
					Gizmos.DrawSphere(new Vector2(TrianglePoints[i, j].xPos, TrianglePoints[i, j].yPos), 0.01f);
				}
			}

		}
	}

}
