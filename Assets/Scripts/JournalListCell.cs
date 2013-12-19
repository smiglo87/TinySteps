using UnityEngine;
using System.Collections;
using System;

public class JournalListCell : MonoBehaviour {

	public UserManager userManager;

	public UILabel eventDate;
	public UILabel eventTitle;
	public UITexture eventPicture;

	public GameObject entryRoot;



	public void Refresh(object obj)
	{
		if (obj.GetType() == typeof(Journal))
		{
			Journal eventOnTheList = (Journal)obj;

			entryRoot.SetActive(true);

			eventDate.text = eventOnTheList.eventDate.ToString("dd.MM.yyyy");
			
			eventTitle.text = eventOnTheList.eventTitle.ToString();

			GameObject userM = GameObject.Find("UserManager");
			userManager = userM.GetComponent<UserManager>();
			//if(userManager.babies[userManager.currentBaby].journals != "") StartCoroutine(photoManager.GetTexture(userManager.babies[userManager.currentBaby].journal.eventPicture, value => picture.mainTexture = value));
			
		}
	}


}
