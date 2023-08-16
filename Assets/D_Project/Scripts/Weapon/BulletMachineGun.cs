using System.Collections;
using System.Collections.Generic;
using TowerDefense.Game;
using UnityEngine;

public class BulletMachineGun : MonoBehaviour
{
    public Transform target;
    public float bulletSpeed;
    public int bulletDamage = 20;
    Vector3 targetPosition;


    private bool isFired = false; // Xác định xem viên đạn đã được bắn ra hay chưa
    private Vector3 startPosition;


    public void Start()
    {
        startPosition = transform.position;
    }

    public void Update()
    {
        if (isFired)
        {
            MoveBullet();

            // Kiểm tra va chạm với bot
            if (target != null && Vector3.Distance(transform.position, targetPosition) < 0.2f)
            {
                gameObject.SetActive(false);
                isFired = false;
            }

            // Kiểm tra viên đạn đã bay đủ khoảng cách 3f chưa
            if (Vector3.Distance(transform.position, startPosition) > 50f)
            {
                gameObject.SetActive(false);
                isFired = false;
            }
        }
    }
    public void MoveBullet()
    {
        if (Vector3.Distance(transform.position, target.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, bulletSpeed * Time.deltaTime);
        }
    }

    public void SetDirection(Transform n_target)
    {
        target = n_target;

        targetPosition = n_target.position;

        isFired = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Kiểm tra nếu va chạm với bot hoặc trứng bot
        if (other.CompareTag(Constant.TAG_BOT))
        {
            Bot bot = other.GetComponent<Bot>();
            if (bot != null)
            {
                bot.TakeDamage(bulletDamage); // Gây sát thương cho bot
            }

            gameObject.SetActive(false);
            isFired = false;
        }
        else if (other.CompareTag(Constant.TAG_BOT))
        {
            Bot bot = other.GetComponent<Bot>();
            if (bot != null)
            {
                bot.TakeDamage(bulletDamage); // Gây sát thương cho trứng bot
            }

            gameObject.SetActive(false);
            isFired = false;
        }
    }
}