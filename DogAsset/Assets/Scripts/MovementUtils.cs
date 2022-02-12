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
    //moon.Transform.rotation=Quaternion.RotateTowards(moon.Transform.rotation,target.Transform.rotation,float time)
    public bool turn_until_facing(GameObject target, bool and_start_moving = false)
    {
        if (is_looking_at(target))
        {

            if (and_start_moving)
                start_moving();
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
    public bool walk_until_touching(GameObject target, float dist = 1f)
    {
        if (is_touching(target, dist))
        {
            stop_moving();
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
        float dist = Vector3.Distance(dog.transform.position, target.transform.position);
        return dist <= distance;
    }

    private bool is_looking_at(GameObject target)
    {
        return basic_behav.GetPlayerOffset(0, 32, 0.125f, true, target) == 0;
    }

    public bool looking_directly_at(GameObject target)
    {
        Vector3 target_pos = dog.transform.InverseTransformPoint(target.transform.position);
        if(target_pos.x == 0.0f)
        {
            Debug.Log("True MIDDLE");
            return true;
        }

        else
        {
            Debug.Log("TRAGET LOCAL POS: " + target_pos);
            Debug.Log("CORRECTING COURSE");
            direction = (target.transform.position - dog.transform.position).normalized;
            rotation = Quaternion.LookRotation(direction);
            dog.transform.rotation = Quaternion.Slerp(dog.transform.rotation, rotation, speed * Time.deltaTime);

            basic_behav.x_goal = basic_behav.walking_value;
            basic_behav.WalkForward();
            basic_behav.y_acceleration = 2f;
            return false;
        }
    }

    private void start_turning_towards(GameObject target)
    {
        anim_controll.ChangeAnimationState(anim.aggresive_blend_tree);
        basic_behav.y_goal = basic_behav.walking_value;
        basic_behav.choose_direction_to_walk_into(target);

    }


    private void stop_turning()
    {
        basic_behav.x_goal = basic_behav.standing_value;
    }
    private void start_moving()
    {
        //stop_turning();
        Debug.Log("START MOVING FOWARD");
        anim_controll.ChangeAnimationState(anim.aggresive_blend_tree);
        //basic_behav.y_goal = basic_behav.walking_value;
        basic_behav.WalkForward();
        basic_behav.y_acceleration = 2f;
    }

    public  void stop_moving()
    {
        basic_behav.y_goal = basic_behav.standing_value;
        basic_behav.x_goal = basic_behav.standing_value;
    }
}
