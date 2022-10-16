using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFollow : MonoBehaviour
{
    public Transform projectile;
    public Transform farLeft;
    public Transform farRight;


    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = projectile.position.x;
        newPosition.x = Mathf.Clamp(newPosition.x, farLeft.position.x, farRight.position.x); //newposition.x can be any value between farleftx and farright x
        transform.position = newPosition;    
    }


}
