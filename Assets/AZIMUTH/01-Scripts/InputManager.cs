//@ Emmanuel Garraud CopyLeft 2017
// InputManager for managing mouse inputs
// InputManager pour la gestion des inputs de la souris
//**************************************************************************************************
using UnityEngine;
using System.Collections;
// Delegate function (which allows to invoke several methods on a single event in runtime)
// Here the coordinates of the moving mouse constitute the event
// Fonction delegate (qui permet d'invoquer plusieurs méthodes sur un seul évenement en runtime)
// Ici ce sont les coordonnées de la souris qui bouge qui constitue l'event
public delegate void MouseMoved(float xMovement, float yMovement);

public class InputManager : MonoBehaviour
{

    private float _xMovement;
    private float _yMovement;
    [Header("     Exclusion Management")]
	[Space(5)]
    public bool isNotActive = false;

    //Declaration of the moving mouse event (in relation to the delegate)
    //Déclaration de l'évènement de la souris qui bouge (en relation avec le delegate)
    public static event MouseMoved MouseMoved;

    // Static function
    // Fonction statique 
    private static void OnMouseMoved(float xmovement, float ymovement)
    {
        var handler = MouseMoved;
        //If the handler is different from null, we transmit the change
        //Si l'handler est différent de null on transmet le changement
        if (handler != null) handler(xmovement, ymovement);
    }
    //Test on the update if we move the mouse while clicking on the left click
    //Teste sur l'update si on bouge la souris en cliquant sur le clic gauche
    private void InvokeActionOnInput()
    {
        //Test du clic gauche enfoncé
        if (Input.GetMouseButton(0)&&!isNotActive)
        {
            _xMovement = Input.GetAxis("Mouse X");
            _yMovement = Input.GetAxis("Mouse Y");
            OnMouseMoved(_xMovement, _yMovement);
        }
    }
   // Test of possible actions
   // Test des actions possibles
    void Update()
    {
        InvokeActionOnInput();
    }
}
//**************************************************************************************************
//@ Emmanuel Garraud CopyLeft 2017
//**************************************************************************************************