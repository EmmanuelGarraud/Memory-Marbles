// Marble generator script
// Emmanuel Garraud 2022
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MarbleManager : MonoBehaviour
{
    [SerializeField] GameObject marblePrefab;
    [SerializeField] GameObject startPosition;
    [SerializeField] MarbleDatasManager MDM;
    [SerializeField] Button btReinject;
    [SerializeField] TMP_Dropdown dropMarbleList;
    [SerializeField] TMP_Text nbMarbles;

    private Vector3 spawnPosition;
    private int marbleIndex;
    private int oldMarbleIndex;
    private string marbleNameTemp;
    private int marbleCount;
    private List<string> m_DropOptions = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        // Gravity initialization to zero
        Physics.gravity = new Vector3(0, 0, 0);
        // Spawn position assignment
        spawnPosition = startPosition.transform.position;
        // Call function to Reset list of marble objects
        MDM.ResetMarbleObject();
        dropMarbleList.ClearOptions();
        marbleNameTemp = "";
        marbleCount = 0;
        m_DropOptions = new List<string>();
        btReinject.interactable = false;
        dropMarbleList.interactable = false;
        marbleIndex = -1;
        nbMarbles.text = "0";
    }
    // Creation of a new marble
    public void NewMarble()
    {
        // Rotation 
        Quaternion rotation = Random.rotation;
        // Scale
        float scale = ScaleRandom();
        Vector3 newScale = new Vector3(scale,scale,scale);
        // Torque
        float torkeX = TorkeRandom();
        float torkeY = TorkeRandom();
        float torkeZ = TorkeRandom();
        // Velocity power
        float power = PowerRandom();

        GameObject marble = Instantiate(marblePrefab, spawnPosition, rotation);

        //Assign unique material
        Renderer rend = marble.GetComponent<Renderer> ();
        rend.material = new Material(MDM.matMarbleBase);
        // Assign scale
        marble.transform.localScale = newScale;
        // Rescale light Halo
        MarbleBehaviour marbleBe = marble.GetComponent<MarbleBehaviour>();
        marbleBe.lightMarble.range = scale+(scale*10f/20f);
        // Assign the light decay duration
        marbleBe.lightOffDelay = MDM.lightDuration;
        // Assign the starting & ending light intensity
        marbleBe.startLigthIntensity = MDM.startingLightIntensity;
        marbleBe.endLigthIntensity = MDM.endingLightIntensity;
        // Assign the emission color & starting & ending values of emission color intensity
        marbleBe.emissionColorValue = MDM.valueEmissionColor;
        marbleBe.startEmissionColorIntensity = MDM.startingEmissionColorIntensity;
        marbleBe.endEmissionColorIntensity = MDM.endingEmissionColorIntensity;
        // Assign torque
        Rigidbody rb = marble.GetComponent(typeof(Rigidbody)) as Rigidbody;
        rb.AddTorque(torkeX, torkeY, torkeZ);
        // Add velocity power
        rb.velocity = startPosition.transform.TransformDirection(MDM.powerDirection)*power;
        // Add created gameObject to MarbleDatasManager
        MDM.marbleObject.Add(marble);
        marbleCount++;
        nbMarbles.text = marbleCount.ToString();
        if (marbleCount == 1) 
        {
            dropMarbleList.interactable = true;
            m_DropOptions.Add("No Select");
        }
        marbleNameTemp = "M "+marbleCount.ToString();
        m_DropOptions.Add(marbleNameTemp);
        AddItemsInDropDown();
        DeselectMarbleObject();

    }

    // Reactive the light on defined marble object
    public void LightReinjection()
    {
        marbleIndex = dropMarbleList.value-1;
        MDM.MarbleReinjection(marbleIndex);
        DeselectMarbleObject();
    }

    // Select or deselect a marble object
    public void SelectMarble()
    {
        if (dropMarbleList.value < 1)
        {
            btReinject.interactable = false;
            DeselectMarbleObject();
        }
        else
        {
            if (marbleIndex > -1)
            {
                MDM.MarbleSelection(marbleIndex,false);
            }
            marbleIndex = dropMarbleList.value-1;
            btReinject.interactable = true;
            MDM.MarbleSelection(marbleIndex,true);
        }
    }

    // Internal functions
    private float ScaleRandom()
    {
        return Random.Range(MDM.minimumSize,MDM.maximumSize);
    }
    private float TorkeRandom()
    {
        return Random.Range(-1.0f, 1.0f);
    }
    private float PowerRandom()
    {
        return Random.Range(MDM.minimumPower, MDM.maximumPower);
    }
    private void AddItemsInDropDown()
    {
        dropMarbleList.ClearOptions();
        dropMarbleList.AddOptions(m_DropOptions);
    }
    private void DeselectMarbleObject()
    {
        if (marbleIndex > -1)
        {
            MDM.MarbleSelection(marbleIndex,false);
            marbleIndex = -1;
            dropMarbleList.value = 0;
        }
    }
}
