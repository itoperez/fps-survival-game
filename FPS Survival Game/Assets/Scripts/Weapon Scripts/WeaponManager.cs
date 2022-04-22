using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private WeaponHandler[] weapons;

    private int current_Weapon_Index;

    private void Start()
    {
        // start game with AXE as default weapon
        current_Weapon_Index = 0;
        weapons[current_Weapon_Index].gameObject.SetActive(true);

    } // start

    private void Update()
    {
        // hotkey bar for weapons 1-6 on keyboard
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TurnOnSelectedWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TurnOnSelectedWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TurnOnSelectedWeapon(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TurnOnSelectedWeapon(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            TurnOnSelectedWeapon(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            TurnOnSelectedWeapon(5);
        }

    } // update

    void TurnOnSelectedWeapon(int weaponIndex)
    {
        // avoid draw weapon animation code below if current weapon selected again
        if (current_Weapon_Index == weaponIndex)
        {
            return;
        }

        // deactivate current, set new, update new to current (weapon)
        weapons[current_Weapon_Index].gameObject.SetActive(false);
        weapons[weaponIndex].gameObject.SetActive(true);
        current_Weapon_Index = weaponIndex;

    } // turns on selected weapon

    public WeaponHandler GetCurrentSelectedWeapon()
    {
        return weapons[current_Weapon_Index];

    } // return current weapon

}
