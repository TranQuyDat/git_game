using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace section3
{
    public class BackgroundControler : MonoBehaviour
    {
        [SerializeField] private Material bigstar;
        [SerializeField] private Material mediumstar;
        [SerializeField] private Material nebula;

        [SerializeField] private float scrollspeed_bigstar;
        [SerializeField] private float scrollspeed_mediumstar;
        [SerializeField] private float scrollspeed_nebula;

        private int MainTextid;




        // Start is called before the first frame update
        void Start()
        {
            MainTextid = Shader.PropertyToID("_MainTex");
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 offset = bigstar.GetTextureOffset(MainTextid);
            offset += new Vector2(0, scrollspeed_bigstar * Time.deltaTime);
            bigstar.SetTextureOffset(MainTextid, offset);

            offset = mediumstar.GetTextureOffset(MainTextid);
            offset += new Vector2(0, scrollspeed_mediumstar * Time.deltaTime);
            mediumstar.SetTextureOffset(MainTextid, offset);

            offset = nebula.GetTextureOffset(MainTextid);
            offset += new Vector2(0, scrollspeed_nebula * Time.deltaTime);
            nebula.SetTextureOffset(MainTextid, offset);
        }
    }
}