using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRocketTower : MonoBehaviour
{
    public float speed;
    public int damageRocket;

    private Transform target;
    private bool isFly = false;


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
        transform.Translate((direction + Vector3.up) * speed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_BOT))
        {
            Debug.Log("1");
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
