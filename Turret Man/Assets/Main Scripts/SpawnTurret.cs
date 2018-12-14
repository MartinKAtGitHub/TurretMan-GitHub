using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTurret : MonoBehaviour {

    public GameObject SpawnPoint;
    public GameObject GunTurretPrefab;
    public float offset;
   [SerializeField] Camera cam;

	public bool ShowTestBox = true;
    [SerializeField]GameObject GhostBox;
	// Use this for initialization

	float x;
	float y;

	void Start ()
    {
        cam = Camera.main;

		if(ShowTestBox == true) {
			GhostBox.SetActive(true);
		} else {
			GhostBox.SetActive(false);
		}


	}
	
	// Update is called once per frame
	void Update ()
    {
		if(ShowTestBox == true) {
			GhostBoxCalc();
		}

        if (Input.GetMouseButtonUp(0))
        {
			var mousPos = cam.ScreenToWorldPoint(Input.mousePosition);

			var playerX = Mathf.Floor((transform.position.x) / 0.5f) * 0.5f;//Player Snapped Node Position X
			var playerY = Mathf.Floor((transform.position.y - offset) / 0.5f) * 0.5f;//Player Snapped Node Position Y (- offset) 
	
			if(transform.position.x / 0.5f % 1 < 0.5f) {//Increasing The Distance The Player Can Click To The Left (-1 == 2 nodes)
				x = Mathf.Clamp(Mathf.Floor((mousPos.x) / 0.5f) * 0.5f, playerX - 1f, playerX + 0.5f);//Finding Mouse Node Position X, Then Clamping It To Find The Most Left And Most Right Position The Mouse Can Be At
			} else {//Increasing The Distance The Player Can Click To The Right (+1 == 2 nodes)
				x = Mathf.Clamp(Mathf.Floor((mousPos.x) / 0.5f) * 0.5f, playerX - 0.5f, playerX + 1f);//Finding Mouse Node Position X, Then Clamping It To Find The Most Left And Most Right Position The Mouse Can Be At
			}

			if ((transform.position.y - offset) / 0.5f % 1 < 0.5f) {//Increasing The Distance The Player Can Click Down (-1 == 2 nodes)
				y = Mathf.Clamp(Mathf.Floor((mousPos.y) / 0.5f) * 0.5f, playerY - 0.5f, playerY + 1f);//Finding Mouse Node Position Y, Then Clamping It To Find The Most Left And Most Right Position The Mouse Can Be At
			} else {//Increasing The Distance The Player Can Click Up (+1 == 2 nodes)
				y = Mathf.Clamp(Mathf.Floor((mousPos.y) / 0.5f) * 0.5f, playerY - 0.5f, playerY + 1f);//Finding Mouse Node Position Y, Then Clamping It To Find The Most Left And Most Right Position The Mouse Can Be At
			}

			Instantiate(GunTurretPrefab, new Vector3(x + 0.25f, y + 0.25f, 0), Quaternion.identity);
        //    Debug.Log("SpawnTurret");
        }

        if(Input.GetMouseButtonUp(1))
        {
            var SpawnPos = new Vector3( Mathf.RoundToInt(SpawnPoint.transform.position.x /0.5f) * 0.5f + 0.25f, Mathf.RoundToInt(SpawnPoint.transform.position.y/ 0.5f) * 0.5f + 0.25f, 0);
            Instantiate(GunTurretPrefab, SpawnPos, Quaternion.identity);

        }



    }


    private void GhostBoxCalc()
    {
		var mousPos = cam.ScreenToWorldPoint(Input.mousePosition);

		var playerX = Mathf.Floor((transform.position.x) / 0.5f) * 0.5f;//Player Snapped Node Position X
		var playerY = Mathf.Floor((transform.position.y - offset) / 0.5f) * 0.5f;//Player Snapped Node Position Y


		if (transform.position.x / 0.5f % 1 < 0.5f) {//Increasing The Distance The Player Can Click To The Left (-1 == 2 nodes)
			x = Mathf.Clamp(Mathf.Floor((mousPos.x) / 0.5f) * 0.5f, playerX - 1f, playerX + 0.5f);//Finding Mouse Node Position X, Then Clamping It To Find The Most Left And Most Right Position The Mouse Can Be At
		} else {//Increasing The Distance The Player Can Click To The Right (+1 == 2 nodes)
			x = Mathf.Clamp(Mathf.Floor((mousPos.x) / 0.5f) * 0.5f, playerX - 0.5f, playerX + 1f);//Finding Mouse Node Position X, Then Clamping It To Find The Most Left And Most Right Position The Mouse Can Be At
		}

		if ((transform.position.y - offset) / 0.5f % 1 < 0.5f) {//Increasing The Distance The Player Can Click Down (-1 == 2 nodes)
			y = Mathf.Clamp(Mathf.Floor((mousPos.y) / 0.5f) * 0.5f, playerY - 0.5f, playerY + 1f);//Finding Mouse Node Position Y, Then Clamping It To Find The Most Left And Most Right Position The Mouse Can Be At
		} else {//Increasing The Distance The Player Can Click Up (+1 == 2 nodes)
			y = Mathf.Clamp(Mathf.Floor((mousPos.y) / 0.5f) * 0.5f, playerY - 0.5f, playerY + 1f);//Finding Mouse Node Position Y, Then Clamping It To Find The Most Left And Most Right Position The Mouse Can Be At
		}
		//	Debug.Log("X = ( " + x + ") Y = (" + y + ")");

		GhostBox.transform.position = new Vector3(x + 0.25f, y + 0.25f, 0);//Visual TestBox

	}
}
