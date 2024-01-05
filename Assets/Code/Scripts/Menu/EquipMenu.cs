using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Radial Menu Used to Equip/Unequip Weapons
/// </summary>
public class EquipMenu : MonoBehaviour
{

    [SerializeField]List<GameObject> weaponButtons;
    [SerializeField]List<Image> weaponIcons;

    public void OnEnable()
    {
        CheckWeapons();
    }

    public void EquipWeapon(int weaponSlot)
    {
        WeaponManager.ChangeWeapon(weaponSlot);
        EventManager.EquipMenuEvent();
    }

    /// <summary>
    /// If we have the weapon that coresponds to the radial menu slot
    /// the button is enabled and an icon of the weapon is displayed
    /// </summary>
    public void CheckWeapons() 
    {
        var enumerator = WeaponManager.weapons.GetEnumerator();
  
        int buttonIndex = 0;
        while (enumerator.MoveNext())
        {
            if (enumerator.Current.Value != null)
            {
                weaponIcons[buttonIndex].enabled=true;
                weaponButtons[buttonIndex].GetComponent<Button>().enabled = true;
            }
            buttonIndex++;
        }
        enumerator.Dispose();


    }
}
