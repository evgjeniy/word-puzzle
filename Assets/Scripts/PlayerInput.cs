using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static Vector2 LastTouchPosition;
    
    public static event Action<Touch> OnSingleTouchScreen;
    public static event Action<Touch, Touch> OnDoubleTouchScreen;

    private void Update()
    {
        if (Input.touchCount == 0) return;

        LastTouchPosition = Input.GetTouch(0).position;
        
        switch (Input.touchCount)
        {
            case 1: 
                OnSingleTouchScreen?.Invoke(Input.GetTouch(0)); 
                break;
            case 2: 
                OnDoubleTouchScreen?.Invoke(Input.GetTouch(0), Input.GetTouch(1)); 
                break;
        }
    }
}