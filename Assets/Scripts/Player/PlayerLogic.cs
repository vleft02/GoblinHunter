using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic
{
    public string playerName;

    public float health;

    public float stamina;

    public float maxStamina;

    public PlayerLogic()
    {
        this.playerName = "Test";
        this.health = 100;
        this.stamina = 50;
        this.maxStamina = 50;
    }


    public void fillData(PlayerData data) 
    {
        this.playerName = data.playerName;
        this.health = data.health;
        this.stamina = data.stamina;
    }

}
