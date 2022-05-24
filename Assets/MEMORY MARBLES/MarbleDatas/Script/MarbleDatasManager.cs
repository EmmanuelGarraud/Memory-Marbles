// Scriptable Object Marble Datas Manager
// Emmanuel Garraud 2022
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "MarbleDatasManager", menuName = "Marble/MarbleDatasManager")]
public class MarbleDatasManager : ScriptableObject
{
    // Defined data values    
    [Space(5, order=0)]
    [Header("   Base material", order=1)]
	[Space(5, order=2)]
    public Material matMarbleBase;
    [Space(5, order=0)]
    [Header("   Duration of light decay", order=1)]
	[Space(5, order=2)]
    public float lightDuration = 30f;
    [Space(5, order=0)]
    [Header("   Reinjection at any time (even if the light is still active)", order=1)]
	[Space(5, order=2)]
    public bool isAnyTimeReinject = true;
    [Space(5, order=0)]
    [Header("   Starting & end values of light intensity", order=1)]
	[Space(5, order=2)]
    public float startingLightIntensity = 2f;
    public float endingLightIntensity = 0f;
    [Space(5, order=0)]
    [Space(5, order=0)]
    [Header("   Value of emission color", order=1)]
	[Space(5, order=2)]
    public Color valueEmissionColor = Color.white; 
    [Header("   Starting & end values of emission color intensity", order=1)]
	[Space(5, order=2)]
    public float startingEmissionColorIntensity = 2f;
    public float endingEmissionColorIntensity = -1f;
    [Space(5, order=0)]
    [Header("   Variations in the size of the spheres", order=1)]
	[Space(5, order=2)]
    public float minimumSize = 0.05f;
    public float maximumSize = 0.25f;
    [Space(5, order=0)]
    [Header("   Ejection power variations", order=1)]
	[Space(5, order=2)]
    public float minimumPower = 0.01f;
    public float maximumPower = 0.05f;
    [Space(5, order=0)]
    [Header("   Direction of ejection", order=1)]
	[Space(5, order=2)]
    public Vector3 powerDirection = Vector3.up;
    // List of marble objects created
    [Space(5, order=0)]
    [Header("   List of Marble spheres created", order=1)]
	[Space(5, order=2)]
    public  List<GameObject> marbleObject = new List<GameObject>();
    //
    private MarbleBehaviour marbleBe;

    // Reset list of marble objects
    public void ResetMarbleObject()
    {
        marbleObject = new List<GameObject>();
    }
    // Re-inject starting light on a marble object
    public void MarbleReinjection(int index)
    {
        marbleBe = marbleObject[index].GetComponent<MarbleBehaviour>(); 
        if (isAnyTimeReinject)
        {
            marbleBe.StartLightingOff();
        }
        else
        {
            if (!marbleBe.isLightActive)
            {
                marbleBe.StartLightingOff();
            }
            // else alert management if the light is active
        }
    }
    // Enable and disable the selection of a marble object
    public void MarbleSelection(int index,bool boolVar)
    {
        marbleBe = marbleObject[index].GetComponent<MarbleBehaviour>(); 
        marbleBe.SelectionMarble(boolVar);
    }
}
