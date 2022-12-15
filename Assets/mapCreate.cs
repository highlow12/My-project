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
    //���ӸŴ����� �ν��Ͻ��� ��� ��������(static ���������� �����ϱ� ���� ����������� �ϰڴ�).
    //�� ���� ������ ���ӸŴ��� �ν��Ͻ��� �� instance�� ��� �༮�� �����ϰ� �� ���̴�.
    //������ ���� private����.
    private static mapCreate instance = null;

    void Awake()
    {
        if (null == instance)
        {
            //�� Ŭ���� �ν��Ͻ��� ź������ �� �������� instance�� ���ӸŴ��� �ν��Ͻ��� ������� �ʴٸ�, �ڽ��� �־��ش�.
            instance = this;

            //�� ��ȯ�� �Ǵ��� �ı����� �ʰ� �Ѵ�.
            //gameObject�����ε� �� ��ũ��Ʈ�� ������Ʈ�μ� �پ��ִ� Hierarchy���� ���ӿ�����Ʈ��� ��������, 
            //���� �򰥸� ������ ���� this�� �ٿ��ֱ⵵ �Ѵ�.
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //���� �� �̵��� �Ǿ��µ� �� ������ Hierarchy�� mapCreate�� ������ ���� �ִ�.
            //�׷� ��쿣 ���� ������ ����ϴ� �ν��Ͻ��� ��� ������ִ� ��찡 ���� �� ����.
            //�׷��� �̹� ���������� instance�� �ν��Ͻ��� �����Ѵٸ� �ڽ�(���ο� ���� mapCreate)�� �������ش�.
            Destroy(this.gameObject);
        }
    }

    //���� �Ŵ��� �ν��Ͻ��� ������ �� �ִ� ������Ƽ. static�̹Ƿ� �ٸ� Ŭ�������� ���� ȣ���� �� �ִ�.
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
         1. ���� �� �����
        2. �� �Ǵ� �Ʒ��� ���ذ� �������� �����
        3. �� ���� �ڳ� �� �����
        4. �ڳ׿��� ���Ʒ� �˻��ϱ�
        5. �ƹ��͵� ���°��� ���ذ� �����
        7. -> 3
        8, ���ذ� ���� ���� �� �̻� ���������  ������ �ʿ� ������ �߰��ϱ�
         */
    }
    void Make_Start_Map()
    {
        var nowmap = Instantiate(StartMap, parent);
        nowmap.transform.position = Vector3.zero;
    }

    public void Make_RnG_Map(GameObject Cornermap)
    {
        
        if (Cornermap.CompareTag("StartMap") || Cornermap.CompareTag("CornerMap"))
        {
            
            var CornermapPosition = Cornermap.transform.position;
            var CornerScale = Cornermap.transform.localScale;
            var nowmap = Runs[Random.Range(0, Runs.Length)];

            nowmap = Instantiate(nowmap, parent);

            nowmap.SetActive(true);
            var nowmapscale = nowmap.transform.localScale;

            var Xtrs = nowmapscale.x / 2 - 0.5f + CornermapPosition.x;
            var Ytrs = CornerScale.y / 2 + nowmapscale.y / 2 + CornermapPosition.y;

            nowmap.transform.position = new Vector3(Xtrs, Ytrs);

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

                var nowmapscale = nowmap.transform.localScale;

                var Xtrs = nowmapscale.x / 2 - 0.5f + RnGscale.x;
                var Ytrs = RnGPosition.y;

                nowmap.transform.position = new Vector3(Xtrs, Ytrs);

            }
        }
    }

}
