using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpearScript : MonoBehaviour
{
    private Rigidbody myBody;

    public float arrow_Speed = 45f;
    public float spear_Speed = 35f;

    private Vector3 targetPosition;

    public float deactivate_Timer = 10f;

    public float damage = 15f;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Invoke("DeactivateGameObject", deactivate_Timer);       
    }

    private void Update()
    {
        // for the rotation of the arrow that took me forever
        transform.rotation = Quaternion.LookRotation(myBody.velocity);          
    }

    public void Launch(Camera mainCamera, bool isArrow)
    {

        if (isArrow)
        {
            // myBody.AddForce(transform.forward * arrow_Speed);
            myBody.velocity = mainCamera.transform.forward * arrow_Speed;

        } else
        {
            // myBody.AddForce(transform.forward * arrow_Speed);
            myBody.velocity = mainCamera.transform.forward * spear_Speed;
        }

        // keeps arrow looking forward (side note: check kinematic on Prefab to make it stand still like pause arrows)
        transform.LookAt(transform.position + myBody.velocity);

    }

    void DeactivateGameObject()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }


    // Attack Point is better than using whole arrow as collider
    /*
    private void OnTriggerEnter(Collider target)
    {
        
        // after collide with enemy deactivate game object
        if (target.tag == Tags.ENEMY_TAG)
        {
            target.GetComponent<HealthScript>().ApplyDamage(damage);

            gameObject.SetActive(false);
        }
    }
    */

}
