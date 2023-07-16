using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone_Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;    // 생성할 오브젝트 프리팹
    public float minSpawnInterval = 1f; // 최소 스폰 간격(초)
    public float maxSpawnInterval = 10f; // 최대 스폰 간격(초)

    private float timer = 0f;           // 타이머 변수
    private float spawnInterval = 0f;    // 랜덤한 스폰 간격

    private void Start()
    {
        // 최초 스폰 간격 설정
        SetRandomSpawnInterval();
    }

    private void Update()
    {
        // 타이머를 업데이트
        timer += Time.deltaTime;

        // 스폰 간격이 경과한 경우
        if (timer >= spawnInterval)
        {
            // 오브젝트를 생성하고 원하는 위치에 배치
            Instantiate(objectToSpawn, transform.position, Quaternion.identity);

            // 다음 스폰 간격 설정
            SetRandomSpawnInterval();

            timer = 0f;    // 타이머 초기화
        }
    }

    private void SetRandomSpawnInterval()
    {
        // 최소 스폰 간격과 최대 스폰 간격 사이의 랜덤한 값을 설정
        spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
    }
}