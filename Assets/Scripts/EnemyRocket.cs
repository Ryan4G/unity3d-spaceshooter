using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("MyGame/EnemyRocket")]
public class EnemyRocket : Rocket
{
    void OnTriggerEnter(Collider other){
        if (other.tag != "Player"){
            return;
        }
        
        Destroy(this.gameObject);
    }
}
