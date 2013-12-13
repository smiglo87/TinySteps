using UnityEngine;
using System.Collections;

public class ViewDashboard : UIView {

	public UserManager userManager;
	public PhotoManager photoManager;


	public UILabel babyName;
	public UILabel babyAge;
	public UITexture profilePicture;
	public Texture2D noAvatarPicture;



	//signs up to hearing the event when baby has changed
	void OnEnable()
	{
		UserManager.OnBabyChanged += UpdateBaby;
	}

	//signs out from hearing when baby has changed
	void OnDisable()
	{
		UserManager.OnBabyChanged -= UpdateBaby;
	}




	public override void Show()
	{
		UpdateBaby();
		base.Show();
	}
	


	void UpdateBaby()
	{
		SetProfilePicture();
		SetBabyName();
		SetBabyAge();
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


	public void SetBabyAge()
	{
		string[] words = userManager.babies[userManager.currentBaby].GetAge().Split(',');
		
		//format baby age
		if(words[0].ToString() == "1")
		{
			if(words[1].ToString() == "1") babyAge.text = "is " + words[0].ToString() + " year and " + words[1].ToString() + " month old today";
			else babyAge.text = "is " + words[0].ToString() + " year and " + words[1].ToString() + " months old today";
		}
		else
		{
			if(words[1].ToString () == "1") babyAge.text = "is " + words[0].ToString() + " years and " + words[1].ToString() + " month old today";
			else babyAge.text = "is " + words[0].ToString() + " years and " + words[1].ToString() + " months old today";
		}
	}

}
