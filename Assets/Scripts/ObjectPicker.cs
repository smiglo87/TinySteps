using UnityEngine;
using System.Collections;

public class ObjectPicker : MonoBehaviour {
	
	
	public static GameObject PickObject( Vector2 screenPos, Camera camera )
	{
		
		if (camera != null){
		    Ray ray = camera.ScreenPointToRay( screenPos );
		    RaycastHit hit; 
		
		    if( Physics.Raycast( ray, out hit ) ){
	
		        return hit.collider.gameObject;
				
			}
		}
	    return null;
		
	}
	
	
}
