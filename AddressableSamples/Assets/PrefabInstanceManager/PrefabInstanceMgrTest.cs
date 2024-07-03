using System.Linq;
using UnityEngine;

public class PrefabInstanceMgrTest : MonoBehaviour
{
    /// <summary>
    /// 프리팹 인스턴스 관리 메니저
    /// </summary>
    public PrefabInstanceManager PrefabInsMgr = new PrefabInstanceManager();

    public string Capsule4 { get; set; } = "Assets/Prefabs/Test4/Capsule4.prefab";
    public string Cube4 { get; set; } = "Assets/Prefabs/Test4/Cube4.prefab";
    public string Sphere4 { get; set; } = "Assets/Prefabs/Test4/Sphere4.prefab";

    /// <summary>
    /// 테스트용 카운터
    /// </summary>
    public int TestCount = 0;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sKeyName"></param>
    /// <param name="sKeyPath"></param>
    /// <returns></returns>
    public GameObject NewInstance(string sKeyName, string sKeyPath)
    {
        PrefabLoadItem goTemp
            = this.PrefabInsMgr.FindAndLoad(sKeyName, sKeyPath);
        GameObject returnInstance
            = this.PrefabInsMgr.NewInstance(goTemp, null);

        //위치 정보 생성
        Vector3 v3Start = new Vector3(Random.Range(-5f, 5f), Random.Range(-3f, 3f), Random.Range(-5f, 5f));
        returnInstance.transform.position = v3Start;

        //테스트용 텍스트 출력
        PrefabTest prefabTemp = returnInstance.GetComponentInChildren<PrefabTest>();
        prefabTemp.TextMesh.text = (++TestCount).ToString();

        return returnInstance;
    }

    /// <summary>
    /// 메모리에 올라간 인스턴스를 뒤에서부터 하나씩 제거한다.
    /// </summary>
    public void Release()
    {
        if (this.PrefabInsMgr.PrefabInsList.Count == 0)
        {
            return;
        }

        
        //맨뒤의 아이템
        PrefabLoadItem findInsItem = this.PrefabInsMgr.PrefabInsList.Last();
        if (findInsItem != null)
        {
            if(0 < findInsItem.InstanceList.Count)
            {
                this.PrefabInsMgr.ReleaseInstance(
                    findInsItem, findInsItem.InstanceList.Last());
            }
            
        }


    }

    /// <summary>
    /// 메모리에 올라간 모든 인스턴스를 제거한다.
    /// </summary>
    public void ReleaseAll()
    {
        this.PrefabInsMgr.ReleaseInstance();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            this.NewInstance(nameof(this.Capsule4), this.Capsule4);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            this.NewInstance(nameof(this.Cube4), this.Cube4);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            this.NewInstance(nameof(this.Sphere4), this.Sphere4);
        }


        if (Input.GetKeyDown(KeyCode.Z))
        {
            this.Release();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            this.ReleaseAll();
        }
    }
}
