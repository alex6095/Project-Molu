using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject parent;
    public GameObject target;
    Vector3 direction;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(0, 0, 0);
        Destroy(gameObject, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire(GameObject parent, GameObject target)
    {
        this.parent = parent;
        this.target = target;
        transform.LookAt(this.target.transform.position);
        direction = this.target.transform.position - transform.position;
        direction = Vector3.Normalize(direction);
        GetComponent<Rigidbody>().velocity = direction * speed;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (parent.tag.Equals("Ally"))
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                collider.gameObject.GetComponent<Enemy>().GetDamage(parent.GetComponent<Ally>().attackDamage);
                Destroy(gameObject);
            }
        }
        else if (parent.tag.Equals("Enemy"))
        {
            if (collider.gameObject.CompareTag("Ally"))
            {
                collider.gameObject.GetComponent<Ally>().GetDamage(parent.GetComponent<Enemy>().attackDamage);
                Destroy(gameObject);
            }
        }
    }
}
