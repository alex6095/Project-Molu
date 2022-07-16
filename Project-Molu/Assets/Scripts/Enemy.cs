using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public float attackRange;
    public bool isRetire;
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        isRetire = false;
        speed = 3;
        maxHealth = 100;
        health = maxHealth;
        attackRange = 0.5f;

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
                transform.LookAt(ally[minIndex].transform);

                Vector3 direction = ally[minIndex].transform.position - gameObject.transform.position;
                if (Vector3.Magnitude(direction) >= attackRange)
                {
                    animator.SetBool("isRun", true);
                    animator.SetBool("isPunch", false);
                    direction = Vector3.Normalize(direction) * speed;
                    GetComponent<Rigidbody>().velocity = direction;
                }
                else
                {
                    animator.SetBool("isRun", false);
                    animator.SetBool("isPunch", true);
                    GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                }
            }
            else
            {
                animator.SetBool("isRun", false);
                animator.SetBool("isPunch", false);
                animator.SetBool("isEnd", true);
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
}
