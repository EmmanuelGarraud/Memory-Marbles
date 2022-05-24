//@ Emmanuel Garraud CopyLeft 2017
// Management of the movement of the camera (in reality of the parent of the camera CamRotation)
// Gestion du mouvement de la camera (en réalité du parent de la caméra CamRotation)
//*********************************************************************************************************************************
using UnityEngine;
using System.Collections;

public class CameraOrbitController : MonoBehaviour {

	//****************************************************
	// Declaration/initialization of public variables
    // Déclaration/initialisation des variables publiques
	//****************************************************
	//[Header("     Smooth effect")]
	//[Space(5)]
	[Space(5, order=0)]
    [Header("     Smooth effect", order=1)]
	[Space(5, order=2)]
    [SerializeField, Range(0.0f, 1.0f)]
    public float _leSmooth = 0.05f;
	[Space(5, order=0)]
	[Header("     Center displacement", order=1)]
	[Space(5, order=2)]
	public Transform _centre;
	public bool _deplacementActif = true;
	[SerializeField, Range(0.0f, 10.0f)]
	public float _vitesse = 0.5f;
	[Space(5, order=0)]
	[Header("     Camera Zoom", order=1)]
	[Space(5, order=2)]
	public Camera _camera = null;
	[SerializeField, Range(1.0f, 179.0f)]
	public float _laFocale = 50.0f;
	public float zoomSpeed = 5.0f;
	public float zoomSensivity = 15.0f;
	public float zoomMin = 5.0f;
	public float zoomMax = 80.0f;
	[Space(5, order=0)]
	[Header("     Cursor management", order=1)]
	[Space(5, order=2)]
	public bool _curseurInvisible = true;
	public bool _curseurAuCentre = false;
	[Space(5, order=0)]
	[Header("     Exclusion Management", order=1)]
	[Space(5, order=2)]
	public bool isNotActive = false;
	//****************************************************
	// Declaration/initialization of private variables
	// Déclaration/initialisation des variables privées
	//****************************************************
	private float _xRotation;
	private float _yYRotation;
	private float _leZoom;
	private bool _deplaceHaut = false;
	private bool _deplaceBas = false;
	private bool _deplaceDroite = false;
	private bool _deplaceGauche = false;
	private bool _deplaceAvant = false;
	private bool _deplaceArriere = false;

