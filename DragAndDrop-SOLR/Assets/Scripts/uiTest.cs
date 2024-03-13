using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiTest : MonoBehaviour
{


    // Reference to the button
    public Button button;

    void Start()
    {
        // Add a listener to the button
        button.onClick.AddListener(OnButtonClick);
    }

     public void OnButtonClick()
    {
        // Write a message to the debug log
        Debug.Log("Button clicked!");
    }
}

