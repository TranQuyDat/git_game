using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace section3
{
    public class GamePause : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txtscore;


        // Start is called before the first frame update
        public void Continue()
        {
            GameManager.instance.btnContinue();
        }

        public void Home()
        {
            GameManager.instance.btnHome();
        }

       
        public void settxt(int score)
        {
            txtscore.text = "SCORE " + score.ToString();
        }
    }
}