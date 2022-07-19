using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GridViewItem : MonoBehaviour
{
    //[SerializeField]
    //private TextMeshProUGUI myText1;
    //[SerializeField]
    //private TextMeshProUGUI myText2;
    //[SerializeField]
    //private Image myImage;


    // Start is called before the first frame update
    public void SetGridUI(string textString1, string textString2,Sprite Image1)           //Sprite Image1
    {
        //Debug.Log();
        
        transform.GetChild(0).GetComponent<Image>().sprite = Image1;
        transform.GetChild(1).GetComponent<TMP_Text>().text = textString1;
        transform.GetChild(2).GetChild(0).GetComponent<TMP_Text>().text = textString2;
        //myImage.sprite = Image1;
        //myText1.text = textString1;
        //myText2.text = textString2;
    }
}
