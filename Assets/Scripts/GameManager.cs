﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[AddComponentMenu("MyGame/GameManager")]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform m_canvas_main;
    public Transform m_canvas_gameover;
    public Text m_text_score;
    public Text m_text_best;
    public Text m_text_life;

    protected int m_score = 0;
    public static int m_highscore = 0;
    protected Player m_player;

    public AudioClip m_musicClip;
    protected AudioSource m_Audio;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        m_Audio = this.gameObject.AddComponent<AudioSource>();
        m_Audio.clip = m_musicClip;
        m_Audio.loop = true;
        m_Audio.Play();

        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        m_text_score = m_canvas_main.transform.Find("Text_score").GetComponent<Text>();
        m_text_life = m_canvas_main.transform.Find("Text_life").GetComponent<Text>();
        m_text_best = m_canvas_main.transform.Find("Text_best").GetComponent<Text>();
  
        m_text_score.text = $"Score {m_score}";
        m_text_best.text = $"Best Score {m_highscore}";
        m_text_life.text = $"Life {m_player.m_life}";

        var restart_button = m_canvas_gameover.transform.Find("Button_restart").GetComponent<Button>();

        restart_button.onClick.AddListener(delegate(){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });

        m_canvas_gameover.gameObject.SetActive(false);
   }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int point){
        m_score += point;

        if (m_highscore < m_score){
            m_highscore = m_score;
        }
        
        m_text_score.text = $"Score {m_score}";
        m_text_best.text = $"Best Score {m_highscore}";
    }

    public void ChangeLife(int life){
        
        m_text_life.text = $"Life {life}";

        if (life <= 0){
            m_canvas_gameover.gameObject.SetActive(true);
        }
    }
}
