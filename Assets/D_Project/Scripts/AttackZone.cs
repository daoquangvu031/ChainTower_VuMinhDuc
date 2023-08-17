using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    public List<GameObject> botsInRange = new List<GameObject>();
    public TurretBase turret;

    private void Update()
    {
        botsInRange.RemoveAll(bot => bot == null || bot.GetComponent<Bot>() == null || bot.GetComponent<Bot>().isDead);

        if (botsInRange.Count > 0)
        {
            GameObject targetBot = GetNearestBot();
            turret.SetTargetBot(targetBot);
        }
        else
        {
            turret.SetTargetBot(null);
        }
    }

    private GameObject GetNearestBot()
    {
        GameObject nearestBot = null;
        float minDistance = Mathf.Infinity;
        foreach (GameObject bot in botsInRange)
        {
            float distance = Vector3.Distance(transform.position, bot.transform.position);
            if (distance < minDistance)
            {
                nearestBot = bot;
                minDistance = distance;
            }
        }
        return nearestBot;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_BOT))
        {
            if (!botsInRange.Contains(other.gameObject))
            {
                botsInRange.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.TAG_BOT))
        {
            if (botsInRange.Contains(other.gameObject))
            {
                botsInRange.Remove(other.gameObject);
            }
        }
    }

}
