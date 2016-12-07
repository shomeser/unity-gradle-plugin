﻿using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class Gradle : MonoBehaviour {
	[MenuItem("Window/Gradle/Resolve 2")]
	public static void Refresh() {
		System.Diagnostics.Process process = new System.Diagnostics.Process();
		process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
		process.StartInfo.WorkingDirectory = Application.dataPath + "/Plugins/Gradle";

		switch (System.Environment.OSVersion.Platform) {
		case System.PlatformID.Unix:
		case System.PlatformID.MacOSX:
			process.StartInfo.FileName = "bash";
			process.StartInfo.Arguments = "gradlew export";
			break;

		default:
			process.StartInfo.FileName = "gradlew.bat";
			process.StartInfo.Arguments = "export";
			break;
		}

		if (process.Start ()) {
			process.WaitForExit ();

			if (process.ExitCode == 0) {
				Debug.Log ("Gradle finished successfully");
				AssetDatabase.Refresh ();
			} else {
				Debug.LogError ("Gradle returns " + process.ExitCode);
			}
		} else {
			Debug.LogError ("Gradle couldn't be started from: " + process.StartInfo.WorkingDirectory);
		}
	}
}
