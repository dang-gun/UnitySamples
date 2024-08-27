using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClickController : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log(gameObject.name + "이(가) 클릭되었습니다.");
        // 클릭 시 실행할 코드 추가
    }
}
