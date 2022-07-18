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
    public Stats stats;

    public Character(string name, int level, int rarity, Stats stats)
    {
        this.name = name;
        this.level = level;
        this.rarity = rarity;
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

    //public Character ReturnClass()
    //{
    //    return new Character(nameText.text, Int32.Parse(levelText.text), Int32.Parse(rarityText.text), Stats());
    //}

    public void SetUi(Character character)
    {
        nameText.text = character.name;
        levelText.text = character.level.ToString();
        rarityText.text = character.rarity.ToString();
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
