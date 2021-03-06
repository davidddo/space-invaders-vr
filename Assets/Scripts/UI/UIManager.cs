﻿using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Class <c>UIManager</c> is used to handle the different menus and holograms.
/// </summary>
public class UIManager : MonoBehaviour
{
    #region Singelton

    public static UIManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion;

    public HologramMananger hologramManager;
    public MainMenu mainMenu;
    public PauseMenu pauseMenu;
    public GameOverMenu gameOverMenu;

    public Countdown countdown;

    /// <summary>
    /// Enables or disables the holograms.
    /// </summary>
    /// <param name="active">True if the the hud should be displayed; otherwise false.</param>
    public void ShowHolograms(bool active)
    {
        hologramManager.ToggleHologram(active);
    }

    /// <summary>
    /// Enables or disables the main menu based on the given value.
    /// </summary>
    /// <param name="active">True if the the main menu should be displayed; otherwise false.</param>
    public void ShowMainMenu(bool active) => mainMenu.gameObject.SetActive(active);

    /// <summary>
    /// Enables or disables the pause menu based on the given value.
    /// </summary>
    /// <param name="active">True if the the pause menu should be displayed; otherwise false.</param>
    public void ShowPauseMenu(bool active) => pauseMenu.gameObject.SetActive(active);

    /// <summary>
    /// Enables or disables the gameover menu based on the given value.
    /// </summary>
    /// <param name="active">True if the the gameover menu should be displayed; otherwise false.</param>
    public void ShowGameOverMenu(bool active) => gameOverMenu.gameObject.SetActive(active);

    /// <summary>
    /// Starts the countdown with the given time. After the timer ended the 
    /// callback function will get called.
    /// </summary>
    /// <param name="time">The time.</param>
    /// <param name="callback">The callback function.</param>
    public IEnumerator StartCountdown(float time, Action callback) => countdown.StartCountdown(time, callback);

    /// <summary>
    /// Stops the currently running countdown.
    /// </summary>
    public void StopCountdown() => countdown.StopCountdown();
}