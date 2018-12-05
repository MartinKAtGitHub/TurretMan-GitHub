using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Potential "Bug" may occur, when only one point is placed. the "bug" occur when a point is place on the line between to points, then one triangle will be merged with another. to fix this there needs to be a special case check where if a point is placed on an existing tirangle side, then that tiangle which connect from the points on the end of the line and the point dissapair.
//But this is a special case, that might never happen cuz no wall or item can take one mappoint. soo its just a prewarning for something that may occur if the rules of the system isnt followed, which in all reality it should.
public class TriangleMaker : MonoBehaviour {

	public PointsForTriangles[,] GetGetpoin() {//TODO Remove When Testing Is Done
		return pointMap;
	}
	public List<Triangles> GetTriangles() {//TODO Remove When Testing Is Done
		return TriangleGroups;
	}





	[Tooltip("Changes From A Perfect Rectangle To The Current Where New Shapes In From Of Rectangles Are Added One By One")]
	public Vector2[] CoreMapAlterations;//Holding All Objects, So That If Stuff Goes Wrong I Can Reload?

	Triangles triangle;
	PointsForTriangles[,] pointMap;

	List<Triangles> TriangleGroups = new List<Triangles>();

	float _denominator = 0;
	float _numerator1 = 0;
	float _numerator2 = 0;
	float _r = 0;
	float _s = 0;

	int startpoint = 0;

	Triangles removed = null;
	Triangles newTriangle = null;

	List<Vector2> points = new List<Vector2>();

	Scene_Startup _SceneDetails;



	public void TriangleMakerSetup(Scene_Startup info) {

		_SceneDetails = info;
		pointMap = new PointsForTriangles[info.TrianglePointsWidth, info.TrianglePointsHeight];
		Maketrianglepoints();
		CreateStartTriangle();

	}

	//go through all mappoints and check is there is a trianglecorner on that point. (it increase seach speed.)
	//if no points are on the new points. then the seach begin (go to 2.)
	//if there are points then i know that the rest of the triangles are close,
	//then all neighbour triangles are added to the list and those are checked upon to see is the object is inside those. 
	//if thats not the case then i know that 

	//the search prosses goes as follor. 
	//seach through all mappoints affected by the new object.

	//- if nothing were found, then i can do the search in 3 different ways.
	//1. do and expanding search with the mappoints untill i find a point that have a triangle corner on it.
	//   then its possible to backtrack from there by going neighbour by neighbour untill the object is found.
	//-  the next step will then be go through all neighbours untill all object points are found.
	//2. do a bruteforce seach and check if a point is within a triangle. this is one seach each triangle.
	//-  the next step will be to go through all neighbours untill the netire object is discovered.
	//3. do a bruteforce seach and check if a line withing the box x2 crosses a line 3x from the triangle.
	//   each search here will happen 2 * 3 times == 6 searches per triangle cuz we use square objects.
	//-  when the first collision of lines happen. then the same prosses occur with neighbour neighbour search untill there is no new collision of lines.


	//1. does not need that much power, the only problem is if its an insanely hughe map and the object is put in center with no onther objects.
	//   but when the objects starts to fill up. this one will take over the performence from the other two.
	//-  for this one all depends on how many triangles there are and how big the map is. the bigger and more triangles the better this will perform.
	//   then on the oposite side, if there is few triangles and the map is big this will be waaaaaay slower then both 2. and 3.


	//2. 31 operations every triangle or 31 * 4 to find faster

	//3. 36 + worst case : 14, this one will actualy never find anyone if its totaly inside another triangle






	//2.





