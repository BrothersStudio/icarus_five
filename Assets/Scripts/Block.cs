﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    bool detached;
    bool spawned_new_block = false;

    public void Fall()
    {
        gameObject.AddComponent<Rigidbody>();
        if (GetComponent<BoxCollider>() != null)
        {
            GetComponent<BoxCollider>().enabled = true;
        }
        else if (GetComponent<SphereCollider>() != null)
        {
            GetComponent<SphereCollider>().enabled = true;
        }
    }

    private void Update()
    {
        // Checking to see if it's stopped moving
        if (GetComponent<Rigidbody>() != null && !GetComponentInChildren<BoxCollision>().for_show)
        {
            if (GetComponent<Rigidbody>().velocity.y > 0)
            {
                detached = true;
            }

            if (detached && !spawned_new_block && GetComponent<Rigidbody>().velocity.magnitude <= 0.1)
            {
                GameObject.Find("UFO").GetComponent<UFO>().SpawnNewBlock();
            }
        }

        if (spawned_new_block && GetComponent<Rigidbody>() != null && 
            !GetComponent<Renderer>().isVisible && GetComponent<Rigidbody>().velocity.magnitude < 0.1)
        {
            Destroy(GetComponent<Rigidbody>());
        }
    }

    public bool IsActiveBlock()
    {
        if (!spawned_new_block)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetInactiveBlock()
    {
        spawned_new_block = true;

        GetComponent<Rigidbody>().mass = 3f;
    }
}
