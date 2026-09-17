using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public AudioSource AudioSorce;

    public GameObject Crystal_model;
    public GameObject Explosion;

  

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            AudioSorce.PlayOneShot(AudioSorce.clip);
            Crystal_model.SetActive(false);
            Explosion.SetActive(true);
            StartCoroutine(reload());
        }
    }

    IEnumerator reload()
    {
        yield return new WaitForSeconds(0.6f);
        Crystal_model.SetActive(true);
        Explosion.SetActive(false);
    }
}

