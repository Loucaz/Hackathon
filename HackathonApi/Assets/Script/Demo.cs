using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine.Networking;

public static class ButtonExtension
{
	public static void AddEventListener<T> (this Button button, T param, Action<T> OnClick)
	{
		button.onClick.AddListener (delegate() {
			OnClick (param);
		});
	}
}

public class Demo : MonoBehaviour
{
	[Serializable]
	public struct Game
	{
		public string Name;
		public string Description;
		public Sprite Icon;
	}
	Product[] catalogue;

	void Awake()
    {
        //_ = StartAsync();

	}


	public void apiInputSearch(string search)
	{
		_ = StartAsync(search);

	}
	async Task StartAsync (string search = null)
	{
		ApiCall api = new ApiCall();
		await api.ControllerAsync(SearchBar.instance.getCode(search));
		catalogue = api.json.products;
		GameObject buttonTemplate = transform.GetChild (0).gameObject;
		GameObject g;

		int N = catalogue.Length;

		bool first = false;
		foreach (Transform child in transform)
		{
			if(first)
				Destroy(child.gameObject);
			first = true;
		}

		for (int i = 0; i < N; i++) {
			g = Instantiate (buttonTemplate, transform);
			StartCoroutine(DownloadImage(catalogue[i].imageUrl, g));
			g.transform.GetChild (1).GetComponent <Text> ().text = catalogue [i].brandName;
			g.transform.GetChild (2).GetComponent <Text> ().text = catalogue [i].name;

			/*g.GetComponent <Button> ().onClick.AddListener (delegate() {
				ItemClicked (i);
			});*/
			g.GetComponent <Button> ().AddEventListener (i, ItemClicked);
		}

		Destroy (buttonTemplate);
	}

	void ItemClicked (int itemIndex)
	{
		Debug.Log ("------------item " + itemIndex + " clicked---------------");
		Debug.Log ("name " + catalogue [itemIndex].brandName);
		Debug.Log ("desc " + catalogue [itemIndex].imageUrl);
	}

	IEnumerator DownloadImage(string MediaUrl,GameObject g)
	{
		UnityWebRequest request = UnityWebRequestTexture.GetTexture("https://"+MediaUrl);
		yield return request.SendWebRequest();
		if (request.result != UnityWebRequest.Result.Success)
			Debug.Log("TAMER: "+request.error);
        else
        {
			Texture2D texture2D = ((DownloadHandlerTexture)request.downloadHandler).texture;
			g.transform.GetChild(0).GetComponent<Image>().sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), Vector2.zero);

		}
	}



}
