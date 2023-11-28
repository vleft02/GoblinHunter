using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic
{
    public string playerName;
/*
    public string location;*/

    public float health;

    public float stamina;

    public float maxStamina;

    public PlayerLogic()
    {
        this.playerName = "Test";
/*        this.location = "Forest";*/
        this.health = 100;
        this.stamina = 100;
        this.maxStamina = 100;
    }


    public void fillData(PlayerData data) 
    {
        this.playerName = data.playerName;
/*        this.location = "Forest";*/
        this.health = data.health;
        this.stamina = data.stamina;
    }

}
