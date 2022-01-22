using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public GameObject dog;
    public GameObject player;
    public GameObject dog_parent;
    public GameObject dir_manager;
    public Animator animator;
    Animation_Controll anim_controll;
    Animations anim;
    Turning_Direction_Handler turn_dir_handler;
    Basic_Behaviour basic_behav;
    Neutral_Behaviour neutral_behav;
    // Start is called before the first frame update
    void Start()
    {
        //access anim controll scipt
        dog = GameObject.Find("GermanShepherd_Prefab");
        player = GameObject.FindGameObjectWithTag("Player");
        dog_parent = GameObject.Find("DOg");
        dir_manager = GameObject.Find("Direction_Manager");
        animator = dog.GetComponent<Animator>();
        anim_controll = dog.GetComponent<Animation_Controll>();
        anim = dog.GetComponent<Animations>();
        turn_dir_handler = dir_manager.GetComponent<Turning_Direction_Handler>();
        basic_behav = dog.GetComponent<Basic_Behaviour>();
        neutral_behav = dog.GetComponent<Neutral_Behaviour>();
    }

    //needed for the look at part
    public Transform target;
    Vector3 target_position;
    void GetTargetPosition(Transform target)
    {
        target_position = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.y);
    }


    // Update is called once per frame
    void Update()
    {
        //look at 
        GetTargetPosition(target);
        transform.LookAt(target_position);

    }
}
