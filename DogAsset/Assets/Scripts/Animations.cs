using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    //animations grouped as they are in the animator 
    //neutral/friendly
    //standing
    public   string stand_01 = "Stand_01";
    public   string trans_lying_to_stand_01 = "Trans_Lying_to_Stand_01";
    public   string trans_sitting_to_stand_01 = "Trans_Lying_to_Stand_01";
    public   string trans_sleeping_to_lying_to_stand_01 = "Trans_Sleeping_to_Lying_to_Stand_01";
    public   string stand_02 = "Stand_02";
    public   string trans_lying_to_stand_02 = "Trans_Lying_to_Stand_02";
    public   string trans_sitting_to_stand_02 = "Trans_Sitting_to_Stand_02";
    //sitting
    public   string sit_00 = "Sitting_00";
    public   string trans_stand_to_sit_00 = "Trans_Stand_to_Sitting_00";
    public   string trans_walk_to_stand_to_sit_00 = "Trans_Walk_to_Stand_to_Sitting_00";
    public   string sit_01 = "Sitting_01";
    public   string trans_stand_to_sit_01 = "Trans_Stand_to_Sitting_01";
    public   string trans_walk_to_stand_to_sit_01 = "Trans_Walk_to_Stand_to_Sitting_01";
    public   string sit_02 = "Sitting_02";
    public   string trans_stand_to_sit_02 = "Trans_Stand_to_Sitting_02";
    public   string trans_walk_to_stand_to_sit_02 = "Trans_Walk_to_Stand_to_Sitting_02";
    //lying
    public   string lying_00 = "Lying_00";
    public   string trans_stand_to_lying_00 = "Trans_Stand_to_Lying_00";
    public  string trans_walk_to_stand_to_lying_00 = "Trans_Walk_to_Stand_to_Lying_00";
    public  string trans_sleep_to_lying = "Trans_Sleeping_to_Lying_00";
    public  string lying_01 = "Lying_01";
    public  string trans_stand_to_lying_01 = "Trans_Stand_to_Lying_00";
    public  string trans_walk_to_stand_to_lying_01 = "Trans_Walk_to_Stand_to_Lying_00";
    public  string lying_02 = "Lying_02";
    public  string trans_stand_to_lying_02 = "Trans_Stand_to_Lying_00";
    public  string trans_walk_to_stand_to_lying_02 = "Trans_Walk_to_Stand_to_Lying_00";
    //sleeping
    public string sleep = "Sleeping_01";
    public   string trans_lying_to_sleep = "Trans_Lying_to_Sleeping_01";
    public   string trans_stand_to_lying_to_sleep = "Trans_Stand_to_Lying__Sleeping_01";
    public   string trans_walk_to_stand_to_lying_to_sleep = "Trans_Walk_to_Stand_to_Lying_to_Sleeping_01";
    //slow walk
    public  string walk_slow = "Loco_WalkSlow_Copy";
    public  string trans_sit_to_stand_to_walk_slow = "Trans_Sitting_to_Stand_plus_WalkSlow";
    public  string trans_lying_to_stand_to_walk_slow = "Trans_Lying_to_Stand_plus_WalkSlow";
    public  string trans_sleep_to_lying_to_stand_to_walk_slow = "Trans_Sleeping_to_Lying_to_Stand_plus_WalkSlow";
    public  string walk_slow_L = "Loco_WalkSlow_L";
    public  string walk_slow_R = "Loco_WalkSlow_R";
    //seek walk
    public  string seek = "Loco_WalkSeek";
    public  string trans_sit_to_stand_to_seek = "Trans_Sitting_to_Stand_plus_seek";
    public  string trans_lying_to_stand_to_seek = "Trans_Lying_to_Stand_plus_seek";
    public  string trans_sleep_to_lying_to_stand_to_seek = "Trans_Sleeping_to_Lying_to_Stand_plus_seek";
    public  string seek_L = "Loco_WalkSlow_Seek_L";
    public  string seek_R = "Loco_WalkSlow_Seek_R";
    public  string turn_left_seek = "Trans_TurnL90_Seek";
    public  string turn_right_seek = "Trans_TurnR90_Seek";
    //normal walk
    public   string walk = "Loco_Walk";
    public   string trans_sit_to_stand_to_walk = "Trans_Sitting_to_Stand_plus_Walk";
    public   string trans_lying_to_stand_to_walk = "Trans_Lying_to_Stand_plus_Walk";
    public   string trans_sleep_to_lying_to_stand_to_walk = "Trans_Sleeping_to_Lying_to_Stand_plus_Walk";
    public   string walk_L= "Loco_Walk_L";
    public   string walk_R = "Loco_Walk_R";
    public   string turn_left_walk = "Trans_TurnL90_Walk";
    public   string turn_right_walk = "Trans_TurnR90_Walk";
    //trot
    public   string trot = "Loco_Trot";
    public   string trans_sit_to_stand_to_trot = "Trans_Sitting_to_Stand_plus_Trot";
    public   string trans_lying_to_stand_to_trot = "Trans_Lying_to_Stand_plus_Trot";
    public   string trans_sleep_to_lying_to_stand_to_trot = "Trans_Sleeping_to_Lying_to_Stand_plus_Trot";
    public   string trot_L = "Loco_Trot_L";
    public   string trot_R = "Loco_Trot_R";
    public   string turn_left_trot = "Trans_TurnL90_Trot";
    public   string turn_right_trot = "Trans_TurnR90_Trot";
    //run
    public   string run = "Loco_Run";
    public   string trans_sit_to_stand_to_run = "Trans_Sitting_to_Stand_plus_Run";
    public   string trans_lying_to_stand_to_run = "Trans_Lying_to_Stand_plus_Run";
    public   string trans_sleep_to_lying_to_stand_to_run = "Trans_Sleeping_to_Lying_to_Stand_plus_Run";
    public   string run_L = "Loco_Run_L";
    public   string run_R = "Loco_Run_R";

    //aggressive
    public   string aggressive_attack_bite_R_long = "Agressive_01_Attack_BiteR_Long";
    public   string aggressive_attack_bite_L_long = "Agressive_01_Attack_BiteL_Long ";
    public   string aggressive_attack_aggressive_bite_R_long = "Agressive_01_Attack_Agressive_BiteL_Long ";


    //animation lists - to pick a random animation
    public List<string> list_standing = new List<string>();

    public List<string> list_sitting = new List<string>();

    public List<string> list_lying = new List<string>();

    public List<string> list_walking = new List<string>();

    public List<string> list_sleeping = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        //fill lists
        list_standing.Add(trans_stand_to_lying_00);
        list_standing.Add(trans_stand_to_sit_00);
        list_standing.Add(walk);
        list_standing.Add(trot);
        list_standing.Add(walk_slow);
        list_standing.Add(run);
        list_standing.Add(seek);
        list_standing.Add(turn_left_seek);
        list_standing.Add(turn_right_seek);

        list_sitting.Add(trans_sit_to_stand_to_walk_slow);
        list_sitting.Add(trans_sit_to_stand_to_walk);
        list_sitting.Add(trans_sit_to_stand_to_seek);
        list_sitting.Add(trans_sit_to_stand_to_trot);
        list_sitting.Add(trans_sit_to_stand_to_run);

        list_lying.Add(trans_lying_to_stand_to_run);
        list_lying.Add(trans_lying_to_stand_to_seek);
        list_lying.Add(trans_lying_to_stand_to_trot);
        list_lying.Add(trans_lying_to_stand_to_walk);
        list_lying.Add(trans_lying_to_stand_to_walk_slow);
        list_lying.Add(trans_lying_to_sleep);

        list_sleeping.Add(trans_sleep_to_lying);
        list_sleeping.Add(trans_sleeping_to_lying_to_stand_01);
        list_sleeping.Add(trans_sleep_to_lying_to_stand_to_run);
        list_sleeping.Add(trans_sleep_to_lying_to_stand_to_seek);
        list_sleeping.Add(trans_sleep_to_lying_to_stand_to_trot);
        list_sleeping.Add(trans_sleep_to_lying_to_stand_to_walk);
        list_sleeping.Add(trans_sleep_to_lying_to_stand_to_walk_slow);

        list_walking.Add(run);
        list_walking.Add(seek);
        list_walking.Add(walk);
        list_walking.Add(walk_slow);
        list_walking.Add(trot);
        list_walking.Add(seek_L);
        list_walking.Add(seek_R);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
