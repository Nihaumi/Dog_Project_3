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

    public bool walking_after_turning_on = false;

    public bool TurningBehaviour()
    {
        Debug.Log("ENTERING TURNING BEHAV");
        walking_after_turning_on = false;
        switch (basic_behav.dog_state)
        {
            case Basic_Behaviour.Animation_state.turning_left:

                Debug.Log("Lennart Log: TURN LEFT!");
                basic_behav.TurnLeft();

                if (basic_behav.y_goal == basic_behav.trot_value)
                {
                    basic_behav.SetShortTimer(0.3f, 1);
                    Debug.Log("running left");
                }

                break;
            case Basic_Behaviour.Animation_state.turning_right:

                Debug.Log("Lennart Log: TURN RIGHT!");
                basic_behav.TurnRight();
                if (basic_behav.y_goal == basic_behav.trot_value)
                {
                    basic_behav.SetShortTimer(0.3f, 1);
                    Debug.Log("running right");
                }

                break;
            case Basic_Behaviour.Animation_state.walking_after_turning:
                basic_behav.SetLongTimer();
                basic_behav.WalkForward();
                basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;
                walking_after_turning_on = true;
                return walking_after_turning_on;
            default:
                break;
        }
        return false;
    }
}
