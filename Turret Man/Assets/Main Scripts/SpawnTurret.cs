using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTurret : InventoryItem {

    //public GameObject SpawnPoint;
    public GameObject GunTurretPrefab;
    public float offset;

    public int Cost;

   [SerializeField] Camera cam;

    [SerializeField] private bool canPlaceTurret;
    [SerializeField] GameObject GhostBox;
    [SerializeField] Animator playerAnimator;
    // Use this for initialization

    float x;
	float y;

	void Start ()
    {
        canPlaceTurret = true;
        cam = Camera.main;
        playerAnimator = GetComponent<Animator>();

		//if(ShowTestBox == true) {
		//	GhostBox.SetActive(true);
		//} else {
		//	GhostBox.SetActive(false);
		//}



	}
	
	// Update is called once per frame
	void Update ()
    {
		
         GhostBox.transform.position = SnappingSystem();

      /*  if (Input.GetMouseButtonUp(1) && canPlaceTurret && CanPlayerPayForMachine())
        {
            playerAnimator.SetTrigger("Build");
            Instantiate(GunTurretPrefab, SnappingSystem(), Quaternion.identity);
            GameManager.Instance.PlayerResources.CurrentResources -= Cost;
        }*/

    }
    

    private Vector3 SnappingSystem()
    {
        var mousPos = cam.ScreenToWorldPoint(Input.mousePosition);

        var playerX = Mathf.Floor((transform.position.x) / 0.5f) * 0.5f;//Player Snapped Node Position X
        var playerY = Mathf.Floor((transform.position.y - offset) / 0.5f) * 0.5f;//Player Snapped Node Position Y


        if (transform.position.x / 0.5f % 1 < 0.5f)
        {//Increasing The Distance The Player Can Click To The Left (-1 == 2 nodes)
            x = Mathf.Clamp(Mathf.Floor((mousPos.x) / 0.5f) * 0.5f, playerX - 1f, playerX + 0.5f);//Finding Mouse Node Position X, Then Clamping It To Find The Most Left And Most Right Position The Mouse Can Be At
        }
        else
        {//Increasing The Distance The Player Can Click To The Right (+1 == 2 nodes)
            x = Mathf.Clamp(Mathf.Floor((mousPos.x) / 0.5f) * 0.5f, playerX - 0.5f, playerX + 1f);//Finding Mouse Node Position X, Then Clamping It To Find The Most Left And Most Right Position The Mouse Can Be At
        }

        if ((transform.position.y - offset) / 0.5f % 1 < 0.5f)
        {//Increasing The Distance The Player Can Click Down (-1 == 2 nodes)
            y = Mathf.Clamp(Mathf.Floor((mousPos.y) / 0.5f) * 0.5f, playerY - 0.5f, playerY + 1f);//Finding Mouse Node Position Y, Then Clamping It To Find The Most Left And Most Right Position The Mouse Can Be At
        }
        else
        {//Increasing The Distance The Player Can Click Up (+1 == 2 nodes)
            y = Mathf.Clamp(Mathf.Floor((mousPos.y) / 0.5f) * 0.5f, playerY - 0.5f, playerY + 1f);//Finding Mouse Node Position Y, Then Clamping It To Find The Most Left And Most Right Position The Mouse Can Be At
        }
        //	Debug.Log("X = ( " + x + ") Y = (" + y + ")");

        return new Vector3(x + 0.25f, y + 0.25f, 0);
    }

    private bool CanPlayerPayForMachine()
    {
        //return GameManager.Instance.PlayerResources.CurrentResources >= cost;

        if (GameManager.Instance.PlayerResources.CurrentResources >= Cost)
        {
            return true;
        }
        else
        {
            Debug.Log("Player can NOT pay for " + GunTurretPrefab.name);
            return false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       // Debug.Log("LOCKED");
        canPlaceTurret = false;  
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
       // Debug.Log("FREE");
        canPlaceTurret = true;
    }

   /* private void OnTriggerStay2D(Collider2D collision)
    {
         Debug.Log("LOCKED === " + collision.name );
        canPlaceTurret = false;
    }*/

    public override void Action()
    {
        if(canPlaceTurret && CanPlayerPayForMachine())
        {
            playerAnimator.SetTrigger("Build");
            Instantiate(GunTurretPrefab, SnappingSystem(), Quaternion.identity);
            GameManager.Instance.PlayerResources.CurrentResources -= Cost;
        }
        else
        {
            Debug.LogWarning("Turret Blocked (" + canPlaceTurret + ") Can Pay For Turret( " + CanPlayerPayForMachine() + ") NEEED UI!!");
        }

     }
}
