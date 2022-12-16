using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    public int minRuns = 9;
    public int maxRuns = 15;

    int maxmaps;
    int mapcount;
    //RaycastHit2D hit;
    //게임매니저의 인스턴스를 담는 전역변수(static 변수이지만 이해하기 쉽게 전역변수라고 하겠다).
    //이 게임 내에서 게임매니저 인스턴스는 이 instance에 담긴 녀석만 존재하게 할 것이다.
    //보안을 위해 private으로.
    private static mapCreate instance = null;

    void Awake()
    {
        if (null == instance)
        {
            //이 클래스 인스턴스가 탄생했을 때 전역변수 instance에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
            instance = this;

            //씬 전환이 되더라도 파괴되지 않게 한다.
            //gameObject만으로도 이 스크립트가 컴포넌트로서 붙어있는 Hierarchy상의 게임오브젝트라는 뜻이지만, 
            //나는 헷갈림 방지를 위해 this를 붙여주기도 한다.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //만약 씬 이동이 되었는데 그 씬에도 Hierarchy에 mapCreate이 존재할 수도 있다.
            //그럴 경우엔 이전 씬에서 사용하던 인스턴스를 계속 사용해주는 경우가 많은 것 같다.
            //그래서 이미 전역변수인 instance에 인스턴스가 존재한다면 자신(새로운 씬의 mapCreate)을 삭제해준다.
            Destroy(this.gameObject);
        }
    }

    //게임 매니저 인스턴스에 접근할 수 있는 프로퍼티. static이므로 다른 클래스에서 맘껏 호출할 수 있다.
    public static mapCreate Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        maxmaps = Random.Range(minRuns, maxRuns);
        //Collider2D hit = Physics2D.OverlapBox(transform.position + Vector3.up, new Vector2(0.7f, 0.7f), 0);

        //if (hit == null)
        // {
        //MakeMap();
        //}
        Make_Start_Map();
        
    }


    GameObject beforemap;
    void MakeMap()
    {
        

        //how mant maps
        int maps = Random.Range(minRuns, maxRuns + 1);

        //make start map
        var startmap = Instantiate(StartMap, parent);
        startmap.transform.position = Vector3.zero;

        var Cornerscale = startmap.transform.localScale;

        beforemap = startmap;

        #region make Run'n'Gun map
        var nowmap = Runs[Random.Range(0, Runs.Length)];
        nowmap = Instantiate(nowmap, parent);

        var nowmapscale = nowmap.transform.localScale;
        #endregion

        var Xtrs = nowmapscale.x / 2 - 0.5f;
        var Ytrs = Cornerscale.y / 2 + nowmapscale.y / 2;

        nowmap.transform.position = new Vector3(Xtrs, Ytrs);


        var RnGscale = nowmap.transform.localScale;

        #region make corner map
        nowmap = Corners[Random.Range(0, Corners.Length)];
        nowmap = Instantiate(nowmap, parent);

        nowmapscale = nowmap.transform.localScale;
        Xtrs = nowmapscale.x / 2 - 0.5f + RnGscale.x;

        nowmap.transform.position = new Vector3(Xtrs, Ytrs);
        #endregion

        //how many maps?
        mapcount++;
    }

    void think()
    {
        /*
         1. 시작 맵 만들기
        2. 위 또는 아래에 런앤건 스테이지 만들기
        3. 그 끝에 코너 맵 만들기
        4. 코네에서 위아래 검사하기
        5. 아무것도 없는곳에 런앤건 만들기
        7. -> 3
        8, 런앤건 맵을 일정 수 이상 만들었으면  마지막 맵에 보스맵 추가하기
         */
    }
    void Make_Start_Map()
    {
        var nowmap = Instantiate(StartMap, parent);
        nowmap.transform.position = Vector3.zero;
    }
    
    public void Make_RnG_Map(GameObject Cornermap, int dir)
    {
        var direction = dir > 0 ? 1 : -1;

        if (Cornermap.CompareTag("StartMap") || Cornermap.CompareTag("CornerMap"))
        {
            
            var CornermapPosition = Cornermap.transform.position;
            var CornerScale = Cornermap.transform.localScale;
            var nowmap = Runs[Random.Range(0, Runs.Length)];

            nowmap = Instantiate(nowmap, parent);
            nowmap.name = "RnG" + mapcount;

            nowmap.SetActive(true);
            var nowmapscale = nowmap.transform.localScale;

            var Xtrs = nowmapscale.x / 2 - 0.5f + CornermapPosition.x;
            var Ytrs = (CornerScale.y / 2 + nowmapscale.y / 2) * direction + CornermapPosition.y;

            nowmap.transform.position = new Vector3(Xtrs, Ytrs );

            mapcount++;
        }
        
    }
    public void Make_Corner_Map(GameObject RnGmap)
    {
        if (mapcount < maxmaps)
        {
            if (RnGmap.CompareTag("RnGMap"))
            {
                var RnGPosition = RnGmap.transform.position;
                var RnGscale = RnGmap.transform.localScale;
                var nowmap = Corners[Random.Range(0, Corners.Length)];

                nowmap = Instantiate(nowmap, parent);
                nowmap.name = "Corner" + mapcount;

                var nowmapscale = nowmap.transform.localScale;

                var Xtrs = RnGscale.x / 2 + RnGPosition.x + nowmapscale.x / 2;
                var Ytrs = RnGPosition.y;

                nowmap.transform.position = new Vector3(Xtrs, Ytrs);

                
            }
        }
    }

    
    public class makemapInfo
    {
        public GameObject obj;
        public int dir;

        public makemapInfo(GameObject OBJ, int direc )
        {
            obj = OBJ;
            dir = direc;
        }
    }

    Queue<makemapInfo> Q_RnGMap = new Queue<makemapInfo>();
    Queue<makemapInfo> Q_CornerMap = new Queue<makemapInfo>();

    public void EnQ_RnGMap(makemapInfo info)
    {
        Q_RnGMap.Enqueue(info);
    }

    public void EnQ_CornerMap(makemapInfo info)
    {
        Q_CornerMap.Enqueue(info);
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {


            if (Q_RnGMap.Count > 0)
            {
                var info = Q_RnGMap.Dequeue();

                Make_RnG_Map(info.obj, info.dir);

            }
            else
            {
                Debug.Log("Making RnG Maps is Done");
            }


            if (Q_CornerMap.Count > 0)
            {
                var info = Q_CornerMap.Dequeue();

                Make_Corner_Map(info.obj);
            }
            else
            {
                Debug.Log("Making Corner Maps is Done");
            }
        }
    }


}
