using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 initialPos;
    public bool hitterPlayer;
    public bool playing = true;
    public Text text;
    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(playing){
            text.text = "";
        }
    }
    private void OnCollisionEnter(Collision collision) {
        if(collision.transform.CompareTag("Wall")){
            //GetComponent<Rigidbody>().velocity = Vector3.zero;
            //GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            //transform.position = initialPos;


            if(playing){
                GameObject.Find("player").GetComponent<Player>().Reset();

                if(hitterPlayer){
                    GameObject.Find("GameManager").GetComponent<gameManager>().playerScore++;
                    text.text = "You scored a point!!";
                }
                else{
                    GameObject.Find("GameManager").GetComponent<gameManager>().botScore++;
                    text.text = "Bot scored a point";
                }
                
                GameObject.Find("bot").GetComponent<Bot>().Reset();
                playing = false;
                //transform.position = initialPos;

            }



        }
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Out")){

        
            if(playing){
                GameObject.Find("player").GetComponent<Player>().Reset();


                if(hitterPlayer){
                    GameObject.Find("GameManager").GetComponent<gameManager>().botScore++;
                    text.text = "Out! Bot scored a point!!";
                }
                else{
                    GameObject.Find("GameManager").GetComponent<gameManager>().playerScore++;
                    text.text = "Out! You scored a point!!";
                }
                GameObject.Find("bot").GetComponent<Bot>().Reset();
                playing = false;
            //transform.position = initialPos;
            }
        }
        
    }

}
