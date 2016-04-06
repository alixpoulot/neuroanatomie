using UnityEngine;
using System.Collections;

public class HeadRotations : MonoBehaviour {

	private Transform headBone;
	private Transform appVestRightGp;
	private Transform appVestLeftGp;
	private Renderer appVestRightMesh;
	private Renderer appVestLeftMesh;

	public float smooth = 50f;

	private Quaternion targetRotation = Quaternion.identity;
	private Quaternion targetRotationAppVestR = Quaternion.identity;
	private Quaternion targetRotationAppVestL = Quaternion.identity;
	


	private Vector3 localLookFront = new Vector3(0f, 0f, 12f);
	private Vector3 localLookLeft = new Vector3(30f, 0f, 12f);
	private Vector3 localLookLeftUp = new Vector3(30f, 0f, 30f);
	private Vector3 localLookLeftDown = new Vector3(30f, 0f, -16f);
	private Vector3 localLookRight = new Vector3(-30f, 0f, 12f);
	private Vector3 localLookRightUp = new Vector3(-30f, 0f, 30f);
	private Vector3 localLookRightDown = new Vector3(-30f, 0f, -16f);

	private Vector3 localLookFrontAppVestR = new Vector3(-90f, 0f, 0f);
	private Vector3 localLookLeftAppVestR = new Vector3(-90f, -15f, 0f);
	private Vector3 localLookLeftUpAppVestR = new Vector3(-100f, -15f, 0f);
	private Vector3 localLookLeftDownAppVestR = new Vector3(-80f, -15f, 0f);
	private Vector3 localLookRightAppVestR = new Vector3(-90f, 15f, 0f);
	private Vector3 localLookRightUpAppVestR = new Vector3(-100f, 15f, 0f);
	private Vector3 localLookRightDownAppVestR = new Vector3(-80f, 15f, 0f);

	private Color32 CSC_base_Color = new Color32(251,221,215,255);
	private Color32 CSC_activated = new Color32(255,0,0,255);
	private Color32 CSC_inhibitated = new Color32(0,0,255,255);

	private Color32 leftCSC_horizontalColor;
	private Color32 leftCSC_anteriorColor;
	private Color32 leftCSC_posteriorColor;

	private Color32 rightCSC_horizontalColor;
	private Color32 rightCSC_anteriorColor;
	private Color32 rightCSC_posteriorColor;

	public enum Directions{FRONT, LEFT, RIGHT, LEFTUP, LEFTDOWN, RIGHTUP, RIGHTDOWN};

	// Use this for initialization
	void Start () {
		headBone = GameObject.FindGameObjectWithTag ("HeadBone").transform;
		appVestRightGp = GameObject.Find ("app_vestibulaire_gp_R").transform;
		appVestLeftGp = GameObject.Find ("app_vestibulaire_gp_L").transform;
		appVestRightMesh = GameObject.Find ("app_vestibulaire_mesh_R").GetComponent<Renderer> ();
		appVestLeftMesh = GameObject.Find ("app_vestibulaire_mesh_L").GetComponent<Renderer> ();

		leftCSC_horizontalColor = CSC_base_Color;
		leftCSC_posteriorColor = CSC_base_Color;
		leftCSC_anteriorColor = CSC_base_Color;

		rightCSC_horizontalColor = CSC_base_Color;
		rightCSC_posteriorColor = CSC_base_Color;
		rightCSC_anteriorColor = CSC_base_Color;

		//Mesh mesh = GameObject.Find ("app_vestibulaire_mesh_L").GetComponent<MeshFilter>().mesh;
		//Debug.Log(mesh.name + " has " + mesh.subMeshCount + " submeshes!");

		LookDirection (Directions.FRONT);
	}
	
	// Update is called once per frame
	void Update () {
		headBone.localRotation = Quaternion.RotateTowards (headBone.localRotation, targetRotation, smooth * Time.deltaTime);
		appVestRightGp.localRotation = Quaternion.RotateTowards (appVestRightGp.localRotation, targetRotationAppVestR, smooth/2 * Time.deltaTime);
		appVestLeftGp.localRotation = Quaternion.RotateTowards (appVestLeftGp.localRotation, targetRotationAppVestL, smooth/2 * Time.deltaTime);

		appVestLeftMesh.materials [1].color = Color32.Lerp (appVestLeftMesh.materials [1].color, leftCSC_anteriorColor, smooth / 10 * Time.deltaTime);
		appVestLeftMesh.materials [3].color = Color32.Lerp (appVestLeftMesh.materials [3].color, leftCSC_posteriorColor, smooth / 10 * Time.deltaTime);
		appVestLeftMesh.materials [5].color = Color32.Lerp (appVestLeftMesh.materials [5].color, leftCSC_horizontalColor, smooth / 10 * Time.deltaTime);

		appVestRightMesh.materials [1].color = Color32.Lerp (appVestRightMesh.materials [1].color, rightCSC_anteriorColor, smooth / 10 * Time.deltaTime);
		appVestRightMesh.materials [3].color = Color32.Lerp (appVestRightMesh.materials [3].color, rightCSC_posteriorColor, smooth / 10 * Time.deltaTime);
		appVestRightMesh.materials [5].color = Color32.Lerp (appVestRightMesh.materials [5].color, rightCSC_horizontalColor, smooth / 10 * Time.deltaTime);
	}

