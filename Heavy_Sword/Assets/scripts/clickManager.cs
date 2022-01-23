using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickManager : MonoBehaviour
{
    public static bool  MouseAbove(string name)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.name == name)
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
}
