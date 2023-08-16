using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBot : MonoBehaviour
{
    public int initialBots;
    public float timeBeforeSpawn2;
    public float timeBeforeSpawn3;
    public Transform[] spawnPoints1;
    public Transform[] spawnPoints2;
    public Transform[] spawnPoints3;
    public GameObject botPrefab;

    private List<GameObject> activeBots = new List<GameObject>();

    private BotPool botPool;
    private bool playerReachedTrigger;
    private bool victoryDisplayed;

    private void Start()
    {
        botPool = new BotPool(botPrefab, initialBots);

        UIMainMenu mainMenu = FindObjectOfType<UIMainMenu>();
        if (mainMenu != null)
        {
            mainMenu.onPlayGame.AddListener(OnPlayGame);
        }
    }

    private void OnPlayGame()
    {
        // Đã bấm nút Play Game, bắt đầu spawn bot
        SpawnBotAtPoint(spawnPoints1, 12);
        StartCoroutine(SpawnBotsRoutine());
    }

    private IEnumerator SpawnBotsRoutine()
    {
        yield return new WaitForSeconds(timeBeforeSpawn2);

        SpawnBotAtPoint(spawnPoints2, 20);

        yield return new WaitForSeconds(timeBeforeSpawn3);

        SpawnBotAtPoint(spawnPoints3, 25);
    }
    private void SpawnBotAtPoint(Transform[] points, int numbertoSpawn)
    {
        for (int i = 0; i < points.Length; i++)
        {

            SpawnAtpoint(points[i], numbertoSpawn / points.Length);
        }
    }

    private void SpawnAtpoint(Transform thispoint, int numbertoSpawn)
    {
        for (int i = 0; i < numbertoSpawn; i++)
        {
            GameObject bot = botPool.GetPooledObject();
            bot.transform.position = thispoint.position;
            botPool.botInitialPositions[bot] = thispoint.position;
            bot.SetActive(true);
            activeBots.Add(bot);
        }

    }


    public void PlayerReachedTrigger()
    {
        playerReachedTrigger = true;
    }

    public void SetVictoryDisplayed(bool value)
    {
        victoryDisplayed = value;
    }
}

