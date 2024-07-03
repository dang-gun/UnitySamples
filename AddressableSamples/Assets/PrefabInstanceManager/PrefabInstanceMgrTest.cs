using System.Linq;
using UnityEngine;

public class PrefabInstanceMgrTest : MonoBehaviour
{
    /// <summary>
    /// ������ �ν��Ͻ� ���� �޴���
    /// </summary>
    public PrefabInstanceManager PrefabInsMgr = new PrefabInstanceManager();

    public string Capsule4 { get; set; } = "Assets/Prefabs/Test4/Capsule4.prefab";
    public string Cube4 { get; set; } = "Assets/Prefabs/Test4/Cube4.prefab";
    public string Sphere4 { get; set; } = "Assets/Prefabs/Test4/Sphere4.prefab";

    /// <summary>
    /// �׽�Ʈ�� ī����
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

        //��ġ ���� ����
        Vector3 v3Start = new Vector3(Random.Range(-5f, 5f), Random.Range(-3f, 3f), Random.Range(-5f, 5f));
        returnInstance.transform.position = v3Start;

        //�׽�Ʈ�� �ؽ�Ʈ ���
        PrefabTest prefabTemp = returnInstance.GetComponentInChildren<PrefabTest>();
        prefabTemp.TextMesh.text = (++TestCount).ToString();

        return returnInstance;
    }

    /// <summary>
    /// �޸𸮿� �ö� �ν��Ͻ��� �ڿ������� �ϳ��� �����Ѵ�.
    /// </summary>
    public void Release()
    {
        if (this.PrefabInsMgr.PrefabInsList.Count == 0)
        {
            return;
        }

        
        //�ǵ��� ������
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
    /// �޸𸮿� �ö� ��� �ν��Ͻ��� �����Ѵ�.
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
