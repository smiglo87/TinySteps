using UnityEngine;
using System.Collections;


/// <summary>
/// Manages all views in the project.
/// </summary>
public class UIViewController : MonoBehaviour {


	public ViewDashboard viewDashboard;
	public UIView viewWelcome;



	void Start()
	{
		viewDashboard.Show();
	}

}
