using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCanvas : MonoBehaviour
{
    public GameObject canvas;


    public static ControllerCanvas instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Une instance "+ this.GetType().Name+ " est déjà existante!");
            return;
        }
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
