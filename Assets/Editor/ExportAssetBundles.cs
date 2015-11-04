using UnityEngine;
using UnityEditor;

public class ExportAssetBundles {
	//在Unity编辑器中添加菜单  
	[MenuItem("Build/Build AssetBundle From Selection")]  
	public static void ExportResourceRGB2()  
	{  
		// 打开保存面板，获得用户选择的路径  
		string path = EditorUtility.SaveFilePanel("Save Resource", "", "New Resource", "assetbundle");  
		
		if (path.Length != 0)  
		{  
			// 选择的要保存的对象  
			Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);  
			//打包  
			BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets, BuildTarget.StandaloneWindows);  
		}  
	}  
	
	[MenuItem("Build/Save Scene")]  
	public static void ExportScene()  
	{  
		// 打开保存面板，获得用户选择的路径  
		string path = EditorUtility.SaveFilePanel("Save Resource", "", "New Resource", "unity3d");  
		
		if (path.Length != 0)
		{  
			// 选择的要保存的对象  
			Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);  
			string[] scenes = {"Assets/scene1.unity"};  
			//打包  
			BuildPipeline.BuildPlayer(scenes,path,BuildTarget.StandaloneWindows,BuildOptions.BuildAdditionalStreamedScenes);  
		}  
	}

	[MenuItem("Build/Save Dependencies")]
	public static void ExportDependencies()
	{
		// 启用交叉引用，用于所有跟随的资源包文件，直到我们调用PopAssetDependencies  
		BuildPipeline.PushAssetDependencies (); 
		var options = BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets;

		// 所有后续资源将共享这一资源包中的内容，由你来确保共享的资源包是否在其他资源载入之前载入  
		BuildPipeline.BuildAssetBundle(AssetDatabase.LoadMainAssetAtPath("assets/artwork/lerpzuv.tif"), null, "Shared.unity3d", options);

		// 这个文件将共享这些资源，但是后续的资源包将无法继续共享它  
		BuildPipeline.PushAssetDependencies();  
		BuildPipeline.BuildAssetBundle(AssetDatabase.LoadMainAssetAtPath("Assets/Artwork/Lerpz.fbx"), null, "Lerpz.unity3d", options);  
		BuildPipeline.PopAssetDependencies();

		// 这个文件将共享这些资源，但是后续的资源包将无法继续共享它  
		BuildPipeline.PushAssetDependencies();  
		BuildPipeline.BuildAssetBundle(AssetDatabase.LoadMainAssetAtPath("Assets/Artwork/explosive guitex.prefab"), null, "explosive.unity3d", options);  
		BuildPipeline.PopAssetDependencies();  

		BuildPipeline.PopAssetDependencies();
	}
}
