using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private int speed = 5;
    private int force = 13;
    public Transform Aim;
    bool hitting;
    public bool serving = true;
    bool rightServed = true;

    Animator animator;

    public GameObject ball;
    Vector3 aimInitial;

    [SerializeField] Transform rightserve;
    [SerializeField] Transform leftserve;

    void Start()
    {
        animator = GetComponent<Animator>();
        aimInitial = Aim.position;

    }

    // Update is called once per frame
    void Update()
    {

        float h = Input.GetAxisRaw("Horizontal"); // get the horizontal axis of the keyboard
        float v = Input.GetAxisRaw("Vertical"); // get the vertical axis of the keyboard

        if(Input.GetKeyDown(KeyCode.F)){
            hitting = true;
        }else if(Input.GetKeyUp(KeyCode.F)){
            hitting = false;
        }

        if(Input.GetKeyDown(KeyCode.R)){
            hitting = true;
            GetComponent<BoxCollider>().enabled = false;
            ball.transform.position = transform.position + new Vector3(-0.5f, -0.5f, 0);
            Vector3 dir = new Vector3(Aim.position.x - transform.position.x, 0 , Aim.position.z - transform.position.z);
            ball.GetComponent<Rigidbody>().velocity = dir.normalized * 16 + new Vector3(0, 5, 0);            
            animator.Play("forehand");
            Aim.position = aimInitial;

            ball.GetComponent<Ball>().hitterPlayer = true;
            ball.GetComponent<Ball>().playing = true;


        }else if(Input.GetKeyUp(KeyCode.R)){
            hitting = false;
            serving = false;
            GetComponent<BoxCollider>().enabled = true;
        }
        
        if(h!=0 || v!=0)
        {
            if(!hitting && !serving){
                transform.Translate(new Vector3(v, 0, -h) * speed * Time.deltaTime); // move on the court
            }
            else{
                Aim.Translate(new Vector3(v, 0, -h) * speed * Time.deltaTime);
            }
        }    
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Ball") && !serving){
            Vector3 dir = new Vector3(Aim.position.x - transform.position.x, 0 , Aim.position.z - transform.position.z);
            other.GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, 5, 0);
            if (ball.transform.position.z > transform.position.z){
                animator.Play("backhand");
            }
            else{
                animator.Play("forehand");
            }

            Aim.position = aimInitial;

            ball.GetComponent<Ball>().hitterPlayer = true;

        }
    }

    public void Reset(){
        if(rightServed){
            transform.position = leftserve.position;
        }
        else{
            transform.position = rightserve.position;
        }

        rightServed = !rightServed;
        serving = true;

    }
}
