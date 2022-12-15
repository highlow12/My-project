using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerMapMake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        mapCreate.Instance.Make_Corner_Map(gameObject);
    }

    
}
