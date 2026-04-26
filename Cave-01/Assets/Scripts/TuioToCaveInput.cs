using UnityEngine;
using TouchScript;
using TouchScript.Pointers;
using System.Collections.Generic;

public class TuioToCaveInput : MonoBehaviour
{
    public CaveTouchToRay cave; // 拖你的脚本

    void Update()
    {
        Debug.Log("Update Running");

        var pointers = TouchManager.Instance.Pointers;

        Debug.Log($"Pointers Count = {pointers.Count}");

        foreach (var p in pointers)
        {
            if (p.Type != Pointer.PointerType.Touch) continue;

            Vector2 screenPos = p.Position;

            Debug.Log($"[TUIO] screenPos = {screenPos}");

            // 👉 转 UV（关键）
            Vector2 uv = new Vector2(
                screenPos.x / Screen.width,
                screenPos.y / Screen.height
            );

            Debug.Log($"[TUIO] screen={screenPos} uv={uv}");

            cave.HandleTouch(uv);
        }
    }
}