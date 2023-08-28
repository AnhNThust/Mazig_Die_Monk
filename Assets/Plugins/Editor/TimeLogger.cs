using UnityEngine;
using UnityEditor;
using System;
using System.IO;
using UnityEngine.SceneManagement;

namespace Test
{
    public class TimeLogger
    {
		static readonly string FilePath = Path.Combine(Application.dataPath, "Plugins/Editor/TestTimeLog");
        static TimeLogger Logger;

        [InitializeOnLoadMethod]
        static void AutoLog()
        {
            if(Logger != null) return;

            Logger = new TimeLogger();
            Logger.Write(() => Logger = null);
        }

        public void Write(Action CallBack)
        {
            FileInfo fi;
            fi = new FileInfo(FilePath);

			var sceneName = SceneManager.GetActiveScene().name;
			if(string.IsNullOrEmpty(sceneName)) sceneName = "UnKnown";

            StreamWriter sw = fi.AppendText();
			sw.WriteLine(string.Format("{0}_{1}", sceneName, DateTime.Now.ToLocalTime()));
            sw.Flush();
            sw.Close();

            CallBack();
        }
    }
}