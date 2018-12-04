using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleMaker : MonoBehaviour {

	float _denominator = 0;
	float _numerator1 = 0;
	float _numerator2 = 0;
	float _r = 0;
	float _s = 0;

	public List<Triangles> GetTriangles() {
		return TriangleGroups;
	}

	public void SetStartTriangles(float diameter) {
		CreateStartTriangle(diameter);
	//	Checkup();
	}


	void CreateStartTriangle(float diameter) {//Create The Two First Triangles That Is the Base Triangles Of The Entire Map

		triangle = new Triangles();
		triangle.TriangleColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
		triangle.A = new Vector2(-diameter / 2, diameter / 2);
		triangle.B = new Vector2(diameter / 2, diameter / 2);
		triangle.C = new Vector2(diameter / 2, -diameter / 2);
		TriangleGroups.Add(triangle);

		triangle = new Triangles();
		triangle.TriangleColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
		triangle.A = new Vector2(-diameter / 2, diameter / 2);
		triangle.B = new Vector2(diameter / 2, -diameter / 2);
		triangle.C = new Vector2(-diameter / 2, -diameter / 2);
		TriangleGroups.Add(triangle);

		#region Previous Logic
		//The Room Is The Start Room, It Requires That The Points Represented Are Able To Be Connected Any Way Without Getting Weird Triangle. (Like A Square But At The Bottom There Is A Tiny Square. Then This Wont Work, That Square Needs To Be Added Later)
		/*		MapCorenerLength = CoreMapAlterations.Length - 1;

				triangle = new Triangles();
				triangle.TriangleColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
				triangle.A = CoreMapAlterations[0];
				PreviousPoints[0] = CoreMapAlterations[0];
				CoreMapAlterations[0] = CoreMapAlterations[MapCorenerLength--];

				for (int j = 0; j < 2; j++) {//Finding The Two Other Points

					shortestDistance = 1000;

					for (int n = 0; n < MapCorenerLength; n++) {//Iterating To Find The Other Point

						if (Vector2.Distance(triangle.A, CoreMapAlterations[n]) < shortestDistance) {
							shortestDistance = Vector2.Distance(triangle.A, CoreMapAlterations[n]);
							_ClosestPoint = n;
						}
					}

					if (j == 0) {
						triangle.B = CoreMapAlterations[_ClosestPoint];
						PreviousPoints[1] = CoreMapAlterations[_ClosestPoint];
						CoreMapAlterations[_ClosestPoint] = CoreMapAlterations[MapCorenerLength--];

					} else {
						triangle.C = CoreMapAlterations[_ClosestPoint];
						PreviousPoints[2] = CoreMapAlterations[_ClosestPoint];
						CoreMapAlterations[_ClosestPoint] = CoreMapAlterations[MapCorenerLength--];
					}

				}

				TriangleGroups.Add(triangle);


				for (int i = 0; i < CoreMapAlterations.Length - 3; i++) {//Triangles To Make. 3 Points == 1 Triangle. 4 Points == 2 Triangles
					triangle = new Triangles();
					triangle.TriangleColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
					triangle.A = PreviousPoints[2];


					shortestDistance = 1000;

					for (int n = 0; n <= MapCorenerLength; n++) {//Iterating To Find The Other Point
						if (Vector2.Distance(PreviousPoints[2], CoreMapAlterations[n]) < shortestDistance) {
							shortestDistance = Vector2.Distance(PreviousPoints[2], CoreMapAlterations[n]);
							_ClosestPoint = n;
						}
					}
					for (int n = 0; n <= MapCorenerLength; n++) {//Iterating To Find The Other Point
						if (Vector2.Distance(PreviousPoints[1], CoreMapAlterations[n]) < shortestDistance) {
							shortestDistance = Vector2.Distance(PreviousPoints[1], CoreMapAlterations[n]);
							_ClosestPoint = n;
						}
					}
					for (int n = 0; n <= MapCorenerLength; n++) {//Iterating To Find The Other Point
						if (Vector2.Distance(PreviousPoints[0], CoreMapAlterations[n]) < shortestDistance) {
							shortestDistance = Vector2.Distance(PreviousPoints[0], CoreMapAlterations[n]);
							_ClosestPoint = n;
						}
					}

					PointInLineA = ((PreviousPoints[0].x - PreviousPoints[2].x) * (CoreMapAlterations[_ClosestPoint].y - PreviousPoints[2].y) - (PreviousPoints[0].y - PreviousPoints[2].y) * (CoreMapAlterations[_ClosestPoint].x - PreviousPoints[2].x));
					PointInLineB = ((PreviousPoints[0].x - PreviousPoints[2].x) * (PreviousPoints[1].y - PreviousPoints[2].y) - (PreviousPoints[0].y - PreviousPoints[2].y) * (PreviousPoints[1].x - PreviousPoints[2].x));

					if (PointInLineA > 0) {
						if (PointInLineB < 0) {//On Oposite Sides
							triangle.A = PreviousPoints[2];
							triangle.B = PreviousPoints[0];
							triangle.C = CoreMapAlterations[_ClosestPoint];
							CoreMapAlterations[_ClosestPoint] = CoreMapAlterations[MapCorenerLength--];
						} else {//On The Same Side.
							PointInLineA = ((PreviousPoints[1].x - PreviousPoints[2].x) * (CoreMapAlterations[_ClosestPoint].y - PreviousPoints[2].y) - (PreviousPoints[1].y - PreviousPoints[2].y) * (CoreMapAlterations[_ClosestPoint].x - PreviousPoints[2].x));
							PointInLineB = ((PreviousPoints[1].x - PreviousPoints[2].x) * (PreviousPoints[0].y - PreviousPoints[2].y) - (PreviousPoints[1].y - PreviousPoints[2].y) * (PreviousPoints[0].x - PreviousPoints[2].x));

							if (PointInLineA > 0) {
								if (PointInLineB < 0) {//On Oposite Sides
									triangle.A = PreviousPoints[2];
									triangle.B = PreviousPoints[1];
									triangle.C = CoreMapAlterations[_ClosestPoint];
									CoreMapAlterations[_ClosestPoint] = CoreMapAlterations[MapCorenerLength--];
								} else {
									triangle.A = PreviousPoints[0];
									triangle.B = PreviousPoints[1];
									triangle.C = CoreMapAlterations[_ClosestPoint];
									CoreMapAlterations[_ClosestPoint] = CoreMapAlterations[MapCorenerLength--];
								}
							} else {
								if (PointInLineB > 0) {//On Oposite Sides
									triangle.A = PreviousPoints[2];
									triangle.B = PreviousPoints[1];
									triangle.C = CoreMapAlterations[_ClosestPoint];
									CoreMapAlterations[_ClosestPoint] = CoreMapAlterations[MapCorenerLength--];
								} else {
									triangle.A = PreviousPoints[0];
									triangle.B = PreviousPoints[1];
									triangle.C = CoreMapAlterations[_ClosestPoint];
									CoreMapAlterations[_ClosestPoint] = CoreMapAlterations[MapCorenerLength--];
								}
							}
						}
					} else if (PointInLineA < 0) {
						if (PointInLineB > 0) {//On Oposite Sides
							triangle.A = PreviousPoints[2];
							triangle.B = PreviousPoints[0];
							triangle.C = CoreMapAlterations[_ClosestPoint];
							CoreMapAlterations[_ClosestPoint] = CoreMapAlterations[MapCorenerLength--];
						} else {//On The Same Side.
							PointInLineA = ((PreviousPoints[1].x - PreviousPoints[2].x) * (CoreMapAlterations[_ClosestPoint].y - PreviousPoints[2].y) - (PreviousPoints[1].y - PreviousPoints[2].y) * (CoreMapAlterations[_ClosestPoint].x - PreviousPoints[2].x));
							PointInLineB = ((PreviousPoints[1].x - PreviousPoints[2].x) * (PreviousPoints[0].y - PreviousPoints[2].y) - (PreviousPoints[1].y - PreviousPoints[2].y) * (PreviousPoints[0].x - PreviousPoints[2].x));

							if (PointInLineA > 0) {
								if (PointInLineB < 0) {//On Oposite Sides
									triangle.A = PreviousPoints[2];
									triangle.B = PreviousPoints[1];
									triangle.C = CoreMapAlterations[_ClosestPoint];
									CoreMapAlterations[_ClosestPoint] = CoreMapAlterations[MapCorenerLength--];
								} else {
									triangle.A = PreviousPoints[0];
									triangle.B = PreviousPoints[1];
									triangle.C = CoreMapAlterations[_ClosestPoint];
									CoreMapAlterations[_ClosestPoint] = CoreMapAlterations[MapCorenerLength--];
								}
							} else {
								if (PointInLineB > 0) {//On Oposite Sides
									triangle.A = PreviousPoints[2];
									triangle.B = PreviousPoints[1];
									triangle.C = CoreMapAlterations[_ClosestPoint];
									CoreMapAlterations[_ClosestPoint] = CoreMapAlterations[MapCorenerLength--];
								} else {
									triangle.A = PreviousPoints[0];
									triangle.B = PreviousPoints[1];
									triangle.C = CoreMapAlterations[_ClosestPoint];
									CoreMapAlterations[_ClosestPoint] = CoreMapAlterations[MapCorenerLength--];
								}
							}
						}
					} else {
						Debug.Log("Hmmmm Its Zero");
					}

					PreviousPoints[0] = triangle.A;
					PreviousPoints[1] = triangle.B;
					PreviousPoints[2] = triangle.C;
					TriangleGroups.Add(triangle);

				}*/

		#endregion

	}



	public void Ceckup(Triangles triangle, Vector2 startPoint, Vector2 endPoint) {

		Debug.Log(IsIntersecting(triangle.A, triangle.B, startPoint, endPoint));//Sending In Two "Vectors" And Checking If They Come Into Contact 
		Debug.Log(IsIntersecting(triangle.B, triangle.C, startPoint, endPoint));//Sending In Two "Vectors" And Checking If They Come Into Contact 
		Debug.Log(IsIntersecting(triangle.C, triangle.A, startPoint, endPoint));//Sending In Two "Vectors" And Checking If They Come Into Contact 


		if (IsIntersecting(triangle.A, triangle.B, startPoint, endPoint) == true) {

		}else if (IsIntersecting(triangle.B, triangle.C, startPoint, endPoint) == true) {

		}else if (IsIntersecting(triangle.C, triangle.A, startPoint, endPoint) == true) {

		}

	}

	bool IsIntersecting(Vector2 a, Vector2 b, Vector2 c, Vector2 d) {

		_denominator = ((b.x - a.x) * (d.y - c.y)) - ((b.y - a.y) * (d.x - c.x));
		_numerator1 = ((a.y - c.y) * (d.x - c.x)) - ((a.x - c.x) * (d.y - c.y));
		_numerator2 = ((a.y - c.y) * (b.x - a.x)) - ((a.x - c.x) * (b.y - a.y));

		if (_denominator == 0) {

			if (_numerator1 == 0 && _numerator2 == 0) {//If The Lines Are Parallel This Is True. Even If The Placement Is Different, As Long As The Direction Is The Same This Is True. So This Check Is Simply If They Truly Are Parallel And Overlapping

				if (a.x != b.x) {//Checking If C And D Are Not On The Same X-Axis Position
					if (a.x < b.x) {//if the a is more than b
						if (c.x >= a.x && c.x <= b.x) {//c is between, a and b
							return true;
						} else if (d.x >= a.x && d.x <= b.x) {//d is between, a and b
							return true;
						} else {
							if (c.x < a.x && d.x < a.x) {//Checking If Both Points Are On The Same Side, If Not Then The Are On Oposite Sides
								return false;
							} else if (c.x > b.x && d.x > b.x) {
								return false;
							} else {
								return true;
							}
						}

					} else {//c is bigger than d
						if (c.x >= b.x && c.x <= a.x) {//c is between, a and b
							return true;
						} else if (d.x >= b.x && d.x <= a.x) {//d is between, a and b
							return true;
						} else {
							if (c.x < b.x && d.x < b.x) {//Checking If Both Points Are On The Same Side, If Not Then The Are On Oposite Sides
								return false;
							} else if (c.x > a.x && d.x > a.x) {
								return false;
							} else {
								return true;
							}
						}
					}

				} else {//This Is A Special Case Situation, Where I Spesificaly Need To Check If They Are Overlapping with the angle of a perfect 90-degrees (c.x == d.x)

					if (a.y < b.y) {//if the a is more than b
						if (c.y >= a.y && c.y <= b.y) {//c is between, a and b
							return true;
						} else if (d.y >= a.y && d.y <= b.y) {//d is between, a and b
							return true;
						} else {
							if (c.y < a.y && d.y < a.y) {//Checking If Both Points Are On The Same Side, If Not Then The Are On Oposite Sides
								return false;
							} else if (c.y > b.y && d.y > b.y) {
								return false;
							} else {
								return true;
							}
						}


					} else {//c is bigger than d
						if (c.y >= b.y && c.y <= a.y) {//c is between, a and b
							return true;
						} else if (d.y >= b.y && d.y <= a.y) {//d is between, a and b
							return true;
						} else {
							if (c.y < b.y && d.y < b.y) {//Checking If Both Points Are On The Same Side, If Not Then The Are On Oposite Sides
								return false;
							} else if (c.y > a.y && d.y > a.y) {
								return false;
							} else {
								return true;
							}
						}
					}

				}
			}
		}

		_r = _numerator1 / _denominator;
		_s = _numerator2 / _denominator;

		return (_r >= 0 && _r <= 1) && (_s >= 0 && _s <= 1);
	}






















	[Tooltip("Changes From A Perfect Rectangle To The Current Where New Shapes In From Of Rectangles Are Added One By One")]
	public Vector2[] CoreMapAlterations;//Holding All Objects, So That If Stuff Goes Wrong I Can Reload?

	Triangles triangle;
	List<Triangles> TriangleGroups = new List<Triangles>();
	/*
	int _ClosestPoint = 0;
	float shortestDistance = 0;
	Vector3[] PreviousPoints = new Vector3[3];
	int MapCorenerLength = 0;

	float PointInLineA = 0;
	float PointInLineB = 0;

	bool s_ab = false;


	public Vector2[] objectPoints;


	float area(int TriangleAX, int TriangleAY, int TriangleBX, int TriangleBY, int TriangleCX, int TriangleCY) {

		float x = 0;
		int PointX = 0;
		int PointY = 0;

		x = (TriangleAX * (TriangleBY - TriangleCY) + TriangleBX * (TriangleCY - TriangleAY) + TriangleCX * (TriangleAY - TriangleBY));

		x = (PointX * (TriangleBY - TriangleCY) + TriangleBX * (TriangleCY - PointY) + TriangleCX * (PointY - TriangleBY)) + (TriangleAX * (PointY - TriangleCY) + PointX * (TriangleCY - TriangleAY) + TriangleCX * (TriangleAY - PointY)) + (TriangleAX * (TriangleBY - PointY) + TriangleBX * (PointY - TriangleAY) + PointY * (TriangleAY - TriangleBY));


		float t1 = TriangleAX * (TriangleBY - TriangleCY);
		float t2 = TriangleBX * (TriangleCY - TriangleAY);
		float t3 = TriangleCX * (TriangleAY - TriangleBY);



		x = (PointX * (TriangleBY - TriangleCY) + TriangleBX * (TriangleCY - PointY) + TriangleCX * (PointY - TriangleBY));


		x = (TriangleAX * (PointY - TriangleCY) + PointX * (TriangleCY - TriangleAY) + TriangleCX * (TriangleAY - PointY));


		x = (TriangleAX * (TriangleBY - PointY) + TriangleBX * (PointY - TriangleAY) + PointY * (TriangleAY - TriangleBY));


		return (TriangleAX * (TriangleBY - TriangleCY) + TriangleBX * (TriangleCY - TriangleAY) + TriangleCX * (TriangleAY - TriangleBY));

	}

	bool isinside(int TriangleAX, int TriangleAY, int TriangleBX, int TriangleBY, int TriangleCX, int TriangleCY, int PointX, int PointY) {

		float w1 = (TriangleAX * (TriangleCY - TriangleAY) + (PointY - TriangleAY) * (TriangleCX - TriangleAX) - PointX * (TriangleCY - TriangleAY)) / ((TriangleBY - TriangleAY) * (TriangleCX - TriangleAX) - (TriangleBX - TriangleAX) * (TriangleCY - TriangleAY));

		float w2 = (PointY - TriangleAY - w1 * (TriangleBY - TriangleAY)) / (TriangleCY - TriangleAY);

		Debug.Log(w1 + " | " + w2);
		if (w1 >= 0 && w2 >= 0 && (w1 + w2) <= 1) {
			Debug.Log("|| true");
		} else {
			Debug.Log("|| false");
		}

		return ((area(TriangleAX, TriangleAY, TriangleBX, TriangleBY, TriangleCX, TriangleCY) ==
			area(PointX, PointY, TriangleBX, TriangleBY, TriangleCX, TriangleCY) + area(TriangleAX, TriangleAY, PointX, PointY, TriangleCX, TriangleCY) + area(TriangleAX, TriangleAY, TriangleBX, TriangleBY, PointX, PointY)));

	}


	// Use this for initialization
	void Start() {
		Vector2 va = new Vector2(-5, -5);
		Vector2 vb = new Vector2(0, 5);
		Vector2 vc = new Vector2(5, -5);

		Debug.Log(isinside((int)va.x, (int)va.y, (int)vb.x, (int)vb.y, (int)vc.x, (int)vc.y, -15, 0));

		float pointAx = -5;
		float pointAb = -5;
		float a = (1 * pointAx) - (1 * pointAb) + 0;

		pointAx = 0;
		pointAb = 5;
		a = (1 * pointAx) - (1 * pointAb) + 0;

		pointAx = 5;
		pointAb = -5;
		a = (1 * pointAx) - (1 * pointAb) + 0;

		Vector2 A = new Vector2(-2, -2);
		Vector2 B = new Vector2(0, 2);
		Vector2 C = new Vector2(2, -2);

		Vector2 LA = new Vector2(5, 1);
		Vector2 LB = new Vector2(-5, -1);

		objectPoints = new Vector2[4];
		objectPoints[0] = new Vector2(-1, 1);
		objectPoints[1] = new Vector2(1, 1);
		objectPoints[2] = new Vector2(1, -1);
		objectPoints[3] = new Vector2(-1, -1);

//		CreateStartTriangle();


	}
	Vector2 p1 = new Vector2(-5, -5);
	Vector2 p2 = new Vector2(0, 5);
	Vector2 p3 = new Vector2(5, -5);
	*/

	/* Check whether P and Q lie on the same side of line AB */
/*	float Side(Vector2 p, Vector2 q, Vector2 a, Vector2 b) {
		float z1 = (b.x - a.x) * (p.y - a.y) - (p.x - a.x) * (b.y - a.y);
		float z2 = (b.x - a.x) * (q.y - a.y) - (q.x - a.x) * (b.y - a.y);
		return z1 * z2;
	}*/

	/* Check whether segment P0P1 intersects with triangle t0t1t2 */
//	string Intersecting(Vector2 p0, Vector2 p1, Vector2 t0, Vector2 t1, Vector2 t2) {
		/* Check whether segment is outside one of the three half-planes
		 * delimited by the triangle. */
//		float f1 = Side(p0, t2, t0, t1), f2 = Side(p1, t2, t0, t1);
//		float f3 = Side(p0, t0, t1, t2), f4 = Side(p1, t0, t1, t2);
//		float f5 = Side(p0, t1, t2, t0), f6 = Side(p1, t1, t2, t0);
		/* Check whether triangle is totally inside one of the two half-planes
		 * delimited by the segment. */
//		float f7 = Side(t0, t1, p0, p1);
//		float f8 = Side(t1, t2, p0, p1);

		/* If segment is strictly outside triangle, or triangle is strictly
		 * apart from the line, we're not intersecting */
//		if ((f1 < 0 && f2 < 0) || (f3 < 0 && f4 < 0) || (f5 < 0 && f6 < 0)
//			  || (f7 > 0 && f8 > 0))
//		return "NOT_INTERSECTING";

		/* If segment is aligned with one of the edges, we're overlapping */
//		if ((f1 == 0 && f2 == 0) || (f3 == 0 && f4 == 0) || (f5 == 0 && f6 == 0))
//			return "OVERLAPPING";

		/* If segment is outside but not strictly, or triangle is apart but
		 * not strictly, we're touching */
//		if ((f1 <= 0 && f2 <= 0) || (f3 <= 0 && f4 <= 0) || (f5 <= 0 && f6 <= 0)
//			  || (f7 >= 0 && f8 >= 0))
//			return "TOUCHING";

		/* If both segment points are strictly inside the triangle, we
		 * are not intersecting either */
//		if (f1 > 0 && f2 > 0 && f3 > 0 && f4 > 0 && f5 > 0 && f6 > 0)
//			return "NOT_INTERSECTING";

		/* Otherwise we're intersecting with at least one edge */
//		return "INTERSECTING";
//	}

/*	void MapModifyers() {//The Room Is A Square/Rectangle.   Aditional Objects Are Added Later.

		//TODO Add ModifyMap Objects (As Normal Object)

	}

	List<Triangles> TriangleGroupsInside;

	void AddObjects(Vector2[] objectPoints) {//This Have Four Vectors, Upper Points And Lower Points Of A Square
		TriangleGroupsInside = new List<Triangles>();


		Debug.Log(Vector3.Cross(p1 - p2, objectPoints[3] - objectPoints[2]));
		Debug.Log(Vector3.Cross(p2 - p2, objectPoints[3] - objectPoints[2]));
		Debug.Log(Vector3.Cross(p1 - p3, objectPoints[3] - objectPoints[2]));
		//	Debug.Log(Vector3.Cross(p2 - p1, objectPoints[0] - objectPoints[3]));



		//	Debug.Log((p1 - p2) + " | " + (objectPoints[1] - objectPoints[0]));
		Debug.Log((p2 - p1) + " | " + (objectPoints[1] - objectPoints[0]));
		Debug.Log((p3 - p2) + " | " + (objectPoints[1] - objectPoints[0]));
		Debug.Log((p1 - p3) + " | " + (objectPoints[1] - objectPoints[0]));
		//	Debug.Log(Vector3.Cross(p3 - p2, objectPoints[1] - objectPoints[0]));
		//	Debug.Log(Vector3.Cross(p3 - p1, objectPoints[1] - objectPoints[0]));

	*/

		/*		for (int j = 0; j < objectPoints.Length; j++) {




					for (int i = 0; i < TriangleGroups.Count; i++) {

						if (PointInsideTriangle(objectPoints[j].x, objectPoints[j].y, TriangleGroups[i].A.x, TriangleGroups[i].A.y, TriangleGroups[i].B.x, TriangleGroups[i].B.y, 0, 0) == true) {

						}



					}



				}*/

		//Where Am I

		//The Triangle Im In, Put The Triangle Points Into An OpenList Which Shall Be Searched Through

		//Attach The Points In The OpenList To The New Objects Nodes

		//Start In One Side 

		//Tomorrow 


	}

