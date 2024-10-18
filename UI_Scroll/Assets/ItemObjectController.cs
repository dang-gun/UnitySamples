using System;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class ItemObjectController : MonoBehaviour
{
    #region �ܺο��� ������ �̺�Ʈ
    /// <summary>
    /// ������ Ŭ�������� �̺�Ʈ ������ ���� �븮��
    /// </summary>
    public delegate void ItemClickDelegate(ItemObjectController contThis);
    /// <summary>
    /// �������� Ŭ�������� �߻��� �̺�Ʈ
    /// </summary>
    public ItemClickDelegate OnItemClick;
    /// <summary>
    /// �������� Ŭ�������� �߻��� �̺�Ʈ ȣ��
    /// </summary>
    private void OnItemClickCall()
    {
        if (null != OnItemClick)
        {
            this.OnItemClick(this);
        }
    }
    #endregion


    /// <summary>
    /// ��ü ����
    /// </summary>
    public Button Item;
    

    /// <summary>
    /// ����
    /// </summary>
    public TextMeshProUGUI Title;
    
    /// <summary>
    /// ���� �̹���
    /// </summary>
    public Image InfoImage;
    /// <summary>
    /// ũ���� �̹���
    /// </summary>
    public Image CreditImage;

    

    void Awake()
    {
        //��ũ��Ʈ���� ��ü ã��

        //�� �������� ���� ��ư
        this.Item = GetComponent<Button>();
        this.Item.onClick.AddListener(OnItemClickCall);

        //���� �ؽ�Ʈ
        this.Title 
            = this.transform.Find("BtnText")
                    .GetComponent<TextMeshProUGUI>();

        //���� �̹���
        this.InfoImage
            = this.transform.Find("InfoImage")
                    .GetComponent<Image>();
        //ũ���� �̹���
        this.CreditImage
            = this.transform.Find("CreditImage")
                    .GetComponent<Image>();
    }

}
