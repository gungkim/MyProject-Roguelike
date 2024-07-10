using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // 적 프리팹
    public float spawnInterval = 5.0f; // 적 생성 주기

    private Camera mainCamera; // 메인 카메라
    private Transform playerTransform; // 플레이어 트랜스폼

    private void Start()
    {
        mainCamera = Camera.main;
        playerTransform = GameManager.Instance.Player.transform;
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPosition = GetRandomPositionOutsideViewport();
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        // 적이 플레이어를 향해 공격하도록 설정
        EnemyBase enemy = newEnemy.GetComponent<EnemyBase>();
        if (enemy != null && playerTransform != null)
        {
            enemy.Chase();
        }
    }

    private Vector2 GetRandomPositionOutsideViewport()
    {
        Vector2 spawnPosition = Vector2.zero;

        // 카메라의 뷰포트를 기준으로 적이 화면 외부에 스폰되도록 설정
        float xOffset = 1.2f; // X 방향 확장 비율
        float yOffset = 1.2f; // Y 방향 확장 비율

        bool isPositionValid = false;
        while (!isPositionValid)
        {
            float randomX = Random.Range(0f, 1f);
            float randomY = Random.Range(0f, 1f);

            Vector3 viewportPoint = new Vector3(randomX, randomY, mainCamera.nearClipPlane);
            spawnPosition = mainCamera.ViewportToWorldPoint(viewportPoint);

            // 플레이어와 너무 가까이 생성되지 않도록 거리 체크
            float minDistanceToPlayer = 5f; // 플레이어와 최소 거리
            if (Vector2.Distance(spawnPosition, playerTransform.position) > minDistanceToPlayer)
            {
                isPositionValid = true;
            }
        }

        return spawnPosition;
    }

    private void Update()
    {
        // 스포너를 플레이어 주변에 유지
        if (playerTransform != null)
        {
            transform.position = playerTransform.position;
        }
    }
}
