#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Test
{
    public static class ExportPackeage
    {
        
        public static void Export(string path)
        {
            var paths = GetAllAssetsPath();
            AssetDatabase.ExportPackage(paths.ToArray(), path, ExportPackageOptions.Recurse | ExportPackageOptions.Default);
        }

        public static List<string> GetAllAssetsPath()
        {
            List<string> Paths = new List<string>();

            DirectoryInfo di = new DirectoryInfo(Application.dataPath);
            FileInfo[] files = di.GetFiles("*", SearchOption.AllDirectories);

            foreach (FileInfo f in files)
            {
                if (f.Name.StartsWith(".") || f.Name.EndsWith(".meta")) continue;

                var idx = f.FullName.IndexOf("Assets");
                var unityFilePath = f.FullName.Substring(idx, f.FullName.Length - idx);
                if (unityFilePath != string.Empty) Paths.Add(unityFilePath);
            }

            return Paths;
        }
    }
}

#endif