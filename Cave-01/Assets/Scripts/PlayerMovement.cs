using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // 防止物理旋转
    }

    void Update()
    {
        // 获取输入轴
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // 创建移动向量
        Vector3 movement = new Vector3(-horizontal, 0f, -vertical);

        // 应用移动
        if (movement != Vector3.zero)
        {
            // // 旋转角色朝向移动方向
            // Quaternion toRotation = Quaternion.LookRotation(movement);
            // transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 10f);
            
            // 移动角色
            rb.MovePosition(transform.position + movement * moveSpeed * Time.deltaTime);
        }
    }
}

