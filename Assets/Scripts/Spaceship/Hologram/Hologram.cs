﻿using UnityEngine;

/// <summary>
/// Class <c>Hologram</c> is used to enable or disable the specific hologram.
/// </summary>
public class Hologram : MonoBehaviour
{
    public Material lightOnMaterial;
    public Material lightOffMaterial;

    public GameObject projectorLight;
    public GameObject hologram;
    public Animator animator;

    /// <summary>
    /// Enables or disables the entire game object which contains the hologram.
    /// </summary>
    public void EnableHologram(bool value)
    {
        hologram.SetActive(value);
    }

    /// <summary>
    /// EEnables or disables the hologram and starts the according animation.
    /// </summary>
    public void ToggleHologram(bool value)
    {
        projectorLight.GetComponent<MeshRenderer>().material = value ? lightOnMaterial : lightOffMaterial;
        animator.SetTrigger(value ? "On" : "Off");
    }
}
