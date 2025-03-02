using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithTiles : MonoBehaviour
{
    public WraithTile tilePrefab; // 배치할 타일 프리팹
    public int objectCount; // 생성할 오브젝트 개수
    public float radius = 2f; // 캐릭터와 오브젝트 사이 거리
    // Start is called before the first frame update

    public void Init()
    {
        PlaceObjectsAround();
    }

    void PlaceObjectsAround()
    {
        BasePlayer.Instance.tiles.Clear();
        objectCount = WTMain.Instance.dicPlayerStatTemplate[(ushort)WTMain.Instance.playerData.userUnitId].total_tile_count;
        float angleStep = 360f / objectCount; // 오브젝트 간 각도 간격

        for (int i = 0; i < objectCount; i++)
        {
            float angle = angleStep * i * Mathf.Deg2Rad; // 각도를 라디안으로 변환
            float x = transform.position.x + Mathf.Cos(angle) * radius;
            float y = transform.position.y + Mathf.Sin(angle) * radius;

            Vector3 spawnPosition = new Vector3(x, y, transform.position.z);
            var obj = Instantiate(tilePrefab, spawnPosition, Quaternion.identity, transform);
            obj.index = i;
            BasePlayer.Instance.tiles.Add(obj);
        }
        BasePlayer.Instance.SetSpecialTile();
    }
}
