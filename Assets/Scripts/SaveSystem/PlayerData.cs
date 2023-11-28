using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public float health;
    public float stamina;
    public float[] position;
    public float[] rotation;
    public int[] weapons;
    public int currentWeapon;
    public int kills;
    public string location;

    public PlayerData() 
    {
        playerName = "Test";
        health = 100;
        stamina = 100;
        position =new float[3];
        position[0] = 0;
        position[1] = 0;
        position[2] = 0;
        rotation = new float[3];
        rotation[0] = 0;
        rotation[1] = 0;
        rotation[2] = 0;
        weapons = new int[4];
        weapons[0] = 1;
        weapons[1] = 0;
        weapons[2] = 0;
        weapons[3] = 0;
        currentWeapon = 0;
        kills = 999;
        location = "Serres";
    }

    public PlayerData(string playerName)
    {
        this.playerName = playerName;
        health = 100;
        stamina = 100;
        //First spawn Position in the game
        position = new float[3];
        position[0] = 0;
        position[1] = 0;
        position[2] = 0;
        rotation = new float[3];
        rotation[0] = 0;
        rotation[1] = 0;
        rotation[2] = 0;
        //Only Fists Available in the Begining
        weapons = new int[4];
        weapons[0] = 1;
        weapons[1] = 0;
        weapons[2] = 0;
        weapons[3] = 0;
        currentWeapon = 0;
        kills = 0;
        location = "Forest";
    }


    public PlayerData(PlayerLogic player, Transform transform, Dictionary<PlayerWeapon,Weapon>  weapons, Weapon currentWeapon)
    {
        this.playerName = player.playerName;
        health = player.health;
        stamina = player.stamina;
        //First spawn Position in the game
        this.position = new float[3];
        this.position[0] = transform.position.x;
        this.position[1] = transform.position.y;
        this.position[2] = transform.position.z;
        this.rotation = new float[3];
        this.rotation[0] = transform.rotation.eulerAngles.x;
        this.rotation[1] = transform.rotation.eulerAngles.y;
        this.rotation[2] = transform.rotation.eulerAngles.z;
        //Only Fists Available in the Begining
        this.weapons = new int[4];
        var enumerator = weapons.GetEnumerator();
        int index = 0;
        while (enumerator.MoveNext())
        {
            if (enumerator.Current.Value != null)
            {
                this.weapons[index] = 1;
                if (currentWeapon == enumerator.Current.Value) 
                {
                    this.currentWeapon = index;
                }
            }
            index++;
        }


        kills = 0;

        this.location = SceneManager.GetActiveScene().name;
    }

}