	public void LookFront(){LookDirection (Directions.FRONT);}
	public void LookLeft(){LookDirection (Directions.LEFT);}
	public void LookRight(){LookDirection (Directions.RIGHT);}
	public void LookLeftUp(){LookDirection (Directions.LEFTUP);}
	public void LookLeftDown(){LookDirection (Directions.LEFTDOWN);}
	public void LookRightUp(){LookDirection (Directions.RIGHTUP);}
	public void LookRightDown(){LookDirection (Directions.RIGHTDOWN);}

	public void LookDirection(Directions dir){
		switch (dir) {
			case Directions.FRONT:
				targetRotation = Quaternion.Euler (localLookFront);
				targetRotationAppVestR = Quaternion.Euler(localLookFrontAppVestR);
				targetRotationAppVestL = Quaternion.Euler(localLookFrontAppVestR);
				leftCSC_anteriorColor = CSC_base_Color;
				leftCSC_posteriorColor = CSC_base_Color;
				leftCSC_horizontalColor = CSC_base_Color;
				rightCSC_anteriorColor = CSC_base_Color;
				rightCSC_posteriorColor = CSC_base_Color;
				rightCSC_horizontalColor = CSC_base_Color;
				break;
			case Directions.LEFT:
				targetRotation = Quaternion.Euler (localLookLeft);
				targetRotationAppVestR = Quaternion.Euler(localLookLeftAppVestR);
				targetRotationAppVestL = Quaternion.Euler(localLookLeftAppVestR);
				leftCSC_anteriorColor = CSC_base_Color;
				leftCSC_posteriorColor = CSC_base_Color;
				leftCSC_horizontalColor = CSC_activated;
				rightCSC_anteriorColor = CSC_base_Color;
				rightCSC_posteriorColor = CSC_base_Color;
				rightCSC_horizontalColor = CSC_inhibitated;

				break;
			case Directions.RIGHT:
				targetRotation = Quaternion.Euler (localLookRight);
				targetRotationAppVestR = Quaternion.Euler(localLookRightAppVestR);
				targetRotationAppVestL = Quaternion.Euler(localLookRightAppVestR);
				leftCSC_anteriorColor = CSC_base_Color;
				leftCSC_posteriorColor = CSC_base_Color;
				leftCSC_horizontalColor = CSC_inhibitated;
				rightCSC_anteriorColor = CSC_base_Color;
				rightCSC_posteriorColor = CSC_base_Color;
				rightCSC_horizontalColor = CSC_activated;
				break;
			case Directions.LEFTUP:
				targetRotation = Quaternion.Euler (localLookLeftUp);
				targetRotationAppVestR = Quaternion.Euler(localLookLeftUpAppVestR);
				targetRotationAppVestL = Quaternion.Euler(localLookLeftUpAppVestR);
				leftCSC_anteriorColor = CSC_base_Color;
				leftCSC_posteriorColor = CSC_activated;
				leftCSC_horizontalColor = CSC_base_Color;
				rightCSC_anteriorColor = CSC_inhibitated;
				rightCSC_posteriorColor = CSC_base_Color;
				rightCSC_horizontalColor = CSC_base_Color;
				break;
			case Directions.LEFTDOWN:
				targetRotation = Quaternion.Euler (localLookLeftDown);
				targetRotationAppVestR = Quaternion.Euler(localLookLeftDownAppVestR);
				targetRotationAppVestL = Quaternion.Euler(localLookLeftDownAppVestR);
				leftCSC_anteriorColor = CSC_base_Color;
				leftCSC_posteriorColor = CSC_inhibitated;
				leftCSC_horizontalColor = CSC_base_Color;
				rightCSC_anteriorColor = CSC_activated;
				rightCSC_posteriorColor = CSC_base_Color;
				rightCSC_horizontalColor = CSC_base_Color;
				break;
			case Directions.RIGHTUP:
				targetRotation = Quaternion.Euler (localLookRightUp);
				targetRotationAppVestR = Quaternion.Euler(localLookRightUpAppVestR);
				targetRotationAppVestL = Quaternion.Euler(localLookRightUpAppVestR);
				leftCSC_anteriorColor = CSC_inhibitated;
				leftCSC_posteriorColor = CSC_base_Color;
				leftCSC_horizontalColor = CSC_base_Color;
				rightCSC_anteriorColor = CSC_base_Color;
				rightCSC_posteriorColor = CSC_activated;
				rightCSC_horizontalColor = CSC_base_Color;
				break;
			case Directions.RIGHTDOWN:
				targetRotation = Quaternion.Euler (localLookRightDown);
				targetRotationAppVestR = Quaternion.Euler(localLookRightDownAppVestR);
				targetRotationAppVestL = Quaternion.Euler(localLookRightDownAppVestR);
				leftCSC_anteriorColor = CSC_activated;
				leftCSC_posteriorColor = CSC_base_Color;
				leftCSC_horizontalColor = CSC_base_Color;
				rightCSC_anteriorColor = CSC_base_Color;
				rightCSC_posteriorColor = CSC_inhibitated;
				rightCSC_horizontalColor = CSC_base_Color;
				break;
		}
	}
}
