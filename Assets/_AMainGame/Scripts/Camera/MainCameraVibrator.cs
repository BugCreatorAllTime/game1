using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MainCameraVibrator : MonoBehaviour
{
    [SerializeField]
    private UnityEvent vibrate_Delegate;
    
    [ContextMenu("Vibrate")]
    public void Vibrate()
    {
        vibrate_Delegate?.Invoke();
    }
}
