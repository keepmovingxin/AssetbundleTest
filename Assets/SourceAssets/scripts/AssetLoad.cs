using System;
using UnityEngine;
using System.Collections;

public class AssetLoad : MonoBehaviour {

	private string BundleURL = "file:///test.assetbundle";
	private string SceneURL = "file:///testScene.unity3d";

	// Use this for initialization
	void Start () {
		BundleURL = "file://"+Application.dataPath+"/test.assetbundle";
		SceneURL = "file://"+Application.dataPath+"/testScene.unity3d";
		Debug.Log(BundleURL);
		Debug.Log(SceneURL);
		StartCoroutine(DownloadAssetAndScene());
	}

	// Download assetbundle
	IEnumerator DownloadAssetAndScene() {
		// download assetbundle,load sprite
		using (WWW asset = new WWW(BundleURL)) {
			yield return asset;
			AssetBundle bundle = asset.assetBundle;
			Instantiate(bundle.LoadAsset("TestPrefab"));
			bundle.Unload(false);
			yield return new WaitForSeconds(5);
		}

		// download scene,load scene
		using (WWW scene = new WWW(SceneURL)) {
			yield return scene;
			AssetBundle bundle = scene.assetBundle;
			Application.LoadLevel("scene1");
		}
	}


	// Update is called once per frame
	void Update () {
	
	}
}
