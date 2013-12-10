using UnityEngine;
using System.Collections;


/// <summary>
/// Manages all views in the project.
/// </summary>
public class UIViewController : MonoBehaviour {

	public Transform guiCamera;
	public UIView currentView;

	public ViewWelcome viewWelcome;
	public ViewBabyRegistration viewBabyRegistration;
	public ViewBabyRegistration2 viewBabyRegistration2;
	public ViewDashboard viewDashboard;


	public void ChangeView(UIView target)
	{
		if(currentView != null) currentView.Hide();
		guiCamera.localPosition = target.transform.localPosition;
		currentView = target;
		currentView.Show();
	}


	public void ToWelcomeView()
	{
		ChangeView(viewWelcome);
	}
	
	public void ToBabyRegistrationView()
	{
		ChangeView(viewBabyRegistration);
	}
	
	public void ToBabyRegistrationView2()
	{
		ChangeView(viewBabyRegistration2);
	}
	
	public void ToDashboardMainView()
	{
		ChangeView(viewDashboard);
	}


}
