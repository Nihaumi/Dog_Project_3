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

    //scripts
    Turning_Direction_Handler turning_dir_handler;

    // Start is called before the first frame update
    void Start()
    {
        //obj & scripts
        dir_manager = GameObject.Find("Direction_Manager");
        turning_dir_handler = dir_manager.GetComponent<Turning_Direction_Handler>();

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
 
            collided = true;
            GetCollidedObject(this.gameObject);
            Debug.Log("COLLISION with cube: " + side.name);
            if( collision.gameObject.tag == "Corner")
            {
                hit_corner = true;
            }
        }


    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("EXIT collision with: " + collision.gameObject.name);
        if (collision.gameObject.tag == "Environment" || collision.gameObject.tag == "Corner")
        {
            collided = false;
            hit_corner = false;
        }
    }
    public void GetCollidedObject(GameObject cube)
    {
        side = cube;
    }
}
