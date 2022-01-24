using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Sources : MonoBehaviour
{
    public AudioSource aggressive_bark;
    public AudioSource panting_calm;
    public AudioSource panting;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void StopAllSounds()
    {
        aggressive_bark.Stop();
        panting.Stop();
        panting_calm.Stop();
    }
}
