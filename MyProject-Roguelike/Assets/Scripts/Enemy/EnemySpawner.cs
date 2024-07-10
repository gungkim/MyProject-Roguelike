using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // �� ������
    public float spawnInterval = 5.0f; // �� ���� �ֱ�

    private Camera mainCamera; // ���� ī�޶�
    private Transform playerTransform; // �÷��̾� Ʈ������

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

        // ���� �÷��̾ ���� �����ϵ��� ����
        EnemyBase enemy = newEnemy.GetComponent<EnemyBase>();
        if (enemy != null && playerTransform != null)
        {
            enemy.Chase();
        }
    }

    private Vector2 GetRandomPositionOutsideViewport()
    {
        Vector2 spawnPosition = Vector2.zero;

        // ī�޶��� ����Ʈ�� �������� ���� ȭ�� �ܺο� �����ǵ��� ����
        float xOffset = 1.2f; // X ���� Ȯ�� ����
        float yOffset = 1.2f; // Y ���� Ȯ�� ����

        bool isPositionValid = false;
        while (!isPositionValid)
        {
            float randomX = Random.Range(0f, 1f);
            float randomY = Random.Range(0f, 1f);

            Vector3 viewportPoint = new Vector3(randomX, randomY, mainCamera.nearClipPlane);
            spawnPosition = mainCamera.ViewportToWorldPoint(viewportPoint);

            // �÷��̾�� �ʹ� ������ �������� �ʵ��� �Ÿ� üũ
            float minDistanceToPlayer = 5f; // �÷��̾�� �ּ� �Ÿ�
            if (Vector2.Distance(spawnPosition, playerTransform.position) > minDistanceToPlayer)
            {
                isPositionValid = true;
            }
        }

        return spawnPosition;
    }

    private void Update()
    {
        // �����ʸ� �÷��̾� �ֺ��� ����
        if (playerTransform != null)
        {
            transform.position = playerTransform.position;
        }
    }
}
