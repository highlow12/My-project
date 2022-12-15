using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RnGMapMake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    { 
        mapCreate.Instance.Make_RnG_Map(this.gameObject);
    }

    // Update is called once per frame
   
}
