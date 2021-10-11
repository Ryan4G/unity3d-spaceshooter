using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("MyGame/SuperEnemy")]
public class SuperEnemy : Enemy
{
    public Transform m_rocket;
    protected float m_fireTime = 1;
    protected Transform m_player;
    protected override void UpdateMove()
    {
        m_fireTime -= Time.deltaTime;
        if (m_fireTime <= 0){
            m_fireTime = 1;

            if (m_player != null){
                Vector3 relativePos = m_player.position - m_transform.position;
                Instantiate(m_rocket, m_transform.position, Quaternion.LookRotation(relativePos));

                if (m_audio != null){
                    m_audio.PlayOneShot(m_shootClip);
                }
            }
            else{
                GameObject obj = GameObject.FindGameObjectWithTag("Player");

                if (obj != null){
                    m_player = obj.transform;
                }
            }
        }

        this.transform.Translate(new Vector3(0, 0, -m_speed * Time.deltaTime));
    }
}
