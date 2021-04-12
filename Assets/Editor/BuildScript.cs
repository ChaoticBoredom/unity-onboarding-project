using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Editor
{
    public class BuildScript
    {
        [MenuItem("Build/Server %#&s")]
        static void BuildServer()
        {
            BuildPlayerOptions options = new BuildPlayerOptions();
            options.locationPathName = "server";
            options.options = BuildOptions.EnableHeadlessMode;
            
            BuildWithOptions(options);
        }

        [MenuItem("Build/Client %#&c")]
        static void BuildClient()
        {
            BuildPlayerOptions options = new BuildPlayerOptions();
            options.locationPathName = "client";
            
            BuildWithOptions(options);
        }

        [MenuItem("Build/Both %#&x")]
        static void BuildBoth()
        {
            BuildServer();
            BuildClient();
        }

        private static void BuildWithOptions(BuildPlayerOptions options)
        {
            string[] scenes = {"Assets/Scenes/SampleScene.unity"};
            options.scenes = scenes;
            options.target = BuildTarget.StandaloneOSX;
            options.options = BuildOptions.EnableHeadlessMode;

            BuildReport report = BuildPipeline.BuildPlayer(scenes, options.locationPathName, options.target, options.options);

            if (report.summary.result == BuildResult.Succeeded)
            {
                Debug.Log("Build Succeeded");
            }

            if (report.summary.result == BuildResult.Failed)
            {
                Debug.Log("Build Failed");
            }
        }
    }
}
