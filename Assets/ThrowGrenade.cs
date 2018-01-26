using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ThrowGrenade :NetworkBehaviour {
    public Button ThrowButton;
    public Text GrenadesLeftText;
    public GameObject GrenadePrefab;
    public VirtualJoyStick joyStick;
    public Image GrenadeImg;

    public Transform fPoint;
    public Vector3 fPos;
    float rotZ;

    int NoofGrenadesLeft = 3;

    float ThrowRate = 0.1f;
    float TimeToThrow=0;
    // Use this for initialization
    void Start () {

        fPoint = transform.Find("Arm/Gun/firepoint");
        //ThrowButton = GameObject.Find("GrenadeButton").GetComponentInChildren<Button>();
        //if (ThrowButton == null)
        { Debug.Log("Grenade button NOT found"); }
        // GetComponent<Button>();
        //ThrowButton.onClick.AddListener(Throw);
        Debug.Log("GrenadeScript"+ThrowButton);

    }

    // Update is called once per frame
    void Update () {
        fPos = fPoint.position;
        rotZ = Mathf.Atan2(joyStick.Vertical(), joyStick.Horizontal()) * Mathf.Rad2Deg;
        if(Time.time - TimeToThrow<0)
            GrenadesLeftText.text = ( TimeToThrow- Time.time ).ToString("f0");
        else
            GrenadeImg.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
    }

    [Command]
    public void CmdThrow()
    {
        if (TimeToThrow < Time.time && NoofGrenadesLeft>0)
        {
           
           // NoofGrenadesLeft -= 1;
            TimeToThrow = Time.time + 1/ThrowRate;
            GrenadeImg.GetComponent<Image>().color = new Color32(255, 255, 255, 122);
            Debug.Log("ThrowingGrenade");
            GameObject Grenade = (GameObject)Instantiate(GrenadePrefab, fPos, Quaternion.Euler(0f, 0f, rotZ));
            Grenade.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(10000, 1000));
            Grenade.GetComponent<Rigidbody2D>().AddTorque(500);
            NetworkServer.Spawn(Grenade);
        }
    }
}
