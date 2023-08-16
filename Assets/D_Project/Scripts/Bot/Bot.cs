using System.Collections;
using System.Collections.Generic;
using TowerDefense.Game;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class Bot : MonoBehaviour
{
    [SerializeField] public NavMeshAgent agent;

    public string deadAnimParam = "IsDead";
    public string currentAnimName;
    public GameObject coinPrefab;   
    public int currentHealth;
    public Transform target;
    public ParticleSystem boomPraticleSystem;
    public int maxHealth;
    public int botDamage;
    public Health health;
    public Animator anim;
    public bool isDead;

    private IState currentState;

    public void Start()
    {

        BotManager botManager = FindObjectOfType<BotManager>();
        if (botManager != null)
        {
            botManager.AddBot(this);
        }

        GameObject player = GameObject.FindGameObjectWithTag("Character");
        if (player != null)
        {
            SetTarget(player.transform);
        }

        currentHealth = maxHealth;
        health.SetMaxHealth(maxHealth);
    }

    public void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    TakeDamage(20);
        //}
        if (currentHealth <= 0)
        {
            Die();
        }
        if (!isDead && target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    private IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(3f);

        BotPool botPool = FindObjectOfType<BotPool>();
        
        if (botPool != null)
        {
            botPool.ReturnToPool(gameObject);
        }
    }

    public void DisableCollider()
    {
        GetComponent<Collider>().enabled = false;
    }

    public void DisableAgent()
    {
        agent.isStopped = true;
    }

    public void Die()
    {
        if (isDead) return;

        TriggerBoomEffect();

        anim.SetBool(deadAnimParam, true); // Set animation param "isDead" to true

        isDead = true;

        DisableAgent();

        gameObject.tag = Constant.TAG_UNTAGGED;

        DisableCollider();

        GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);

        StartCoroutine(DieCoroutine());
    }
    private void TriggerBoomEffect()
    {
        if (boomPraticleSystem != null)
        {
            boomPraticleSystem.gameObject.SetActive(true);

            boomPraticleSystem.transform.position = transform.position;
            
            boomPraticleSystem.Play();
        }
    }

        public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        health.SetHealth(currentHealth);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            Player player = other.GetComponent<Player>();
            if(player != null)
            {
                player.TakeDamage(botDamage);
            }
        }
    }

    public void SetTarget(Transform newTarget)
    {
        this.target = newTarget;
    }

    public void StopMoving()
    {
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
    }

    //public void ChangeState(IState newState)
    //{
    //    currentState?.OnExit(this);

    //    currentState = newState;

    //    currentState?.OnEnter(this);
    //}

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
}
