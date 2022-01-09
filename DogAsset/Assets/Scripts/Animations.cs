using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    //animations grouped as they are in the animator 

    //neutral/friendly
    //standing
    const string stand_01 = "Stand_01";
    const string trans_lying_to_stand_01 = "Trans_Lying_to_Stand_01";
    const string trans_sitting_to_stand_01 = "Trans_Lying_to_Stand_01";
    const string trans_sleeping_to_lying_to_stand_01 = "Trans_Sleeping_to_Lying_to_Stand_01";
    const string stand_02 = "Stand_02";
    const string trans_lying_to_stand_02 = "Trans_Lying_to_Stand_02";
    const string trans_sitting_to_stand_02 = "Trans_Sitting_to_Stand_02";
    //sitting
    const string sit_00 = "Sitting_00";
    const string trans_stand_to_sit_00 = "Trans_Stand_to_Sitting_00";
    const string trans_walk_to_stand_to_sit_00 = "Trans_Walk_to_Stand_to_Sitting_00";
    const string sit_01 = "Sitting_01";
    const string trans_stand_to_sit_01 = "Trans_Stand_to_Sitting_01";
    const string trans_walk_to_stand_to_sit_01 = "Trans_Walk_to_Stand_to_Sitting_01";
    const string sit_02 = "Sitting_02";
    const string trans_stand_to_sit_02 = "Trans_Stand_to_Sitting_02";
    const string trans_walk_to_stand_to_sit_02 = "Trans_Walk_to_Stand_to_Sitting_02";
    //lying
    const string lying_00 = "Lying_00";
    const string trans_stand_to_lying_00 = "Trans_Stand_to_Lying_00";
    const string trans_walk_to_stand_to_lying_00 = "Trans_Walk_to_Stand_to_Lying_00";
    const string trans_sleep_to_lying = "Trans_Sleeping_to_Lying_00";
    const string lying_01 = "Lying_01";
    const string trans_stand_to_lying_01 = "Trans_Stand_to_Lying_00";
    const string trans_walk_to_stand_to_lying_01 = "Trans_Walk_to_Stand_to_Lying_00";
    const string lying_02 = "Lying_02";
    const string trans_stand_to_lying_02 = "Trans_Stand_to_Lying_00";
    const string trans_walk_to_stand_to_lying_02 = "Trans_Walk_to_Stand_to_Lying_00";
    //sleeping
    const string sleep = "Sleeping_01";
    const string trans_lying_to_sleep = "Trans_Lying_to_Sleeping_01";
    const string trans_stand_to_lying_to_sleep = "Trans_Stand_to_Lying__Sleeping_01";
    const string trans_walk_to_stand_to_lying_to_sleep = "Trans_Walk_to_Stand_to_Lying_to_Sleeping_01";
    //slow walk
    const string walk_slow = "Loco_WalkSlow_Copy";
    const string trans_sit_to_stand_to_walk_slow = "Trans_Sitting_to_Stand_plus_WalkSlow";
    const string trans_lying_to_stand_to_walk_slow = "Trans_Lying_to_Stand_plus_WalkSlow";
    const string trans_sleep_to_lying_to_stand_to_walk_slow = "Trans_Sleeping_to_Lying_to_Stand_plus_WalkSlow";
    const string walk_slow_L = "Loco_WalkSlow_L";
    const string walk_slow_R = "Loco_WalkSlow_R";
    //seek walk
    const string seek = "Loco_WalkSeek";
    const string trans_sit_to_stand_to_seek = "Trans_Sitting_to_Stand_plus_seek";
    const string trans_lying_to_stand_to_seek = "Trans_Lying_to_Stand_plus_seek";
    const string trans_sleep_to_lying_to_stand_to_seek = "Trans_Sleeping_to_Lying_to_Stand_plus_seek";
    const string seek_L = "Loco_WalkSlow_Seek_L";
    const string seek_R = "Loco_WalkSlow_Seek_R";
    const string turn_left_seek = "Trans_TurnL90_Seek";
    const string turn_right_seek = "Trans_TurnR90_Seek";
    //normal walk
    const string walk = "Loco_Walk";
    const string trans_sit_to_stand_to_walk = "Trans_Sitting_to_Stand_plus_Walk";
    const string trans_lying_to_stand_to_walk = "Trans_Lying_to_Stand_plus_Walk";
    const string trans_sleep_to_lying_to_stand_to_walk = "Trans_Sleeping_to_Lying_to_Stand_plus_Walk";
    const string walk_L= "Loco_Walk_L";
    const string walk_R = "Loco_Walk_R";
    const string turn_left_walk = "Trans_TurnL90_Walk";
    const string turn_right_walk = "Trans_TurnR90_Walk";
    //trot
    const string trot = "Loco_Trot";
    const string trans_sit_to_stand_to_trot = "Trans_Sitting_to_Stand_plus_Trot";
    const string trans_lying_to_stand_to_trot = "Trans_Lying_to_Stand_plus_Trot";
    const string trans_sleep_to_lying_to_stand_to_trot = "Trans_Sleeping_to_Lying_to_Stand_plus_Trot";
    const string trot_L = "Loco_Trot_L";
    const string trot_R = "Loco_Trot_R";
    const string turn_left_trot = "Trans_TurnL90_Trot";
    const string turn_right_trot = "Trans_TurnR90_Trot";
    //run
    const string run = "Loco_Run";
    const string trans_sit_to_stand_to_run = "Trans_Sitting_to_Stand_plus_Run";
    const string trans_lying_to_stand_to_run = "Trans_Lying_to_Stand_plus_Run";
    const string trans_sleep_to_lying_to_stand_to_run = "Trans_Sleeping_to_Lying_to_Stand_plus_Run";
    const string run_L = "Loco_Run_L";
    const string run_R = "Loco_Run_R";

    //aggressive
    const string aggressive_attack_bite_R_long = "Agressive_01_Attack_BiteR_Long";
    const string aggressive_attack_bite_L_long = "Agressive_01_Attack_BiteL_Long ";
    const string aggressive_attack_aggressive_bite_R_long = "Agressive_01_Attack_Agressive_BiteL_Long ";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
