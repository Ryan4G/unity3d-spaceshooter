using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("MyGame/Enemy")]
public class Enemy : MonoBehaviour
{

    public AudioClip m_shootClip;

    public Transform m_explosionFX;
    
    public int m_point = 10;
    public float m_speed = 1;
    public float m_life = 10;
    protected float m_rotSpeed = 30;

    protected AudioSource m_audio;
    
    protected Transform m_transform;

    internal Renderer m_renderer;

    internal bool m_isActive = false;

    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.transform;
        m_renderer = this.GetComponent<Renderer>();
        m_audio = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMove();

        if (m_isActive && !this.m_renderer.isVisible){
            Destroy(this.gameObject);
        }
    }

    protected virtual void UpdateMove(){
        float rx = Mathf.Sin(Time.time) * Time.deltaTime;

        m_transform.Translate(new Vector3(rx, 0, -m_speed * Time.deltaTime));
    }

    void OnBecameVisible() {
        m_isActive = true;
    }

    void OnTriggerEnter(Collider other){
        if (other.tag == "PlayerRocket"){
            Rocket rocket = other.GetComponent<Rocket>();

            if (rocket != null){
                m_life -= rocket.m_power;

                if (m_life == 0){

                    GameManager.Instance.AddScore(m_point);
                    
                    Instantiate(m_explosionFX, m_transform.position, Quaternion.identity);
                
                    Destroy(this.gameObject);
                }
            }
        }
        else if(other.tag == "Player"){
            m_life = 0;

            Instantiate(m_explosionFX, m_transform.position, Quaternion.identity);
                
            Destroy(this.gameObject);
        }
    }
}
