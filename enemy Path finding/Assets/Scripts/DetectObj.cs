using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObj : MonoBehaviour
{
    public string objectTagName = "";
    public bool obstruction;
    public GameObject obj;
    private Collider colnow;
    void OnTriggerStay(Collider col)
    {
        if (col != null && !col.isTrigger && col.gameObject.layer == 6) // checks if the object has the right tag
        {

            obstruction = true;
            obj = col.gameObject;
            colnow = col;
        }


        if (objectTagName == "" && !obstruction)
        {
            if (col != null && !col.isTrigger)
            {
                obstruction = true;
                colnow = col;
            }

        }



    }

    private void Update()
    {

        if (obj == null || !colnow.enabled)
        {
            obstruction = false;
        }
        if (obj != null)
        {
            if (!obj.activeInHierarchy)
            {
                obstruction = false;
            }
        }
    }







    void OnTriggerExit(Collider col)
    {
        if (col == colnow)
        {
            obstruction = false;
        }

    }

}
