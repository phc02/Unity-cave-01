using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchPickup : MonoBehaviour
{
    [Header("输入")]
    public bool Hit;
    private bool lastHit;

    [Header("树枝模型（子物体）")]
    public GameObject visual;

    [Header("动画")]
    public float growDuration = 2f;
    private float timer = 0f;
    private bool isAnimating = false;

    private Vector3 initialScale;

    void Start()
    {
        // ❗只隐藏模型，不隐藏整个物体
        visual.SetActive(false);
        initialScale = visual.transform.localScale;
    }

    void LateUpdate()
    {
        if (Hit && !lastHit)
        {
            OnPress();
        }

        lastHit = Hit;
        Hit = false;

        if (isAnimating)
        {
            timer += Time.deltaTime;
            float t = timer / growDuration;

            visual.transform.localScale = initialScale * Mathf.Lerp(0.3f, 1.5f, t);

            if (timer >= growDuration)
            {
                FinishPickup();
            }
        }
    }

    void OnPress()
    {
        if (GameState.HasBranch) return;

        Debug.Log("点击树 → 获取树枝");

        visual.SetActive(true);

        timer = 0f;
        isAnimating = true;
    }

    void FinishPickup()
    {
        isAnimating = false;

        Debug.Log("获得树枝！");

        GameState.HasBranch = true;

        visual.SetActive(false);
    }
}