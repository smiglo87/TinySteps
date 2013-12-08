using UnityEngine;
using System.Collections;

public class ViewDashboard : UIView {

	public UserManager userManager;
	public PhotoManager photoManager;


	public UILabel babyName;
	public UILabel babyAge;
	public UITexture profilePicture;
	public Texture2D noAvatarPicture;



	public override void Show()
	{
		Debug.Log("ViewDashboard show called");
		SetProfilePicture();
		SetBabyName();
		base.Show();
	}


	public void SetProfilePicture()
	{
		
		if (userManager.babies[userManager.currentBaby].profilePicture != "") StartCoroutine(photoManager.GetTexture(userManager.babies[userManager.currentBaby].profilePicture, value => profilePicture.mainTexture = value));
		else profilePicture.mainTexture = noAvatarPicture;
	}



	public void SetBabyName()
	{
		babyName.text = userManager.babies[userManager.currentBaby].babyName;
	}

}
