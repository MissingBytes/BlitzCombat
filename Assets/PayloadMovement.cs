using Prototype.NetworkLobby;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PayloadMovement : NetworkBehaviour {
    public Rigidbody2D rb;
    int i;
    Vector3[] TravelPoints;
    public float Radius = 7f;
    public Collider[] colliders;
    public LayerMask mask;

 public Text timerText;
    public Text Winner;
    public float Totaltime;
    private float startTime;
    public RectTransform PayloadProgress;
    public float TDistance=0;
    public float step;
    //float PayloadProgress=0;
    [SyncVar(hook = "OnPayloadProgress")]float DistanceTravelled;
    public Text ProgressPercentage;
    
    // Use this for initialization
    void Start () {
        //PColor = GetComponent<ArmRotation>().PlayerColor;
        //Totaltime=
        //timerText = GameObject.Find("Timer").GetComponent<Text>();
        DistanceTravelled = 0;
        startTime = Time.time;
        Totaltime = 240;
        i = 0;
        rb = GetComponent<Rigidbody2D>();

        TravelPoints = new []{new Vector3(-6.47f, 13.6f),
                             new Vector3(-13f, 13.6f ), 
                             new Vector3(-13f,-3.2f),
                             new Vector3(6.3f,-3.2f),
                             new Vector3(6.3f,25.1f),
                             new Vector3(32.7f,25.1f),
                             new Vector3(32.7f,-3.2f),
                             new Vector3(52f,-3.2f),
                             new Vector3(52f,13.6f),
                             new Vector3(45.5f,13.6f) };
        transform.position = TravelPoints[0];
        TDistance = TotalDistance(TravelPoints);
      //  Debug.Log("Total"+ TDistance);
    }
    

    // Update is called once per frame
    void Update ()
    {
        
        if (Time.time - startTime > Totaltime || transform.position == TravelPoints[TravelPoints.Length - 1])
        { 
            if (Time.time - startTime > Totaltime)
                Winner.text = "PUSH FAILED, RED WINS";
            if (transform.position == TravelPoints[TravelPoints.Length - 1])
                Winner.text = "PUSH SUCCESFUL, BLUE WINS";

            //System.Threading.Thread.Sleep(5000);
            //NetworkManager.singleton.ServerChangeScene("a");
           

        }
        else
        {
           
            float t = Totaltime - (Time.time - startTime);
            string minutes = ((int)t / 60).ToString();
            string sec = (t % 60).ToString("f1");
            timerText.text = minutes + ":" + sec;

            step = 1.5f * Time.deltaTime;

            if (transform.position == TravelPoints[i] && TravelPoints.Length - 1 > i)
                i++;

            colliders = Physics.OverlapSphere(transform.position, Radius, mask);
           // Debug.Log("In Payload Scirpt");
            foreach (Collider col in colliders)
            {
               // bool RedNear = false;
                foreach (Collider co in colliders)
                    if (co.transform.parent.gameObject.GetComponent<ArmRotation>().PlayerColor == Color.red)
                    { //RedNear = true;
                        goto skip;
                    }
                if (col.transform.parent.gameObject.GetComponent<ArmRotation>().PlayerColor == Color.blue)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, TravelPoints[i], step);
                        DistanceTravelled += step ;
                    
                        Debug.Log("Distanceperc,Distance,Tdist"+ DistanceTravelled / TDistance);
                        Debug.Log("Distance"+ DistanceTravelled);
                        Debug.Log("Tdist"+ TDistance);
                    }
                //Debug.Log("Moving Payload");
            }
            skip:;
        }

        //rb.velocity = new Vector2(0, 1);

    }

    public float TotalDistance(Vector3[] Points)
    {
        float distance = 0;
        for (int i = 0; i < Points.Length-1; i++)
        {
            distance += (Points[i + 1] - Points[i]).magnitude;
        }

            return distance;
    }

    void OnPayloadProgress(float DistanceTravelled)
    {
        
        PayloadProgress.sizeDelta = new Vector2((DistanceTravelled / 168.23f) * 500, PayloadProgress.sizeDelta.y);
        ProgressPercentage.text = string.Format("{0:N0}",(DistanceTravelled / 168.23f) * 100) +"%"; 

    }


}
