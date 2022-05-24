// Marble behavior for each entity
// Emmanuel Garraud 2022
using UnityEngine;

public class MarbleBehaviour : MonoBehaviour
{
    public Light lightMarble;
    public GameObject selection3D;
    public float lightOffDelay = 30.0f;
    // Material defined for each Marble object
    public Material matMarble;
    // Boolean indicative of light activity
    public bool isLightActive;
    // Marble light effect settings
    public float startLigthIntensity = 2f;
    public float endLigthIntensity = 0f;
    public float startEmissionColorIntensity = 2f;
    public float endEmissionColorIntensity = -1f;
    public Color emissionColorValue = Color.white; 
    // Local variables
    private float timeStartedLightOff;
    private float currentEmissionColorIntensity;
    
    // Start is called before the first frame update
    void Start()
    {
        Renderer rend = this.GetComponent<Renderer> ();
        matMarble = rend.material;
        SelectionMarble(false);
        StartLightingOff();
    }
    // Initialization to start the animation from light to dark
    public void StartLightingOff()
    {
        isLightActive = true;
        timeStartedLightOff = Time.time; 
        lightMarble.intensity = startLigthIntensity;
        matMarble.SetVector("_EmissionColor", emissionColorValue * startEmissionColorIntensity);
    }
    // Marble is selected or not selected
    public void SelectionMarble(bool IsSelect)
    {
        selection3D.SetActive(IsSelect);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        // As long as isLightActive is true, the animation towards darkness continues
        if(isLightActive)
        {
            float timeSinceStarted = Time.time - timeStartedLightOff;
            float percentageComplete = timeSinceStarted / lightOffDelay;

            lightMarble.intensity = Mathf.Lerp(startLigthIntensity, endLigthIntensity, percentageComplete);

            currentEmissionColorIntensity = Mathf.Lerp(startEmissionColorIntensity, endEmissionColorIntensity, percentageComplete);

            matMarble.SetVector("_EmissionColor", emissionColorValue * currentEmissionColorIntensity);

            if(percentageComplete >= 1.0f)
            {
                isLightActive = false;
            }
        }
    }
}
