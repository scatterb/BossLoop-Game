using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkeletonActionScript : MonoBehaviour {

    public Rigidbody2D rigidbody;
    public PlayerController player; 
    // Use this for initialization
    void Start () {
        rigidbody.freezeRotation = true;
    }

    // Update is called once per frame
    void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject hitplayer = collision.gameObject;
        Vector2 knockBackDirection = (hitplayer.transform.position - this.transform.position).normalized;
        player.getKnockedBack(knockBackDirection);
    }

   

}
