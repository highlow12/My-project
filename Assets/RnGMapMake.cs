using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RnGMapMake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
//Collider2D hit = Physics2D.OverlapBox(transform.position + Vector3.up * (transform.localScale.y + 0.5f), new Vector2(01f, 1f),0); 

        //if (hit == null)
        //{
            mapCreate.Instance.EnQ_RnGMap(new mapCreate.makemapInfo(gameObject, 1));

            
        //}
        //hit = Physics2D.OverlapBox(transform.position + Vector3.down * (transform.localScale.y + 0.5f), new Vector2(1f, 1f), 0);
        //if (hit == null)
            //{
                mapCreate.Instance.EnQ_RnGMap(new mapCreate.makemapInfo(gameObject, -1));
            //}
        

        // Update is called once per frame

    }
}
