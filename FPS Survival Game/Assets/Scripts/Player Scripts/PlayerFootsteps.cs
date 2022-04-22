using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    private AudioSource footstep_Sound;

    [SerializeField]
    private AudioClip[] footstep_Clip;

    private CharacterController character_Controller;

    [HideInInspector]
    public float volume_Min, volume_Max;

    private float accumulated_Distance;

    [HideInInspector]
    public float step_Distance;

    private void Awake()
    {
        footstep_Sound = GetComponent<AudioSource>();

        character_Controller = GetComponentInParent<CharacterController>();

    } // awake

    private void Update()
    {
        CheckToPlayFootstepSound();  
        
    } // update

    void CheckToPlayFootstepSound()
    {
        // only play sound if not airborne, return otherwise
        if (!character_Controller.isGrounded)
        {
            return;
        }

        // if character is moving handle footstep sounds
        if (character_Controller.velocity.sqrMagnitude > 0)
        {
            accumulated_Distance += Time.deltaTime;

            if (accumulated_Distance > step_Distance * 1.5f)
            {
                footstep_Sound.volume = Random.Range(volume_Min, volume_Max);
                footstep_Sound.clip = footstep_Clip[Random.Range(0, footstep_Clip.Length)];
                footstep_Sound.Play();

                accumulated_Distance = 0;
            }

        } else // reset distance between steps
        {
            accumulated_Distance = 0;
        }

    } // check to play footstep sound

}
