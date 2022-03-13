using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forceMove = 5;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(horizontalMove*Time.deltaTime,0,verticalMove*Time.deltaTime) * forceMove, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision with " + collision.gameObject.tag);
        if(collision.gameObject.CompareTag("Key"))
        {
            gameManager.gotKey = true;
            Destroy(GameObject.FindGameObjectWithTag("Key"));
            Debug.Log("GotKey Collision with " + collision.gameObject.tag);
        }
        else if (collision.gameObject.CompareTag("GoodEnemy"))
        {
            forceMove *= 1.5f;
            Debug.Log("double speed Collision with " + collision.gameObject.tag);
        }
        else if (collision.gameObject.CompareTag("BadEnemy"))
        {
            gameManager.gameOver = true;
            Destroy( gameObject );
            GameObject goodEnemyLeft = GameObject.FindGameObjectWithTag("GoodEnemy");
            if (goodEnemyLeft != null)
                Destroy(goodEnemyLeft);
            GameObject badEnemyLeft = GameObject.FindGameObjectWithTag("BadEnemy");
            if (badEnemyLeft != null)
                Destroy(badEnemyLeft);
            Debug.Log("Game Over Collision with " + collision.gameObject.tag);
        }
    }
}
