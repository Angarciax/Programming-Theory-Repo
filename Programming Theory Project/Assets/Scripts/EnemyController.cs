using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem enemyExplosionParticle;
    protected GameObject playerObject;
    private int lifeTime = 20 ;
    private float m_forceSpeed = 1.0f; // ENCAPSULATION
    public float forceSpeed  // ENCAPSULATION
    {
        get { return m_forceSpeed;  } // getter returns backing field
        set
        {
            if (value < 5)
                m_forceSpeed = 5;
            else if (value > 10)
            {
                m_forceSpeed = 10;
            }
            else
                m_forceSpeed = value;
        } // setter uses backing field
    }

    void Start()
    {
        Debug.Log("Setting playerObject");
        playerObject = GameObject.FindGameObjectWithTag("Player");
        if( playerObject == null ) Debug.Log("oH!. is null");
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
    }

    // move : By default, enemy moves randmoly when stopped
    protected virtual void MoveEnemy()  // POLYMORPHISM
    {
        float speedx = gameObject.GetComponent<Rigidbody>().velocity.x;
        float speedz = gameObject.GetComponent<Rigidbody>().velocity.z;
        Debug.Log("MoveEnemy base: sppedx=" + speedx + " speedz=" + speedz + " force_speed=" + m_forceSpeed);
        if ( Mathf.Abs( speedx ) < 0.05 && Mathf.Abs(speedz) < 0.05  )
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3( Random.Range(-1,1), 0,Random.Range(-1,1))*m_forceSpeed, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            ExplosionEnemy();
            Destroy(gameObject);
        }
        
    }

    private void ExplosionEnemy()
    {
        ParticleSystem explosion = Instantiate(enemyExplosionParticle);
        explosion.transform.position = transform.position;
        explosion.Play();
    }
}
