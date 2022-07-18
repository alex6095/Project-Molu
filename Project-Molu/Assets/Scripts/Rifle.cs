using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    GameObject riflePrefab;
    GameObject rifleObject;
    public GameObject launcher;

    // Start is called before the first frame update
    void Awake()
    {
        riflePrefab = Resources.Load("Prefabs/Skills/Bullet") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Fire()
    {
        if (tag.Equals("Ally"))
        {
            rifleObject = Instantiate(riflePrefab);
            rifleObject.transform.position = launcher.transform.position;

            GameObject target = GetComponent<Ally>().target;
            rifleObject.GetComponent<Bullet>().Fire(this.gameObject, target);
        }
        else if (tag.Equals("Enemy"))
        {
            rifleObject = Instantiate(riflePrefab);
            rifleObject.transform.position = launcher.transform.position;

            GameObject target = GetComponent<Enemy>().target;
            rifleObject.GetComponent<Bullet>().Fire(this.gameObject, target);
        }
    }
}
