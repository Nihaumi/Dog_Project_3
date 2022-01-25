using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{    
    //other script
    public GameObject dog;
    Basic_Behaviour basic_behav;

    //targets
    GameObject aim_hand_right;
    GameObject aim_hand_left;
    GameObject aim_head;

    //target obj
    GameObject Player_eyes;
    GameObject hand_left;
    GameObject hand_right;

    //hand positions
    Vector3 hand_left_position;
    Vector3 hand_right_position;
    // Start is called before the first frame update
    void Start()
    {
        dog = GameObject.Find("GermanShepherd_Prefab");
        basic_behav = dog.GetComponent<Basic_Behaviour>();

        //targets
        aim_hand_left = GameObject.Find("AimtargetHandLeft");
        aim_hand_right = GameObject.Find("AimtargetHandRight");
        aim_head = GameObject.Find("AimTargetHead");

        //target obj
        hand_left = GameObject.Find("OVRHandPrefabLeft");
        hand_right = GameObject.Find("OVRHandPrefabRight");
        Player_eyes = GameObject.Find("CenterEyeAnchor");

        //hand positions
        hand_left_position = hand_left.transform.position;
        hand_right_position = hand_right.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float IsCloseToLeftHand()
    {
        basic_behav.dist_left_hand_to_dog = Vector3.Distance(hand_left.transform.position, dog.transform.position);
        return basic_behav.dist_left_hand_to_dog;
    }
    public float IsCloseToRightHand()
    {
        basic_behav.dist_right_hand_to_dog = Vector3.Distance(hand_right.transform.position, dog.transform.position);
        return basic_behav.dist_right_hand_to_dog;
    }

    float hands_moving_timer = 0;

    public bool AreHandsMoving()
    {

        hands_moving_timer -= Time.deltaTime;
        if (hands_moving_timer > 0)
        {
            return true;
        }

        float moving_value = 0.005f;
        Vector3 displacement_left = hand_left.transform.position - hand_left_position;
        hand_left_position = hand_left.transform.position;

        Vector3 displacement_right = hand_right.transform.position - hand_right_position;
        hand_right_position = hand_right.transform.position;

        if (displacement_left.magnitude > moving_value || displacement_right.magnitude > moving_value)
        {
            hands_moving_timer = 2;
            Debug.Log("HAND MOVING");
            return true;
        }
        else
        {
            Debug.Log("HAND NOT MOVING");
            return false;
        }

    }

}
