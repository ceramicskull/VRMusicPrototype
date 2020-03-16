using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayKey : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayNote(){
        audioSource.Play();
        Debug.Log("working?");
    }
    public void StopNote(){
        audioSource.Stop();
    }
}
