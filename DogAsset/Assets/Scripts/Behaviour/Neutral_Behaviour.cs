using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neutral_Behaviour : MonoBehaviour
{
    //other script
    public GameObject dog;
    public GameObject dog_parent;
    public GameObject dir_manager;
    Animation_Controll anim_controll;
    Animations anim;
    Turning_Direction_Handler turn_dir_handler;
    Basic_Behaviour basic_behav;

    // Start is called before the first frame update
    void Start()
    {
        //access anim controll scipt
        dog = GameObject.Find("GermanShepherd_Prefab");
        dog_parent = GameObject.Find("DOg");
        dir_manager = GameObject.Find("Direction_Manager");
        anim_controll = dog.GetComponent<Animation_Controll>();
        anim = dog.GetComponent<Animations>();
        turn_dir_handler = dir_manager.GetComponent<Turning_Direction_Handler>();
        basic_behav = dog.GetComponent<Basic_Behaviour>();
    }

    // Update is called once per frame
    void Update()
    { 
    }

    public void NeutralBehaviour() { 

        switch (basic_behav.dog_state)
        {            
            case Basic_Behaviour.Animation_state.standing:
                anim_controll.ChangeAnimationState(anim.list_standing[basic_behav.random_index]);
                //Debug.Log("standinglist item at rndindex: " + basic_behav.random_index + "is:" + anim.list_standing[basic_behav.random_index]);
                if (basic_behav.random_index == 0)
                {
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.lying;
                }
                if (basic_behav.random_index == 1)
                {
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.sitting;
                }
                if (basic_behav.random_index > 1)
                {
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;
                }
                basic_behav.SetLongTimer();
                break;
            case Basic_Behaviour.Animation_state.sitting:
                anim_controll.ChangeAnimationState(anim.list_sitting[basic_behav.random_index]);
                //Debug.Log("sitting list item at rndindex: " + basic_behav.random_index + "is:" + anim.list_sitting[basic_behav.random_index]);
                basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;
                basic_behav.SetLongTimer();
                break;
            case Basic_Behaviour.Animation_state.lying:
                anim_controll.ChangeAnimationState(anim.list_lying[basic_behav.random_index]);
                //Debug.Log("lying list item at rndindex: " + basic_behav.random_index + "is:" + anim.list_lying[basic_behav.random_index]);
                if (basic_behav.random_index == 0)
                {
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.sleeping;
                }
                if (basic_behav.random_index > 0)
                {
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;
                }
                basic_behav.SetLongTimer();
                break;
            case Basic_Behaviour.Animation_state.sleeping:
                anim_controll.ChangeAnimationState(anim.list_sleeping[basic_behav.random_index]);
                //Debug.Log("sleeping list item at rndindex: " + basic_behav.random_index + "is:" + anim.list_sleeping[basic_behav.random_index]);
                if (basic_behav.random_index == 0)
                {
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.lying;
                }
                if (basic_behav.random_index == 1)
                {
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.standing;
                }
                if (basic_behav.random_index > 1)
                {
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;
                }
                basic_behav.SetLongTimer();
                break;
            case Basic_Behaviour.Animation_state.walking:
                anim_controll.ChangeAnimationState(anim.list_walking[basic_behav.random_index]);
                //Debug.Log("walking list item at rndindex: " + basic_behav.random_index + "is:" + anim.list_walking[basic_behav.random_index]);
                if (basic_behav.random_index == 0)
                {
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.standing;
                    basic_behav.SetLongTimer();
                }
                if (basic_behav.random_index == 4)
                {
                    basic_behav.SetShortTimer(0.3f, 1);
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;
                }
                else
                {
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;
                    basic_behav.SetLongTimer();
                }
                break;
            default:
                return;
        }

    }
}
