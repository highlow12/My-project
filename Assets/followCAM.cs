using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCAM : MonoBehaviour
{
    
    public Transform Player;

    Vector3 trans_PC;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void LateUpdate()
    {

        Vector3 pos = new Vector3(Player.position.x, transform.position.y, transform.position.z);
        transform.position = pos;
    }
}
