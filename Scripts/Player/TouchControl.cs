// TouchControl
// Allen Jones
// For use with Dew
// Created 8/30/16
//
// determines if an object is touched

using UnityEngine;
using System.Collections;

public class TouchControl : MonoBehaviour
{

    public GameManager GameManager;

    //Var
    private Vector2 touchCache;
    private int screenHeight;
    private int screenWidth;
   // private float maxPickingDistance = 10000;// increase if needed, depending on your scene size
   // private Vector3 startPos;
   // private Transform pickedObject = null;

    //The renderer component
    private SpriteRenderer rend;

    //future pos of touch in 3d space
   // private Vector2 pos, pos2;
  //  private Vector3 movement;
    private GameObject ooMover;
    private RaycastHit2D hitInfo;
    private float xx;
    private float yy;
    private Touch touch;
    public bool firstTouch;

    // Use this for initialization
    void Start()
    {
        firstTouch = true;
        //Set rend, our renderer component
        rend = GetComponent<SpriteRenderer>();

        //Cache called function variables
        screenHeight = Screen.height;
        screenWidth = Screen.width;

        ooMover = null;
    }

    // Update is called once per frame
    void Update()
    {
        //////////If running game in editor////////////////////////////////////////////////////////////////////
#if UNITY_EDITOR
        //check for mouse input
        /*if (Input.GetMouseButton(0))
        {
            touch = Input.mousePosition;
         
        }//end getmousebutton*/
#endif
            //////////END If running game in editor////////////////////////////////////////////////////////////////////
            //check for  input on ttt.
            if (Input.touchCount > 0)
        {
            touch = Input.touches[0];
            //Create horizontal plane/*
            /* Plane horPlane = new Plane(Vector3.up, Vector3.zero);

             //Gets the ray at position where the screen is touched
             Ray ray = Camera.main.ScreenPointToRay(touch.position);

             if (touch.phase == TouchPhase.Began)
             {
                 RaycastHit2D hitInfo = Physics2D.Raycast((Camera.main.ScreenToWorldPoint(touch.position)), Vector2.zero);

                // RaycastHit hit = new RaycastHit();
                 if (hitInfo)
                 {
                     pickedObject = hitInfo.transform;
                     startPos = touch.position;
                 }
                 else
                 {
                     pickedObject = null;
                 }
             }
             else if (touch.phase == TouchPhase.Moved)
             {
                 if (pickedObject != null)
                 {
                     //float distance1 = 0f;
                     //if (horPlane.Raycast(ray, out distance1))
                     //{
                         pickedObject.transform.position = touch.position;
                     // }
                 }
             }
             else if (touch.phase == TouchPhase.Ended)
             {
                 pickedObject = null;
             }
             //////////////////////////////////
             */


            //if it is the start of the press
            //if (touch.phase == TouchPhase.Began)
            //pos = (Camera.main.ScreenToWorldPoint(touch.position));

            //If the touch has yet to release
            if (touch.phase == TouchPhase.Began)
            {
               
                //RaycastHit to determine hit
                hitInfo = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);

                //If a hit, see what it is
                if (hitInfo)
                {
                    //Check that it is this object
                    if (hitInfo.transform.gameObject == this.gameObject)
                    {
                        //if so, confirm with red color
                        //rend.color = Color.red;
                        ooMover = this.gameObject;
                        if (firstTouch)
                        {
                            GameManager.StartGamePlay();
                            firstTouch = false;
                        }
                        //movement = pos2 - pos;


                    }
                    else ooMover = null;
                }
                else ooMover = null;
            }
            else if (touch.phase != TouchPhase.Ended)
            {
                if (ooMover != null)
                {
                    //Cache touch position
                    touchCache = touch.position;

                    //update pos of object to touch pos
                    //calculate touch xy to 3D space xyz

                    //Set x (float 0-5)
                    xx = (Mathf.Clamp(touchCache.x / screenWidth * 0.7f, 0, 0.7f));
                    // convert from positive quad 1 to all 4 quads (0=-3, 2.5=0, 5=3)
                    xx = ((xx * 2f - 0.7f)*0.6f);
                    //Set y (float 0-5)
                    yy = (Mathf.Clamp(touchCache.y / screenHeight * 0.7f, 0, 0.7f));
                    // convert from positive quad 1 to all 4 quads (0=-5, 2.5=0, 5=5)
                    yy = (yy * 2 - 0.7f);

                    //Add values to 3D vector and Set the objects position
                    ooMover.transform.position = new Vector3(xx, yy);

                }

            }
            //once released, return to white
            //else rend.color = Color.white;
        }//end for 
    }
}
