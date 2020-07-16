using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    private int speed = 3;
    private int force = 13;
    //public Transform Aim;
    bool hitting;

    public Transform[] targets;

    Animator animator;

    public Transform ball;

    Vector3 targetPosition;

    [SerializeField] Transform botPos;


    // bot only moves in z-direction

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameObject.Find("player").GetComponent<Player>().serving){
            targetPosition.z = ball.position.z;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Ball") && !GameObject.Find("player").GetComponent<Player>().serving){
            int rand = Random.Range(0,targets.Length);            
            Vector3 dir = targets[rand].position - transform.position;
            other.GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, 6, 0);
            if (ball.position.z > transform.position.z){
                animator.Play("backhand");
            }
            else{
                animator.Play("forehand");
            }

            ball.GetComponent<Ball>().hitterPlayer = false;

        }
    }

    public void Reset(){
        transform.position = botPos.position;
    }
}
