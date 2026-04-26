using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveTouchToRay : MonoBehaviour
{
    [Header("玩家头部（射线起点）")]
    public Transform playerHead;

    [Header("三面墙（地面先不做）")]
    public Wall leftWall;
    public Wall frontWall;
    public Wall rightWall;

    [Header("射线检测")]
    public float maxDistance = 10f;
    public LayerMask interactLayer;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 uv = new Vector2(
                Input.mousePosition.x / Screen.width,
                Input.mousePosition.y / Screen.height
            );

            Debug.Log($"[鼠标位置] {Input.mousePosition} → UV {uv}");

            HandleTouch(uv);
        }
    }

    // 输入：田字格坐标（0~1）
    public void HandleTouch(Vector2 uv)
    {
        Debug.Log($"[输入UV] {uv}");

        // 1️⃣ 判断象限
        int quadrant = GetQuadrant(uv);
        Debug.Log($"[象限] {quadrant} (0左墙 1前墙 2右墙 3地面)");

        // 2️⃣ 局部UV
        Vector2 localUV = GetLocalUV(uv, quadrant);
        Debug.Log($"[局部UV] {localUV}");

        // 3️⃣ 墙面点
        Vector3 targetPoint = GetWallPoint(quadrant, localUV);
        Debug.Log($"[墙面点] {targetPoint}");

        if (targetPoint == Vector3.zero)
        {
            Debug.LogWarning("⚠️ 未使用的象限（可能是地面）");
            return;
        }

        // 4️⃣ 射线
        Vector3 origin = playerHead.position;
        Vector3 dir = (targetPoint - origin).normalized;

        Debug.Log($"[射线] origin={origin}, dir={dir}");

        Ray ray = new Ray(origin, dir);

        // 可视化射线（更明显一点）
        Debug.DrawRay(origin, dir * 100f, Color.red, 10f);

        // 👉 在墙面画一个点（超级重要）
        // DebugDrawPoint(targetPoint, Color.green);
        // SpawnDebugPoint(targetPoint);

        // 5️⃣ Raycast
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, interactLayer))
        {
            Debug.Log($"✅ 命中物体: {hit.collider.name}");

            var target = hit.collider.GetComponent<Tuiocr_Disappear>();
            var branch = hit.collider.GetComponent<BranchPickup>();
            var fire = hit.collider.GetComponent<CampfireController>();
            var door = hit.collider.GetComponent<DoorController>();
            if (door != null)
            {
                door.Hit = true;
            }
            if (branch != null)
            {
                branch.Hit = true;
            }
            if (fire != null)
            {
                fire.Hit = true;
            }
            if (target != null)
            {
                target.Hit = true;
            }
            else
            {
                Debug.LogWarning("⚠️ 命中了，但没有脚本");
            }
        }
        else
        {
            Debug.Log("❌ 射线未命中任何物体");
        }
    }

    // 判断田字格象限
    int GetQuadrant(Vector2 uv)
    {
        bool right = uv.x > 0.5f;
        bool top = uv.y > 0.5f;

        if (!right && top) return 0;   // 左上 → 左墙
        if (right && top) return 1;    // 右上 → 前墙
        if (!right && !top) return 2;  // 左下 → 右墙
        return 3;                      // 右下 → 地面（暂不用）
    }

    // 映射到局部0~1
    Vector2 GetLocalUV(Vector2 uv, int q)
    {
        float u = (uv.x % 0.5f) * 2f;
        float v = (uv.y % 0.5f) * 2f;
        return new Vector2(u, v);
    }

    // 根据墙计算世界点
    Vector3 GetWallPoint(int q, Vector2 uv)
    {
        Wall wall = null;

        switch (q)
        {
            case 0: wall = leftWall; break;
            case 1: wall = frontWall; break;
            case 2: wall = rightWall; break;
            default: return Vector3.zero;
        }

        return wall.origin + uv.x * wall.right + uv.y * wall.up;
    }

    void DebugDrawPoint(Vector3 pos, Color color)
    {
        float size = 0.1f;

        Debug.DrawLine(pos - Vector3.up * size, pos + Vector3.up * size, color, 0.2f);
        Debug.DrawLine(pos - Vector3.right * size, pos + Vector3.right * size, color, 0.2f);
        Debug.DrawLine(pos - Vector3.forward * size, pos + Vector3.forward * size, color, 0.2f);
    }

    void SpawnDebugPoint(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;
        sphere.transform.localScale = Vector3.one * 0.1f;

        Destroy(sphere, 1f);
    }
}

[System.Serializable]
public class Wall
{
    public Vector3 origin;
    public Vector3 right;
    public Vector3 up;
}
