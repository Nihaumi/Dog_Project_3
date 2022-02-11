using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Behaviour001 : MonoBehaviour
{
    MovementUtils MU;
    GameObject dog;

    public enum Step
    {
        Turning,
        WalkToTarget,
        SitDown,
        Stop
    }

    GameObject player_target;
    Step current_step;

    // Start is called before the first frame update
    void Start()
    {
        dog = GameObject.Find("GermanShepherd_Prefab");
        MU = dog.GetComponent<MovementUtils>();
        player_target = GameObject.Find("target");
        current_step = Step.Turning;
    }

    /* 1. drehen
     * 2. wenn auf target gucken stehen
     * 3. laufen zum target
     * 4. wenn da, stehen
     * */

    // Update is called once per frame
    void Update()
    {
        switch (current_step)
        {
            case Step.Turning:
                /* 
                 * 1. drehen
                 * 2. wenn auf target gucken stehen
                 */
                bool are_we_facing_the_player = MU.turn_until_facing(player_target, true);

                if (are_we_facing_the_player)
                    current_step = Step.WalkToTarget;
                break;
            case Step.WalkToTarget:
                /*
                 * 3. laufen zum target
                 */
                bool are_we_touching_the_player = MU.walk_until_touching(player_target);

                if (are_we_touching_the_player)
                    current_step = Step.SitDown;
                break;
            case Step.SitDown:
                MU.sit_down();
                current_step = Step.Stop;
                break;
            case Step.Stop:
                /*
                 * 4. Do nothing
                 */
                break;
            default:
                break;
        }
    }


}
