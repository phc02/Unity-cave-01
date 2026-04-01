using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aspect1 : MonoBehaviour
{
    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    // 每帧最后执行，强制覆盖 URP 的设置
    void LateUpdate()
    {
        cam.aspect = 2.1417f; // 宽高比
        cam.fieldOfView = 39.6f; // 垂直FOV
    }
}
