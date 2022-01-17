using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Collision_Behaviour : MonoBehaviour
{
    //other script
    GameObject dog;
    Animation_Controll anim_controll;
    Animations anim;
    Behaviour_Switch behav_switch;
    Neutral_Behaviour neutral_behav;

    //Animator
    public Animator animator;

    //turning
    int random_turn_index;
    public bool triggered = false;
    public bool collided = false;

    //timer after turning
    int min_time;
    int max_time;

    // Start is called before the first frame update
    void Start()
    {
        //access anim controll scipt
        dog = GameObject.Find("GermanShepherd_Prefab");
        anim_controll = dog.GetComponent<Animation_Controll>();
        anim = dog.GetComponent<Animations>();
        behav_switch = dog.GetComponent<Behaviour_Switch>();
        neutral_behav = dog.GetComponent<Neutral_Behaviour>();

        //Animator
        animator = dog.GetComponent<Animator>();

        //collisions


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Environment")
        {
            Debug.Log("trigger");
            // neutral_behav.GetRandomIndexFromList(anim.list_turn);//gets random animation from turning list
            //anim_controll.ChangeAnimationState(anim.walk_slow_L);
            //StartCoroutine(DogCommandWithWaitCoroutine(anim.turn_left_seek));
            //behav_switch.DisableScripts();
            neutral_behav.dog_state = Neutral_Behaviour.Animation_state.turning;
            neutral_behav.change_anim_timer = 0;
            //collided = true;
            triggered = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        triggered = false;
        //collided = false;
        //animator.SetTrigger("triggered");
        Debug.Log("end of trigger");
        //neutral_behav.change_anim_timer = 2;
        neutral_behav.dog_state = Neutral_Behaviour.Animation_state.walking_after_turning;
        CalculateChangeAnimationTimer();
    }

    void CalculateChangeAnimationTimer()
    {
        if (anim_controll.current_state == anim.walk_slow_L)
        {
            neutral_behav.change_anim_timer = 2;
        }
        if(anim_controll.current_state == anim.trot_L)
        {
            neutral_behav.change_anim_timer = 0;
        }
        if(anim_controll.current_state != anim.walk_slow_L && anim_controll.current_state != anim.trot_L)
        {
            neutral_behav.change_anim_timer = 1;
        }
        max_time = min_time;
    }


    //wait for few seconds before playing next animation
    IEnumerator DogCommandWithWaitCoroutine(string new_state)
    {
        yield return new WaitForSeconds(2);
        anim_controll.ChangeAnimationState(new_state);
    }
}
