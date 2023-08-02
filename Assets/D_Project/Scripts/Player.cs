using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Animator Anim;
    [SerializeField] string currentAnimName;

    public Text coin;
    public Rigidbody rb;
    public Health health;
    public float moveSpeed;
    public int currentHealth;
    public int maxHealth = 100;
    public int currentCoins = 0;
    public float attackCooldown;

    private Vector3 input;
    private bool canAttack = true;

    public void Start()
    {
        JoystickControl.EnableJoystick();

        UpdateCoinText();

        StartCoroutine(AttackRoutine());

        currentHealth = maxHealth;
        health.SetMaxHealth(maxHealth);
    }
    public void Update()
    {
        input = JoystickControl.direct;

        //    if (Input.GetKeyDown(KeyCode.Space))
        //    {
        //        TakeDamage(20);
        //    }

        /// public counterTimeAttack;
        /// private timer = 0; - ngoai update
        /// 
        /// 
        /// timer += Time.deltaTime;
        /// if(timer > counterTimeAttack)
        /// {
        /// if(target == null) thi khong danh
        /// attack();
        /// timer = 0;
        /// }

    }

    private void FixedUpdate()
    {
        if (input != Vector3.zero)
        {
            // Di chuyển player theo hướng JoystickControl.direct
            rb.velocity = input * moveSpeed;
            // Quay player theo hướng JoystickControl.direct
            transform.forward = input;
            ChangeAnim(Constant.ANIM_RUN);
        }
        else
        {
            rb.velocity = Vector3.zero;
            ChangeAnim(Constant.ANIM_IDLE);
        }
    }

    public void TakeDamage(int damege)
    {
        currentHealth -= damege;
        health.SetHealth(currentHealth);
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            Anim.ResetTrigger(animName);
            currentAnimName = animName;
            Anim.SetTrigger(currentAnimName);
        }
    }

    public void ResetAnim()
    {
        ChangeAnim("");
    }

    public void Attack()
    {

    }

    public IEnumerator AttackRoutine()
    {
        while (true)
        {
            if (canAttack)
            {
                Attack();
                yield return new WaitForSeconds(attackCooldown);
            }
            else
            {
                yield return null;
            }
        }
    }

    private void UpdateCoinText()
    {
        coin.text = currentCoins.ToString();
    }

    public void AddCoin(int amount)
    {
        currentCoins += amount;
        UpdateCoinText();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_COIN))
        {
            other.GetComponent<Coin>().EatCoin();
            AddCoin(1);
        }
    }
}