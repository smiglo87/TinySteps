using UnityEngine;
using System.Collections;
using Prime31;

public class PhotoManager : MonoBehaviour {

	public UserManager userManager;
	public PopUpManager popUpManager;
	public UIViewController viewController;
	public ViewBabyRegistration viewBabyRegistration;
	
	
	public void PromptForPhoto()
	{
		EtceteraBinding.promptForPhoto(0.25f, PhotoPromptType.CameraAndAlbum, 0.5f, true);
		
	}
	
	
	void OnEnable()
	{
		EtceteraManager.imagePickerCancelledEvent += ImagePickerCancelled;
		EtceteraManager.imagePickerChoseImageEvent += PhotoTaken;
	}
	
	void onDisable()
	{
		EtceteraManager.imagePickerCancelledEvent -= ImagePickerCancelled;
		EtceteraManager.imagePickerChoseImageEvent -= PhotoTaken;
	}
	
	void ImagePickerCancelled()
	{
		Debug.Log("imagePickerCancelled");
	}
	
	void PhotoTaken(string imagePath)
	{
		StartCoroutine(_PhotoTaken(imagePath));
	}
	

	IEnumerator _PhotoTaken(string imagePath)
	{
		Debug.Log("image picker chose image: " + imagePath);
		if(viewController.currentView == viewController.viewBabyRegistration)
		{
			Texture2D takenImage = new Texture2D(1,1);
			yield return StartCoroutine(GetTexture(imagePath, value => takenImage = value));
			
			if(takenImage.width > 1)
			{
				viewBabyRegistration.profilePicture.mainTexture = takenImage;
				viewBabyRegistration.profilePicture.MarkAsChanged();
				userManager.registerBabyProfilePicturePath = imagePath;
			}
			else
			{
				popUpManager.ShowError("Failed", "There was a problem with loading your picture. Please try again.");
			}
		}
		
	}
	
	public IEnumerator GetTexture(string imagePath, System.Action<Texture2D> result)
	{
		WWW localFile = new WWW("file://" + imagePath);
        yield return localFile;
		
        if (localFile.error == null)
		{
			result(localFile.texture);
			
		}
        else
        {
            Debug.Log("Open file error: "+localFile.error);
        }
		yield return null;
	}
		
		
		
		
		
		
		
		
		
		
		
}
