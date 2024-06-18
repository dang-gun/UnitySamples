using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

/// <summary>
/// 지정된 라벨을 로드하여 사용하는 예제[비동기]
/// </summary>
/// <remarks>
/// https://planek.tistory.com/28
/// </remarks>
public class Addressable_LabalLoad : MonoBehaviour
{
    /// <summary>
    /// 테스트용 카운터
    /// </summary>
    public int TestCount = 0;

    /// <summary>
    /// 어드레서블이 전달하는 Label
    /// </summary>
    public AssetLabelReference AssetLabel;

    /// <summary>
    /// 빌드타겟의 경로 리스트
    /// </summary>
    private IList<IResourceLocation> LocationList;


    /// <summary>
    /// 메모리에 로드된 게임오브젝트 리스트
    /// <para>가지고 있다가 Destroy에 사용한다.</para>
    /// </summary>
    private List<GameObject> GameObjectInstanceList = new List<GameObject>();

    /// <summary>
    /// 지정된 레이블에 해당하는 경로 리스트를 로드한다.
    /// </summary>
    public void GetLocations()
    {
        //지정된 레블과 일치하는 경로를 가져온다.(key가 되는 경로)
        //경로만 불러오는 것이라 오브젝트가 생성되지는 않는다.
        Addressables.LoadResourceLocationsAsync(AssetLabel.labelString).Completed +=
            (handle) =>
            {
                this.LocationList = handle.Result;
                Debug.Log("GetLocations : " + this.LocationList.Count);
            };
    }

    /// <summary>
    /// 로드된 경로중 랜덤한 한개를 추출하여 화면에 출력한다.
    /// </summary>
    public void Instantiate()
    {
        
        //랜덤한 경로 한개 추출
        IResourceLocation location = LocationList[Random.Range(0, LocationList.Count)];
        //위치 정보 생성
        Vector3 v3Start = new Vector3(Random.Range(-5f, 5f), Random.Range(-3f, 3f), Random.Range(-5f, 5f));

        //경로로 GameObject를 생성한다.
        //InstantiateAsync는 UnityEngine.Instantiate와 동일한 동작을 한다.
        //단지 Addressables가 관리하는 자원을 복사하는 것이라 Addressables의 관리를 받을 수 있다.
        //(예> Release)
        Addressables.InstantiateAsync(location, v3Start, Quaternion.identity).Completed 
            +=(handle) =>
            {
                ///테스트용 텍스트 출력
                PrefabTest prefabTemp = handle.Result.GetComponentInChildren<PrefabTest>();
                prefabTemp.TextMesh.text = (++TestCount).ToString();

                // 생성된 개체의 참조값 캐싱
                this.GameObjectInstanceList.Add(handle.Result);
            };
    }

    /// <summary>
    /// 메모리에 올라간 인스턴스를 뒤에서부터 하나씩 제거한다.
    /// </summary>
    public void Release()
    {
        if (this.GameObjectInstanceList.Count == 0)
        {
            return;
        }
            

        var index = this.GameObjectInstanceList.Count - 1;
        // InstantiateAsync <-> ReleaseInstance
        // Destroy함수로써 ref count가 0이면 메모리 상의 에셋을 언로드한다.
        Addressables.ReleaseInstance(this.GameObjectInstanceList[index]);
        this.GameObjectInstanceList.RemoveAt(index);
    }

    /// <summary>
    /// 메모리에 올라간 모든 인스턴스를 제거한다.
    /// </summary>
    public void ReleaseAll()
    {
        foreach(GameObject goItem in this.GameObjectInstanceList)
        {
            Debug.Log(goItem.ToString());

            Addressables.ReleaseInstance(goItem);
        }

        this.GameObjectInstanceList.Clear();
    }




    private void Awake()
    {
        this.GetLocations();
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            this.Instantiate();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            this.Release();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            this.ReleaseAll();
        }
    }

}

