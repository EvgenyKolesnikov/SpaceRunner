using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus_SpeedDown : MonoBehaviour
{
    public float SpeedDown;

    public AudioSource AudioSorce;

  

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Platforms_Moving.speed_coef -= SpeedDown;
            AudioSorce.PlayOneShot(AudioSorce.clip);

        }
    }

}
