using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DrawUIEvents : MonoBehaviour
{
    private UIDocument _document;
    //public PlayerAbility will be used to connect the player's ability with the UI

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
        
    }

   
}
