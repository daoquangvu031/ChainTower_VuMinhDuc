using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotPool : MonoBehaviour
{
    public GameObject prefab;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform spawnPoint3;
    public int initialSize = 5;

    public Dictionary<GameObject, Vector3> botInitialPositions = new Dictionary<GameObject, Vector3>();

    private List<GameObject> pooledObjects;

    private void Awake()
    {
        pooledObjects = new List<GameObject>(); 

        for (int i = 0; i < initialSize; i++)
        {
            AddObjectToPool();
        }
    }

    public BotPool(GameObject prefab, int initialSize)
    {
        this.prefab = prefab;
        pooledObjects = new List<GameObject>();
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (pooledObjects[i] != null && !pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        // Nếu không có object nào trong pool khả dụng, thêm một object mới vào pool
        return AddObjectToPool();
    }

    public void ReturnToPool(GameObject bot)
    {
        if (bot != null)
        {

            bot.SetActive(true);

            //bot.transform.position = Vector3.zero;
            int randomSpawnPoint = Random.Range(0, 3);

            if (randomSpawnPoint == 0)
            {
                bot.transform.position = spawnPoint1.position;
            }
            else if (randomSpawnPoint == 1)
            {
                bot.transform.position = spawnPoint2.position;
            }
            else
            {
                bot.transform.position = spawnPoint3.position;
            }

            bot.GetComponent<Collider>().enabled = true;

            // Đặt bot lên NavMesh
            NavMeshAgent agent = bot.GetComponent<NavMeshAgent>();
            if (agent != null && NavMesh.SamplePosition(bot.transform.position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
            {
                agent.Warp(hit.position);
            }

            // Reset lại trạng thái, máu của bot
            Bot botComponent = bot.GetComponent<Bot>();
            if (botComponent != null)
            {
                {
                    botComponent.isDead = false;
                    botComponent.gameObject.tag = Constant.TAG_BOT;
                    botComponent.anim.SetBool(botComponent.deadAnimParam, false);
                    botComponent.currentHealth = botComponent.maxHealth;
                    botComponent.health.SetMaxHealth(botComponent.maxHealth);
                }
            }
        }
    }

    private GameObject AddObjectToPool()
    {
        GameObject obj = Object.Instantiate(prefab);
        obj.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }
}
