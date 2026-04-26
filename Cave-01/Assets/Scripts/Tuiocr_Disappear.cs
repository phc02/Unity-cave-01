using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuiocr_Disappear : MonoBehaviour
{
    [Header("输入状态（由外部系统赋值）")]
    public bool Hit;

    private bool lastHit;

    [Header("行为配置")]
    public bool destroyObject = false; // true=销毁，false=隐藏

    void LateUpdate()
    {
        // 检测“按下瞬间”（false → true）
        if (Hit && !lastHit)
        {
            OnPress();
        }

        // 更新状态
        lastHit = Hit;

        // ⚠️ 每帧重置，等待外部重新赋值
        Hit = false;
    }

    private void OnPress()
    {
        if (destroyObject)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
