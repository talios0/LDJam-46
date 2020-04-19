using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WallFade : MonoBehaviour
{
    public MeshRenderer[] walls;
    public float interpTime;
    public float interpTimeIn;

    private List<int> fadeOutWalls;
    private List<int> fadedOut;

    private List<int> fadeInWalls;
    private List<int> fadedIn;
    private void Start()
    {
        fadeOutWalls = new List<int>();
        fadedOut = new List<int>();
        fadeInWalls = new List<int>();
        fadedIn = new List<int>();
    }

    private void FixedUpdate()

    {

        {
            if (transform.rotation.y > -0.55 && transform.rotation.y < 0.55)
            {
                if (!fadeOutWalls.Contains(5))
                {
                    fadeOutWalls.Add(5);
                }
            }
            else if (!fadeInWalls.Contains(5)) { fadeOutWalls.Remove(5); fadedOut.Remove(5); fadeInWalls.Add(5); }

            if ((transform.eulerAngles.y < 330 && transform.eulerAngles.y > 200))
            {
                if (!fadeOutWalls.Contains(2))
                {
                    fadeOutWalls.Add(2);
                }
            }
            else if (!fadeInWalls.Contains(2)) { fadeOutWalls.Remove(2); fadedOut.Remove(2); fadeInWalls.Add(2); }

            if ((transform.eulerAngles.y < 250 && transform.eulerAngles.y > 115))
            {
                if (!fadeOutWalls.Contains(0))
                {
                    fadeOutWalls.Add(0);
                }
            }
            else if (!fadeInWalls.Contains(0)) { fadeOutWalls.Remove(0); fadedOut.Remove(0); fadeInWalls.Add(0); }

            if ((transform.eulerAngles.y < 152 && transform.eulerAngles.y > 25))
            {
                if (!fadeOutWalls.Contains(1))
                {
                    fadeOutWalls.Add(1);
                }
            }
            else if (!fadeInWalls.Contains(1)) { fadeOutWalls.Remove(1); fadedOut.Remove(1); fadeInWalls.Add(1); }

            FadeOut();
            FadeIn();
        }
    }
        private void FadeOut()
        {
            foreach (int i in fadeOutWalls)
            {
                walls[i].material.SetColor("_BaseColor", new Color(walls[i].material.GetColor("_BaseColor").r, walls[i].material.GetColor("_BaseColor").g, walls[i].material.GetColor("_BaseColor").b, Mathf.Lerp(walls[i].material.GetColor("_BaseColor").a, 0, interpTime)));
                if (walls[i].material.GetColor("_BaseColor").a < 0.25f) { walls[i].material.SetColor("_BaseColor", new Color(walls[i].material.GetColor("_BaseColor").r, walls[i].material.GetColor("_BaseColor").g, walls[i].material.GetColor("_BaseColor").b, 0)); fadedOut.Add(i); }
            }

            foreach (int i in fadedOut)
            {
                if (fadeOutWalls.Contains(i)) fadeOutWalls.Remove(i);
            }

            fadedOut.Clear();
        }

        private void FadeIn()
        {
            foreach (int i in fadeInWalls)
            {
                walls[i].material.SetColor("_BaseColor", new Color(walls[i].material.GetColor("_BaseColor").r, walls[i].material.GetColor("_BaseColor").g, walls[i].material.GetColor("_BaseColor").b, walls[i].material.GetColor("_BaseColor").a + 255 / 5));
                if (walls[i].material.GetColor("_BaseColor").a > 253f) { walls[i].material.SetColor("_BaseColor", new Color(walls[i].material.GetColor("_BaseColor").r, walls[i].material.GetColor("_BaseColor").g, walls[i].material.GetColor("_BaseColor").b, 255)); fadedIn.Add(i); }
            }

            foreach (int i in fadedIn)
            {
                if (fadeInWalls.Contains(i)) fadeInWalls.Remove(i);
            }

            fadedIn.Clear();
        }
    }
