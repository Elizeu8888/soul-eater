using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {

        public StateMachine sm;

        public IdleState idleState;
        public WalkingState walkingState;

        public DetectObj checkForRotation;
        public DetectObj checkForLeft;
        public DetectObj checkForRight;


        public float dirNum;

        public Transform checkPointer;
        
        public Rigidbody m_Rigidbody;


        public Collider colli;
        public Animator anim;
        public float m_MovementSpeed;
        public float m_TopSpeed;
        public Transform[] movementPoints;
        public GameObject navTracker;


        NavMeshAgent navAgent;
        RaycastHit hit;
        public LayerMask groundLayers;
        Vector3 lineTowardsNav;
        bool canMove;

        public float rate;


        void Start()
        {
            sm = gameObject.AddComponent<StateMachine>();
            navAgent = navTracker.GetComponent<NavMeshAgent>();
            m_Rigidbody = gameObject.GetComponent<Rigidbody>();


            idleState = new IdleState(this, sm);
            walkingState = new WalkingState(this, sm);

            sm.Init(idleState);
        }

        void Update()
        {
            sm.CurrentState.LogicUpdate();


            Vector3 heading = navTracker.transform.position - transform.position;
            dirNum = AngleDir(transform.forward, heading, transform.up);
        }

        void FixedUpdate()
        {

            sm.CurrentState.PhysicsUpdate();
            if (m_Rigidbody.velocity.magnitude >= m_TopSpeed)
            {
                Vector3 endVelocity = m_Rigidbody.velocity;
                endVelocity.z *= rate;
                endVelocity.x *= rate;
                m_Rigidbody.velocity = endVelocity;
            }
        }

        public bool RayCastForNav()
        {
            RaycastHit[] cols = Physics.RaycastAll(checkPointer.position, navTracker.transform.position - transform.position, 75f );

            float minDist = 16f;
            float dist = Vector3.Distance(navTracker.transform.position, transform.position);


            foreach (RaycastHit c in cols)
            {
                Debug.DrawRay(checkPointer.position, navTracker.transform.position - transform.position, Color.green);

                if (c.transform.gameObject.tag == "ground")
                {
                    
                    return false;
                }
                else if(c.transform.gameObject.tag != "ground")
                {
                    return true;
                }

            }
            return true;


        }

        public void PlayAnim(string animName)
        {
            anim.Play(animName);
        }

        public void SetNavAgent(int num)
        {

            navAgent.SetDestination(movementPoints[num].position);

        }

        public void MoveRBtoNav()
        {
            lineTowardsNav = navTracker.transform.position - transform.position;

            m_Rigidbody.AddForce(transform.forward * m_MovementSpeed * Time.deltaTime, ForceMode.Impulse);
            

        }

        public bool CheckForGhost()
        {

            Collider[] cols = Physics.OverlapBox(colli.bounds.center, colli.bounds.extents, colli.transform.rotation);

            foreach (Collider c in cols)
            {

                if (c.transform == navTracker.transform)
                {

                    return true;
                }
                else
                {
                    return false;
                }

            }

            return false;
        }

        public void RotatingEnemy()
        {

            if(checkForRotation.obstruction == true && AngleDir(Vector3.forward, navTracker.transform.position - transform.position, Vector3.up) == -1f)
            {

                switch (checkForRight.obstruction)
                { 
                    case false:
                        transform.Rotate(Vector3.up * Time.deltaTime * 250f);
                        break;
                    case true:
                        transform.Rotate(Vector3.up * Time.deltaTime * -250f);
                        break;                     

                }

            }
            else if (checkForRotation.obstruction == true && AngleDir(Vector3.forward, navTracker.transform.position - transform.position, Vector3.up) == 1f)
            {
                switch (checkForLeft.obstruction)
                {
                    case false:
                        transform.Rotate(Vector3.up * Time.deltaTime * -250f);
                        break;
                    case true:
                        transform.Rotate(Vector3.up * Time.deltaTime * 250f);
                        break;

                }

            }
            else if(sm.CurrentState != idleState)
            {
                var lookPos = navTracker.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 4f);
            }

        }

        float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
        {
            Vector3 perp = Vector3.Cross(fwd, targetDir);
            float dir = Vector3.Dot(perp, up);

            if (dir > 0f)
            {
                return 1f;
            }
            else if (dir < 0f)
            {
                return -1f;
            }
            else
            {
                return 0f;
            }
        }


    }
}    
