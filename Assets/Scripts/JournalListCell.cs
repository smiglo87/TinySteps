using UnityEngine;
using System.Collections;
using System;

public class JournalListCell : MonoBehaviour {

	public UserManager userManager;

	public UILabel date;
	public UILabel title;
	public UITexture picture;

	public void Refresh(object obj)
	{
		if (obj.GetType() == typeof(Journal))
		{
			Journal eventOnTheList = (Journal)obj;

			date.text = eventOnTheList.eventDate.ToString("dd.MM.yyyy");
			
			title.text = eventOnTheList.eventTitle.ToString();

			GameObject userM = GameObject.Find("UserManager");
			userManager = userM.GetComponent<UserManager>();
			//if(userManager.babies[userManager.currentBaby].journals != "") StartCoroutine(photoManager.GetTexture(userManager.babies[userManager.currentBaby].journal.eventPicture, value => picture.mainTexture = value));
			
		}
	}


}
