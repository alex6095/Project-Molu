using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using System;


//“name” : “Unity - Chan WGS”
// 		“level” : 1
//		“stats” : { “speed” : 3, “hp” : 70, “attackRange” : 1.3, “attackDamage” 20 }
//		“rarity” : 3

public class Character
{
    public string name;
    public int level;
    public int rarity;
    public string weapon;
    public Stats stats;

    public Character(string name, int level, int rarity, string weapon, Stats stats)
    {
        this.name = name;
        this.level = level;
        this.rarity = rarity;
        this.weapon = weapon;
        this.stats = stats;
    }
}
public class Stats
{
    public float speed;
    public float maxHealth;
    public float attackRange;
    public float attackDamage;

    public Stats(float speed, float maxHealth, float attackRange, float attackDamage)
    {
        this.speed = speed;
        this.maxHealth = maxHealth;
        this.attackRange = attackRange;
        this.attackDamage = attackDamage;
    }
}

public class CharacterBox : MonoBehaviour
{

    //public TMP_InputField nameInput;
    public TMP_Text nameText;
    public TMP_Text levelText;
    public TMP_Text rarityText;

    private Dictionary<string, List<float>> modelMap = new Dictionary<string, List<float>>() {
        { "Unity-Chan", new List<float>() { 150, 0 } },
        { "Misaki", new List<float>() { 150, -6 } },
        { "Unity-Chan WGS", new List<float>() { 150, 4 } },
        { "WizardYuko", new List<float>() { 150, -4 } },
        { "Yuko", new List<float>() { 150, -6 } },
    };

    private List<float> xAxis = new List<float>()
    {
        -55,
        -18.5f,
        18.5f,
        55
    };

    private GameObject characterPrefab;

    //public Character ReturnClass()
    //{
    //    return new Character(nameText.text, Int32.Parse(levelText.text), Int32.Parse(rarityText.text), Stats());
    //}

    public void SetUi(Character character, int idx)
    {
        nameText.text = character.name;
        levelText.text = character.level.ToString();
        rarityText.text = character.rarity.ToString();

        characterPrefab = Resources.Load("Prefabs/ShowWindow/"+character.name+"_model") as GameObject;
        GameObject characterObj = Instantiate(characterPrefab, new Vector3(xAxis[idx]+modelMap[character.name][1], -11, 80), Quaternion.Euler(new Vector3(0, modelMap[character.name][0], 0)));
        //Debug.Log("Parent : "+characterObj.transform.parent);
        characterObj.transform.localScale = new Vector3(30, 30, 30);
        // 나중에 바꾸는 것 고려
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
