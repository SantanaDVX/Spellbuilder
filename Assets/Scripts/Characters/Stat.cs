using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat {
    
    [SerializeField]
    private float baseValue;
    private List<float> additiveModifiers = new List<float>();

    public Stat(float baseValue) {
        this.baseValue = baseValue;
    }

    public float GetValue() {
        float finalValue = baseValue;
        //additiveModifiers.ForEach(x => finalValue += x);
        return finalValue;
    }

    public void AddAdditiveModifier(float mod) {
        if (mod != 0.0f) {
            additiveModifiers.Add(mod);
        }
    }

    public void RemoveAdditiveModifier(float mod) {
        if (mod != 0.0f) {
            additiveModifiers.Remove(mod);
        }
    }

}
