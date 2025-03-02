using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithTiles : MonoBehaviour
{
    public WraithTile tilePrefab; // ��ġ�� Ÿ�� ������
    public int objectCount; // ������ ������Ʈ ����
    public float radius = 2f; // ĳ���Ϳ� ������Ʈ ���� �Ÿ�
    // Start is called before the first frame update

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        PlaceObjectsAround();
    }

    void PlaceObjectsAround()
    {
        BasePlayer.Instance.tiles.Clear();
        objectCount = WTMain.Instance.dicPlayerStatTemplate[(ushort)WTMain.Instance.playerData.userUnitId].total_tile_count;
        float angleStep = 360f / objectCount; // ������Ʈ �� ���� ����

        for (int i = 0; i < objectCount; i++)
        {
            float angle = angleStep * i * Mathf.Deg2Rad; // ������ �������� ��ȯ
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
