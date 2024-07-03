
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

/// <summary>
/// 프리팹의 원본 인스턴스를 관리하는 모델
/// </summary>
/// <remarks>
/// 어드레서블의 경로(path)를 자동생성된것을 그대로 쓰기위해 만든 라이브러리이다.
/// <para>원래를 어드레서블 없이 사용하기 위해 만든 것으로 지금도 약간만 수정하면 단독으로 사용할 수 있다.</para>
/// <para>string로된 변수명과 변수를 nameof([변수])를 통해 관리하도로 구성되어 있다.</para>
/// <para>하지만 어드레서블의 경로(path)를 직접 관리한다면 필요없어서 딱 한번 사용하고 더 이상 사용하고 있지 않다.(=검증이 되지 않음)</para>
/// </remarks>
public class PrefabInstanceManager
{
    /// <summary>
    /// 로드한 프리팹 리스트
    /// </summary>
    public List<PrefabLoadItem> PrefabInsList { get; set; } 
        = new List<PrefabLoadItem>();

    
    /// <summary>
    /// 가지고 있는 프리팹 리스트에서 해당 이름을 검색한다.
    /// </summary>
    /// <param name="sName"></param>
    /// <returns></returns>
    public PrefabLoadItem Find(string sName)
    {
        PrefabLoadItem findData
            = this.PrefabInsList
                    .Where(w => w.Name == sName)
                    .FirstOrDefault();

        return findData;
    }

    /// <summary>
    /// 생성된 프리팹을 찾고 없으면 로드하여 저장한 후 해당 개체를 리턴한다.
    /// </summary>
    /// <param name="sName"></param>
    /// <param name="sPath"></param>
    /// <returns></returns>
    public PrefabLoadItem FindAndLoad(
        string sName
        , string sPath)
    {
        PrefabLoadItem findData
            = this.PrefabInsList
                    .Where(w => w.Name == sName)
                    .FirstOrDefault();

        if (null == findData)
        {//해당 개체가 없다.

            findData = new PrefabLoadItem();
            findData.Name = sName;
            findData.KeyPath = sPath;


            //프리팹 로드 시작
            var asset = Addressables.LoadAssetAsync<GameObject>(sPath);
            GameObject goTemp = asset.WaitForCompletion();

            if (asset.Status == AsyncOperationStatus.Succeeded)
            {
                findData.Prefab = asset.Result;
                this.PrefabInsList.Add(findData);
                //Debug.Log("GameObject 로드 성공!");
            }
            else
            {
                throw new System.Exception("프리팹 생성 실패 : " + sPath);
            }
        }

        return findData;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="prefabIns"></param>
    /// <exception cref="System.Exception"></exception>
    public GameObject NewInstance(
        PrefabLoadItem prefabIns
        , Transform transform)
    {

        //프리팹을 메모리에 로드한 결과
        AsyncOperationHandle<GameObject> asset;

        if(null != transform)
        {
            //asset = Addressables.InstantiateAsync(prefabIns.Prefab, transform);
            asset = Addressables.InstantiateAsync(prefabIns.KeyPath, transform);
        }
        else
        {
            //asset = Addressables.InstantiateAsync(prefabIns.Prefab);
            asset = Addressables.InstantiateAsync(prefabIns.KeyPath);
        }


        //로드 시도
        GameObject newTemp = asset.WaitForCompletion();

        if (asset.Status == AsyncOperationStatus.Succeeded)
        {//로드 성공

            //생성된 인스턴스의 리스트를 별도로 관리한다.
            prefabIns.InstanceList.Add(newTemp);
        }
        else
        {
            throw new System.Exception("프리팹 인스턴스 생성 실패 : " + prefabIns.Name);
        }

        return newTemp;
    }

    /// <summary>
    /// 지정된 오브젝트를 제거한다.
    /// </summary>
    /// <param name="prefabIns"></param>
    /// <param name="goTarget"></param>
    public void ReleaseInstance(
        PrefabLoadItem prefabIns
        , GameObject goTarget)
    {
        prefabIns.InstanceList.Remove(goTarget);
        Addressables.ReleaseInstance(goTarget);
    }
    /// <summary>
    /// 지정된 아이템이 가지고 있는 모든 리스트를 제거한다.
    /// </summary>
    /// <param name="prefabIns"></param>
    public void ReleaseInstance(PrefabLoadItem prefabIns)
    {
        while(0 < prefabIns.InstanceList.Count)
        {//리스트가 있다.

            GameObject goItem = prefabIns.InstanceList.First();
            this.ReleaseInstance(prefabIns, goItem);
        }


    }
    /// <summary>
    /// 가지고 있는 모든 아이템을 제거 한다.
    /// </summary>
    public void ReleaseInstance()
    {
        while (0 < this.PrefabInsList.Count)
        {//리스트가 있다.

            PrefabLoadItem prefabIns = this.PrefabInsList.First();
            this.ReleaseInstance(prefabIns);

            this.PrefabInsList.Remove(prefabIns);
        }
    }
}

/// <summary>
/// 읽어들인 프리팹 아이템
/// <para>이 프리팹을 이용하여 생성된 인스턴스의 리스트도 가지고 있는다.</para>
/// </summary>
/// <remarks>
/// nameof([변수])를 통해 변수의 이름을 사용할 수 있다.
/// </remarks>
public class PrefabLoadItem
{
    /// <summary>
    /// 프리팹을 구분하기위한 이름
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 검색용 경로
    /// </summary>
    public string KeyPath { get; set; } = string.Empty;

    /// <summary>
    /// 로드된 프리팹
    /// <para></para>
    /// </summary>
    public GameObject Prefab { get; set; } = null;

    /// <summary>
    /// 이 프리팹을 사용하여 생성된 인스턴스 리스트
    /// </summary>
    public List<GameObject> InstanceList { get; set; } = new List<GameObject>();
}


