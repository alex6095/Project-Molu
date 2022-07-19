using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float maxHealth;
    public float health;
    public float attackRange;
    public float attackDamage;
    public bool isRetire;
    public bool isAttack;
    Animator animator;

    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        isRetire = false;
        isAttack = true;
        health = maxHealth;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!BattleManager.Instance.IsBattlePhase()) return;

        if (isRetire)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Retire") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            GameObject[] ally = BattleManager.Instance.GetAlly();
            float min = 1000000;
            int minIndex = -1;
            for (int i = 0; i < ally.Length; i++)
            {
                if (ally[i].gameObject.activeSelf && !ally[i].gameObject.GetComponent<Ally>().isRetire)
                {
                    float distance = Vector3.Distance(gameObject.transform.position, ally[i].transform.position);
                    if (distance < min)
                    {
                        min = distance;
                        minIndex = i;
                    }
                }
            }

            if (minIndex != -1)
            {
                target = ally[minIndex];

                transform.LookAt(target.transform);

                Vector3 direction = target.transform.position - gameObject.transform.position;
                if (Vector3.Magnitude(direction) >= attackRange)
                {
                    animator.SetBool("isRun", true);
                    animator.SetBool("isAttack", false);
                    direction = Vector3.Normalize(direction);
                    GetComponent<Rigidbody>().velocity = direction * speed;
                }
                else
                {
                    animator.SetBool("isRun", false);
                    animator.SetBool("isAttack", true);
                    GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                }
            }
            else if (BattleManager.Instance.mode != 1)
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

                BattleManager.Instance.GameOver(false);
            }
            else
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }
        }
    }

    public void GetDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Retire();
        }
    }

    void Retire()
    {
        animator.SetBool("isRetire", true);
        isRetire = true;
    }

    public float getHPRate()
    {
        return health / maxHealth;
    }

    void AttackStart()
    {
        isAttack = true;
    }

    void AttackFinish()
    {
        isAttack = false;
    }
}
