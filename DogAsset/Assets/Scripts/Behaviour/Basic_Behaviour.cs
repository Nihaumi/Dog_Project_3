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
    public Animator animator;
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
        animator = dog.GetComponent<Animator>();
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

    //coodinates in Blend Tree
    public float x_axis = 0f;
    public float y_axis = 0f;
    public float x_goal = 0f;
    public float y_goal = 0f;

    public float x_acceleration = 3f;
    public float turning_y_acceleration = 3f;
    public float y_acceleration = 0.5f;
    public float default_y_acceleration = 0.5f;

    public float standing_value = 0;
    public float walking_slow_value = 0.25f;
    public float seek_value = 0.5f;
    public float walking_value = 1f;
    public float trot_value = 1.5f;


    /*todo fo rblend tree:
     * while loop with thresholds for each wakling state
     * erstmal nur für: walkSlow, walk, seek, trot, run
     * variablen für thresholds -> min y in while loop, x ist immer 0
     * evtl in basic behaviour integrieren weil für alle gleich
     * change animation state nicht gebraucht
     * stop animation player????
     * 
     * reset  X und Y function
     * Function mit thershold paramater
    */

    //Sets parameters in animator
    public void SetBlendTreeParameters()
    {
        animator.SetFloat("X", x_axis);
        animator.SetFloat("Y", y_axis);
    }

    public void ResetParameter()
    {
        x_axis = 0;
        y_axis = 0;
        y_goal = 0;
        x_goal = 0;
    }

    //increases X axis until specific walking animation is reached
    public void IncreaseXAxisToValue(float value)
    {
        if (x_axis < value)
        {
            x_axis += Time.deltaTime * x_acceleration;
            x_axis = Mathf.Min(x_axis, value);

        }
    }
    //decreases X axis until specific walking animation is reached
    public void DecreaseXAxisToValue(float value)
    {
        if (x_axis > value)
        {
            x_axis -= Time.deltaTime * x_acceleration;
            x_axis = Mathf.Max(x_axis, value);
        }
    }

    //increases Y axis until specific walking animation is reached
   public void IncreaseYAxisToValue(float value)
    {
        if (y_axis < value)
        {

            y_axis += Time.deltaTime * y_acceleration;
            y_axis = Mathf.Min(y_axis, value);
        }
    }
    //decreases Y axis until specific walking animation is reached
    public void DecreaseYAxisToValue(float value)
    {
        if (y_axis > value)
        {

            y_axis -= Time.deltaTime * y_acceleration;
            y_axis = Mathf.Max(y_axis, value);

        }
    }

    /*TODO:
     * on collision: set x and y goals to turning L/R
     * change blending tree immediately according to current goals
     * 
     * afer collision: set x and y goals to walking forward
     * change blend tree after goals reached
     * 
    */


    public void TurnLeft()
    {
        if (y_goal != 0)
        {
            x_goal = -y_goal;
            y_goal = 0;
            //x_axis = -y_axis;
            //y_axis = 0;
        }
    }
    public void TurnRight()
    {
        if (y_goal != 0)
        {
            x_goal = y_goal;
            y_goal = 0;
            //x_axis = y_axis;
            //y_axis = 0;
        }
    }
    public void WalkForward()
    {
        if (x_goal != 0)
        {
            y_goal = Mathf.Abs(x_goal);
            x_goal = 0;
            //y_axis = Mathf.Abs(x_axis);
            //x_axis = 0;
        }
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
        SetBlendTreeParameters();
        IncreaseYAxisToValue(y_goal);
        DecreaseYAxisToValue(y_goal);
        IncreaseXAxisToValue(x_goal);
        DecreaseXAxisToValue(x_goal);


        if (change_anim_timer <= 0)
        {
      
            ChooseRandomIndex();

            turning_behav.TurningBehaviour();
            //behaviours

            if (behav_switch.neutral_script.enabled && !turning_behav.walking_after_turning_on)
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