	//******************************************************************************************************************************
	// Function of rotation of the parent of the camera according to the event of the mouse
    // Fonction de rotation du parent de la caméra en fonction de l'event de la souris
	//******************************************************************************************************************************
    private void Rotate(float xMovement, float yMovement)
    {
        _xRotation += xMovement;
        _yYRotation += yMovement;
    }
	//******************************************************************************************************************************
	// INITIALIZATION
    // INITIALISATION
	//******************************************************************************************************************************
	void Start ()
	{
		Cursor.visible = true;
        InputManager.MouseMoved += Rotate;
		_camera.fieldOfView = _laFocale;
		_leZoom = _laFocale;
	}
	//******************************************************************************************************************************
	// UPDATE
	// ACTUALISATION
	//******************************************************************************************************************************
	void Update ()
	{
		//******************************************************************************************************************************
		// Linear interpolations of the rotations according to the applied smooth
        // Interpolations linéaires des rotations en fonction du smooth appliqué
		//******************************************************************************************************************************
        _xRotation = Mathf.Lerp(_xRotation, 0, _leSmooth);
        _yYRotation = Mathf.Lerp(_yYRotation, 0, _leSmooth);
		// Degree rotation transformation applied to the GameObject (CamRotation)
        // Transformation de rotation en degrés appliquée au GameObject (CamRotation)
        transform.eulerAngles += new Vector3(0, _xRotation, -_yYRotation);
		//******************************************************************************************************************************
		// MANAGEMENT OF THE CURSOR ON THE LEFT CLICK
		// GESTION DU CURSEUR SUR LE CLIC GAUCHE
		//******************************************************************************************************************************
		// LEFT CLICK HOLD
		// CLIC GAUCHE ENFONCE
		if (Input.GetMouseButtonDown(0)&&!isNotActive)
		{
			if (_curseurAuCentre)
			{
				Cursor.lockState = CursorLockMode.Locked;  // cursor stuck in the center of the window // curseur bloqué au centre de la fenêtre
			}
			else {
				Cursor.lockState = CursorLockMode.Confined; // cursor confined to the window // curseur confiné dans la fenêtre
			}
			if (_curseurInvisible) Cursor.visible = false;
		}
		// LEFT CLICK UP
		// CLIC GAUCHE RELEVE
		if (Input.GetMouseButtonUp(0)&&!isNotActive)
		{
			if (_curseurInvisible) Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None; // free cursor // curseur libre
		}
		//******************************************************************************************************************************
		// MOUSE WHEEL MANAGEMENT FOR ZOOM
		// GESTION MOLETTE SOURIS POUR LE ZOOM
		//******************************************************************************************************************************
		if (Input.GetAxis("Mouse ScrollWheel") != 0f && !isNotActive) // forward
		{
			_leZoom -= Input.GetAxis ("Mouse ScrollWheel") * zoomSensivity;

			_leZoom = Mathf.Clamp(_leZoom, zoomMin, zoomMax);
		}
		//******************************************************************************************************************************
		// MANAGEMENT OF KEYBOARD KEYS - UP - DOWN - RIGHT - LEFT - FORWARD - BACKWARD
		// GESTION DES TOUCHES CLAVIER - HAUT - BAS - DROITE - GAUCHE - AVANT - ARRIERE
		//******************************************************************************************************************************
		if (_deplacementActif&&!isNotActive){
			// Gestion touche PageUp - déplacement vers le haut
			if (Input.GetKeyDown(KeyCode.PageUp)) _deplaceHaut = true;
			if (Input.GetKeyUp(KeyCode.PageUp)) _deplaceHaut = false;
			// Gestion touche PageDown - déplacement vers le bas
			if (Input.GetKeyDown(KeyCode.PageDown)) _deplaceBas = true;
			if (Input.GetKeyUp(KeyCode.PageDown)) _deplaceBas = false;
			// Gestion touche Flèche gauche - déplacement vers la gauche
			if (Input.GetKeyDown(KeyCode.LeftArrow)) _deplaceGauche = true;
			if (Input.GetKeyUp(KeyCode.LeftArrow)) _deplaceGauche = false;
			// Gestion touche Flèche droite - déplacement vers la droite
			if (Input.GetKeyDown(KeyCode.RightArrow)) _deplaceDroite = true;
			if (Input.GetKeyUp(KeyCode.RightArrow)) _deplaceDroite = false;
			// Gestion touche Flèche haut - déplacement vers l'avant
			if (Input.GetKeyDown(KeyCode.UpArrow)) _deplaceAvant = true;
			if (Input.GetKeyUp(KeyCode.UpArrow)) _deplaceAvant = false;
			// Gestion touche Flèche bas - déplacement vers l'arrière
			if (Input.GetKeyDown(KeyCode.DownArrow)) _deplaceArriere = true;
			if (Input.GetKeyUp(KeyCode.DownArrow)) _deplaceArriere = false;
		}
	}
	//******************************************************************************************************************************
	// RE-UPDATE
	// RE-ACTUALISATION
	//******************************************************************************************************************************
	void LateUpdate()
	{
		//***************************************************
		// UPDATE ZOOM
		// ACTUALISATION DU ZOOM
		//***************************************************
		_camera.fieldOfView = Mathf.Lerp (_camera.fieldOfView, _leZoom, Time.deltaTime * zoomSpeed);
		//***************************************************
		// UPDATE Up/Down/Right/Left/Forward/Backward
		// ACTUALISATION Haut/Bas/Droite/Gauche/Avant/Arriere
		//***************************************************
		if (_deplacementActif&&!isNotActive){
			if (_deplaceHaut) _centre.position += Vector3.up * Time.deltaTime * _vitesse;
			if (_deplaceBas) _centre.position += Vector3.down * Time.deltaTime * _vitesse;
			if (_deplaceGauche) _centre.position += Vector3.left * Time.deltaTime * _vitesse;
			if (_deplaceDroite) _centre.position += Vector3.right * Time.deltaTime * _vitesse;
			if (_deplaceAvant) _centre.position += Vector3.forward * Time.deltaTime * _vitesse;
			if (_deplaceArriere) _centre.position += Vector3.back * Time.deltaTime * _vitesse;
			}
	}
	//******************************************************************************************************************************
	// Will only be activated when the script is triggered on the GameObject
    // This allows the update of the start of the InputManager
    // Ne sera activé que lorsque le script est déclenché sur le GameObject
    // Cela permet la remise à jour du start de l'InputManager
	//******************************************************************************************************************************
    void OnDestroy()
    {
        InputManager.MouseMoved += Rotate;    
    }
}
//**********************************************************************************************************************************
//@ Emmanuel Garraud CopyLeft 2017
//**********************************************************************************************************************************