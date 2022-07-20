using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;

public class SkillObject : MonoBehaviour
{

    GameObject effectPrefab;
    PhotonView pv;
    // Start is called before the first frame update
    void Start()
    {
        effectPrefab = Resources.Load("Prefabs/Effects/BigExplosion") as GameObject;
        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        if (pv.IsMine)
        {
            Explosion(true);
            pv.RPC("Explosion", RpcTarget.Others, false);
            
        }
    }

    [PunRPC]
    void Explosion(bool isMySkill)
    {
        GameObject effectObject = Instantiate(effectPrefab, transform.position, transform.rotation);
        Destroy(effectObject, 3.0f);

        Vector3 top = new Vector3(transform.localPosition.x, transform.localPosition.y + 2, transform.localPosition.z);
        Vector3 bottom = new Vector3(transform.localPosition.x, transform.localPosition.y - 2, transform.localPosition.z);
        Collider[] colls = Physics.OverlapCapsule(top, bottom, transform.localScale.x / 2);

        float skillDamage = 70;

        if (isMySkill) {
            if (PhotonManager.instance.playerNo == 1)
            {
                for (int i = 0; i < colls.Length; i++)
                {
                    if (colls[i].gameObject.CompareTag("Enemy"))
                    {
                        colls[i].gameObject.GetComponent<Enemy>().GetDamage(skillDamage);
                    }
                }
            }
            else
            {
                for (int i = 0; i < colls.Length; i++)
                {
                    if (colls[i].gameObject.CompareTag("Ally"))
                    {
                        colls[i].gameObject.GetComponent<Ally>().GetDamage(skillDamage);
                    }
                }
            }
        }
        else
        {
            if (PhotonManager.instance.playerNo == 1)
            {
                for (int i = 0; i < colls.Length; i++)
                {
                    if (colls[i].gameObject.CompareTag("Ally"))
                    {
                        colls[i].gameObject.GetComponent<Ally>().GetDamage(skillDamage);
                    }
                }
            }
            else
            {
                for (int i = 0; i < colls.Length; i++)
                {
                    if (colls[i].gameObject.CompareTag("Enemy"))
                    {
                        colls[i].gameObject.GetComponent<Enemy>().GetDamage(skillDamage);
                    }
                }
            }
        }
    }

}
