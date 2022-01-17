using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turning_Direction_Handler : MonoBehaviour
{
    //scripts
    Collision_Detection col_det_left;
    Collision_Detection col_det_right;
    Neutral_Behaviour neutral_behav;

    //objects
    GameObject dog;
    GameObject left_cube;
    GameObject right_cube;

    public bool turning;

    // Start is called before the first frame update
    void Start()
    {
        //access to other scripts
        dog = GameObject.Find("GermanShepherd_Prefab");
        neutral_behav = dog.GetComponent<Neutral_Behaviour>();

        //get obj and scripts - left and right cube
        left_cube = GameObject.Find("left");
        right_cube = GameObject.Find("right");
        col_det_left = left_cube.GetComponent<Collision_Detection>();
        col_det_right = right_cube.GetComponent<Collision_Detection>();
        turning = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if dog is currently not turning
        if (!turning)
        {
            SetTurningDirection();
        }
        //when neither cube is colliding
        if (!col_det_left.collided && !col_det_right.collided)
        {
            StopTurning();
        }
    }

    void StopTurning()
    {
        //if we are still turning
        if (turning)
        {
            //stop turning and continue walking
            ToggleTurning();
            neutral_behav.dog_state = Neutral_Behaviour.Animation_state.walking_after_turning;
        }
    }

    //turn left opr right
    void SetTurningDirection()
    {
        //left and right cube collide at the same time --> Turn left and set turning true
        if (col_det_right.collided && col_det_left.collided)
        {
            Debug.Log("BOTH --> Left");
            ToggleTurning();
            neutral_behav.dog_state = Neutral_Behaviour.Animation_state.turning_left;
            SetAnimationTimerToZero();
            SetAnimationTimerToZero();
            return;
        }
        //left cube collides --> turn right and set turning true
        if (col_det_left.collided)
        {
            Debug.Log("COLLIDED left. TURN right");
            neutral_behav.dog_state = Neutral_Behaviour.Animation_state.turning_right;
            ToggleTurning();
            SetAnimationTimerToZero();
        }
        //right cube collides --> turn left and set turning true
        if (col_det_right.collided)
        {
            Debug.Log("COLLIDED right. TURN left");
            neutral_behav.dog_state = Neutral_Behaviour.Animation_state.turning_left;
            ToggleTurning();
            SetAnimationTimerToZero();
        }

    }

    void SetAnimationTimerToZero()
    {
        neutral_behav.change_anim_timer = 0;
    }
    void ToggleTurning()
    {
        if (turning) turning = false;
        if (!turning) turning = true;
    }

}
