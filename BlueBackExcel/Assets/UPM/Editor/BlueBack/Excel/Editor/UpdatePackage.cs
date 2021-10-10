

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * @brief パッケージ更新。自動生成。
*/


/** BlueBack.Excel.Editor
*/
#if(UNITY_EDITOR)
namespace BlueBack.Excel.Editor
{
	/** UpdatePackage
	*/
	#if(!DEF_USER_BLUEBACK_EXCEL)
	public static class UpdatePackage
	{
		/** MenuItem_BlueBack_Excel_UpdatePackage
		*/
		[UnityEditor.MenuItem("BlueBack/Excel/UpdatePackage")]
		public static void MenuItem_BlueBack_Excel_UpdatePackage()
		{
			string t_version = GetLastReleaseNameFromGitHub("bluebackblue",Version.packagename);
			if(t_version == null){
				#if(UNITY_EDITOR)
				DebugTool.EditorLogError("GetLastReleaseNameFromGitHub : connect error");
				#endif
			}else if(t_version.Length <= 0){
				UnityEditor.PackageManager.Client.Add("https://github.com/bluebackblue/Excel.git?path=BlueBackExcel/Assets/UPM");
			}else{
				UnityEditor.PackageManager.Client.Add("https://github.com/bluebackblue/Excel.git?path=BlueBackExcel/Assets/UPM#" + t_version);
			}
		}

		/** DownloadBinary
		*/
		private static byte[] DownloadBinary(string a_url)
		{
			try{
				using(UnityEngine.Networking.UnityWebRequest t_webrequest = ((System.Func<UnityEngine.Networking.UnityWebRequest>)(()=>{
					return UnityEngine.Networking.UnityWebRequest.Get(a_url);
				}))()){
					UnityEngine.Networking.UnityWebRequestAsyncOperation t_async = t_webrequest.SendWebRequest();
					while(true){
						System.Threading.Thread.Sleep(1);
						if(t_async.isDone == true){
							if(t_webrequest.error != null){
								string t_text = "";
								if(t_webrequest.downloadHandler.text != null){
									t_text = t_webrequest.downloadHandler.text;
								}
								#if(UNITY_EDITOR)
								DebugTool.EditorLogError(a_url + " : " + t_webrequest.error + " : " + t_text);
								#endif
								return null;
							}else{
								return t_webrequest.downloadHandler.data;
							}
						}
					}
				}
			}catch(System.Exception t_exception){
				DebugTool.EditorLogError(a_url + " : " + t_exception.Message + "\n" + t_exception.StackTrace);
				return null;
			}
		}

		/** GetLastReleaseNameFromGitHub
		*/
		private static string GetLastReleaseNameFromGitHub(string a_auther,string a_reposname)
		{
			try{
				byte[] t_binary = DownloadBinary("https://api.github.com/repos/" + a_auther + "/" + a_reposname + "/releases/latest");
				if(t_binary != null){
					string t_text = System.Text.Encoding.UTF8.GetString(t_binary,0,t_binary.Length);
					System.Text.RegularExpressions.Match t_match = System.Text.RegularExpressions.Regex.Match(t_text,".*(?<name>\\\"tag_name\\\")\\s*\\:\\s*\\\"(?<value>[a-zA-Z0-9_\\.]*)\\\".*");
					t_text = t_match.Groups["value"].Value;
					if(t_text != null){
						return t_text;
					}else{
						#if(UNITY_EDITOR)
						DebugTool.EditorLogError(a_auther + " : " + a_reposname + " : text == null");
						#endif
						return null;
					}
				}else{
					#if(UNITY_EDITOR)
					DebugTool.EditorLogError(a_auther + " : " + a_reposname + " : binary == null");
					#endif
					return null;
				}
			}catch(System.Exception t_exception){
				#if(UNITY_EDITOR)
				DebugTool.EditorLogError(a_auther + " : " + a_reposname + " : " + t_exception.Message + "\n" + t_exception.StackTrace);
				#endif
				return null;
			}
		}
	}
	#endif
}
#endif
