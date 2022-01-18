using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neutral_Behaviour : MonoBehaviour
{
    public enum Animation_state //is a class
    {
        sitting,
        standing,
        walking,
        running,
        lying,
        sleeping,
        aggressiv,
        turning_right,
        turning_left,
        walking_after_turning
    };

    public Animation_state dog_state;

    //timer
    public float change_anim_timer;
    int starting_timer = 2;
    int new_timer;
    int min_timer;
    int max_timer;

    //other script
    public GameObject dog;
    public GameObject dog_parent;
    public GameObject dir_manager;
    Animation_Controll anim_controll;
    Animations anim;
    Turning_Direction_Handler turn_dir_handler;

    private void Awake()
    {
        change_anim_timer = starting_timer;
    }

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

        //state
        anim_controll.current_state = anim.walk;
        dog_state = Animation_state.walking;

        //timer
        min_timer = starting_timer;
        max_timer = min_timer;
    }

    //Timer
    void ResetTimerFunction()
    {
        if (change_anim_timer <= 0)
        {
            change_anim_timer = ChooseRandomTimer();
        }
    }
    int ChooseRandomTimer()
    {
        new_timer = Random.Range(min_timer, max_timer);
        return new_timer;
    }

    public void SetShortTimer(float min_time, float max_time)
    {
        min_timer = Mathf.RoundToInt(min_time);
        max_timer = Mathf.RoundToInt(max_time);
    }

    void SetLongTimer()
    {
        min_timer = 3;
        max_timer = 7;
    }

    //choosing random index of animation lists
    int random_index = 0;
    int list_length = 0;
    int ChooseRandomIndex()
    {
        switch (dog_state)
        {
            case Animation_state.walking_after_turning:
                GetRandomIndexFromList(anim.list_walking_after_turning);
                break;
            case Animation_state.standing:
                GetRandomIndexFromList(anim.list_standing);
                DisplayList(anim.list_standing);
                break;
            case Animation_state.sitting:
                GetRandomIndexFromList(anim.list_sitting); ;
                DisplayList(anim.list_sitting);
                break;
            case Animation_state.sleeping:
                GetRandomIndexFromList(anim.list_sleeping);
                DisplayList(anim.list_sleeping);
                break;
            case Animation_state.walking:
                GetRandomIndexFromList(anim.list_walking);
                DisplayList(anim.list_walking);
                break;
            case Animation_state.running:
                GetRandomIndexFromList(anim.list_running);
                DisplayList(anim.list_running);
                break;
            case Animation_state.lying:
                GetRandomIndexFromList(anim.list_lying);
                DisplayList(anim.list_lying);
                break;

            default:
                break;
        }
        //Debug.Log("index " + random_index);

        return random_index;
    }
    public void GetRandomIndexFromList(List<string> list)
    {
        random_index = Random.Range(0, list.Count);
    }

    void DisplayList(List<string> list)
    {
        list_length = list.Count;
        int i = 0;
        while (i < list_length)
        {
            //Debug.Log("list item number: "+ i + "is" + list[i]);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        change_anim_timer = change_anim_timer - Time.deltaTime;

        if (change_anim_timer <= 0)
        {
            //collision_behav.animator.ResetTrigger("triggered");
            ChooseRandomIndex();
            switch (dog_state)
            {
                case Animation_state.turning_left:
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
                        SetShortTimer(0.3f, 1);
                    }
                    if (turn_dir_handler.turn_90_deg)
                    {
                        anim_controll.ChangeAnimationState(anim.turn_left_90_deg_L);
                    }
                    break;
                case Animation_state.turning_right:
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
                        SetShortTimer(0.3f, 1);
                    }
                    if (turn_dir_handler.turn_90_deg)
                    {
                        Debug.Log("KOMME REIN");
                        anim_controll.ChangeAnimationState(anim.turn_left_90_deg_R);
                    }
                    break;
                case Animation_state.standing:
                    anim_controll.ChangeAnimationState(anim.list_standing[random_index]);
                    //Debug.Log("standinglist item at rndindex: " + random_index + "is:" + anim.list_standing[random_index]);
                    if (random_index == 0)
                    {
                        dog_state = Animation_state.lying;
                    }
                    if (random_index == 1)
                    {
                        dog_state = Animation_state.sitting;
                    }
                    if (random_index > 1)
                    {
                        dog_state = Animation_state.walking;
                    }
                    SetLongTimer();
                    break;
                case Animation_state.sitting:
                    anim_controll.ChangeAnimationState(anim.list_sitting[random_index]);
                    //Debug.Log("sitting list item at rndindex: " + random_index + "is:" + anim.list_sitting[random_index]);
                    dog_state = Animation_state.walking;
                    SetLongTimer();
                    break;
                case Animation_state.lying:
                    anim_controll.ChangeAnimationState(anim.list_lying[random_index]);
                    //Debug.Log("lying list item at rndindex: " + random_index + "is:" + anim.list_lying[random_index]);
                    if (random_index == 0)
                    {
                        dog_state = Animation_state.sleeping;
                    }
                    if (random_index > 0)
                    {
                        dog_state = Animation_state.walking;
                    }
                    SetLongTimer();
                    break;
                case Animation_state.sleeping:
                    anim_controll.ChangeAnimationState(anim.list_sleeping[random_index]);
                    //Debug.Log("sleeping list item at rndindex: " + random_index + "is:" + anim.list_sleeping[random_index]);
                    if (random_index == 0)
                    {
                        dog_state = Animation_state.lying;
                    }
                    if (random_index == 1)
                    {
                        dog_state = Animation_state.standing;
                    }
                    if (random_index > 1)
                    {
                        dog_state = Animation_state.walking;
                    }
                    SetLongTimer();
                    break;
                case Animation_state.walking_after_turning:
                    if (anim_controll.current_state == anim.walk_slow_L || anim_controll.current_state == anim.walk_slow_R)
                    {
                        anim_controll.ChangeAnimationState(anim.walk_slow);
                    }
                    if (anim_controll.current_state == anim.walk_L || anim_controll.current_state == anim.walk_R || anim_controll.current_state == anim.walk)
                    {
                        anim_controll.ChangeAnimationState(anim.walk);
                    }
                    if (anim_controll.current_state == anim.trot_L || anim_controll.current_state == anim.trot_R)
                    {
                        anim_controll.ChangeAnimationState(anim.trot);
                        SetShortTimer(0.3f, 1);
                    }
                    if (anim_controll.current_state == anim.seek_L || anim_controll.current_state == anim.seek_R)
                    {
                        anim_controll.ChangeAnimationState(anim.seek);
                    }
                    dog_state = Animation_state.walking;
                    break;
                case Animation_state.walking:
                    anim_controll.ChangeAnimationState(anim.list_walking[random_index]);
                    //Debug.Log("walking list item at rndindex: " + random_index + "is:" + anim.list_walking[random_index]);
                    if (random_index == 0)
                    {
                        dog_state = Animation_state.standing;
                        SetLongTimer();
                    }
                    if(random_index == 4)
                    {
                        SetShortTimer(0.3f, 1);
                        dog_state = Animation_state.walking;
                    }
                    else
                    {
                        dog_state = Animation_state.walking;
                        SetLongTimer();
                    }                    
                    break;
                default:
                    return;
            }
            ResetTimerFunction();
            //Debug.Log("new state " + dog_state);
        }
    }
}