/*


	void FindNewPoints() {

		float pointX = 4;
		float pointY = 0;

		float triangleAX = 1;
		float triangleAY = 1;
		float triangleBX = 2;
		float triangleBY = 3;
		float triangleCX = 3;
		float triangleCY = 1;

		Debug.Log(PointInsideTriangle(pointX, pointY, triangleAX, triangleAY, triangleBX, triangleBY, triangleCX, triangleCY));

	}





	//Found This On Zer Internett. Made TeenyTiny Changes, stackoverflow.com/questions/2049582/how-to-determine-if-a-point-is-in-a-2d-triangle
	bool PointInsideTriangle(float sX, float sY, float aX, float aY, float bX, float bY, float cX, float cY) {//Checking If sX || sY (Which Is The Point Im Checking) Is Inside The 3 Points Of The Triangle

		s_ab = (bX - aX) * (sY - aY) - (bY - aY) * (sX - aX) > 0;

		if ((cX - aX) * (sY - aY) - (cY - aY) * (sX - aX) > 0 == s_ab) return false;

		if ((cX - bX) * (sY - bY) - (cY - bY) * (sX - bX) > 0 != s_ab) return false;

		return true;
	}
	




	void OnDrawGizmosSelected() {

		if (TriangleGroups.Count > 0) {
			for (int i = 0; i < TriangleGroups.Count; i++) {

				Gizmos.color = TriangleGroups[i].TriangleColor;
				Gizmos.DrawLine(TriangleGroups[i].A, TriangleGroups[i].B);
				Gizmos.DrawLine(TriangleGroups[i].B, TriangleGroups[i].C);
				Gizmos.DrawLine(TriangleGroups[i].C, TriangleGroups[i].A);

			}
		}
	}


	void testingremove(int TriangleCX, int TriangleCY, int PointX, int PointY) {

		float w1 = ((TriangleCY) + (PointY) * (TriangleCX) - PointX * (TriangleCY)) / ((TriangleCX));

		float w2 = (PointY - w1) / (TriangleCY);

		Debug.Log(w1 + " | " + w2);
		if (w1 >= 0 && w2 >= 0 && (w1 + w2) <= 1) {
			Debug.Log("|| true");
		} else {
			Debug.Log("|| false");
		}


	}




	float sign(Vector2 p1, Vector2 p2, Vector2 p3) {
		return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
	}

	bool PointInTriangle(Vector2 pt, Vector2 v1, Vector2 v2, Vector2 v3) {
		float d1, d2, d3;
		bool has_neg, has_pos;

		d1 = sign(pt, v1, v2);
		d2 = sign(pt, v2, v3);
		d3 = sign(pt, v3, v1);

		has_neg = (d1 < 0) || (d2 < 0) || (d3 < 0);
		has_pos = (d1 > 0) || (d2 > 0) || (d3 > 0);

		return !(has_neg && has_pos);
	}






}
*/

public class Triangles {
	public Color TriangleColor;//Only for Testing Purposes

	public Vector2 A;
	public Vector2 B;
	public Vector2 C;

	public Triangles AB;
	public Triangles AC;
	public Triangles BC;
}

[System.Serializable]
public class MapAlterations {

	public Vector2 A;
	public Vector2 B;
	public Vector2 C;
	public Vector2 D;

}