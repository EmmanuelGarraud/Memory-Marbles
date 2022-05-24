 //@ Emmanuel Garraud CopyLeft 2017
// ExclusionManager to manage exclusion on UI interface areas: To be adapted as needed
// ExclusionManager pour gérer l'exclusion sur des zone d'interface UI : A adapter selon besoins
//**************************************************************************************************
 using UnityEngine;
 using UnityEngine.EventSystems; 

public class ExclusionManager : MonoBehaviour
, IPointerClickHandler
     , IDragHandler
     , IPointerEnterHandler
     , IPointerExitHandler
{
    public InputManager inputManager;
    public CameraOrbitController COC;   // To be activated or deactivated according to use
    // public ObjetOrbitController OOC;  // To be activated or deactivated according to use
    private SpriteRenderer sprite;

    void Awake()
     {
        sprite = GetComponent<SpriteRenderer>();
     }

    public void OnPointerClick(PointerEventData eventData) // 3
     {
        // Debug.Log("I was clicked");
        // if clic
     }
    public void OnDrag(PointerEventData eventData)
     {
        // Debug.Log("I'm being dragged!");
        // if drag
     }
     public void OnPointerEnter(PointerEventData eventData)
     {
        // if enter
        //Debug.Log("Enter sprite !");
        inputManager.isNotActive = true;
        COC.isNotActive = true;
        // OOC.isNotActive = true;
     }
 
     public void OnPointerExit(PointerEventData eventData)
     {
        // if exit
        //Debug.Log("Exit sprite !");
        inputManager.isNotActive = false;
        COC.isNotActive = false;
        // OOC.isNotActive = false;
     }
    
}
