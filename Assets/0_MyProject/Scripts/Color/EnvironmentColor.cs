using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentColor : MonoBehaviour
{
    public float delta;
    public float timeChange;
    public Material shader_color;
    
    
    private float timer = 0;
    private bool state;
    
    private void Start()
    {
       
    }

    private void Update()
    {
        delta = change_color(timeChange);
        
        RenderSettings.skybox.SetFloat("_Exposure", delta - 0.5f);
        shader_color.SetColor("_EmissionColor", new Color(1- delta, 1- delta, 1- delta));
    }

    private float change_color(float time)
    {
 
        if(timer < 0)
        {
            state = true;
        }
        if (timer > 1)
        {
            state = false;
        }

        if(state)
        {
            timer += Time.deltaTime / time ;
        }
        else
        {
            timer -=Time.deltaTime / time;
        }
        return timer;
    }
}
