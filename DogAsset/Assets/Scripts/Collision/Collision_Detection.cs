using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_Detection : MonoBehaviour
{
    public bool collided;
    public bool hit_corner;

    //cube that collided
    public GameObject side;

    //objects
    GameObject dir_manager;
    GameObject dog;

    //scripts
    Turning_Direction_Handler turning_dir_handler;
    Basic_Behaviour basic_behav;

    // Start is called before the first frame update
    void Start()
    {
        //obj & scripts
        dog = GameObject.Find("GermanShepherd_Prefab");
        dir_manager = GameObject.Find("Direction_Manager");
        turning_dir_handler = dir_manager.GetComponent<Turning_Direction_Handler>();
        basic_behav = dog.GetComponent<Basic_Behaviour>();

        collided = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Environment" || collision.gameObject.tag == "Corner")
        {
            if (gameObject.name == "left_trot" || gameObject.name == "right_trot")
            {
                if(basic_behav.y_goal == basic_behav.trot_value)
                {
                    collided = true;
                    GetCollidedObject(this.gameObject);
                    Debug.Log("TROT COLLISION with cube: " + side.name);
                    if (collision.gameObject.tag == "Corner")
                    {
                        hit_corner = true;
                    }
                }
            }
           else if(basic_behav.y_goal != basic_behav.trot_value && !collided)
            {
                collided = true;
                GetCollidedObject(this.gameObject);
                Debug.Log("COLLISION with cube: " + side.name);
                if (collision.gameObject.tag == "Corner")
                {
                    hit_corner = true;
                }
            }
        }


    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Environment" || collision.gameObject.tag == "Corner")
        {
            Debug.Log("EXIT collision with: " + collision.gameObject.name);
            collided = false;
            hit_corner = false;
            turning_dir_handler.turn_90_deg = false;
        }
    }
    public void GetCollidedObject(GameObject cube)
    {
        side = cube;
    }
}
