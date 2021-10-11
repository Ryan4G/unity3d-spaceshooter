using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("MyGame/EnemySpawn")]
public class EnemySpawn : MonoBehaviour
{
    public Transform m_enemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemy(){
        while(true){
            yield return new WaitForSeconds(Random.Range(5, 10));
            Instantiate(m_enemyPrefab, transform.position, Quaternion.identity);
        }
    }

    void OnDrawGizmos(){
        Gizmos.DrawIcon(this.transform.position, "item.png", true);
    }
}
