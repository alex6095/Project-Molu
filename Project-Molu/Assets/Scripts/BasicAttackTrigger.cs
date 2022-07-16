using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackTrigger : MonoBehaviour
{
    float damage;
    public GameObject parent;
    public bool attacked;

    // Start is called before the first frame update
    void Start()
    {
        damage = 10;
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
                collider.gameObject.GetComponent<Enemy>().GetDamage(damage);
                attacked = true;
            }
        }
        else if (parent.tag.Equals("Enemy"))
        {
            if (collider.gameObject.CompareTag("Ally"))
            {
                collider.gameObject.GetComponent<Ally>().GetDamage(damage);
                attacked = true;
            }
        }
    }
}
