using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

/// <summary>
/// 지정된 그때그때 한개씩 동기로 로드하는 예제 예제[비동기]
/// </summary>
public class Addressable_OneAsync : MonoBehaviour
{
    /// <summary>
    /// 이미 읽어들이 자원 리스트
    /// </summary>
    public List<PrefabInstanceItemAsyncDataModel> PrefabInsList 
        = new List<PrefabInstanceItemAsyncDataModel>();

    /// <summary>
    /// 화면에 생성된 인스턴스를 가지고 있을 리스트
    /// </summary>
    public List<GameObject> InstanceList = new List<GameObject>();

    /// <summary>
    /// 테스트용 카운터
    /// </summary>
    public int TestCount = 0;

    /// <summary>
    /// 지정된 경로의 프리팹을 로드한다.
    /// </summary>
    /// <param name="sKeyPath"></param>
    /// <returns></returns>
    /// <exception cref="System.Exception"></exception>
    public void LoadPrefab(string sKeyPath)
    {
        PrefabInstanceItemAsyncDataModel findData
            = this.PrefabInsList
                    .Where(w => w.KeyPath == sKeyPath)
                    .FirstOrDefault();

        if (null == findData)
        {//해당 개체가 없다.

            findData = new PrefabInstanceItemAsyncDataModel();
            findData.KeyPath = sKeyPath;

            //프리팹 로드 시작
            Addressables.LoadAssetAsync<GameObject>(sKeyPath).Completed 
                += (handle) => 
                {
                    if(handle.Status == AsyncOperationStatus.Succeeded)
                    {
                        //프리팹 저장
                        findData.Prefab = handle.Result;
                        //로드 성공을 저장함
                        findData.LoadComplete = true;
                        //로드된 리스트에 추가
                        this.PrefabInsList.Add(findData);
                        Debug.Log("GameObject 로드 성공!");
                    }
                    else
                    {
                        throw new System.Exception("프리팹 로드 실패 : " + sKeyPath);
                    }
                };
        }
        else if (false == findData.LoadComplete)
        {
            Debug.Log("아직 프리팹이 로드되지 않았습니다.");
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sKeyPath"></param>
    /// <exception cref="System.Exception"></exception>
    public void NewInstance(string sKeyPath)
    {
        //위치 정보 생성
        Vector3 v3Start = new Vector3(Random.Range(-5f, 5f), Random.Range(-3f, 3f), Random.Range(-5f, 5f));

        //프리팹의 인스턴스를 생성하여 개층 구조(화면)에 추가한다.(= 메모리에 로드)
        Addressables.InstantiateAsync(sKeyPath, v3Start, Quaternion.identity).Completed 
            += (handle) => 
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {

                    //생성된 개체 로드
                    GameObject newGO = handle.Result;

                    //테스트용 텍스트 출력
                    PrefabTest PrefabTemp = newGO.GetComponentInChildren<PrefabTest>();
                    PrefabTemp.TextMesh.text = (++TestCount).ToString();
                    //생성된 인스턴스의 리스트를 별도로 관리한다.
                    this.InstanceList.Add(newGO);
                }
                else
                {
                    throw new System.Exception("프리팹 인스턴스 생성 실패 : " + sKeyPath);
                }
            };
    }


    /// <summary>
    /// 메모리에 올라간 인스턴스를 뒤에서부터 하나씩 제거한다.
    /// </summary>
    public void Release()
    {
        if (this.InstanceList.Count == 0)
        {
            return;
        }


        int index = this.InstanceList.Count - 1;
        ////Addressables를 이용한 인스턴스 제거
        Addressables.ReleaseInstance(this.InstanceList[index]);
        this.InstanceList.RemoveAt(index);
    }

    /// <summary>
    /// 메모리에 올라간 모든 인스턴스를 제거한다.
    /// </summary>
    public void ReleaseAll()
    {
        foreach (GameObject goItem in this.InstanceList)
        {
            Debug.Log(goItem.ToString());

            Addressables.ReleaseInstance(goItem);
        }

        this.InstanceList.Clear();
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            string sKeyPath = "Assets/Prefabs/Test3/Capsule3.prefab";
            this.LoadPrefab(sKeyPath);
            this.NewInstance(sKeyPath);
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            string sKeyPath = "Assets/Prefabs/Test3/Cube3.prefab";
            this.LoadPrefab(sKeyPath);
            this.NewInstance(sKeyPath);
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            string sKeyPath = "Assets/Prefabs/Test3/Sphere3.prefab";
            this.LoadPrefab(sKeyPath);
            this.NewInstance(sKeyPath);
        }


        if (Input.GetKeyDown(KeyCode.M))
        {
            this.Release();
        }
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            this.ReleaseAll();
        }
    }

}

/// <summary>
/// 비동기로 로드된 프리팹을 관리하기위한 데이터 모델
/// </summary>
public class PrefabInstanceItemAsyncDataModel
{
    /// <summary>
    /// 리소스 검색에 사용되는 키(경로와 동일하다.)
    /// </summary>
    public string KeyPath { get; set; } = string.Empty;

    /// <summary>
    /// 비동기 로드가 완료되었는지 여부
    /// </summary>
    public bool LoadComplete { get; set; } = false;

    /// <summary>
    /// 로드된 프리팹
    /// </summary>
    public GameObject Prefab { get; set; } = null;
}
