using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone_Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;    // ������ ������Ʈ ������
    public float minSpawnInterval = 1f; // �ּ� ���� ����(��)
    public float maxSpawnInterval = 10f; // �ִ� ���� ����(��)

    private float timer = 0f;           // Ÿ�̸� ����
    private float spawnInterval = 0f;    // ������ ���� ����

    private void Start()
    {
        // ���� ���� ���� ����
        SetRandomSpawnInterval();
    }

    private void Update()
    {
        // Ÿ�̸Ӹ� ������Ʈ
        timer += Time.deltaTime;

        // ���� ������ ����� ���
        if (timer >= spawnInterval)
        {
            // ������Ʈ�� �����ϰ� ���ϴ� ��ġ�� ��ġ
            Instantiate(objectToSpawn, transform.position, Quaternion.identity);

            // ���� ���� ���� ����
            SetRandomSpawnInterval();

            timer = 0f;    // Ÿ�̸� �ʱ�ȭ
        }
    }

    private void SetRandomSpawnInterval()
    {
        // �ּ� ���� ���ݰ� �ִ� ���� ���� ������ ������ ���� ����
        spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
    }
}