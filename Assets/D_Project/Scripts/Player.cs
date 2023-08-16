using System.Collections;
using System.Collections.Generic;
using TowerDefense.Towers;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Animator Anim;
    [SerializeField] string currentAnimName;

    public Text coin;
    public Rigidbody rb;
    public int maxHealth;
    public Health health;
    public float moveSpeed;
    public int currentCoins;
    public int currentHealth;
    public float attackCooldown;
    public Vector3 initialPosition;

    private Vector3 input;
    private bool canAttack;

    public void Start()
    {
        UIManager.Ins.OpenUI<UIMainMenu>();

        JoystickControl.DisableJoystick();

        UpdateCoinText();

        StartCoroutine(AttackRoutine());

        currentHealth = maxHealth;

        health.SetMaxHealth(maxHealth);

        initialPosition = transform.position;

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
    public void Reset()
    {
        health.SetMaxHealth(maxHealth);
        currentHealth = 100;

        currentCoins = 100;
        UpdateCoinText();
    }

    public void TakeDamage(int damege)
    {
        currentHealth -= damege;
        health.SetHealth(currentHealth);
        CheckHealth();
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

    public void UpdateCoinText()
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
        else if (other.CompareTag(Constant.TAG_WIN))
        {
            ShowVictoryCanvas();
        }
    }

    public void CheckHealth()
    {
        if(currentHealth <= 0)
        {
            ShowDefeatCanvas();
        }
    }

    public void ShowDefeatCanvas()
    {
        JoystickControl.DisableJoystick();

        UIManager.Ins.OpenUI<UIDefeat>();

        FindObjectOfType<BotManager>().RemoveAllBots();

        foreach (var bot in FindObjectsOfType<Bot>())
        {
            Destroy(bot.gameObject);
        }
        UIDefeat.defeatCanvasDisplayed = true;

    }

    public void ShowVictoryCanvas()
    {
        FindObjectOfType<BotManager>().RemoveAllBots();
        
        foreach (var bot in FindObjectsOfType<Bot>())
        {
            Destroy(bot.gameObject);
        }

        JoystickControl.DisableJoystick();

        UIManager.Ins.OpenUI<UIVictory>();

        FindObjectOfType<SpawnBot>().SetVictoryDisplayed(true);
    }
}