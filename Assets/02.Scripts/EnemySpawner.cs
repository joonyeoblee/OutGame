using System;
using Redcode.Pools;
using Unity.FPS.AI;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public float SpawnTimes = 1f;
    private float _currentTime;

    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= SpawnTimes)
        {
            _currentTime = 0f;

            // 생성 전에 풀에 남은 오브젝트가 있는지 확인
            EnemyController enemy = PoolManager.Instance.GetFromPool<EnemyController>();
            if (enemy != null)
            {
                Vector3 randomVector = new Vector3(Random.Range(-1.0f, 1.0f), 0f, Random.Range(-1.0f, 1.0f));
                enemy.transform.position = transform.position + randomVector;
            }
        }
    }
}
