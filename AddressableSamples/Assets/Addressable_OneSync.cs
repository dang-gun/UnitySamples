using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

/// <summary>
/// 지정된 그때그때 한개씩 동기로 로드하는 예제 예제[동기]
/// </summary>
public class Addressable_OneSync : MonoBehaviour
{
    /// <summary>
    /// 이미 읽어들이 자원 리스트
    /// </summary>
    public List<PrefabInstanceItemDataModel> PrefabInsList 
        = new List<PrefabInstanceItemDataModel>();

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
        PrefabInstanceItemDataModel findData
            = this.PrefabInsList
                    .Where(w => w.KeyPath == sKeyPath)
                    .FirstOrDefault();

        if (null == findData)
        {//해당 개체가 없다.

            findData = new PrefabInstanceItemDataModel();
            findData.KeyPath = sKeyPath;

            //프리팹 로드 시작
            AsyncOperationHandle<GameObject> loadAsset 
                = Addressables.LoadAssetAsync<GameObject>(sKeyPath);
            GameObject loadGO = loadAsset.WaitForCompletion();

            if (loadAsset.Status == AsyncOperationStatus.Succeeded)
            {
                //프리팹 저장
                findData.Prefab = loadAsset.Result;
                //로드된 리스트에 추가
                this.PrefabInsList.Add(findData);
                Debug.Log("GameObject 로드 성공!");
            }
            else
            {
                throw new System.Exception("프리팹 로드 실패 : " + sKeyPath);
            }
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sKeyPath"></param>
    /// <exception cref="System.Exception"></exception>
    public GameObject NewInstance(string sKeyPath)
    {
        //위치 정보 생성
        Vector3 v3Start = new Vector3(Random.Range(-5f, 5f), Random.Range(-3f, 3f), Random.Range(-5f, 5f));

        //프리팹의 인스턴스를 생성하여 개층 구조(화면)에 추가한다.(= 메모리에 로드)
        AsyncOperationHandle<GameObject> prefabNew
            = Addressables.InstantiateAsync(sKeyPath, v3Start, Quaternion.identity);

        //생성 시도
        GameObject newGO = prefabNew.WaitForCompletion();

        if (prefabNew.Status == AsyncOperationStatus.Succeeded)
        {//생성 성공

            //테스트용 텍스트 출력
            PrefabTest prefabTemp = newGO.GetComponentInChildren<PrefabTest>();
            prefabTemp.TextMesh.text = (++TestCount).ToString();
            //생성된 인스턴스의 리스트를 별도로 관리한다.
            this.InstanceList.Add(newGO);
        }
        else
        {
            throw new System.Exception("프리팹 인스턴스 생성 실패 : " + sKeyPath);
        }

        return newGO;
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

        //맨 뒤 인덱스
        int index = this.InstanceList.Count - 1;
        //Addressables를 이용한 인스턴스 제거
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
        if (Input.GetKeyDown(KeyCode.G))
        {
            string sKeyPath = "Assets/Prefabs/Test2/Capsule2.prefab";
            this.LoadPrefab(sKeyPath);
            this.NewInstance(sKeyPath);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            string sKeyPath = "Assets/Prefabs/Test2/Cube2.prefab";
            this.LoadPrefab(sKeyPath);
            this.NewInstance(sKeyPath);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            string sKeyPath = "Assets/Prefabs/Test2/Sphere3.prefab";
            this.LoadPrefab(sKeyPath);
            this.NewInstance(sKeyPath);
        }


        if (Input.GetKeyDown(KeyCode.K))
        {
            this.Release();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            this.ReleaseAll();
        }
    }

}

/// <summary>
/// 로드된 프리팹을 관리하기위한 데이터 모델
/// </summary>
public class PrefabInstanceItemDataModel
{
    /// <summary>
    /// 리소스 검색에 사용되는 키(경로와 동일하다.)
    /// </summary>
    public string KeyPath { get; set; } = string.Empty;

    /// <summary>
    /// 로드된 프리팹
    /// </summary>
    public GameObject Prefab { get; set; } = null;
}