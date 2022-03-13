using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadEnemyController : EnemyController // INHERITANCE
{
    protected override void MoveEnemy()  // POLYMORPHISM
    {
        Vector3 pointToPlayer;
        pointToPlayer = playerObject.transform.position - transform.position ;
        gameObject.GetComponent<Rigidbody>().AddForce(pointToPlayer * forceSpeed * Time.deltaTime, ForceMode.Impulse);
    }
}
