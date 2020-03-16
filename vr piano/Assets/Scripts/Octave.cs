using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octave : MonoBehaviour
{
    // Start is called before the first frame update
    public float octave = 1f;
    public AudioSource[] keys;
    void Start()
    {
        foreach(var key in keys)
        {
            key.pitch = octave;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
