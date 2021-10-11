using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("MyGame/Player")]
public class Player : MonoBehaviour
{
    public int m_life = 3;

    public float m_speed = 1;

    public Transform m_rocket;

    public AudioClip m_shootClip;

    public Transform m_explosionFX;

    public LayerMask m_inputMask;

    protected AudioSource m_audio;
    protected Vector3 m_targetPos;
    private Transform m_transform;

    private float m_rocketTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.transform;
        m_audio = this.GetComponent<AudioSource>();
        m_targetPos = this.m_transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTo();

        float movev = 0;
        float moveh = 0;

        if(Input.GetKey(KeyCode.UpArrow)){
            movev += m_speed * Time.deltaTime;
        }
        
        if(Input.GetKey(KeyCode.DownArrow)){
            movev -= m_speed * Time.deltaTime;
        }
        
        if(Input.GetKey(KeyCode.LeftArrow)){
            moveh -= m_speed * Time.deltaTime;
        }
        
        if(Input.GetKey(KeyCode.RightArrow)){
            moveh += m_speed * Time.deltaTime;
        }

        m_rocketTimer -= Time.deltaTime;

        if (m_rocketTimer <= 0){
            m_rocketTimer = 0.1f;
            
            if(Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)){
                Instantiate(m_rocket, m_transform.position, m_transform.rotation);

                m_audio.PlayOneShot(m_shootClip);
            }
        }

        m_transform.Translate(new Vector3(moveh, 0, movev));
    }

    void OnTriggerEnter(Collider other){
        if (other.tag == "Enemy" || other.tag == "EnemyRocket"){
            m_life -= 1;

            GameManager.Instance.ChangeLife(m_life);

            if (m_life <= 0){
                Instantiate(m_explosionFX, m_transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }

    void MoveTo(){
        if (Input.GetMouseButton(0)){
            Vector3 ms = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(ms);
            RaycastHit hitinfo;
            bool iscast = Physics.Raycast(ray, out hitinfo, 1000, m_inputMask);

            if(iscast){
                m_targetPos = hitinfo.point;
                
            }

            Vector3 pos = Vector3.MoveTowards(m_transform.position, m_targetPos, m_speed * Time.deltaTime);

            this.m_transform.position = pos;
        }
    }
}
