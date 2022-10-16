using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDragging : MonoBehaviour
{

    public LineRenderer catapultLineFront;
    public LineRenderer catapultLineBack;

    private Ray leftCatapultToProjectileRay;
    private SpringJoint2D spring;
    private Transform rock;
    private float circleRadius;
    private bool clickedOn;
    private Rigidbody2D rb2d;
    private CircleCollider2D circle;
    private Vector2 prevVelocity;
    private float originCatapultX = -5.43f;
    private float originCatapultY = -2.37f;

    private Vector3 catapultRearAttach = new Vector3(-5.43f,-1.6f,0);
    private Vector3 catapultFrontAttach = new Vector3(-5.88f,-1.69f,0);

    void Awake() // stuff that need to be done before start function
    {
        spring = GetComponent<SpringJoint2D>();
        rb2d = GetComponent<Rigidbody2D>();
        rock =  GetComponent<Transform>();
        circle = GetComponent<CircleCollider2D>();
        catapultLineFront.widthMultiplier = 0.5f;
        catapultLineBack.widthMultiplier = 0.3f;
    }

    // Start is called before the first frame update
    void Start()
    {
        LineRendererSetup();
        leftCatapultToProjectileRay = new Ray(catapultFrontAttach, Vector3.zero);
        circleRadius = circle.radius/3.85f;
    }

    // Update is called once per frame
    void Update()
    {
        // LineRenderUpdate();
        if (clickedOn==true)
        {
            Dragging();
        }

        if(spring != null)
        {
            LineRenderUpdate();
        }
        else //otherwise the catapult lines are disabled
        {
            catapultLineFront.enabled = false;
            catapultLineBack.enabled = false;
            Destroy(spring);
        }

    }

    private void OnMouseDown()
    {
        spring.enabled = false;
        clickedOn = true;
    }
    private void OnMouseUp()
    {
        spring.enabled = true;
        rb2d.isKinematic = false;
        clickedOn = false;
    }

    void Dragging()
    {
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 catapultToMouse = mouseWorldPoint - new Vector3(originCatapultX, originCatapultY, 0);
        mouseWorldPoint.z = 0f;
        transform.position = mouseWorldPoint;

    }

    void LineRendererSetup()
    {
        
        catapultLineFront.SetPosition(0, catapultFrontAttach);
        catapultLineBack.SetPosition(0, catapultRearAttach);
        
        catapultLineFront.sortingLayerName = "Foreground";
        catapultLineBack.sortingLayerName = "Foreground";

        catapultLineFront.sortingOrder = 2;
        catapultLineBack.sortingOrder = 1;
    }

    void LineRenderUpdate()
    {
        Vector2 catapultToProjectile = transform.position - catapultFrontAttach;
        leftCatapultToProjectileRay.direction = catapultToProjectile;
        Vector3 holdPoint = leftCatapultToProjectileRay.GetPoint(catapultToProjectile.magnitude + circleRadius);
        catapultLineFront.SetPosition(1, holdPoint);
        catapultLineBack.SetPosition(1, holdPoint);
    }
}
