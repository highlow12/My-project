using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapCreate : MonoBehaviour
{
    [SerializeField]
    Transform parent;

    [SerializeField]
    GameObject StartMap;

    [SerializeField]
    GameObject[] Corners;

    [SerializeField]
    GameObject[] Runs;

    public int minRuns = 5;
    public int maxRuns = 15;

    // Start is called before the first frame update
    void Start()
    {
        MakeMap();
    }

    void MakeMap()
    {
        float Xtrs;
        float Ytrs;

        GameObject nowmap;
        
        //how mant maps
        int maps = Random.Range(minRuns, maxRuns + 1);

        //make start map
        var startmap = Instantiate(StartMap, parent);
        startmap.transform.position = Vector3.zero;

        var Cornerscale = startmap.transform.localScale;

        #region make Run'n'Gun map
        nowmap = Runs[Random.Range(0, Runs.Length)];
        nowmap = Instantiate(nowmap, parent);

        var nowmapscale = nowmap.transform.localScale;
        #endregion

        Xtrs = nowmapscale.x /2 - 0.5f;
        Ytrs = Cornerscale.y / 2 + nowmapscale.y / 2;

        nowmap.transform.position = new Vector3(Xtrs, Ytrs);
        

        var RnGscale = nowmap.transform.localScale;

        #region make corner map
        nowmap = Corners[Random.Range(0, Corners.Length)];
        nowmap = Instantiate(nowmap, parent);

        nowmapscale = nowmap.transform.localScale;
        Xtrs = nowmapscale.x / 2 - 0.5f + RnGscale.x;

        nowmap.transform.position = new Vector3(Xtrs, Ytrs);
        #endregion
    }
}
