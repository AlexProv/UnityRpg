using UnityEngine;
using System.Collections;

public class mouseSelect : MonoBehaviour {

	Ray ray;
	Material selectedMaterial;
	Material bufferMaterial;
	GameObject selectedObject;
	
	// Use this for initialization
	void Start () {
		selectedMaterial = Resources.Load("SelectedMateiral") as Material;
	}
	
	// Update is called once per frame
	void Update () {
		//lr.SetPosition(0,this.transform.position);
		//Vector2 v = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		int mask = ~((1 << LayerMask.NameToLayer("Floor")) | (1 << LayerMask.NameToLayer("Characters")));
		if(Physics.Raycast(ray,out hitInfo,100,mask))
		{
			bool wasnull = false;
			if(bufferMaterial == null || selectedObject == null)
			{
				selectedObject = hitInfo.collider.gameObject;
				bufferMaterial = hitInfo.collider.gameObject.renderer.material;
				wasnull = true;
			}
				
			if(hitInfo.collider.gameObject.Equals(selectedObject) && !wasnull)
			{
				Debug.Log(hitInfo.collider.name);
			}
			else
			{
				selectedObject.renderer.material = bufferMaterial;
				selectedObject = hitInfo.collider.gameObject;
				bufferMaterial = selectedObject.renderer.material;
				selectedObject.renderer.material = selectedMaterial;
			}
		}
		else
		{
			if(selectedObject != null)
				selectedObject.renderer.material = bufferMaterial;
		}
		
	}
}
