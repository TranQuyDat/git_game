using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace section3
{
    public class Home : MonoBehaviour
    {
       
        // Start is called before the first frame update
        void Start()
        {
           
            
        }

        public void Play()
        {
            GameManager.instance.btnPlay();
        }
    }
}