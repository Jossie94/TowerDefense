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

    [Header("Building")]

    private GameObject turret;

    public Vector3 positionOffset;

    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        buildManager = BuildManager.instance;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (buildManager.GetTurretToBuild() == null)
            return;


        if (turret != null)
        {
            Debug.Log("Can't build there!");
                return;
        }

        GameObject turretToBuild = buildManager.GetTurretToBuild();
       turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);

        //Build a tuuret
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (buildManager.GetTurretToBuild() == null)
            return;

        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
