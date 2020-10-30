using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityMonetization : MonoBehaviour
{
    string googlePlay_ID = "3869871";
    bool gameMode = true;
    void Start()
    {
        Advertisement.Initialize(googlePlay_ID, gameMode);
    }

    public void ShowADS() 
    {
        Advertisement.Show();
    }
}
