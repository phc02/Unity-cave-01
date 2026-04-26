using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("输入")]
    public bool Hit;
    private bool lastHit;

    [Header("门设置")]
    public Transform leftDoor;
    public Transform rightDoor;

    [Header("旋转角度")]
    public float openAngle = 75f;
    public float rotateSpeed = 2f;

    [Header("场景移动")]
    public Transform sceneRoot;
    public float moveDistance = 5f;
    public float moveSpeed = 2f;

    private bool isOpening = false;
    private bool isMoving = false;

    private Quaternion leftStartRot;
    private Quaternion rightStartRot;

    private Quaternion leftTargetRot;
    private Quaternion rightTargetRot;

    private Vector3 sceneStartPos;
    private Vector3 sceneTargetPos;

    void Start()
    {
        leftStartRot = leftDoor.rotation;
        rightStartRot = rightDoor.rotation;

        leftTargetRot = leftStartRot * Quaternion.Euler(0, openAngle, 0);
        rightTargetRot = rightStartRot * Quaternion.Euler(0, -openAngle, 0);

        sceneStartPos = sceneRoot.position;
        sceneTargetPos = sceneStartPos + new Vector3(0, 0, moveDistance);
    }

    void LateUpdate()
    {
        // 点击检测
        if (Hit && !lastHit)
        {
            OnPress();
        }

        lastHit = Hit;
        Hit = false;

        // 门旋转动画
        if (isOpening)
        {
            leftDoor.rotation = Quaternion.Slerp(leftDoor.rotation, leftTargetRot, Time.deltaTime * rotateSpeed);
            rightDoor.rotation = Quaternion.Slerp(rightDoor.rotation, rightTargetRot, Time.deltaTime * rotateSpeed);

            // 判断是否基本到位
            if (Quaternion.Angle(leftDoor.rotation, leftTargetRot) < 0.5f)
            {
                isOpening = false;
                isMoving = true;
            }
        }

        // 场景移动
        if (isMoving)
        {
            sceneRoot.position = Vector3.Lerp(sceneRoot.position, sceneTargetPos, Time.deltaTime * moveSpeed);

            if (Vector3.Distance(sceneRoot.position, sceneTargetPos) < 0.05f)
            {
                isMoving = false;
                Debug.Log("进入完成");
            }
        }
    }

    void OnPress()
    {
        Debug.Log("门被触摸");

        if (isOpening || isMoving) return;

        isOpening = true;
    }
}
