using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Friendly_Behaviour : MonoBehaviour
{
    //other script
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

    //constraint gedöns
    GameObject neck;
    GameObject head;
    GameObject left_eye;
    GameObject right_eye;

    //targets
    GameObject aim_hand_right;
    GameObject aim_hand_left;
    GameObject aim_head;

    //target obj
    GameObject Player_eyes;
    GameObject hand_left;
    GameObject hand_right;

    //contraint components
    MultiAimConstraint neck_constraint;
    MultiAimConstraint head_constraint;
    MultiAimConstraint left_eye_constraint;
    MultiAimConstraint right_eye_constraint;

    //hands distance und so
    public float hand_close = 1f;
    public float dist_left_hand_to_dog;
    public float dist_right_hand_to_dog;
    Vector3 hand_left_position;
    Vector3 hand_right_position;


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


        //rig layer obj
        neck = GameObject.Find("Neck3Aim");
        head = GameObject.Find("Head aim");
        right_eye = GameObject.Find("EyeRight Aim");
        left_eye = GameObject.Find("EyeLeft Aim");

        //targets
        aim_hand_left = GameObject.Find("AimtargetHandLeft");
        aim_hand_right = GameObject.Find("AimtargetHandRight");
        aim_head = GameObject.Find("AimTargetHead");

        //target obj
        hand_left = GameObject.Find("OVRHandPrefabLeft");
        hand_right = GameObject.Find("OVRHandPrefabRight");
        Player_eyes = GameObject.Find("CenterEyeAnchor");

        //multi aim constraints
        neck_constraint = neck.GetComponent<MultiAimConstraint>();
        head_constraint = head.GetComponent<MultiAimConstraint>();
        right_eye_constraint = right_eye.GetComponent<MultiAimConstraint>();
        left_eye_constraint = left_eye.GetComponent<MultiAimConstraint>();

        hand_left_position = hand_left.transform.position;
        hand_right_position = hand_right.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        IsCloseToLeftHand();
        IsCloseToRightHand();
        AreHandsMoving();
        SetFollowObject();

        //if not facing player turn face towards player
        //bleib stehen für mind 10 sekunden
        //sitz
        //platz
        //schlaf
        if (TouchingPlayer())
        {
            //Debug.Log("touching player");
        }

        //if hand is touching dog, make eyes look at player eyes
        //and head facing player 
        // bleib stehen solange berührung
        //sitz nach 10 sekunden
        //platz nach 10 sekunden
        if (TouchingHand())
        {
            Debug.Log("touchiung Hand");
        }

        //wenn hand bestimmte geschwindigekit hat ... oder sich viel bewegt(bestimmte distanz oder zeit)
        //follow hand movement with head
        if (HandIsMoving())
        {
            Debug.Log("Hand is Moving");
        }
        //und wenn facing player 
        //aye contact
        if (PlayerHeadIsMoving())
        {
            Debug.Log("Player Head moving");
        }
    }

    /*TODO
     * farge ob hände sich bewegen 
     * ja: follow hands
     * nein: follow head
     */

    //checks if left or right hand is closer
    //sets weight of the target source objects accordingly to which one should be followed
    float timer = 0;
    bool AreHandsMoving()
    {

        timer -= Time.deltaTime;
        if(timer > 0){
            return true;
        }

        float moving_value = 0.005f;
        Vector3 displacement_left = hand_left.transform.position - hand_left_position;
        hand_left_position = hand_left.transform.position;

        Vector3 displacement_right = hand_right.transform.position - hand_right_position;
        hand_right_position = hand_right.transform.position;
        
        if (displacement_left.magnitude > moving_value || displacement_right.magnitude > moving_value)
        {
            timer = 2;
            Debug.Log("HAND MOVING");
            return true;
        }
        else
        {
            Debug.Log("HAND NOT MOVING");
            return false;
        }

    }

    void SetFollowObject()
    {
        if (!AreHandsMoving())
        {   //look at Head and away from left and right Hand
            SetWeightConstraint(neck_constraint, 2);
            SetWeightConstraint(head_constraint, 2);
            SetWeightConstraint(right_eye_constraint, 2);
            SetWeightConstraint(left_eye_constraint, 2);
        }
        else if (dist_left_hand_to_dog < dist_right_hand_to_dog)
        {   //look at left Hand and away from Head and right Hand
            SetWeightConstraint(neck_constraint, 1);
            SetWeightConstraint(head_constraint, 1);
            SetWeightConstraint(right_eye_constraint, 1);
            SetWeightConstraint(left_eye_constraint, 1);

            Debug.Log("TRACK LEFT");
        }
        else if (dist_right_hand_to_dog < dist_left_hand_to_dog)
        {   //look at right Hand and away from Head and left Hand
            SetWeightConstraint(neck_constraint, 0);
            SetWeightConstraint(head_constraint, 0);
            SetWeightConstraint(right_eye_constraint, 0);
            SetWeightConstraint(left_eye_constraint, 0);
            
            Debug.Log("TRACK RIGHT");
        }

    }

    //TODO: Maybe changerate variable
    //focus: 0 -> right, 1 -> left, 2 -> head
    void SetWeightConstraint(MultiAimConstraint constraint, int focus)
    {
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
            default:
                Debug.Log("BRUH WTF");
                break;
        }
        a.SetWeight(0, weight_update_right);
        a.SetWeight(1, weight_update_left);
        a.SetWeight(2, weight_update_head);
        constraint.data.sourceObjects = a;
    }
    //Mathf.Max(y_axis, value);


    public float dist_to_player;
    bool TouchingPlayer()
    {
        dist_to_player = Vector3.Distance(player.transform.position, dog.transform.position);
        if (dist_to_player < 2f)
        {
            return true;
        }
        return false;
    }
    bool TouchingHand()
    {
        return false;
    }

    bool HandIsMoving()
    {
        return false;
    }
    bool PlayerHeadIsMoving()
    {
        return false;
    }

    bool IsFacingPlayer()
    {
        return false;
    }

    float IsCloseToLeftHand()
    {
        dist_left_hand_to_dog = Vector3.Distance(hand_left.transform.position, dog.transform.position);
        return dist_left_hand_to_dog;
    }
    float IsCloseToRightHand()
    {
        dist_right_hand_to_dog = Vector3.Distance(hand_right.transform.position, dog.transform.position);
        return dist_right_hand_to_dog;
    }






    public void NeutralBehaviour()
    {

        switch (basic_behav.dog_state)
        {
            case Basic_Behaviour.Animation_state.standing:

                basic_behav.ResetParameter();
                neutral_behav.SetBoolForWalkingBT();
                //Debug.Log("standinglist item at rndindex: " + basic_behav.random_index + "is:" + anim.list_standing[basic_behav.random_index]);
                if (basic_behav.random_index == 0)
                {
                    anim_controll.ChangeAnimationState(anim.list_standing[basic_behav.random_index]);
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.lying;
                }
                if (basic_behav.random_index == 1)
                {
                    anim_controll.ChangeAnimationState(anim.list_standing[basic_behav.random_index]);
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.sitting;
                }
                if (basic_behav.random_index > 1 && basic_behav.random_index < 4)
                {
                    anim_controll.ChangeAnimationState(anim.blend_tree);
                    if (basic_behav.random_index == 2)
                    {
                        basic_behav.y_goal = basic_behav.walking_slow_value;
                    }
                    if (basic_behav.random_index == 3)
                    {
                        basic_behav.y_goal = basic_behav.walking_value;
                    }
                    //SetBlendTreeParameters();
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;
                }
                basic_behav.SetLongTimer();
                Debug.Log("standing list item at rndindex: " + basic_behav.random_index + "is:" + anim.list_standing[basic_behav.random_index]);
                break;
            case Basic_Behaviour.Animation_state.sitting:
                anim_controll.ChangeAnimationState(anim.trans_sitting_to_stand_02);

                basic_behav.ResetParameter();
                neutral_behav.SetBoolForWalkingBT();
                if (basic_behav.random_index == 0)
                {
                    basic_behav.y_goal = basic_behav.walking_slow_value;
                }
                if (basic_behav.random_index == 1)
                {
                    basic_behav.y_goal = basic_behav.walking_value;
                }
                if (basic_behav.random_index == 2)
                {
                    neutral_behav.SetBoolForSeekBT();
                    basic_behav.y_goal = basic_behav.seek_value;
                }
                //SetBlendTreeParameters();
                basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;
                basic_behav.SetLongTimer();
                Debug.Log("sitting list item at rndindex: " + basic_behav.random_index + "is:" + anim.list_sitting[basic_behav.random_index]);
                break;
            case Basic_Behaviour.Animation_state.lying:

                basic_behav.ResetParameter();
                //Debug.Log("lying list item at rndindex: " + basic_behav.random_index + "is:" + anim.list_lying[basic_behav.random_index]);
                if (basic_behav.random_index == 0)
                {
                    anim_controll.ChangeAnimationState(anim.list_lying[basic_behav.random_index]);
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.sleeping;
                }
                else
                {
                    anim_controll.ChangeAnimationState(anim.trans_lying_to_stand_02);
                    neutral_behav.SetBoolForWalkingBT();
                    if (basic_behav.random_index == 1)
                    {
                        basic_behav.y_goal = basic_behav.walking_slow_value;
                    }
                    if (basic_behav.random_index == 2)
                    {
                        basic_behav.y_goal = basic_behav.walking_value;
                    }
                    if (basic_behav.random_index == 3)
                    {
                        neutral_behav.SetBoolForSeekBT();
                        basic_behav.y_goal = basic_behav.seek_value;
                    }
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;
                }
                basic_behav.SetLongTimer();
                Debug.Log("lyingg list item at rndindex: " + basic_behav.random_index + "is:" + anim.list_lying[basic_behav.random_index]);
                break;
            case Basic_Behaviour.Animation_state.sleeping:

                basic_behav.ResetParameter();
                neutral_behav.SetBoolForWalkingBT();
                //Debug.Log("sleeping list item at rndindex: " + basic_behav.random_index + "is:" + anim.list_sleeping[basic_behav.random_index]);
                if (basic_behav.random_index == 0)
                {
                    anim_controll.ChangeAnimationState(anim.list_sleeping[basic_behav.random_index]);

                    basic_behav.dog_state = Basic_Behaviour.Animation_state.lying;
                }
                else if (basic_behav.random_index == 1)
                {
                    anim_controll.ChangeAnimationState(anim.list_sleeping[basic_behav.random_index]);

                    basic_behav.dog_state = Basic_Behaviour.Animation_state.standing;
                }
                else
                {
                    anim_controll.ChangeAnimationState(anim.trans_sleeping_to_lying_to_stand_02);
                    if (basic_behav.random_index == 2)
                    {
                        basic_behav.y_goal = basic_behav.walking_slow_value;
                    }
                    if (basic_behav.random_index == 3)
                    {
                        basic_behav.y_goal = basic_behav.walking_value;
                    }
                    if (basic_behav.random_index == 4)
                    {
                        neutral_behav.SetBoolForSeekBT();
                        basic_behav.y_goal = basic_behav.seek_value;
                    }
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;
                }
                basic_behav.SetLongTimer();
                Debug.Log("sleeping list item at rndindex: " + basic_behav.random_index + "is:" + anim.list_sleeping[basic_behav.random_index]);
                break;
            case Basic_Behaviour.Animation_state.walking:

                if (anim_controll.current_state != anim.blend_tree)
                {
                    anim_controll.ChangeAnimationState(anim.blend_tree);
                }
                basic_behav.SetLongTimer();
                if (basic_behav.random_index == 0)
                {
                    basic_behav.dog_state = Basic_Behaviour.Animation_state.standing;
                    basic_behav.y_goal = basic_behav.standing_value;
                }
                else
                {
                    //anim_controll.ChangeAnimationState(anim.blend_tree);
                    if (basic_behav.random_index == 1)
                    {
                        if (anim_controll.current_state == anim.blend_tree_seek)
                        {
                            neutral_behav.SwitchToOrFromSeekingBehaviour(anim.blend_tree);
                        }
                        basic_behav.y_goal = basic_behav.walking_slow_value;
                    }
                    if (basic_behav.random_index == 2)
                    {
                        if (anim_controll.current_state == anim.blend_tree_seek)
                        {
                            neutral_behav.SwitchToOrFromSeekingBehaviour(anim.blend_tree);
                        }
                        basic_behav.y_goal = basic_behav.walking_value;
                    }
                    if (basic_behav.random_index == 3)
                    {
                        if (anim_controll.current_state != anim.blend_tree_seek)
                        {
                            neutral_behav.SwitchToOrFromSeekingBehaviour(anim.blend_tree_seek);
                        }
                        basic_behav.y_goal = basic_behav.seek_value;
                    }

                    basic_behav.dog_state = Basic_Behaviour.Animation_state.walking;

                }
                Debug.Log("walking list item at rndindex: " + basic_behav.random_index + "is:" + anim.list_walking[basic_behav.random_index]);
                break;
            default:
                return;
        }

    }


}
