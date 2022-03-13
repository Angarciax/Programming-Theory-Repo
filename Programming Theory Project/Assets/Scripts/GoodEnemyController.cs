using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodEnemyController : EnemyController // INHERITANCE
{
    protected override void MoveEnemy()  // POLYMORPHISM
    {
        Vector3 pointAwayPlayer;
        pointAwayPlayer = transform.position - playerObject.transform.position ;
        gameObject.GetComponent<Rigidbody>().AddForce(pointAwayPlayer * forceSpeed * Time.deltaTime, ForceMode.Impulse);
    }

}
