using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillButton : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    GameObject skillPrefab;
    GameObject skillObject;
    public float skillDamage;
    GameObject effectPrefab;

    GameObject blackCover;
    GameObject black;
    public float coolTime;
    float leftTime;

    // Start is called before the first frame update
    void Start()
    {
        skillPrefab = Resources.Load("Prefabs/Skills/Skill_Sample") as GameObject;
        effectPrefab = Resources.Load("Prefabs/Effects/BigExplosion") as GameObject;
        skillDamage = 20;

        blackCover = transform.GetChild(0).gameObject;
        black = blackCover.transform.GetChild(0).gameObject;
        coolTime = 3.0f;
        leftTime = 0.0f;

        blackCover.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (leftTime > 0.0f)
        {
            return;
        }

        skillObject = Instantiate(skillPrefab);
        int layerMask = 1 << LayerMask.NameToLayer("Ground");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            skillObject.transform.position = hit.point;
        }
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (leftTime > 0.0f)
        {
            return;
        }

        int layerMask = 1 << LayerMask.NameToLayer("Ground");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            skillObject.transform.position = hit.point;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (leftTime > 0.0f)
        {
            return;
        }

        int layerMask = 1 << LayerMask.NameToLayer("Ground");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            skillObject.transform.position = hit.point;
            ActivateSkill();
        }
    }

    void ActivateSkill()
    {
        //쿨타임
        blackCover.SetActive(true);
        StartCoroutine(CoolTime(coolTime));

        //범위 내의 적들 찾아서 데미지 주기
        GameObject effectObject = MonoBehaviour.Instantiate(effectPrefab);
        effectObject.transform.position = skillObject.transform.position;

        Vector3 top = new Vector3(skillObject.transform.localPosition.x, skillObject.transform.localPosition.y + 2, skillObject.transform.localPosition.z);
        Vector3 bottom = new Vector3(skillObject.transform.localPosition.x, skillObject.transform.localPosition.y - 2, skillObject.transform.localPosition.z);
        Collider[] colls = Physics.OverlapCapsule(top, bottom, skillObject.transform.localScale.x / 2);

        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].gameObject.CompareTag("Enemy"))
            {
                colls[i].gameObject.GetComponent<Enemy>().GetDamage(skillDamage);
            }
        }

        //스킬 쓰면 범위 표시는 삭제
        Destroy(skillObject.gameObject);
    }

    IEnumerator CoolTime(float time) {
        leftTime = time;
        while (leftTime > 0.0f) {
            leftTime -= Time.deltaTime;
            blackCover.GetComponent<Image>().fillAmount = leftTime / coolTime;
            yield return new WaitForFixedUpdate();
        }
        blackCover.SetActive(false);
    }

}
