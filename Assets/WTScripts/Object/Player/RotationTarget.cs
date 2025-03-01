using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTarget : MonoBehaviour
{
    [SerializeField]
    private float rSpeed = 5f;
    public void RotateTowards(Vector2 targetPosition)
    {
        Vector2 dir = targetPosition - (Vector2)transform.position;

        if (dir != Vector2.zero)
        {
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion targetRot = Quaternion.Euler(0, 0, angle);

            // 부드럽게 회전 (Lerp 사용)
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * rSpeed);
        }
    }
}
