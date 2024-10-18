using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;
using UnityEngine.UI;

public class ItemManagerController : MonoBehaviour
{
    /// <summary>
    /// ���������� ����� ������
    /// </summary>
    public GameObject ItemPrefab;

    /// <summary>
    /// �������� ������ ����Ʈ
    /// </summary>
    private List<ItemObjectController> ItemList
        = new List<ItemObjectController>();

    #region UI ����� ��ü

    /// <summary>
    /// �������� ǥ���� ������ ����
    /// </summary>
    public GameObject ListView_Content;


    /// <summary>
    /// ����Ʈ ��ü ���� ��ư
    /// </summary>
    public Button ClearAllBtn;

    /// <summary>
    /// ����Ʈ ���� ������ �߰��ϴ� ��ư
    /// </summary>
    public Button AddBtn;
    #endregion

    /// <summary>
    /// ������ ����� ī����
    /// </summary>
    private int AddCount = 0;
    void Awake()
    {
        if(null == this.ItemPrefab)
        {//�������� �������� �ʾҴ�.

            //���� �˻�
            this.ItemPrefab
                //= Resources.Load<GameObject>("ItemObject");
                = AssetDatabase.LoadAssetAtPath<GameObject>("ItemObject");
        }
        


        //UI ���� **********
        this.ListView_Content
            = GameObject.Find("List View")
                        .transform.Find("Viewport/Content")
                        .gameObject;

        this.ClearAllBtn = this.transform.Find("ClearAllBtn")
                                .GetComponent<Button>();
        this.ClearAllBtn.onClick.AddListener(() => { this.ItemList_ClearAll(); });

        this.AddBtn = this.transform.Find("AddBtn")
                            .GetComponent<Button>();
        this.AddBtn.onClick.AddListener(() => { this.ItemList_AddNew(); });

    }

    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    /// <summary>
    /// ��ü ����Ʈ�� ����.
    /// </summary>
    public void ItemList_ClearAll()
    {
        //��� �ڽ��� ã�� �ı��Ѵ�.
        foreach (Transform child in this.ListView_Content.transform)
        {
            Destroy(child.gameObject);
        }

        //����Ʈ�� ����.
        this.ItemList.Clear();
    }

    /// <summary>
    /// ����Ʈ�� ���� �������� �ϳ� �����Ͽ� �ִ´�.
    /// </summary>
    public void ItemList_AddNew()
    {
        //�������� �����Ѵ�. ****
        ItemObjectController newItem
            = Instantiate(this.ItemPrefab)
                .GetComponent<ItemObjectController>();

        //������ ���� ���� ****
        newItem.Title.text
            = string.Format("Add{0} : {1}"
                            , ++AddCount
                            , UnityEngine.Random.Range(1, 100));

        newItem.CreditImage.color
            = new Color(UnityEngine.Random.Range(0, 1f)
                        , UnityEngine.Random.Range(0, 1f)
                        , UnityEngine.Random.Range(0, 1f));




        //����Ʈ�� �߰� ****
        this.ItemList.Add(newItem);

        //UI�� �߰�
        newItem.gameObject.transform.SetParent(this.ListView_Content.transform);
    }
}
