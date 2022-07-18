using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackTrigger : MonoBehaviour
{
    public GameObject parent;
    bool isAttack;

    // Start is called before the first frame update
    void Start()
    {
        isAttack = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        
        if (parent.tag.Equals("Ally"))
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                if (parent.GetComponent<Ally>().isAttack)
                {
                    collider.gameObject.GetComponent<Enemy>().GetDamage(parent.GetComponent<Ally>().attackDamage);
                }
            }
        }
        else if (parent.tag.Equals("Enemy"))
        {
            if (collider.gameObject.CompareTag("Ally"))
            {
                if (parent.GetComponent<Enemy>().isAttack)
                {
                    collider.gameObject.GetComponent<Ally>().GetDamage(parent.GetComponent<Enemy>().attackDamage);
                }
            }
        }
    }
}
