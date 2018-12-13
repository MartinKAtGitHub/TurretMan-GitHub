using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTurret : MonoBehaviour {

    public GameObject SpawnPoint;
    public GameObject GunTurretPrefab;
    public float offset;
   [SerializeField] Camera cam;

    [SerializeField]GameObject GhostBox;
	// Use this for initialization
	void Start ()
    {
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update ()
    {
        GhostBoxCalc();

        if (Input.GetMouseButtonUp(0))
        {
            var mousPos = cam.ScreenToWorldPoint(Input.mousePosition);


           // var test = (mousPos - transform.position).normalized;
            
           // Debug.Log(test *= 0.5f);


           var x = Mathf.Clamp(mousPos.x, Mathf.Round(transform.position.x / 0.5f ) * 0.5f - 0.26f, Mathf.Round(transform.position.x / 0.5f) * 0.5f + 0.26f) ;
           var y = Mathf.Clamp(mousPos.y, Mathf.Round((transform.position.y - offset )/ 0.5f )* 0.5f  - 0.26f, Mathf.Round((transform.position.y - offset) / 0.5f )* 0.5f  + 0.26f);

            Debug.Log("X = ( " + x + ") Y = (" + y +")");

            //Debug.Log(mousPos);
            //var SpawnPos = new Vector3( Mathf.RoundToInt(SpawnPoint.transform.position.x /0.5f) * 0.5f + 0.25f, Mathf.RoundToInt(SpawnPoint.transform.position.y/ 0.5f) * 0.5f + 0.25f, 0);

          
            var SpawnPos = new Vector3( Mathf.Round(x / 0.5f) * 0.5f  , Mathf.Round(y / 0.5f )* 0.5f, 0);
            
            //Debug.Log(SpawnPos.x + " | " + SpawnPos.y);
           // var SpawnPos = new Vector3(x,y);

            Instantiate(GunTurretPrefab, SpawnPos, Quaternion.identity);
            Debug.Log("SpawnTurret");
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

        var x = Mathf.Clamp(mousPos.x, Mathf.Round(transform.position.x / 0.5f) * 0.5f - 0.26f, Mathf.Round(transform.position.x / 0.5f) * 0.5f + 0.26f);
        var y = Mathf.Clamp(mousPos.y, Mathf.Round((transform.position.y - offset) / 0.5f) * 0.5f - 0.26f, Mathf.Round((transform.position.y - offset) / 0.5f) * 0.5f + 0.26f);

        Debug.Log("X = ( " + x + ") Y = (" + y + ")");

        GhostBox.transform.position = new Vector3(Mathf.Round(x / 0.5f) * 0.5f , Mathf.Round(y / 0.5f) * 0.5f, 0);

    }
}
