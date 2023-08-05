using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace section3
{

    public enum gamestate
    {
        Null,
        Home,
        gameplay,
        gamePause,
        gameOver,

    }
    public class GameManager : MonoBehaviour
    {
        private static GameManager m_instance;
        public static GameManager instance
        {
            get
            {
                if (m_instance == null)
                    m_instance = FindObjectOfType<GameManager>();
                return m_instance;
            }

        }

        [SerializeField] private Home home;
        [SerializeField] private GamePlay gameplay;
        [SerializeField] private GamePause gamepause;
        [SerializeField] private GameOver gameover;
        [SerializeField] private wavedata[] wave;
        private int cur_waveindex;
        private gamestate state = gamestate.Null;
        private int score = 0;
       
        
        public Action<int> OnscoreChange;

        private void Awake()
        {
            if (m_instance == null)
                m_instance = this;
            else if (m_instance != this)
                Destroy(gameObject);
        }
        void Start()
        {
            gameplay.gameObject.SetActive(false);
            gamepause.gameObject.SetActive(false);
            gameover.gameObject.SetActive(false);
            home.gameObject.SetActive(false);
            setstate(gamestate.Home);
        }

        // Update is called once per frame


        public void addscore(int score)
        {
            this.score += score;
            if (OnscoreChange != null)
                OnscoreChange(this.score);
            if(SpawnManager.instance.isclear())
            {
                cur_waveindex++;
                if (cur_waveindex == wave.Length)
                    gameOver(true);
                else
                    SpawnManager.instance.startbattle(wave[cur_waveindex]);
            }
            
        }
        public int getscore()
        {
            return this.score;
        }

        void setstate(gamestate state)
        {
            
            home.gameObject.SetActive(state == gamestate.Home);
            gameplay.gameObject.SetActive(state == gamestate.gameplay);
            gamepause.gameObject.SetActive(state == gamestate.gamePause);
            gameover.gameObject.SetActive(state == gamestate.gameOver);

            if (state == gamestate.Home)
            {
                Audiomanager.instance.playHomeMusic();
            }
            else Audiomanager.instance.playBattlrmusic();
        }
    
        
       
        public gamestate getstate()
        {
            return this.state ;
        }

        public void btnHome()
        {
            state = gamestate.Home;
            setstate(state);
            SpawnManager.instance.Clear();
            
        }

        public void gameOver(bool win)
        {
            gameover.settxtwin(win);
            gameover.txtScore(this.score);
            setstate(gamestate.gameOver);
            
        }

        public void btnPause()
        {
            state = gamestate.gamePause;
            gamepause.settxt(score);
            setstate(state);
        }

        public void btnContinue()
        {
            state = gamestate.gameplay;
            setstate(state);
        }

        public void btnPlay()
        {
            cur_waveindex = 0;
            state = gamestate.gameplay;
            SpawnManager.instance.startbattle(wave[cur_waveindex]);
            setstate(state);
            score = 0;
            if (OnscoreChange != null)
                OnscoreChange(score);
        }


        public bool isActive()
        {
            return this.state != gamestate.gameplay;
        }
    }
}