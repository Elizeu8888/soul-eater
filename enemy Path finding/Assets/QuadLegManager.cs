using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadLegManager : MonoBehaviour
{
    public Transform frontRightRef, frontLeftRef, backRightRef, backLeftRef;
    public Transform frontRight, frontLeft, backRight, backLeft;
    float timer_FR, timer_FL, timer_BR, timer_BL;

    public float stepSpacing;
    public float minimumTimeBetweenSteps = 1f;
    float timerForStep = 1f;

    void Update()
    {
        timerForStep -= Time.deltaTime;       
    }

    void FixedUpdate()
    {
        FootSetter();
    }


    public void FootSetter()
    {

        float distance_FR = Vector3.Distance(frontRightRef.position, frontRight.position);

        float distance_FL = Vector3.Distance(frontLeftRef.position, frontLeft.position);

        float distance_BR = Vector3.Distance(backRightRef.position, backRight.position);

        float distance_BL = Vector3.Distance(backLeftRef.position, backLeft.position);


        MovingFoot(distance_BL, stepSpacing, backLeft, backLeftRef,timer_BL);
        MovingFoot(distance_BR, stepSpacing, backRight, backRightRef,timer_BR);
        MovingFoot(distance_FL, stepSpacing, frontLeft, frontLeftRef,timer_FL);
        MovingFoot(distance_FR, stepSpacing, frontRight, frontRightRef,timer_FR);
        


    }

    void MovingFoot(float distance, float stepSpace, Transform foot, Transform footRef, float timerforstep)
    {
        if (distance >= stepSpace && timerforstep <= 0)
        {
            //foot.position = footRef.position;
            foot.position = Vector3.MoveTowards(foot.position, footRef.position, 3f) ;
            timerforstep = minimumTimeBetweenSteps;
        }
    }


}
