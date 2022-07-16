using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarHandler : MonoBehaviour
{
    public GameObject targetCharacter;
    GameObject hp;
    Image hpImage;
    Sprite green;
    Sprite red;

    // Start is called before the first frame update
    void Start()
    {
        hp = transform.GetChild(0).gameObject;
        hpImage = hp.GetComponent<Image>();

        green = Resources.Load<Sprite>("Images/Green");
        red = Resources.Load<Sprite>("Images/Red");

        if (targetCharacter.CompareTag("Ally"))
        {
            hpImage.sprite = green;
        }
        else if (targetCharacter.CompareTag("Enemy"))
        {
            hpImage.sprite = red;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(targetCharacter.transform.position + new Vector3(0, 1.0f, -1.0f));

        if (targetCharacter.CompareTag("Ally"))
        {
            hpImage.fillAmount = targetCharacter.GetComponent<Ally>().getHPRate();
            if (targetCharacter.GetComponent<Ally>().isRetire)
            {
                gameObject.SetActive(false);
            }
        }
        else if (targetCharacter.CompareTag("Enemy"))
        {
            hpImage.fillAmount = targetCharacter.GetComponent<Enemy>().getHPRate();
            if (targetCharacter.GetComponent<Enemy>().isRetire)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
