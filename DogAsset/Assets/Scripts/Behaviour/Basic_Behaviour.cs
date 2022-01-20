using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_Behaviour : MonoBehaviour
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
    GameObject behav_manager;
    Animation_Controll anim_controll;
    Animations anim;
    Turning_Direction_Handler turn_dir_handler;
    Neutral_Behaviour neutral_behav;
    Behaviour_Switch behav_switch;
    Turning_Behaviour turning_behav;

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
        behav_manager = GameObject.Find("Behaviour_Manager");
        anim_controll = dog.GetComponent<Animation_Controll>();
        anim = dog.GetComponent<Animations>();
        turn_dir_handler = dir_manager.GetComponent<Turning_Direction_Handler>();
        neutral_behav = dog.GetComponent<Neutral_Behaviour>();
        behav_switch = behav_manager.GetComponent<Behaviour_Switch>();
        turning_behav = dog.GetComponent<Turning_Behaviour>();

        //state
        anim_controll.current_state = anim.stand_02;
        dog_state = Animation_state.standing;

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

    public void SetLongTimer()
    {
        min_timer = 3;
        max_timer = 7;
    }

    //choosing random index of animation lists
    public int random_index = 0;
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
      
            ChooseRandomIndex();

            //behaviours
            turning_behav.TurningBehaviour();
            if (behav_switch.neutral_script.enabled)
            {
                neutral_behav.NeutralBehaviour();
            }
            if (behav_switch.friendly_script.enabled)
            {

            }

            ResetTimerFunction();
            //Debug.Log("new state " + dog_state);
        }
    }
}
