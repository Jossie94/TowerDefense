using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Node : MonoBehaviour
{
    [Header("Color")]

    public Color hoverColor;

    private Renderer rend;

    private Color startColor;

    [Header("Building")]

    private GameObject turret;

    public Vector3 positionOffset;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    void OnMouseDown()
    {
        if (turret != null)
        {
            Debug.Log("Can't build there!");
                return;
        }

        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
       turret = (GameObject)Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);

        //Build a tuuret
    }

    void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
