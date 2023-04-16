using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class resetBall : MonoBehaviour
{

    public GameObject Ball;
    public TextMeshProUGUI PlayerScore;

    private int Score = 0;


    private Vector3 RespawnPos;
    // Start is called before the first frame update
    void Start()
    {
        RespawnPos = Ball.transform.position;
        PlayerScore.text = "Score: 0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        Ball.transform.position = RespawnPos;
        Score++;
        PlayerScore.text = "Score: " + Score.ToString();
    }
}
