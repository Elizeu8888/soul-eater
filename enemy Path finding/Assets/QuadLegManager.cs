using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Enemy
{


    public class QuadLegManager : MonoBehaviour
    {
        public Transform frontRightRef, frontLeftRef, backRightRef, backLeftRef;
        public Transform frontRight, frontLeft, backRight, backLeft;
        float timer_FR, timer_FL, timer_BR, timer_BL;

        public float stepSpacing = 1f;
        public float minimumTimeBetweenSteps = 1f;
        float timerForStep = 1f;

        public LayerMask whatCanStand;
        public float length;
        public float radiusForRay;
        public float heightAdjustment = 15f;


        private RaycastHit legHitFR;
        private RaycastHit legHitFL;
        private RaycastHit legHitBR;
        private RaycastHit legHitBL;

        EnemyMovement plyScript;

        Vector3 footrefForGiz;

        void Start()
        {
            plyScript = gameObject.GetComponent<EnemyMovement>();
        }

        void Update()
        {
            timer_BL -= Time.deltaTime;
            timer_BR -= Time.deltaTime;
            timer_FL -= Time.deltaTime;
            timer_FR -= Time.deltaTime;


        }

        void FixedUpdate()
        {
            FootSetter();

            float ymedium = (frontRight.position.y + frontLeft.position.y + backLeft.position.y + backRight.position.y) / 4;

            //float rotMedium = (legHitBL.normal.y + legHitBR.normal.y + legHitFR.normal.y + legHitFL.normal.y) / 4;

            var y = frontRight.position - backRight.position + frontLeft.position - backLeft.position;
            var x = frontRight.position - frontLeft.position + backRight.position - backLeft.position;


            var z = Vector3.Cross(y, x);


            var rotation = Quaternion.LookRotation(-y, z);
            rotation.z = 0f;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 1f);





            if (transform.position.y >= ymedium + 1.1f)
            {
                plyScript.m_Rigidbody.AddForce(-transform.up * heightAdjustment, ForceMode.Impulse);
            }
            else if (transform.position.y <= ymedium + 1.1f)
            {
                plyScript.m_Rigidbody.AddForce(transform.up * heightAdjustment, ForceMode.Impulse);
            }
            

        }


        public void FootSetter()
        {

            float distance_FR = Vector3.Distance(frontRightRef.position, frontRight.position);

            float distance_FL = Vector3.Distance(frontLeftRef.position, frontLeft.position);

            float distance_BR = Vector3.Distance(backRightRef.position, backRight.position);

            float distance_BL = Vector3.Distance(backLeftRef.position, backLeft.position);

            if (FootRaycastBL(length, backLeftRef))
                MovingFoot(distance_BL, stepSpacing, backLeft, backLeftRef, timer_BL, legHitBL);
            if (FootRaycastBR(length, backRightRef))
                MovingFoot(distance_BR, stepSpacing, backRight, backRightRef, timer_BR, legHitBR);
            if (FootRaycastFL(length, frontLeftRef))
                MovingFoot(distance_FL, stepSpacing, frontLeft, frontLeftRef, timer_FL, legHitFL);
            if (FootRaycastFR(length, frontRightRef))
                MovingFoot(distance_FR, stepSpacing, frontRight, frontRightRef, timer_FR, legHitFR);






        }

        void MovingFoot(float distance, float stepSpace, Transform foot, Transform footRef, float timerforstep, RaycastHit legHit)
        {

            
                if (distance >= stepSpace && timerforstep <= 0)
                {
                    foot.position = Vector3.MoveTowards(foot.position, legHit.point, 3f);
                    timerforstep = minimumTimeBetweenSteps;
                }
            

        }

        bool FootRaycastFR(float distanceCheck, Transform footRef)
        {
            footrefForGiz = footRef.position;

            if (Physics.SphereCast(footRef.position, radiusForRay, footRef.forward, out legHitFR, distanceCheck, whatCanStand))
            {
                Debug.DrawRay(footRef.position, footRef.forward * length, Color.red);

                return true;
            }
            else
                return false;
        }
        bool FootRaycastFL(float distanceCheck, Transform footRef)
        {
            footrefForGiz = footRef.position;

            if (Physics.SphereCast(footRef.position, radiusForRay, footRef.forward, out legHitFL, distanceCheck, whatCanStand))
            {
                Debug.DrawRay(footRef.position, footRef.forward * length, Color.red);

                return true;
            }
            else
                return false;
        }
        bool FootRaycastBR(float distanceCheck, Transform footRef)
        {
            footrefForGiz = footRef.position;

            if (Physics.SphereCast(footRef.position, radiusForRay, footRef.forward, out legHitBR, distanceCheck, whatCanStand))
            {
                Debug.DrawRay(footRef.position, footRef.forward * length, Color.red);

                return true;
            }
            else
                return false;
        }
        bool FootRaycastBL(float distanceCheck, Transform footRef)
        {
            footrefForGiz = footRef.position;

            if (Physics.SphereCast(footRef.position, radiusForRay, footRef.forward, out legHitBL, distanceCheck, whatCanStand))
            {
                Debug.DrawRay(footRef.position, footRef.forward * length, Color.red);

                return true;
            }
            else
                return false;
        }




        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Debug.DrawLine(footrefForGiz, footrefForGiz - transform.up * legHitBL.distance);
            Gizmos.DrawWireSphere(footrefForGiz - transform.up * legHitBL.distance, radiusForRay);
        }


    }

}

