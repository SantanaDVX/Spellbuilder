using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CharacterStats : MonoBehaviour {
    
    public Stat maxMana;
    [SerializeField]
    public float currentMana { get; private set; }
    public Stat maxHealth;
    [SerializeField]
    public float currentHealth { get; private set; }

    public Stat manaRecharge;
    public Stat healthRecharge;

    public Dictionary<DamageType, Stat> resistances = new Dictionary<DamageType, Stat>();

    private void Awake() {
        currentMana = maxMana.GetValue();
        currentHealth = maxHealth.GetValue();

        foreach (DamageType dmgType in Enum.GetValues(typeof(DamageType))) {
            resistances[dmgType] = new Stat(0f);
        }
    }

    private void Update() {
        currentMana += manaRecharge.GetValue() * Time.deltaTime;
        currentMana = Mathf.Min(currentMana, maxMana.GetValue());

        currentHealth += healthRecharge.GetValue() * Time.deltaTime;
        currentHealth = Mathf.Min(currentHealth, maxHealth.GetValue());
    }

    public void TakeDamage(int dmg, DamageType dmgType) {
        float damagePostMitigation = dmg - (resistances[DamageType.Global].GetValue() + resistances[dmgType].GetValue());
        currentHealth -= damagePostMitigation;
        Debug.Log(this.name + " took " + damagePostMitigation + " damage, at " + currentHealth + " health.");
        if (currentHealth <= 0) {
            Die();
        }
    }

    public virtual void Die() { }

    public void SpendMana(int manaCost) {
        currentMana -= currentMana;
    }
}
