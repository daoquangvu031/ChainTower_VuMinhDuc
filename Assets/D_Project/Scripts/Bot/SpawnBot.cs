using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBot : MonoBehaviour
{
    public int botsPerWave = 6; 
    public Transform spawnPoint; 
    public GameObject botPrefab; 
    public float spawnInterval = 3f; 

    private BotPool botPool;

    private void Start()
    {
        botPool = new BotPool(botPrefab, botsPerWave); // Khởi tạo
        StartCoroutine(SpawnBotsRoutine());
    }

    private IEnumerator SpawnBotsRoutine()
    {
        while (true)
        {
            SpawnBots();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnBots()
    {
        for (int i = 0; i < botsPerWave; i++)
        {
            GameObject bot = botPool.GetPooledObject(); // Lấy từ BotPool
            bot.transform.position = spawnPoint.position;
            bot.SetActive(true);
        }
    }
}
