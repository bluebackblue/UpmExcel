

/**
 * Copyright (c) blueback
 * Released under the MIT License
 * @brief デバッグツール。
*/


/** BlueBack.Excel
*/
namespace BlueBack.Excel
{
	/** DebugTool
	*/
	public class DebugTool
	{
		/** Assert
		*/
		#if(DEF_BLUEBACK_EXCEL_ASSERT)
		public static void Assert(bool a_flag,System.Exception a_exception = null)
		{
			if(a_flag != true){
				Config.ERRORPROC(a_exception,null);
			}
		}
		#endif

		/** Assert
		*/
		#if(DEF_BLUEBACK_EXCEL_ASSERT)
		public static void Assert(bool a_flag,string a_message)
		{
			if(a_flag != true){
				Config.ERRORPROC(null,a_message);
			}
		}
		#endif

		/** ErrorProc
		*/
		#if(DEF_BLUEBACK_EXCEL_ASSERT)
		public static void ErrorProc(System.Exception a_exception,string a_message)
		{
			if(a_message != null){
				UnityEngine.Debug.LogError(a_message);
			}

			if(a_exception != null){
				UnityEngine.Debug.LogError(a_exception.ToString());
			}

			UnityEngine.Debug.Assert(false);
		}
		#endif

		/** EditorLog
		*/
		#if(UNITY_EDITOR)
		public static void EditorLog(string a_text)
		{
			UnityEngine.Debug.Log(a_text);
		}
		#endif

		/** EditorLogError
		*/
		#if(UNITY_EDITOR)
		public static void EditorLogError(string a_text)
		{
			UnityEngine.Debug.LogError(a_text);
		}
		#endif
	}
}

