#if !DISABLE_UNI_BUILD_SUMMARY_WRITER

using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Kogane
{
	/// <summary>
	/// ビルド開始時に BuildReport をテキストファイルに書き込むエディタ拡張
	/// </summary>
	public sealed class BuildReportWriter :
		IPreprocessBuildWithReport,
		IPostprocessBuildWithReport
	{
		//==============================================================================
		// 定数(static readonly)
		//==============================================================================
		private static readonly string TEMP_FOLDER_PATH = "Assets/UniBuildSummaryWriter_Temp";
		private static readonly string TEXT_FILE_PATH   = TEMP_FOLDER_PATH + "/Resources/build_report.txt";

		//==============================================================================
		// プロパティ
		//==============================================================================
		public int callbackOrder => 0;

		//==============================================================================
		// 関数
		//==============================================================================
		/// <summary>
		/// ビルドが開始する前に呼び出されます
		/// </summary>
		public void OnPreprocessBuild( BuildReport report )
		{
			var json = JsonUtility.ToJson( new JsonBuildReport( report ), true );

			var directoryName = Path.GetDirectoryName( TEXT_FILE_PATH );

			if ( !Directory.Exists( directoryName ) )
			{
				Directory.CreateDirectory( directoryName );
			}

			File.WriteAllText( TEXT_FILE_PATH, json );

			AssetDatabase.ImportAsset( TEXT_FILE_PATH );
		}

		/// <summary>
		/// ビルドが成功した後に呼び出されます
		/// </summary>
		public void OnPostprocessBuild( BuildReport report )
		{
			AssetDatabase.DeleteAsset( TEMP_FOLDER_PATH );
		}
	}
}

#endif