	void CreateStartTriangle() {//Create The Two First Triangles That Is the Base Triangles Of The Entire Map

		triangle = new Triangles {
			TriangleColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)),
			A = new Vector2(_SceneDetails.TrianglePointsStartXPoint, _SceneDetails.TrianglePointsStartYPoint),//lower left
			B = new Vector2(_SceneDetails.TrianglePointsStartXPoint, _SceneDetails.TrianglePointsStartYPoint + ((_SceneDetails.TrianglePointsHeight - 1) * _SceneDetails.TrianglePointsDistance)),//upper left
			C = new Vector2(_SceneDetails.TrianglePointsStartXPoint + ((_SceneDetails.TrianglePointsWidth - 1) * _SceneDetails.TrianglePointsDistance), _SceneDetails.TrianglePointsStartYPoint + ((_SceneDetails.TrianglePointsHeight - 1) * _SceneDetails.TrianglePointsDistance))//upper right
		};

		pointMap[(int)((triangle.A.x - _SceneDetails.TrianglePointsStartXPoint) / _SceneDetails.TrianglePointsDistance), (int)((triangle.A.y - _SceneDetails.TrianglePointsStartYPoint) / _SceneDetails.TrianglePointsDistance)].triangleRef.Add(triangle);//Setting A Refrense Of This Triangle In Each CornerPoint Of The Triangle
		pointMap[(int)((triangle.B.x - _SceneDetails.TrianglePointsStartXPoint) / _SceneDetails.TrianglePointsDistance), (int)((triangle.B.y - _SceneDetails.TrianglePointsStartYPoint) / _SceneDetails.TrianglePointsDistance)].triangleRef.Add(triangle);//Setting A Refrense Of This Triangle In Each CornerPoint Of The Triangle
		pointMap[(int)((triangle.C.x - _SceneDetails.TrianglePointsStartXPoint) / _SceneDetails.TrianglePointsDistance), (int)((triangle.C.y - _SceneDetails.TrianglePointsStartYPoint) / _SceneDetails.TrianglePointsDistance)].triangleRef.Add(triangle);//Setting A Refrense Of This Triangle In Each CornerPoint Of The Triangle
		TriangleGroups.Add(triangle);

		triangle = new Triangles {
			TriangleColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)),
			A = new Vector2(_SceneDetails.TrianglePointsStartXPoint + ((_SceneDetails.TrianglePointsWidth - 1) * _SceneDetails.TrianglePointsDistance), _SceneDetails.TrianglePointsStartYPoint + ((_SceneDetails.TrianglePointsHeight - 1) * _SceneDetails.TrianglePointsDistance)),//upper right
			C = new Vector2(_SceneDetails.TrianglePointsStartXPoint + ((_SceneDetails.TrianglePointsWidth - 1) * _SceneDetails.TrianglePointsDistance), _SceneDetails.TrianglePointsStartYPoint),//lower right
			B = new Vector2(_SceneDetails.TrianglePointsStartXPoint, _SceneDetails.TrianglePointsStartYPoint),//lower left
		};

		pointMap[(int)((triangle.A.x - _SceneDetails.TrianglePointsStartXPoint) / _SceneDetails.TrianglePointsDistance), (int)((triangle.A.y - _SceneDetails.TrianglePointsStartYPoint) / _SceneDetails.TrianglePointsDistance)].triangleRef.Add(triangle);//Setting A Refrense Of This Triangle In Each CornerPoint Of The Triangle
		pointMap[(int)((triangle.B.x - _SceneDetails.TrianglePointsStartXPoint) / _SceneDetails.TrianglePointsDistance), (int)((triangle.B.y - _SceneDetails.TrianglePointsStartYPoint) / _SceneDetails.TrianglePointsDistance)].triangleRef.Add(triangle);//Setting A Refrense Of This Triangle In Each CornerPoint Of The Triangle
		pointMap[(int)((triangle.C.x - _SceneDetails.TrianglePointsStartXPoint) / _SceneDetails.TrianglePointsDistance), (int)((triangle.C.y - _SceneDetails.TrianglePointsStartYPoint) / _SceneDetails.TrianglePointsDistance)].triangleRef.Add(triangle);//Setting A Refrense Of This Triangle In Each CornerPoint Of The Triangle
		TriangleGroups.Add(triangle);
		
	}

	void Maketrianglepoints() {

		for (int i = 0; i < _SceneDetails.TrianglePointsWidth; i++) {
			for (int j = 0; j < _SceneDetails.TrianglePointsHeight; j++) {

				pointMap[i, j].xPos = _SceneDetails.TrianglePointsStartXPoint + (i * _SceneDetails.TrianglePointsDistance);//-1 == all the way left
				pointMap[i, j].yPos = _SceneDetails.TrianglePointsStartYPoint + (j * _SceneDetails.TrianglePointsDistance);//-1 == all the way down
				pointMap[i, j].triangleRef = new List<Triangles>();


			}
		}
	}










	public void CheckIfPointIsInside(Vector2 point) {

		startpoint = TriangleGroups.Count - 1;

		for (int j = startpoint; j >= 0; j--) {

			points.Clear();
			removed = TriangleGroups[j];

			if (PointInsideTriangle(removed.A.x, removed.A.y, removed.B.x, removed.B.y, removed.C.x, removed.C.y, point.x, point.y) == true) {

				points.Add(removed.A);
				points.Add(removed.B);
				points.Add(removed.C);


				pointMap[(int)((removed.A.x - _SceneDetails.TrianglePointsStartXPoint) / _SceneDetails.TrianglePointsDistance), (int)((removed.A.y - _SceneDetails.TrianglePointsStartYPoint) / _SceneDetails.TrianglePointsDistance)].triangleRef.Remove(removed);//Removing A Refrense Of This Triangle In Each CornerPoint Of The Triangle
				pointMap[(int)((removed.B.x - _SceneDetails.TrianglePointsStartXPoint) / _SceneDetails.TrianglePointsDistance), (int)((removed.B.y - _SceneDetails.TrianglePointsStartYPoint) / _SceneDetails.TrianglePointsDistance)].triangleRef.Remove(removed);//Removing A Refrense Of This Triangle In Each CornerPoint Of The Triangle
				pointMap[(int)((removed.C.x - _SceneDetails.TrianglePointsStartXPoint) / _SceneDetails.TrianglePointsDistance), (int)((removed.C.y - _SceneDetails.TrianglePointsStartYPoint) / _SceneDetails.TrianglePointsDistance)].triangleRef.Remove(removed);//Removing A Refrense Of This Triangle In Each CornerPoint Of The Triangle
				TriangleGroups.Remove(removed);

				for (int i = 0; i < points.Count - 1; i++) {
					newTriangle = new Triangles {
						TriangleColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)),
						A = points[i],
						B = points[i + 1],
						C = point
					};

					pointMap[(int)((newTriangle.A.x - _SceneDetails.TrianglePointsStartXPoint) / _SceneDetails.TrianglePointsDistance), (int)((newTriangle.A.y - _SceneDetails.TrianglePointsStartYPoint) / _SceneDetails.TrianglePointsDistance)].triangleRef.Add(newTriangle);//Setting A Refrense Of This Triangle In Each CornerPoint Of The Triangle
					pointMap[(int)((newTriangle.B.x - _SceneDetails.TrianglePointsStartXPoint) / _SceneDetails.TrianglePointsDistance), (int)((newTriangle.B.y - _SceneDetails.TrianglePointsStartYPoint) / _SceneDetails.TrianglePointsDistance)].triangleRef.Add(newTriangle);//Setting A Refrense Of This Triangle In Each CornerPoint Of The Triangle
					pointMap[(int)((newTriangle.C.x - _SceneDetails.TrianglePointsStartXPoint) / _SceneDetails.TrianglePointsDistance), (int)((newTriangle.C.y - _SceneDetails.TrianglePointsStartYPoint) / _SceneDetails.TrianglePointsDistance)].triangleRef.Add(newTriangle);//Setting A Refrense Of This Triangle In Each CornerPoint Of The Triangle
					TriangleGroups.Add(newTriangle);
				}
				newTriangle = new Triangles {
					TriangleColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)),
					A = points[0],
					B = points[points.Count - 1],
					C = point
				};

				pointMap[(int)((newTriangle.A.x - _SceneDetails.TrianglePointsStartXPoint) / _SceneDetails.TrianglePointsDistance), (int)((newTriangle.A.y - _SceneDetails.TrianglePointsStartYPoint) / _SceneDetails.TrianglePointsDistance)].triangleRef.Add(newTriangle);//Setting A Refrense Of This Triangle In Each CornerPoint Of The Triangle
				pointMap[(int)((newTriangle.B.x - _SceneDetails.TrianglePointsStartXPoint) / _SceneDetails.TrianglePointsDistance), (int)((newTriangle.B.y - _SceneDetails.TrianglePointsStartYPoint) / _SceneDetails.TrianglePointsDistance)].triangleRef.Add(newTriangle);//Setting A Refrense Of This Triangle In Each CornerPoint Of The Triangle
				pointMap[(int)((newTriangle.C.x - _SceneDetails.TrianglePointsStartXPoint) / _SceneDetails.TrianglePointsDistance), (int)((newTriangle.C.y - _SceneDetails.TrianglePointsStartYPoint) / _SceneDetails.TrianglePointsDistance)].triangleRef.Add(newTriangle);//Setting A Refrense Of This Triangle In Each CornerPoint Of The Triangle
				TriangleGroups.Add(newTriangle);
			}
		}

	}

	public void LineCheckup(Triangles triangle, Vector2 startPoint, Vector2 endPoint) {

//		Debug.Log(IsIntersecting(triangle.A, triangle.B, startPoint, endPoint));//Just To Check If True Or False, Delete After. TODO
//		Debug.Log(IsIntersecting(triangle.B, triangle.C, startPoint, endPoint));//Just To Check If True Or False, Delete After. TODO
//		Debug.Log(IsIntersecting(triangle.C, triangle.A, startPoint, endPoint));//Just To Check If True Or False, Delete After. TODO

		if (LinesIntersecting(triangle.A, triangle.B, startPoint, endPoint) == true) {//Checking If Two Lines Are Overlapping/Colliding (SIDE AB Of The Triangle With Line StartEnd)
			//TODO Ineracting With Triangle
		}else if (LinesIntersecting(triangle.B, triangle.C, startPoint, endPoint) == true) {//Checking If Two Lines Are Overlapping/Colliding (SIDE BC Of The Triangle With Line StartEnd)
			//TODO Ineracting With Triangle
		} else if (LinesIntersecting(triangle.C, triangle.A, startPoint, endPoint) == true) {//Checking If Two Lines Are Overlapping/Colliding (SIDE CA Of The Triangle With Line StartEnd)
			//TODO Ineracting With Triangle
		}

	}
	
	bool LinesIntersecting(Vector2 a, Vector2 b, Vector2 c, Vector2 d) {//Line Line Intersection Test, True If Intersecting (found this on the internet, only modified part is the thing within the "if (_denominator == 0) {" scope, which is an aditional check if parallel lines are overlapping or not : "https://gamedev.stackexchange.com/questions/26004/how-to-detect-2d-line-on-line-collision"

		_denominator = ((b.x - a.x) * (d.y - c.y)) - ((b.y - a.y) * (d.x - c.x));
		_numerator1 = ((a.y - c.y) * (d.x - c.x)) - ((a.x - c.x) * (d.y - c.y));
		_numerator2 = ((a.y - c.y) * (b.x - a.x)) - ((a.x - c.x) * (b.y - a.y));

		if (_denominator == 0) {//this is true when the line is parallel with one of the searching line

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
	
	bool PointInsideTriangle(float TriangleAX, float TriangleAY, float TriangleBX, float TriangleBY, float TriangleCX, float TriangleCY, float PointX, float PointY) {//Checking If A Point Is Inside A Triangle (Found On the Internet Nothing Modified : "https://www.youtube.com/watch?v=HYAgJN3x4GA"

		_numerator1 = ((TriangleAX * (TriangleCY - TriangleAY)) + ((PointY - TriangleAY) * (TriangleCX - TriangleAX)) - (PointX * (TriangleCY - TriangleAY))) / (((TriangleBY - TriangleAY) * (TriangleCX - TriangleAX)) - ((TriangleBX - TriangleAX) * (TriangleCY - TriangleAY)));

		_numerator2 = (PointY - TriangleAY - (_numerator1 * (TriangleBY - TriangleAY))) / (TriangleCY - TriangleAY);

		if (_numerator1 >= 0 && _numerator2 >= 0 && (_numerator1 + _numerator2) <= 1) {
			return true;
		} else {
			return false;
		}

	}




	void RemovePoints() {





	}


















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


public struct PointsForTriangles {
	public float xPos;
	public float yPos;
	public List<Triangles> triangleRef;
}