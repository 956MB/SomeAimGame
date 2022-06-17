//using System.Management;
using System.Linq;
using UnityEngine;

using SomeAimGame.Utilities;

namespace SomeAimGame.Core {
    namespace Video {
        public class VideoDropdownManager : MonoBehaviour {
            public GameObject dropdownSelectButton;
            public GameObject displayModesDropdownBody, resolutionDropdownBody, monitorDropdownBody, antiAliasDropdownBody;
            public GameObject dropdownItemPrefab;

            private string[] aspectRatios = { "16:9", "16:10", "4:3" };

            public static VideoDropdownManager videoDropdownManager;
            private void Awake() { videoDropdownManager = this; }

            /// <summary>
            /// Inits all video setting dropdowns with available options.
            /// </summary>
            public static void InitAllVideoSettingsDropdowns() {
                // TODO: get parent dropdown button w/h and pass that to newly created dropdown item prefabs
                InitDisplayModesDropdownItems();
                InitResolutionDropdownItems();
                InitMonitorsDropdownItems();
                InitAntiAliasDropdownItems();
            }
            /// <summary>
            /// Loads all available display mode options into display modes dropdown from DisplayModes enum.
            /// </summary>
            public static void InitDisplayModesDropdownItems() {
                DropdownUtils.CreateDropdownItems_Loop(VideoDropdowns.DISPLAY_MODE, videoDropdownManager.dropdownItemPrefab, videoDropdownManager.displayModesDropdownBody, FullScreenMode.FullScreenWindow, FullScreenMode.ExclusiveFullScreen, FullScreenMode.MaximizedWindow, FullScreenMode.Windowed);

                //Debug.Log(videoDropdownManager.dropdownSelectButton.transform.GetComponent<RectTransform>().sizeDelta.x);
                //Debug.Log(videoDropdownManager.dropdownSelectButton.transform.GetComponent<RectTransform>().sizeDelta.y);
                //Debug.Log(videoDropdownManager.displayModesDropdownBody.transform.GetComponent<RectTransform>().sizeDelta);
            }
            /// <summary>
            /// Loads all available resolution options into resolution dropdown from available monitor resolutions.
            /// </summary>
            public static void InitResolutionDropdownItems() {
                Resolution[] fullscreenResolutions = VideoSettingUtil.ReturnAvailableResolutions();
                int setInt = 0;

                for (int i = fullscreenResolutions.Length-1; i > 0; i--) {
                    Resolution currentRes = fullscreenResolutions[i];
                    int refresh           = currentRes.refreshRate;
                    
                    // check if resolution refresh rate is within range of currently set refresh: 143, 144, 145 Hz
                    if(refresh >= VideoSettings.resolutionRefreshRate-1 && refresh <= VideoSettings.resolutionRefreshRate+1) {
                        string aspectRatio = Util.ReturnAspectRatio_string(currentRes.width, currentRes.height);

                        if (videoDropdownManager.aspectRatios.Any(aspectRatio.Contains)) {
                            string formattedResString = $"{currentRes.width} x {currentRes.height} {aspectRatio} ({currentRes.refreshRate}Hz)";
                            DropdownUtils.CreateDropdownItem(1, setInt, formattedResString, videoDropdownManager.dropdownItemPrefab, videoDropdownManager.resolutionDropdownBody);
                            setInt++;
                        }
                    }
                }
            }
            /// <summary>
            /// Loads all available monitor options into monitor dropdown from connected monitors.
            /// </summary>
            public static void InitMonitorsDropdownItems() {
                WindowsDisplayAPI.DisplayConfig.PathDisplayTarget[] connectedDisplays = VideoSettingUtil.ReturnConnectedMonitors_WindowsDisplayAPI();

                for (int displayIdx = 0; displayIdx < connectedDisplays.Length; displayIdx++) {
                    DropdownUtils.CreateDropdownItem(2, displayIdx, $"Display {displayIdx+1}: {connectedDisplays[displayIdx].FriendlyName}", videoDropdownManager.dropdownItemPrefab, videoDropdownManager.monitorDropdownBody);
                }

                /* no longer used attempt at getting connected monitor details
                SelectQuery Sq = new SelectQuery("Win32_DesktopMonitor");
                ManagementObjectSearcher objOSDetails = new ManagementObjectSearcher(Sq);
                ManagementObjectCollection osDetailsCollection = objOSDetails.Get();

                int i = 1;

                foreach (ManagementObject mo in osDetailsCollection) {
                    Debug.Log(string.Format("Name : {0}", (string)mo["Name"]));
                    Debug.Log(string.Format("Availability: {0}", (ushort)mo["Availability"]));
                    Debug.Log(string.Format("Caption: {0}", (string)mo["Caption"]));
                    Debug.Log(string.Format("ConfigManagerUserConfig: {0}", mo["ConfigManagerUserConfig"].ToString()));
                    Debug.Log(string.Format("CreationClassName : {0}", (string)mo["CreationClassName"]));
                    Debug.Log(string.Format("Description: {0}", (string)mo["Description"]));
                    Debug.Log(string.Format("DeviceID : {0}", (string)mo["DeviceID"]));
                    Debug.Log(string.Format("ErrorCleared: {0}", (string)mo["ErrorCleared"]));
                    Debug.Log(string.Format("ErrorDescription : {0}", (string)mo["ErrorDescription"]));
                    Debug.Log(string.Format("ConfigManagerUserConfig: {0}", mo["ConfigManagerUserConfig"].ToString()));
                    Debug.Log(string.Format("LastErrorCode : {0}", mo["LastErrorCode"]).ToString());
                    Debug.Log(string.Format("MonitorManufacturer : {0}", mo["MonitorManufacturer"]).ToString());
                    Debug.Log(string.Format("PNPDeviceID: {0}", (string)mo["PNPDeviceID"]));
                    Debug.Log(string.Format("MonitorType: {0}", (string)mo["MonitorType"]));
                    Debug.Log(string.Format("PixelsPerXLogicalInch : {0}", mo["PixelsPerXLogicalInch"].ToString()));
                    Debug.Log(string.Format("PixelsPerYLogicalInch: {0}", mo["PixelsPerYLogicalInch"].ToString()));
                    Debug.Log(string.Format("ScreenHeight: {0}", mo["ScreenHeight"].ToString()));
                    Debug.Log(string.Format("ScreenWidth : {0}", mo["ScreenWidth"]).ToString());
                    Debug.Log(string.Format("Status : {0}", (string)mo["Status"]));
                    Debug.Log(string.Format("SystemCreationClassName : {0}", (string)mo["SystemCreationClassName"]));
                    Debug.Log(string.Format("SystemName: {0}", (string)mo["SystemName"]));

                    Debug.Log($"-------- END OF LINE HERE {i} --------");
                    i += 1;
                }

                Display[] connectedMonitors = ReturnConnectedMonitors();

                for (int i = fullscreenResolutions.Length - 1; i > 0; i--) {
                    CreateDropdownItem(videoDropdownManager.dropdownItemPrefab, videoDropdownManager.resolutionDropdownBody, fullscreenResolutions[i].ToString());
                }
                */
            }
            /// <summary>
            /// Loads all available anti aliasing options into anti aliasing dropdown from AntiAliasType enum.
            /// </summary>
            public static void InitAntiAliasDropdownItems() {
                DropdownUtils.CreateDropdownItems_Loop(VideoDropdowns.ANTI_ALIASING, videoDropdownManager.dropdownItemPrefab, videoDropdownManager.antiAliasDropdownBody, AntiAliasType.NONE, AntiAliasType.SMAA, AntiAliasType.FXAA, AntiAliasType.TAA);
            }
        }
    }
}
