using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Resetter : MonoBehaviour
{
    public Rigidbody2D rb2dProjectile;
    public float resetSpeed = 0.025f;

    private float resetSpeedSqr; // Unity Engine processes square speeds faster than regular (CPU optimization)
    private SpringJoint2D spring;

    // Start is called before the first frame update
    void Start()
    {
        resetSpeedSqr = resetSpeed * resetSpeed;
        spring = rb2dProjectile.GetComponent<SpringJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // user inputted reset
        {
            Reset();
        }

        if (spring == null && rb2dProjectile.velocity.sqrMagnitude < resetSpeedSqr) // projectile stopped moving while spring is deleted
        {
            Reset();
        }

        //OnTriggerEnter2D();
 
    }

    void Reset()
    {
        SceneManager.LoadScene("Main");
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() == rb2dProjectile)
        {
            Reset();
        }
    }
}
