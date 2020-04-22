using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Top")
        {
            WallFade.collideTop = true;
        }
        else if (other.name == "Bottom")
        {
            WallFade.collideBottom = true;
        }
        else if (other.name == "Back") WallFade.collideBack = true;
        else if (other.name == "Front") WallFade.collideFront = true;
        else if (other.name == "Left") WallFade.collideLeft = true;
        else if (other.name == "Right") WallFade.collideRight = true;


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Top")
        {
            WallFade.collideTop = false;
        }
        else if (other.name == "Bottom")
        {
            WallFade.collideBottom = false;
        }
        else if (other.name == "Back") WallFade.collideBack = false;
        else if (other.name == "Front") WallFade.collideFront = false;
        else if (other.name == "Left") WallFade.collideLeft = false;
        else if (other.name == "Right") WallFade.collideRight = false;
    }

}
