using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireController : MonoBehaviour
{
    [Header("输入")]
    public bool Hit;
    private bool lastHit;

    [Header("篝火动画")]
    public Animator animator;

    private bool isLit = false;

    void LateUpdate()
    {
        if (Hit && !lastHit)
        {
            OnPress();
        }

        lastHit = Hit;
        Hit = false;
    }

    void OnPress()
    {
        if (!GameState.HasBranch)
        {
            Debug.Log("未获得树枝，无法点火");
            return;
        }

        if (!isLit)
        {
            Debug.Log("点燃篝火！");
            isLit = true;

            if (animator != null)
            {
                animator.SetTrigger("Fire");
            }
        }
    }
}
