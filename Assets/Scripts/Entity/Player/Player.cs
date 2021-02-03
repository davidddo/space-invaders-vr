﻿using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    #region Singelton

    public static Player instance;

    void Awake()
    {
        instance = this;
    }

    #endregion;

    public PlayerControls controls;

    [Header("Hands")]
    public Hand leftHand;
    public Hand rightHand;

    [Header("Enemy Attack Settings")]
    public int maxSimultaneousAttackers = 1;

    public delegate void NearDeath();
    public event NearDeath OnNearDeath;

    List<int> attackers;

    protected override void Start()
    {
        base.Start();
        attackers = new List<int>();
        AudioManager.instance.PlaySound(Sound.Engine, gameObject.transform.position);
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        AudioManager.instance.StopSound(Sound.Alarm);
        ForceField.instance.IsForceFieldEnabled = false;
        GameState.instance.SetState(GameStateType.GameOver);
    }

    protected override void OnDamaged(float damage)
    {
        base.OnDamaged(damage);
        if (CurrentHealth <= 25f)
        {
            AudioManager.instance.PlaySound(Sound.Alarm, gameObject.transform.position);
            OnNearDeath?.Invoke();
        }
    }

    /// <summary>
    /// This method gets called if an enemy tries to attack the player. If the
    /// set limit of simultaneous attackers is currently not reached and is not
    /// already attacking the enemy will get the access.
    /// </summary>
    /// <param name="enemy">The enemy who made the request</param>
    public bool OnRequestAttack(int id)
    {
        if (attackers.Count < maxSimultaneousAttackers)
        {
            if (!attackers.Contains(id)) attackers.Add(id);
            return true;
        }

        return false;
    }

    /// <summary>
    /// This method gets called if the attacking enemy is either dead or is no
    /// longer attacking the player. In order to free the space for new attacking
    /// requests the given enemy will be removed from the attacker list.
    /// </summary>
    /// <param name="enemy">The enemy who attacked the player</param>
    public void OnCancelAttack(int id)
    {
        attackers.Remove(id);
    }

    public void EnableHands(bool value)
    {
        leftHand.EnableHand(value);
        rightHand.EnableHand(value);
    }

    public void Reset()
    {
        attackers = new List<int>();
        CurrentHealth = maxHealth;
    }
}
