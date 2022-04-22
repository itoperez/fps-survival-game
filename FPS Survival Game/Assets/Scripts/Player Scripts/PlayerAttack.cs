using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager weapon_Manager;

    public float assualtRifleFireRate = 50f;             // range 0-100
    private float nextTimeToFire;
    public float revolverDamage = 20f, shotgunDamage = 45f, assualtRifleDamage = 10f;

    private Animator zoomCameraAnim;
    private bool zoomed;

    private Camera mainCam;

    private GameObject crosshair;

    private bool is_Aiming;

    [SerializeField]
    private GameObject arrow_Prefab, spear_Prefab;

    private void Awake()
    {
        weapon_Manager = GetComponent<WeaponManager>();

        zoomCameraAnim = transform.Find(Tags.LOOK_ROOT).transform.Find(Tags.ZOOM_CAMERA).GetComponent<Animator>();

        crosshair = GameObject.FindWithTag(Tags.CROSSHAIR);

        mainCam = Camera.main;

    } // awake

    private void Update()
    {
        WeaponShoot();
        ZoomInAndOut();

    } // update

    void WeaponShoot()
    {
        // rapid fire weapons aka ASSAULT RIFLE
        if (weapon_Manager.GetCurrentSelectedWeapon().fireType == WeaponFireType.MULTIPLE)
        {
            if (Input.GetMouseButton(0) && Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 20f / assualtRifleFireRate;

                weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();

                BulletFired(assualtRifleDamage);
            }        
        }
        else // single fire weapons
        {
            if (Input.GetMouseButtonDown(0))
            {
                // strike with AXE
                if (weapon_Manager.GetCurrentSelectedWeapon().tag == Tags.AXE_TAG)
                {
                    weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();
                }

                // shoot REVOLVER and SHOTGUN
                if (weapon_Manager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.BULLET)
                {
                    weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();

                    if (weapon_Manager.GetCurrentSelectedWeapon().name == Tags.REVOLVER_TAG)
                    {
                        BulletFired(revolverDamage);
                    } else
                    {
                        BulletFired(shotgunDamage);
                    }

                    

                } else // shoot BOW and throw SPEAR
                {
                    if (is_Aiming)
                    {
                        weapon_Manager.GetCurrentSelectedWeapon().ShootAnimation();

                        if(weapon_Manager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.ARROW)
                        {
                            // throw ARROW
                            ThrowArrowOrSpear(true);

                        } else if (weapon_Manager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.SPEAR)
                        {
                            // throw SPEAR
                            ThrowArrowOrSpear(false);
                        }
                    }
                }
            }
        }

    } // weapon shoot

    void ZoomInAndOut()
    {
        // Aim with "FP Camera" for REVOLVER, SHOTGUN, and ASSAULT RIFLE
        if (weapon_Manager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.AIM)
        {
            // press button down
            if (Input.GetMouseButtonDown(1))    
            {
                zoomCameraAnim.Play(AnimationTags.ZOOM_IN_ANIM);

                crosshair.SetActive(false);
            }
            // release button up
            if (Input.GetMouseButtonUp(1))      
            {
                zoomCameraAnim.Play(AnimationTags.ZOOM_OUT_ANIM);

                crosshair.SetActive(true);
            }
        } // if weapon can be aimed and zoomed in

        // Aim with a animation for SPEAR and BOW
        if (weapon_Manager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.SELF_AIM)
        {
            // press button down
            if (Input.GetMouseButtonDown(1))
            {
                weapon_Manager.GetCurrentSelectedWeapon().Aim(true);
                is_Aiming = true;
            }
            // release button up
            if (Input.GetMouseButtonUp(1))
            {
                weapon_Manager.GetCurrentSelectedWeapon().Aim(false);
                is_Aiming = false;
            }
        } // if weapon is self aim

    } // zoom in and out

    void ThrowArrowOrSpear(bool throwArrow)
    {
        if (throwArrow)
        {
            GameObject arrow = Instantiate(arrow_Prefab);
            arrow.transform.position = mainCam.transform.position + (mainCam.transform.forward * 1.2f);

            arrow.GetComponent<ArrowSpearScript>().Launch(mainCam, throwArrow);
        } else
        {
            GameObject spear = Instantiate(spear_Prefab);
            spear.transform.position = mainCam.transform.position + (mainCam.transform.forward * 1.2f);

            spear.GetComponent<ArrowSpearScript>().Launch(mainCam, throwArrow);
        }

    } // throw arrow or spear

    void BulletFired(float damage)
    {
        RaycastHit hit;

        if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out hit))
        {
            print("We hit: " + hit.transform.gameObject.name);
            if (hit.transform.tag == Tags.ENEMY_TAG)
            {
                hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);
            }

        }

    } // bullet fired

}
