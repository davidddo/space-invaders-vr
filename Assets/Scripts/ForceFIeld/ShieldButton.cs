﻿using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShieldButton : XRBaseInteractable
{
    public event Action OnButtonPress;

    float yMin = 0f;
    float yMax = 0f;
    bool previousePress = false;

    float previousHandHeight = 0f;
    XRBaseInteractor hoverInteractor;

    protected override void Awake()
    {
        base.Awake();
        onHoverEntered.AddListener(StartPress);
        onHoverExited.AddListener(EndPress);
    }

    void Start()
    {
        SetMinMax();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        onHoverEntered.RemoveListener(StartPress);
        onHoverExited.RemoveListener(EndPress);
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if (hoverInteractor)
        {
            float newHandHeight = GetLocalYPosition(hoverInteractor.transform.position);
            float handDifference = previousHandHeight - newHandHeight;
            previousHandHeight = newHandHeight;

            float newPosition = transform.localPosition.y - handDifference;
            SetYPosition(newPosition);

            bool inPosition = InPosition();
            if (inPosition && inPosition != previousePress)
            {
                OnButtonPress?.Invoke();
            }

            previousePress = inPosition;
        }
    }

    void StartPress(XRBaseInteractor interactor)
    {
        hoverInteractor = interactor;
        previousHandHeight = GetLocalYPosition(interactor.transform.position);
        Player.instance.HideHands();
    }

    void EndPress(XRBaseInteractor interactor)
    {
        hoverInteractor = null;
        previousHandHeight = 0f;
        previousePress = false;

        SetYPosition(yMax);
        Player.instance.ShowHands();
    }

    void SetMinMax()
    {
        Collider collider = GetComponent<Collider>();
        yMin = transform.localPosition.y - (collider.bounds.size.y * 0.5f);
        yMax = transform.localPosition.y;
    }

    void SetYPosition(float position)
    {
        Vector3 newPosition = transform.localPosition;
        newPosition.y = Mathf.Clamp(position, yMin, yMax);
        transform.localPosition = newPosition;
    }

    float GetLocalYPosition(Vector3 position)
    {
        Vector3 localPosition = transform.root.InverseTransformPoint(position);
        return localPosition.y;
    }

    bool InPosition()
    {
        float range = Mathf.Clamp(transform.localPosition.y, yMin, yMax + 0.1f);
        return transform.localPosition.y == range;
    }
}