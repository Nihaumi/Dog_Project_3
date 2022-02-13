using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementUtils : MonoBehaviour
{
    GameObject dog;
    Animation_Controll anim_controll;
    Animations anim;
    Basic_Behaviour basic_behav;
    float speed = 10f;

    Vector3 direction;
    Quaternion rotation;
    void Start()
    {
        //other script
        dog = GameObject.Find("GermanShepherd_Prefab");
        anim_controll = dog.GetComponent<Animation_Controll>();
        anim = dog.GetComponent<Animations>();
        basic_behav = dog.GetComponent<Basic_Behaviour>();
    }

    public void sit_down()
    {
        anim_controll.ChangeAnimationState(anim.friendly_trans_stand_to_sitting);
    }
    public void lay_down()
    {
        anim_controll.ChangeAnimationState(anim.trans_stand_to_lying_00);
    }
    public bool turn_until_facing(GameObject target, bool and_start_moving = false)
    {
        Debug.Log("TURN GOAL: " + basic_behav.x_goal);
        if (is_looking_at(target))
        {
            if (and_start_moving)
                start_moving_straight();
            else
                stop_turning();
            return true;
        }
        else
        {
            start_turning_towards(target);
            return false;
        }
    }
    public bool walk_until_touching(GameObject target, float dist = 1f, bool stopping = true)
    {
        if (is_touching(target, dist))
        {
            if (stopping)
            {
                stop_moving();
            }
            return true;
        }
        else
        {
            //start_moving();
            Debug.Log("LOOKING DIRECTLY");
            looking_directly_at(target);
            return false;
        }
    }
    private bool is_touching(GameObject target, float distance = 1f)
    {
        float dist = get_dist_to_target(target);
        return dist <= distance;
    }

    private float get_dist_to_target(GameObject target)
    {
        float dist = Vector3.Distance(dog.transform.position, target.transform.position);
        return dist;
    }

    private bool is_looking_at(GameObject target)
    {
        return basic_behav.GetPlayerOffset(0, 16, 0.25f, true, target) == 0;
    }

    public bool looking_directly_at(GameObject target)
    {
        Vector3 target_pos = dog.transform.InverseTransformPoint(target.transform.position);
        if (Mathf.Round(target_pos.x * 10000) / 10000f == 0.0f)
        {
            Debug.Log("True MIDDLE");
            return true;
        }

        else
        {
            //Debug.Log("TRAGET LOCAL POS: " + target_pos);
            //Debug.Log("CORRECTING COURSE");
            direction = (target.transform.position - dog.transform.position).normalized;
            rotation = Quaternion.LookRotation(direction);
            dog.transform.rotation = Quaternion.Slerp(dog.transform.rotation, rotation, speed * Time.deltaTime);

            basic_behav.WalkForward();
            basic_behav.y_acceleration = 2f;
            return false;
        }
    }

    private void start_turning_towards(GameObject target)
    {
        change_blend_tree_if_necessary(false);
        basic_behav.choose_direction_to_walk_into(target);
    }

    public bool walk_until_complete_speed(float speed = 0.25f)
    {
        if (speed < 0.01f)
        {
            reset_acceleration();
            if (Mathf.Abs(basic_behav.y_axis) + Mathf.Abs(basic_behav.x_axis) < speed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (Mathf.Abs(basic_behav.y_axis) + Mathf.Abs(basic_behav.x_axis) > speed)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void stop_turning()
    {
        basic_behav.x_goal = Basic_Behaviour.standing_value;
        change_blend_tree_if_necessary(true);
    }
    public void start_moving()
    {
        //stop_turning();
        change_blend_tree_if_necessary(true);
        //basic_behav.y_goal = basic_behav.walking_value;
        basic_behav.WalkForward();
        basic_behav.y_acceleration = 2f;
    }

    public void start_moving_straight()
    {
        change_blend_tree_if_necessary(false);
        basic_behav.WalkForward();
        basic_behav.y_acceleration = 2f;
    }

    public void stop_moving()
    {
        change_blend_tree_if_necessary(true);
        basic_behav.y_goal = Basic_Behaviour.standing_value;
        basic_behav.x_goal = Basic_Behaviour.standing_value;
    }

    public bool distance_from_target(GameObject target, float dist = 7f)
    {
        if (get_dist_to_target(target) < dist)
        {
            //Debug.Log("PLAYER DIST: " + get_dist_to_target(target));
            walk_back();
            //start_turning_towards(target); --> makes him dance
            return false;
        }
        else
        {
            stop_moving();
            return true;
        }
    }

    public void reset_acceleration()
    {
        basic_behav.y_acceleration = basic_behav.default_y_acceleration;
        basic_behav.x_acceleration = 1f;
    }

    public void walk_back()
    {
        change_blend_tree_if_necessary(true);
        basic_behav.x_goal = Basic_Behaviour.standing_value;
        basic_behav.y_goal = -Basic_Behaviour.walking_value;
    }

    private void change_blend_tree_if_necessary(bool standing)
    {
        if (anim_controll.current_state != anim.blending_BT)
        {
            anim_controll.ChangeAnimationState(anim.blending_BT);
        }
        if (standing)
        {
            basic_behav.ChangeBlendingBT(Basic_Behaviour.blending_bt_standing);
        }
        else
        {
            basic_behav.ChangeBlendingBT(Basic_Behaviour.blending_bt_no_standing);

        }
    }
}
