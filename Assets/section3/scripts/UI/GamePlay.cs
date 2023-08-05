using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace section3
{
    public class GamePlay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txtscore;
        [SerializeField] private Image imghpbar;


        // Start is called before the first frame update
        void Start()
        { 
        }
        private void OnEnable()
        {
            GameManager.instance.OnscoreChange += OnscoreChange;
            SpawnManager.instance.getPlayer.onhpchange += onhpchange;
        }
        private void OnDisable()
        {
            GameManager.instance.OnscoreChange -= OnscoreChange;
            SpawnManager.instance.getPlayer.onhpchange -= onhpchange;
        }

        private void onhpchange(int curhp, int maxhp)
        {
            imghpbar.fillAmount = curhp * 1f / maxhp;
        }

        public void Pause()
        {
            GameManager.instance.btnPause();
        }
        private void OnscoreChange(int score)
        {
            txtscore.text = "SCORE " + score;
        }
    }
}