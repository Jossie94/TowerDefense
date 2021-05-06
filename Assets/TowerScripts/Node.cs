using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    [Header("Color")]

    public Color hoverColor;

    private Renderer rend;

    private Color startColor;

    public Color notEnoughMoneyColor;

    [Header("Building")]

    public GameObject turret;

    public Vector3 positionOffset;

    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition ()
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;


        if (turret != null)
        {
            Debug.Log("Can't build there!");
                return;
        }

        buildManager.BuildTurretOn(this);

        //Build a tuuret
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }

        
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
