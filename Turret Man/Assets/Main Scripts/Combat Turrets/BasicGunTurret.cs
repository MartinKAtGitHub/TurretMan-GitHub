using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGunTurret : MonoBehaviour {
    
    public GameObject Target;
    public float Range;
    public float TurretRotationSpeed;
    public int Dmg;
    public int RateOfFire; // Eks 5 shots every sec 5/sec

    private Vector3 m_lastKnownPosition = Vector3.zero;
    Quaternion m_lookAtRotation;

    void Start ()
    {
		
	}
	

	void Update ()
    {
        TrackTarget();
    }



    private void TrackTarget()
    {
        Vector3 relativePos = Target.transform.position - transform.position;
        var rotZ = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
