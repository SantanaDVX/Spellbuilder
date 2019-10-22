using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName = "Spells/New Spell")]
public class SpellStats : ScriptableObject {
    public new string name;
    public int manaCost;
    public float cooldown;
    public GameObject prefab;
    public float castTime;
}
