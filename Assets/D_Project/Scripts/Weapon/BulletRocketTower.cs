using System.Collections;
using System.Collections.Generic;
using TowerDefense.Game;
using UnityEngine;

public class BulletRocketTower : MonoBehaviour
{
    public float speed;
    public int damageRocket;

    private Transform target;
    private void Update()
    {
        if (target == null || !target.gameObject.activeSelf)
        {
            // Đối tượng mục tiêu đã bị phá hủy, hủy đạn
            Destroy(gameObject);
            return;
        }
        transform.LookAt(target.transform.position);

        // Di chuyển đạn tới mục tiêu
        Vector3 direction = (target.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_BOT))
        {
            Bot bot = other.GetComponent<Bot>();
            if (bot != null)
            {
                bot.TakeDamage(damageRocket);
            }

            gameObject.SetActive(false);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
