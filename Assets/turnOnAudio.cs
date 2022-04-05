using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnOnAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void turnOn(){
        AudioListener.pause = false;
    }
    public void turnOff(){
        AudioListener.pause = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
