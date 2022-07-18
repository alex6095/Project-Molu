using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    GameObject magicPrefab;
    GameObject magicObject;
    public GameObject launcher;

    // Start is called before the first frame update
    void Awake()
    {
        magicPrefab = Resources.Load("Prefabs/Skills/FireBall") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Fire()
    {
        if (tag.Equals("Ally"))
        {
            magicObject = Instantiate(magicPrefab);
            magicObject.transform.position = launcher.transform.position;

            GameObject target = GetComponent<Ally>().target;
            magicObject.GetComponent<Magic>().Fire(this.gameObject, target);
        }
        else if (tag.Equals("Enemy"))
        {
            magicObject = Instantiate(magicPrefab);
            magicObject.transform.position = launcher.transform.position;

            GameObject target = GetComponent<Enemy>().target;
            magicObject.GetComponent<Magic>().Fire(this.gameObject, target);
        }
    }
}
