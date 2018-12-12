﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGunTurret : MonoBehaviour {


    public string EnemyTag;
    public GameObject GunBarrel;
    public GameObject Bullet;
    public float bulletSpeed;
    public float DetectionRange;

    public int Dmg;
    /// <summary>
    /// The gun will shoot every X amoun of sec
    /// </summary>
    public float ShootEvery; // Eks 5 shots every sec 5/sec

    public  List<GameObject> targets;
   [SerializeField] private GameObject primaryTarget;

    private float fireTimer;

    //  private Vector3 m_lastKnownPosition = Vector3.zero;
    private Quaternion lookAtRotation;



    void Start ()
    {
        fireTimer = 0;
        GetComponent<CircleCollider2D>().radius = DetectionRange;
	}
	

	void Update ()
    {
        TargetNearest();
        TrackTarget();
        ShootTarget();
    }


    void TargetNearest()
    {
        List<GameObject> validTargets = GetValidTargets();

        GameObject curTarget = null;
        float closestDist = 0.0f;

        for (int i = 0; i < validTargets.Count; i++)
        {
            float dist = Vector3.Distance(transform.position, validTargets[i].transform.position);

            if (!curTarget || dist < closestDist)
            {
                curTarget = validTargets[i];
                closestDist = dist;
            }
        }

        primaryTarget = curTarget;
    }


    private void TrackTarget()
    {
        if(primaryTarget != null)
        {
            Vector3 relativePos = primaryTarget.transform.position - GunBarrel.transform.position;
            var rotZ = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
            lookAtRotation = Quaternion.Euler(0, 0, rotZ);
            GunBarrel.transform.rotation = lookAtRotation;
        }
    }

    void ShootTarget()
    {
        if (!primaryTarget)
        {
            return;
        }
        else
        {
            fireTimer += Time.deltaTime;

            if (fireTimer >= ShootEvery)
            {
                 var bulletClone = Instantiate(Bullet, GunBarrel.transform.position, lookAtRotation);
                bulletClone.GetComponent<TurretBullet>().FireProjectile(GunBarrel, primaryTarget, Dmg, bulletSpeed);
                fireTimer = 0.0f;
            }
        }
    }

    public List<GameObject> GetValidTargets()
    {
        return targets;
    }

    public bool InRange(GameObject go)
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (go == targets[i])
                return true;
        }

        return false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(EnemyTag))
        {
            Debug.Log("Added Enemy");
            targets.Add(other.gameObject); 
        } 
    }

    void OnTriggerExit2D(Collider2D other)
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (other.gameObject == targets[i])
            {
                Debug.Log("Removed Enemy");
                targets.Remove(other.gameObject);
                return;
            }
        }
    }


}
