using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandTracker : MonoBehaviour
{
    public UDPReceive receiver;

    public GameObject[] handPoints;
    public GameObject[] handPoints2;
    public TextMeshProUGUI HandWarningText;
    // hand 1
    public GameObject TrackingPoint1;
    public GameObject TrackingPoint2;
    // hand 2
    public GameObject TrackingPoint3;
    public GameObject TrackingPoint4;
    public float TrackingDistance = 3;

    public bool flatHand = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string data = receiver.data;

        if (data == "No hand Detected"){
            HandWarningText.text = "Hand Out Of Bound";
            // for (int i = 0; i < 21; i++) {
            //     handPoints[i].transform.position = new Vector3(0,0,0);
            // }
        }
        else{
            if (Vector3.Distance(TrackingPoint1.transform.position, TrackingPoint2.transform.position) > TrackingDistance || Vector3.Distance(TrackingPoint3.transform.position, TrackingPoint4.transform.position) > TrackingDistance){
                HandWarningText.text = "Too Close To Camera";
            }
            else{
                HandWarningText.text = "";
            }

            // print(data);
            if (data.Split("], [").Length > 1) {
                // print("TWO HANDS");
                // print(data);
                data = data.Remove(0,2);
                data = data.Remove(data.Length-2, 2);
                string[] datas = data.Split("], [");

                moveHand(datas[0],handPoints);
                moveHand(datas[1],handPoints2);
            } 
            else {
                // print("ONE HANDS");
                data = data.Remove(0,2);
                data = data.Remove(data.Length-2, 2);

                moveHand(data,handPoints);
            }

        }

        
    }
    private void moveHand(string data, GameObject[] pointyPointies) {
        string[] points = data.Split(",");

        if (!flatHand) {
            for (int i = 0; i < 21; i++) {
                float x = 5-float.Parse(points[i*3]) / 70;
                float y = float.Parse(points[i*3 + 1]) / 70;
                float z = float.Parse(points[i*3 + 2]) / 70;

                pointyPointies[i].transform.position = new Vector3(x,y,z);
            }
        } else {
            for (int i = 0; i < 21; i++) {
                float x = 5-float.Parse(points[i*3]) / 70;
                //float y = float.Parse(points[i3 + 1]) / 70;
                float z = float.Parse(points[i*3 + 2]) / 70;

                pointyPointies[i].transform.localPosition = new Vector2(x,z);
            }
        }
    }
}
