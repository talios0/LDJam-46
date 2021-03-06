﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WallFade : MonoBehaviour
{
    public MeshRenderer[] walls;
    public float interpTime;
    public float interpTimeIn;

    public Material transparentMat;
    public Material opaqueMat;

    private CameraZoom zoom;

    public static bool collideTop;
    public static bool collideBottom;
    public static bool collideLeft;
    public static bool collideRight;
    public static bool collideBack;
    public static bool collideFront;

    private void Awake()
    {
        zoom = Camera.main.GetComponent<CameraZoom>();
    }

    private void FixedUpdate()
    {
        if (!zoom.GetZoomDisable())
        {
            if (collideLeft) FadeOut(2);
            else FadeIn(2);

            if (collideRight) FadeOut(1);
            else FadeIn(1);

            if (collideBack) FadeOut(0);
            else FadeIn(0);

            if (collideFront) FadeOut(5);
            else FadeIn(5);
            /*
            if (transform.rotation.y > -0.55 && transform.rotation.y < 0.55)
            {
                FadeOut(5);
            }
            else FadeIn(5);

            if (transform.eulerAngles.y < 330 && transform.eulerAngles.y > 200)
            {
                FadeOut(2);
            }
            else FadeIn(2);

            if (transform.eulerAngles.y < 250 && transform.eulerAngles.y > 115)
            {
                FadeOut(0);
            }
            else FadeIn(0);

            if ((transform.eulerAngles.y < 152 && transform.eulerAngles.y > 25))
            {
                FadeOut(1);
            }
            else FadeIn(1);
            */
            //Debug.Log(;
            if (zoom.GetTopDisable())
            {
                FadeOut(3);
                FadeOut(4);
            } else
            {
                if (collideTop) FadeOut(3);
                else FadeIn(3);
                if (collideBottom) FadeOut(4);
                else FadeIn(4);
            }
        }
        else
        {
            for (int i = 0; i < walls.Length; i++)
            {
                FadeOut(i);
            }
        }

    }
    private void FadeOut(int index)
    {
        walls[index].GetComponent<MeshRenderer>().enabled = false;
        /*
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
        */
    }

    private void FadeIn(int index)
    {
        walls[index].GetComponent<MeshRenderer>().enabled = true;
        /*
        foreach (int i in fadeInWalls)
        {
            walls[i].material.SetColor("_BaseColor", new Color(walls[i].material.GetColor("_BaseColor").r, walls[i].material.GetColor("_BaseColor").g, walls[i].material.GetColor("_BaseColor").b, walls[i].material.GetColor("_BaseColor").a + 255 / 5));
            if (walls[i].material.GetColor("_BaseColor").a > 253f)
            {
                walls[i].material.SetColor("_BaseColor", new Color(walls[i].material.GetColor("_BaseColor").r, walls[i].material.GetColor("_BaseColor").g, walls[i].material.GetColor("_BaseColor").b, 255));
                fadedIn.Add(i);
                Material material = walls[i].material;
                material.SetFloat("_Surface", 0);
                material.SetInt("_SrcBlend", 1);
                material.SetInt("_DstBlend", 0);
                material.SetInt("_ZWrite", 1);
                walls[i].material = material;
            }
        }
        foreach (int i in fadedIn)
        {
            if (fadeInWalls.Contains(i)) fadeInWalls.Remove(i);
        }
        fadedIn.Clear();
        */
    }
}