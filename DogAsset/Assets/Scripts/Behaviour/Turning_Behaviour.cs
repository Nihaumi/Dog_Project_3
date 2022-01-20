using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turning_Behaviour : MonoBehaviour
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

    public void TurningBehaviour()
    {
        switch (basic_behav.dog_state)
        {
            case Basic_Behaviour.Animation_state.turning_left:
                if (turn_dir_handler.turn_90_deg)
                {
                    anim_controll.ChangeAnimationState(anim.turn_left_90_deg_L);
                    break;
                }
                if (anim_controll.current_state == anim.walk || anim_controll.current_state == anim.trans_lying_to_stand_to_walk || anim_controll.current_state == anim.trans_sit_to_stand_to_walk || anim_controll.current_state == anim.trans_sleep_to_lying_to_stand_to_walk || anim_controll.current_state == anim.walk_L)
                {
                    anim_controll.ChangeAnimationState(anim.walk_L);

                }
                if (anim_controll.current_state == anim.walk_slow || anim_controll.current_state == anim.trans_lying_to_stand_to_walk_slow || anim_controll.current_state == anim.trans_sit_to_stand_to_walk_slow || anim_controll.current_state == anim.trans_sleep_to_lying_to_stand_to_walk_slow || anim_controll.current_state == anim.walk_slow_L)
                {
                    anim_controll.ChangeAnimationState(anim.walk_slow_L);


                }
                if (anim_controll.current_state == anim.seek || anim_controll.current_state == anim.trans_lying_to_stand_to_seek || anim_controll.current_state == anim.trans_sit_to_stand_to_seek || anim_controll.current_state == anim.trans_sleep_to_lying_to_stand_to_seek || anim_controll.current_state == anim.turn_right_seek || anim_controll.current_state == anim.turn_left_seek || anim_controll.current_state == anim.seek_L)
                {
                    anim_controll.ChangeAnimationState(anim.seek_L);


                }
                if (anim_controll.current_state == anim.run || anim_controll.current_state == anim.trans_lying_to_stand_to_run || anim_controll.current_state == anim.trans_sit_to_stand_to_run || anim_controll.current_state == anim.trans_sleep_to_lying_to_stand_to_run || anim_controll.current_state == anim.run_L)
                {
                    anim_controll.ChangeAnimationState(anim.run_L);


                }
                if (anim_controll.current_state == anim.trot || anim_controll.current_state == anim.trans_lying_to_stand_to_trot || anim_controll.current_state == anim.trans_sit_to_stand_to_trot || anim_controll.current_state == anim.trans_sleep_to_lying_to_stand_to_trot || anim_controll.current_state == anim.trot_L)
                {
                    anim_controll.ChangeAnimationState(anim.trot_L);
                    basic_behav.SetShortTimer(0.3f, 1);
                }
                break;
            case Basic_Behaviour.Animation_state.turning_right:
                if (turn_dir_handler.turn_90_deg)
                {
                    Debug.Log("KOMME REIN");
                    anim_controll.ChangeAnimationState(anim.turn_left_90_deg_R);
                    break;
                }
                if (anim_controll.current_state == anim.walk || anim_controll.current_state == anim.trans_lying_to_stand_to_walk || anim_controll.current_state == anim.trans_sit_to_stand_to_walk || anim_controll.current_state == anim.trans_sleep_to_lying_to_stand_to_walk || anim_controll.current_state == anim.walk_R)
                {
                    anim_controll.ChangeAnimationState(anim.walk_R);

                }
                if (anim_controll.current_state == anim.walk_slow || anim_controll.current_state == anim.trans_lying_to_stand_to_walk_slow || anim_controll.current_state == anim.trans_sit_to_stand_to_walk_slow || anim_controll.current_state == anim.trans_sleep_to_lying_to_stand_to_walk_slow || anim_controll.current_state == anim.walk_slow_R)
                {
                    anim_controll.ChangeAnimationState(anim.walk_slow_R);


                }
                if (anim_controll.current_state == anim.seek || anim_controll.current_state == anim.trans_lying_to_stand_to_seek || anim_controll.current_state == anim.trans_sit_to_stand_to_seek || anim_controll.current_state == anim.trans_sleep_to_lying_to_stand_to_seek || anim_controll.current_state == anim.turn_right_seek || anim_controll.current_state == anim.turn_left_seek || anim_controll.current_state == anim.seek_R)
                {
                    anim_controll.ChangeAnimationState(anim.seek_R);


                }
                if (anim_controll.current_state == anim.run || anim_controll.current_state == anim.trans_lying_to_stand_to_run || anim_controll.current_state == anim.trans_sit_to_stand_to_run || anim_controll.current_state == anim.trans_sleep_to_lying_to_stand_to_run || anim_controll.current_state == anim.run_R)
                {
                    anim_controll.ChangeAnimationState(anim.run_R);


                }
                if (anim_controll.current_state == anim.trot || anim_controll.current_state == anim.trans_lying_to_stand_to_trot || anim_controll.current_state == anim.trans_sit_to_stand_to_trot || anim_controll.current_state == anim.trans_sleep_to_lying_to_stand_to_trot || anim_controll.current_state == anim.trot_R)
                {
                    anim_controll.ChangeAnimationState(anim.trot_R);
                    basic_behav.SetShortTimer(0.3f, 1);
                }
                break;
            case Basic_Behaviour.Animation_state.walking_after_turning:
                if (anim_controll.current_state == anim.walk_slow_L || anim_controll.current_state == anim.walk_slow_R)
                {
                    anim_controll.ChangeAnimationState(anim.walk_slow);
                }
                if (anim_controll.current_state == anim.walk_L || anim_controll.current_state == anim.walk_R || anim_controll.current_state == anim.walk || anim_controll.current_state == anim.turn_left_90_deg_L || anim_controll.current_state == anim.turn_left_90_deg_L)
                {
                    anim_controll.ChangeAnimationState(anim.walk);
                }
                if (anim_controll.current_state == anim.trot_L || anim_controll.current_state == anim.trot_R)
                {
                    anim_controll.ChangeAnimationState(anim.trot);
                    basic_behav.SetShortTimer(0.3f, 1);
                }
                if (anim_controll.current_state == anim.seek_L || anim_controll.current_state == anim.seek_R)
                {
                    anim_controll.ChangeAnimationState(anim.seek);
                }
                basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;
                break;
        }
    }
}
