using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Ionic.Zip;

namespace Naticord
{
    public partial class Settings : Form
    {
        private readonly Version currentVersion = new Version("0.1.1");
        private List<ISettings> loadedPlugins = new List<ISettings>();

        public Settings()
        {
            InitializeComponent();
        }

        private void checkUpdates_Click(object sender, EventArgs e)
        {
            try
            {
                string jsonData = GetLatestVersionFromGitHub();
                JObject releaseData = JObject.Parse(jsonData);

                string tagName = releaseData["tag_name"].ToString();
                Version latestVersion;
                if (Version.TryParse(tagName, out latestVersion))
                {
                    int comparisonResult = latestVersion.CompareTo(currentVersion);

                    if (comparisonResult > 0)
                    {
                        MessageBox.Show($"A newer version of Naticord is available! (v{latestVersion})",
                                        "Update Available",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                    else if (comparisonResult < 0)
                    {
                        MessageBox.Show($"You are using a newer version of Naticord than the latest available (v{latestVersion}).",
                                        "No Update Available",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("You are using the latest version of Naticord.",
                                        "No Update Available",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }

                    Console.WriteLine("Raw GitHub Data:");
                    Console.WriteLine(jsonData);
                }
                else
                {
                    MessageBox.Show("The version retrieved from GitHub is not in a valid format. This will be fixed soon.",
                                    "Error",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                    Console.WriteLine("Raw GitHub Data:");
                    Console.WriteLine(jsonData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while retrieving data from GitHub: {ex.Message}",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private string GetLatestVersionFromGitHub()
        {
            using (WebClient client = new WebClient())
            {
                client.Headers.Add("User-Agent", "NaticordUpdateChecker");
                string url = "https://api.github.com/repos/n1d3v/naticord/releases/latest";
                return client.DownloadString(url);
            }
        }

        private void addPlugin_Click(object sender, EventArgs e)
        {
            MessageBox.Show("FYI, plugins could have viruses so just beware if you want to use plugins.");
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Zip files (*.zip)|*.zip";
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string zipFilePath = openFileDialog.FileName;
                    string pluginsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");
                    string extractPath = Path.Combine(pluginsDirectory, Path.GetFileNameWithoutExtension(zipFilePath));

                    if (!Directory.Exists(extractPath))
                    {
                        Directory.CreateDirectory(extractPath);
                    }

                    var pluginLoader = new PluginLoader();
                    pluginLoader.ExtractPlugin(zipFilePath, extractPath);

                    var plugins = pluginLoader.LoadPlugins(pluginsDirectory);
                    foreach (var plugin in plugins)
                    {
                        loadedPlugins.Add(plugin);
                        Console.WriteLine($"Loaded plugin: {plugin.Name} Description: {plugin.Description}");
                    }
                }
            }
        }

        private void applyPluginsSettings_Click(object sender, EventArgs e)
        {
            foreach (var plugin in loadedPlugins)
            {
                plugin.ApplySettings();
            }
        }

        public class PluginLoader
        {
            public List<PluginConfig> LoadPlugins(string pluginsDirectory)
            {
                var pluginConfigs = new List<PluginConfig>();

                foreach (var dir in Directory.GetDirectories(pluginsDirectory))
                {
                    var pluginConfigPath = Path.Combine(dir, "plugin.json");
                    if (File.Exists(pluginConfigPath))
                    {
                        var json = File.ReadAllText(pluginConfigPath);
                        var config = JsonConvert.DeserializeObject<PluginConfig>(json);
                        config.AssemblyPath = dir;
                        pluginConfigs.Add(config);
                    }
                }

                return pluginConfigs;
            }

            public void ExtractPlugin(string zipFilePath, string extractPath)
            {
                try
                {
                    using (ZipFile zip = ZipFile.Read(zipFilePath))
                    {
                        zip.ExtractAll(extractPath, ExtractExistingFileAction.OverwriteSilently);
                    }
                    MessageBox.Show("Plugin extracted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to extract plugin: {ex.Message}");
                }
            }
        }

        public interface ISettings
        {
            void ApplySettings();
        }

        public class PluginConfig : ISettings
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string AssemblyPath { get; set; }
            public bool IsCSharpPlugin { get; set; }

            public void ApplySettings()
            {
                try
                {
                    if (IsCSharpPlugin)
                    {
                        var assembly = Assembly.LoadFrom(AssemblyPath);
                        foreach (var type in assembly.GetTypes())
                        {
                            if (typeof(ISettings).IsAssignableFrom(type))
                            {
                                var plugin = (ISettings)Activator.CreateInstance(type);
                                plugin.ApplySettings();
                            }
                        }
                    }
                    else
                    {
                        LoadCppPlugin(AssemblyPath);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to apply settings from plugin '{Name}': {ex.Message}");
                }
            }

            private void LoadCppPlugin(string filePath)
            {
                string dllPath = Path.Combine(filePath, $"{Name}.dll");
                IntPtr dllHandle = LoadLibrary(dllPath);
                if (dllHandle == IntPtr.Zero)
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    Console.WriteLine($"Failed to load DLL '{dllPath}', error code: {errorCode}");
                    return;
                }

                FreeLibrary(dllHandle);
            }

            [DllImport("kernel32.dll", SetLastError = true)]
            private static extern IntPtr LoadLibrary(string lpFileName);

            [DllImport("kernel32.dll", SetLastError = true)]
            private static extern bool FreeLibrary(IntPtr hModule);
        }
    }
}

