using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour {

    /// <summary>
    /// How far the bullet can go before self distruction;
    /// </summary>
    public float MaxBulletTravelDistance;
    public GameObject Target;
    public BasicGunTurret MyTurret;

    private int dmg;
    private float bulletSpeed;

    Vector3 bulletDirection;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position += bulletDirection * (bulletSpeed * Time.deltaTime);
	}

    public void FireProjectile(GameObject launcher, GameObject target, int damage, float bulletSpeed)
    {
        if (launcher && target)
        {
            bulletDirection = (target.transform.position - launcher.transform.position).normalized;
            //  m_fired = true;
            //  m_launcher = launcher;
            Target = target;
            dmg = damage;
            this.bulletSpeed = bulletSpeed;
             Destroy(gameObject, 10.0f);
        }
    }

     void OnCollisionEnter2D(Collision2D other)
     {
        if (other.gameObject.tag == Target.gameObject.tag)
        {
            var enemyStatus = other.gameObject.GetComponent<EnemyHealthSystem>();
            enemyStatus.TakeDmg(dmg);
            //if(enemyStatus.IsEnemyDead())
            //{
            //    MyTurret.RemoveTargetFromValidList(other.gameObject);
            //}
           // Debug.Log("Target Hit = " + other.gameObject.name);
        }

        if (other.gameObject.GetComponent<TurretBullet>() == null)
        {
            Destroy(gameObject);
        }
    }
}
