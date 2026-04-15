using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAVE_DualOutput : MonoBehaviour
{
    [Header("原始UI Camera（输出7710x3770）")]
    public Camera mainUICamera;

    [Header("压缩输出 Camera（输出7680x2160）")]
    public Camera outputCamera;

    [Header("目标显示器")]
    public int display1 = 0; // 原始
    public int display2 = 1; // 投影

    private RenderTexture compressedRT;

    void Start()
    {
        // 1️⃣ 激活第二屏
        if (Display.displays.Length > 1)
        {
            Display.displays[display2].Activate();
        }

        // 2️⃣ 设置主屏分辨率（调试用）
        Screen.SetResolution(7710, 3770, FullScreenMode.Windowed);

        // 3️⃣ 创建压缩RT
        compressedRT = new RenderTexture(7680, 2160, 24);

        // 4️⃣ 设置输出Camera
        outputCamera.targetTexture = compressedRT;
        outputCamera.targetDisplay = display2;

        // 5️⃣ 主UI Camera输出到Display1
        mainUICamera.targetDisplay = display1;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        // 把7710x3770压缩到7680x2160
        Graphics.Blit(src, compressedRT);

        // 正常显示原图
        Graphics.Blit(src, dest);
    }
}