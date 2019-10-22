using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerManager : MonoBehaviour {
    public PlayerController player;

    private static PlayerManager instance;
    public static PlayerManager Instance() {
        if (instance == null) {
            instance = FindObjectOfType<PlayerManager>();
        }
        return instance;    
    }
}
