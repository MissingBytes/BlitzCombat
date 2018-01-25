using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI; 

public class ShowObjective : NetworkBehaviour {
    public string[] Objective = { "PUSH THE NUKE", "DEFEND THE NUKE" };
    Color PColor;
    public Text StartInfoText;
    float startTime;
    

    // Use this for initialization
    void Start () {
        StartInfoText = GetComponent<Text>();
        startTime = Time.time;
        Debug.Log("ISlocla" + isLocalPlayer);
      
        {
            PColor = GetComponentInParent<ArmRotation>().PlayerColor;
            if (PColor == Color.blue)
                StartInfoText.text = "PUSH THE NUKE";
            else if (PColor == Color.red)
                StartInfoText.text = "DEFEND THE NUKE";
        }
        
    }



    // Update is called once per frame
    void Update () {
        if (Time.time - startTime > 3)
            Destroy(StartInfoText);
    }
}
