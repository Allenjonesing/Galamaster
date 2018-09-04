using UnityEngine;
using System.Collections;

public class Scroll : MonoBehaviour {

    //The renderer component
    private Renderer rend;

    //Editor use, for adjustment
    public float scrollSpeed;

    //offset of BG
    private Vector2 offset;


    // Use this for initialization
    void Start ()
    {
        //Set rend, our renderer component
        rend = GetComponent<Renderer>();

        }
	
	// Update is called once per frame
	void Update ()
    {

        //update by time
        offset = new Vector2(0, Time.fixedDeltaTime * scrollSpeed);

        //apply update
        rend.material.mainTextureOffset += offset;

        //set the NG to the players pos
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 1);
    }
}
