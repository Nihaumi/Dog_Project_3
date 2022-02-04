using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
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
        walking_after_turning,
        friendly_walking
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
    public GameObject player;
    public GameObject dog_parent;
    public GameObject dir_manager;
    public GameObject agg_position;
    public Animator animator;
    GameObject behav_manager;
    Animation_Controll anim_controll;
    Animations anim;
    Turning_Direction_Handler turn_dir_handler;
    Neutral_Behaviour neutral_behav;
    Friendly_Behaviour friendly_behav;
    Behaviour_Switch behav_switch;
    Turning_Behaviour turning_behav;
    PlayerInteraction player_interaction;
    Aggressive_Behaviour agg_behav;

    private void Awake()
    {
        change_anim_timer = starting_timer;
    }

    // Start is called before the first frame update
    void Start()
    {
        //access anim controll scipt
        dog = GameObject.Find("GermanShepherd_Prefab");
        player = GameObject.FindGameObjectWithTag("Player");
        dog_parent = GameObject.Find("DOg");
        dir_manager = GameObject.Find("Direction_Manager");
        behav_manager = GameObject.Find("Behaviour_Manager");
        animator = dog.GetComponent<Animator>();
        anim_controll = dog.GetComponent<Animation_Controll>();
        anim = dog.GetComponent<Animations>();
        turn_dir_handler = dir_manager.GetComponent<Turning_Direction_Handler>();
        neutral_behav = dog.GetComponent<Neutral_Behaviour>();
        friendly_behav = dog.GetComponent<Friendly_Behaviour>();
        behav_switch = behav_manager.GetComponent<Behaviour_Switch>();
        turning_behav = dog.GetComponent<Turning_Behaviour>();
        player_interaction = player.GetComponent<PlayerInteraction>();
        agg_behav = dog.GetComponent<Aggressive_Behaviour>();

        //state
        anim_controll.current_state = anim.stand_02;
        dog_state = Animation_state.standing;

        //timer
        min_timer = starting_timer;
        max_timer = min_timer;

        //rig layer obj
        neck_1 = GameObject.Find("NeckAim1");
        neck_2 = GameObject.Find("NeckAim2");
        neck_3 = GameObject.Find("NeckAim3");
        neck_4 = GameObject.Find("NeckAim4");
        head = GameObject.Find("Head aim");
        right_eye = GameObject.Find("EyeRight Aim");
        left_eye = GameObject.Find("EyeLeft Aim");

        //aggtression position
        agg_position = GameObject.Find("agg_position");

        //multi aim constraints
        neck_constraint_1 = neck_1.GetComponent<MultiAimConstraint>();
        neck_constraint_2 = neck_2.GetComponent<MultiAimConstraint>();
        neck_constraint_3 = neck_3.GetComponent<MultiAimConstraint>();
        neck_constraint_4 = neck_4.GetComponent<MultiAimConstraint>();
        head_constraint = head.GetComponent<MultiAimConstraint>();
        right_eye_constraint = right_eye.GetComponent<MultiAimConstraint>();
        left_eye_constraint = left_eye.GetComponent<MultiAimConstraint>();

    }

    //coodinates in Blend Tree
    public float x_axis = 0f;
    public float y_axis = 0f;
    public float x_goal = 0f;
    public float y_goal = 0f;

    public float x_acceleration = 1f;
    public float turning_y_acceleration = 1.5f;
    public float y_acceleration = 0.5f;
    public float default_y_acceleration = 0.5f;

    public float standing_value = 0;
    public float walking_slow_value = 0.25f;
    public float seek_value = 0.5f;
    public float walking_value = 1f;
    public float trot_value = 1.5f;

    //constraint gedöns
    GameObject neck_4;
    GameObject neck_3;
    GameObject neck_2;
    GameObject neck_1;
    GameObject head;
    GameObject left_eye;
    GameObject right_eye;


    //contraint components
    public MultiAimConstraint neck_constraint_1;
    public MultiAimConstraint neck_constraint_2;
    public MultiAimConstraint neck_constraint_3;
    public MultiAimConstraint neck_constraint_4;
    public MultiAimConstraint head_constraint;
    public MultiAimConstraint left_eye_constraint;
    public MultiAimConstraint right_eye_constraint;

    //hands distance und so
    public float hand_close = 1f;
    public float dist_left_hand_to_dog;
    public float dist_right_hand_to_dog;


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
    int ChooseRandomIndex()
    {
        switch (dog_state)
        {
            case Animation_state.walking_after_turning:
                GetRandomIndexFromList(anim.list_walking_after_turning);
                break;
            case Animation_state.standing:
                if (behav_switch.friendly_script.enabled)
                {
                    GetRandomIndexFromList(anim.friendly_list_standing);
                }
                else
                {
                    GetRandomIndexFromList(anim.list_standing);
                }
                break;
            case Animation_state.sitting:
                if (behav_switch.friendly_script.enabled)
                {
                    GetRandomIndexFromList(anim.friendly_list_sitting);
                }
                else
                {
                    GetRandomIndexFromList(anim.list_sitting);
                }
                break;
            case Animation_state.sleeping:
                if (behav_switch.friendly_script.enabled)
                {
                    GetRandomIndexFromList(anim.friendly_list_sleeping);
                }
                else
                {
                    GetRandomIndexFromList(anim.list_sleeping);
                }
                break;
            case Animation_state.walking:
                if (behav_switch.friendly_script.enabled)
                {
                    GetRandomIndexFromList(anim.friendly_list_walking);
                }
                else
                {
                    GetRandomIndexFromList(anim.list_walking);
                }
                break;
            case Animation_state.running:
                GetRandomIndexFromList(anim.list_running);
                break;
            case Animation_state.lying:
                if (behav_switch.friendly_script.enabled)
                {
                    GetRandomIndexFromList(anim.friendly_list_lying);
                }
                else
                {
                    GetRandomIndexFromList(anim.list_lying);
                }
                break;
            case Animation_state.aggressiv:
                GetRandomIndexFromList(anim.agg_list);
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

    //follow hand/head of player
    public void SetFollowObject()
    {

        int focus = 3;

        if (!friendly_behav.enabled)
        {
            focus = 3;
        }
        else if (!player_interaction.AreHandsMoving())
        {   //look at Head and away from left and right Hand
            focus = 2;
        }
        else if (dist_left_hand_to_dog < dist_right_hand_to_dog)
        {   //look at left Hand and away from Head and right Hand
            focus = 1;

            Debug.Log("TRACK LEFT");
        }
        else if (dist_right_hand_to_dog < dist_left_hand_to_dog)
        {   //look at right Hand and away from Head and left Hand
            focus = 0;

            Debug.Log("TRACK RIGHT");
        }
        SetWeightConstraint(neck_constraint_1, focus);
        SetWeightConstraint(neck_constraint_2, focus);
        SetWeightConstraint(neck_constraint_3, focus);
        SetWeightConstraint(head_constraint, focus);
        SetWeightConstraint(right_eye_constraint, focus);
        SetWeightConstraint(left_eye_constraint, focus);
    }

    /*TODO
     * create function or integrate into setweight constraints:
     * change the rig min max values and y offsets
     * if(player HINTER dog) weight 0 ODER BESSER turn around nach left/right je nachdem zu welcher seite, werte wie geradeaus po constraint
     * else if ( player left of dog) min max werte pro constraint, positives offset pro constraint
     * else if( player right of dog) min max werte pro constraint, negatives offset pro constraint
     * else if(player geradeaus fom dog) min max pro constraint, offset 0
     */
    public int focus_2 = 0;
    public int GetPlayerOffset()
    {

        float behind_dog = 0f;
        float before_dog = 1f;
        float beside_dog = 0.6f;
        Vector3 player_pos_local = dog.transform.InverseTransformPoint(player.transform.position);
        if (player_pos_local.z < behind_dog)
        {
            focus_2 = 0;
            Debug.Log("BEHIND");
        }
        else if (player_pos_local.z > before_dog)
        {
            focus_2 = 1;
            if (player_pos_local.x > -beside_dog && player_pos_local.x < beside_dog)
            {
                focus_2 = 2;
                Debug.Log("STRAIGHT");
            }
            else if (player_pos_local.x < -beside_dog)
            {
                focus_2 = 3;
                Debug.Log("LEFT");
            }
            else if (player_pos_local.x > beside_dog)
            {
                focus_2 = 4;
                Debug.Log("RIGHT");
            }
        }
        else
        {
            if (player_pos_local.x < 0)
            {
                focus_2 = 5;
                Debug.Log("CLOSE L");
            }
            else if (player_pos_local.x > 0)
            {
                focus_2 = 6;
                Debug.Log("CLOSE R");
            }
        }

        //Debug.Log("player position " + player_pos_local);

        return focus_2;
    }

    //TODO: Maybe changerate variable
    //focus: 0 -> right, 1 -> left, 2 -> head, 3 -> alle aus
    public void SetWeightConstraint(MultiAimConstraint constraint, int focus)
    {
        var b = constraint.data.offset;
        var a = constraint.data.sourceObjects;
        float curent_weight_right = a.GetWeight(0); //rightHand
        float curent_weight_left = a.GetWeight(1); //leftHand
        float curent_weight_head = a.GetWeight(2); //head

        float weight_change_rate = 1f;
        float weight_update_right = 0, weight_update_left = 0, weight_update_head = 0;

        switch (focus)
        {
            case 0:
                weight_update_right = curent_weight_right + weight_change_rate * Time.deltaTime;
                weight_update_right = Mathf.Min(weight_update_right, 1);
                weight_update_left = curent_weight_left - weight_change_rate * Time.deltaTime;
                weight_update_left = Mathf.Max(weight_update_left, 0);
                weight_update_head = curent_weight_head - weight_change_rate * Time.deltaTime;
                weight_update_head = Mathf.Max(weight_update_head, 0);
                break;
            case 1:
                weight_update_right = curent_weight_right - weight_change_rate * Time.deltaTime;
                weight_update_right = Mathf.Max(weight_update_right, 0);
                weight_update_left = curent_weight_left + weight_change_rate * Time.deltaTime;
                weight_update_left = Mathf.Min(weight_update_left, 1);
                weight_update_head = curent_weight_head - weight_change_rate * Time.deltaTime;
                weight_update_head = Mathf.Max(weight_update_head, 0);
                break;
            case 2:
                weight_update_right = curent_weight_right - weight_change_rate * Time.deltaTime;
                weight_update_right = Mathf.Max(weight_update_right, 0);
                weight_update_left = curent_weight_left - weight_change_rate * Time.deltaTime;
                weight_update_left = Mathf.Max(weight_update_left, 0);
                weight_update_head = curent_weight_head + weight_change_rate * Time.deltaTime;
                weight_update_head = Mathf.Min(weight_update_head, 1);
                break;
            case 3:
                weight_update_right = curent_weight_right - weight_change_rate * Time.deltaTime;
                weight_update_right = Mathf.Max(weight_update_right, 0);
                weight_update_left = curent_weight_left - weight_change_rate * Time.deltaTime;
                weight_update_left = Mathf.Max(weight_update_left, 0);
                weight_update_head = curent_weight_head - weight_change_rate * Time.deltaTime;
                weight_update_head = Mathf.Max(weight_update_head, 0);
                break;
            default:
                break;
        }
        a.SetWeight(1, weight_update_left);
        a.SetWeight(2, weight_update_head);
        constraint.data.sourceObjects = a;
    }

    //turning towrads target object
    Vector3 direction;
    public Quaternion rotation;
    public float speed = 0.1f; //reset in aggressiv behav
    public Vector3 current_position;
    public Vector3 target_pos;
    public bool turning_in_place = false;
    public void TurnToTarget(GameObject target)
    {
        if (!turning_in_place)
        {
            TurnInPlace();
        }
        direction = target.transform.position - dog.transform.position;
        rotation = Quaternion.LookRotation(direction);
        dog.transform.rotation = Quaternion.Lerp(dog.transform.rotation, rotation, speed * Time.deltaTime);
        if (friendly_behav.enabled == true)
        {
            IncreaseSpeed(0.005f);
        }
        else
        {
            IncreaseSpeed(0.005f);
        }
    }
    //make turning speed smoother--> not start fast and get way slower
    void IncreaseSpeed(float increase)
    {
        Debug.Log("SPEEEEEED");
        if (speed < 1 && y_axis > walking_slow_value)
        {
            speed = speed + increase;
        }
    }

    public void TurnInPlace()
    {
        Debug.Log("turning in place");
        if (anim_controll.current_state != anim.aggresive_blend_tree)
        {
            anim_controll.ChangeAnimationState(anim.aggresive_blend_tree);
        }
        x_goal = walking_value;
        WalkForward();
        y_acceleration = turning_y_acceleration;
        turning_in_place = true; //false in agressive behav und Waitbefore wlaking
    }

    //triggers the walkingtowards and sets bools
    public void TurningAndWalkingLogicHandler()
    {
        if (friendly_behav.enabled)
        {
            if (!friendly_behav.started_walking && !friendly_behav.facing_player)
            {
                Debug.Log("start walking - frendo");
                StartCoroutine(WaitBeforeWalkingTowards(friendly_behav.player_target));
                friendly_behav.started_walking = true;
            }
            if (friendly_behav.TouchingPlayer())
            {
                Debug.Log("TOUCHING - frendo");
                friendly_behav.facing_player = true;
                friendly_behav.started_walking = false;
            }
        }
        else if (agg_behav.enabled)
        {
            if (!agg_behav.started_walking && !agg_behav.facing_target)
            {
                Debug.Log("start walking - aggro");
                StartCoroutine(WaitBeforeWalkingTowards(agg_behav.player));
                agg_behav.started_walking = true;
            }
            if (agg_behav.dist_to_target < agg_behav.reached_target)
            {
                Debug.Log("TOUCHING - aggro");
                agg_behav.facing_target = true;
                turning_in_place = false;
                agg_behav.started_walking = false;
            }
        }
    }


    //walking towards
    public IEnumerator WaitBeforeWalkingTowards(GameObject target)
    {
        yield return new WaitForSeconds(1);
        Debug.Log("WALK BITCH");
        //turning_in_place = false;
        //dog.transform.position = Vector3.MoveTowards(current_position, target.transform.position, Time.deltaTime * speed);
    }

    // Update is called once per frame
    void Update()
    {
        change_anim_timer = change_anim_timer - Time.deltaTime;

        //Blend Tree Animation
        SetBlendTreeParameters();
        IncreaseYAxisToValue(y_goal);
        DecreaseYAxisToValue(y_goal);
        IncreaseXAxisToValue(x_goal);
        DecreaseXAxisToValue(x_goal);

        //dog position
        current_position = dog.transform.position;

        //Behaviour
        if (behav_switch.aggressive_script.enabled)
        {//aggressive
            target_pos = agg_position.transform.position;
            agg_behav.dist_to_target = Vector3.Distance(dog.transform.position, agg_position.transform.position);
            agg_behav.dist_to_player = Vector3.Distance(dog.transform.position, player.transform.position);
            agg_behav.StopAgression();

            agg_behav.MoveToPositionAndFacePlayer();
        }
        else if (behav_switch.friendly_script.enabled)
        {//friendly
            SetFollowObject();
            Debug.Log("IS THIS THING ON?!");
            if (y_goal == trot_value)
            {
                y_goal = walking_slow_value;
            }
            player_interaction.IsCloseToLeftHand();
            player_interaction.IsCloseToRightHand();
            player_interaction.AreHandsMoving();
            //TODO uncomment
            friendly_behav.ApproachPlayer();
        }

        //Change Animation on Timer depending on Behaviour
        if (change_anim_timer <= 0)
        {
            ChooseRandomIndex();
            turning_behav.TurningBehaviour();
            //behaviours
            if (behav_switch.neutral_script.enabled && !turning_behav.walking_after_turning_on)
            {
                neutral_behav.NeutralBehaviour();
            }
            else if (behav_switch.friendly_script.enabled)
            {//TODO uncomment
                friendly_behav.FriendlyBehaviour();
                GetPlayerOffset();
            }
            else if (behav_switch.aggressive_script.enabled)
            {
                agg_behav.AggressiveBehaviour();
            }
            ResetTimerFunction();
            //Debug.Log("new state " + dog_state);
        }
    }

}
