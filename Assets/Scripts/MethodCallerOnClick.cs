using UnityEngine;
using System.Collections;

public class MethodCallerOnClick : MonoBehaviour {

	public MonoBehaviour scriptWithMetod;
	public string mehodToInvoke;
	public bool startingCourotine;
	
	
	void Start()
	{
		if (scriptWithMetod == null)
		{
			Debug.LogError("Method caller empty on: " + name);
		}
		
	}
	
	
	void OnClick(){
		
		if (startingCourotine){
			scriptWithMetod.StartCoroutine(mehodToInvoke);
//			Debug.Log("click");
		}
		else {
			scriptWithMetod.Invoke(mehodToInvoke, 0f);	
		}

	}
	
	
}
