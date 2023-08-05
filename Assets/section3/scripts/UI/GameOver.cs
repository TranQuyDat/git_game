using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace section3
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txtscore;
        [SerializeField] private TextMeshProUGUI txtwin;
        
        
        // Start is called before the first frame update
        void Start()
        {
           
        }
        public void txtScore(int score)
        {
            txtscore.text = "Score: " + score.ToString();
        }

        public void settxtwin(bool iswin)
        {
            if (iswin)
                txtwin.text = "YOU WIN";
            else
                txtwin.text = "YOU LOSE";
        }

        public void Home()
        {
            GameManager.instance.btnHome();
        }



    }
}