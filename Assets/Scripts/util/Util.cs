using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Globalization;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

using SomeAimGame.Skybox;
using SomeAimGame.Gamemode;
using SomeAimGame.Targets;
using SomeAimGame.SFX;

namespace SomeAimGame.Utilities {
    public class Util : MonoBehaviour {
        public static Vector3 disabledSubMenuScrollView = new Vector3(0, 0, 0);
        public static Vector3 enabledSubMenuScrollView  = new Vector3(1, 1, 1);

        private static Util util;
        void Awake() { util = this; }

        /// <summary>
        /// Returns if two supplied RectTransform (rectTrans1, rectTrans2) overlap.
        /// </summary>
        /// <param name="rectTrans1"></param>
        /// <param name="rectTrans2"></param>
        /// <returns></returns>
        public static bool IfRectOverlaps(RectTransform rectTrans1, RectTransform rectTrans2) {
            Rect rect1 = new Rect(rectTrans1.localPosition.x, rectTrans1.localPosition.y, rectTrans1.rect.width, rectTrans1.rect.height);
            Rect rect2 = new Rect(rectTrans2.localPosition.x, rectTrans2.localPosition.y, rectTrans2.rect.width, rectTrans2.rect.height);

            return rect1.Overlaps(rect2, true);
            //return rect2.Overlaps(rect1);
        }

        /// <summary>
        /// Prints all Vector3 list (list) elements in string.
        /// </summary>
        /// <param name="list"></param>
        public static void PrintVector3List(List<Vector3> list) {
            string targetList = "";
            for (int i = 0; i < list.Count; i++) {
                targetList += $" {list[i]}";
            }
            Debug.Log($"Vector3 List: {targetList}");
        }
        /// <summary>
        /// Prints all string list (list) elements in string.
        /// </summary>
        /// <param name="list"></param>
        public static void PrintStringList(List<string> list) {
            string targetList = "";
            for (int i = 0; i < list.Count; i++) {
                targetList += $" {list[i]}";
            }
            Debug.Log($"String List: {targetList}");
        }
        /// <summary>
        /// Splits supplied string (splitString) and returns string[].
        /// </summary>
        /// <param name="splitString"></param>
        /// <returns></returns>
        public static string[] SplitString(string splitString) {
            if (splitString != "") {
                if (splitString.Contains(",")) { return splitString.Split(','); }
                return new string[] {splitString};
            } else {
                return null;
            }
        }

        /// <summary>
        /// Appends supplied gameobject (appendObj) to supplied grid layout group (appendGroup).
        /// </summary>
        /// <param name="appendObj"></param>
        /// <param name="appendGroup"></param>
        /// <param name="setToIndex"></param>
        public static void AppendToGridLayoutGroup(GameObject appendObj, GridLayoutGroup appendGroup, bool setToIndex = false) {
            appendObj.transform.SetParent(appendGroup.transform);
            appendObj.transform.localScale    = new Vector3( 1.0f, 1.0f, 1.0f );
            appendObj.transform.localPosition = Vector3.zero;
            if (setToIndex) { appendObj.transform.SetSiblingIndex(appendGroup.transform.childCount - 2); }
        }

        /// <summary>
        /// Returns Color from supplied hex string representaion.
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static Color HexToColor(string hexString) {
            if (hexString.IndexOf('#') != -1) { hexString = hexString.Replace("#", ""); }

            int r = int.Parse(hexString.Substring(0, 2), NumberStyles.AllowHexSpecifier);
            int g = int.Parse(hexString.Substring(2, 2), NumberStyles.AllowHexSpecifier);
            int b = int.Parse(hexString.Substring(4, 2), NumberStyles.AllowHexSpecifier);

            return new Color(r, g, b);
        }
        /// <summary>
        /// Returns hex string representation of supplied Color (color). 
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ColorToHex(Color color) {
            return ColorUtility.ToHtmlStringRGBA(color);
        }

        /// <summary>
        /// Performs forced refresh on supplied scrollview group (rootGroup).
        /// </summary>
        /// <param name="rootGroup"></param>
        public static void RefreshRootLayoutGroup(GameObject rootGroup) {
            LayoutRebuilder.ForceRebuildLayoutImmediate(rootGroup.gameObject.GetComponent<RectTransform>());
        }

        /// <summary>
        /// Checks if supplied string (str) contains only digits.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool DigitsOnly(string str) {
            foreach (char c in str) {
                if (c < '0' || c > '9') return false;
            }
            return true;
        }

        /// <summary>
        /// Returns greatest common denominator from supplied ints (x/y)(width/height).
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static int GCD(int x, int y) {
            int rem;
            while( y != 0 ) {
                rem = x % y;
                x = y;
                y = rem;
            }

            return x;
        }

        /// <summary>
        /// Returns aspect ratio string from supplied ints (x/y)(width/height).
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static string ReturnAspectRatio_string(int x, int y) { return string.Format("{0}:{1}",x/GCD(x,y), y/GCD(x,y)); }

        /// <summary>
        /// Copies supplied string (copyString) to clipboard.
        /// </summary>
        /// <param name="copyString"></param>
        public static void CopyToClipboard(string copyString) { GUIUtility.systemCopyBuffer = copyString; }

        /// <summary>
        /// Sets supplied CanvasGroup (setCanvasGroup) state (enabled/disabled) from supplied bool (isEnabled).
        /// </summary>
        /// <param name="setCanvasGroup"></param>
        /// <param name="isEnabled"></param>
        public static void SetCanvasGroupState_DisableHover(CanvasGroup setCanvasGroup, bool isEnabled) {
            setCanvasGroup.alpha          = isEnabled ? 1f : 0.35f;
            setCanvasGroup.interactable   = isEnabled;
            setCanvasGroup.blocksRaycasts = isEnabled;
        }
        /// <summary>
        ///  Sets supplied CanvasGroup (setCanvasGroup) state (enabled/disabled) and allows hover.
        /// </summary>
        /// <param name="setCanvasGroup"></param>
        public static void SetCanvasGroupState_EnableHover(CanvasGroup setCanvasGroup) { util.SetCanvasGroupState(setCanvasGroup, 0.35f, false, true); }

        /// <summary>
        /// Sets supplied CanvasGroup (setCanvasGroup) state with supplied alpha flaot (setAlpha), interact bool (setInteract) and blocks raycast bool (setBlocksRaycast).
        /// </summary>
        /// <param name="setCanvasGroup"></param>
        /// <param name="setAlpha"></param>
        /// <param name="setInteract"></param>
        /// <param name="setBlocksRaycast"></param>
        private void SetCanvasGroupState(CanvasGroup setCanvasGroup, float setAlpha, bool setInteract, bool setBlocksRaycast) {
            setCanvasGroup.alpha          = 0.35f;
            setCanvasGroup.interactable   = false;
            setCanvasGroup.blocksRaycasts = true;
        }

        /// <summary>
        /// Sets supplied value TMP_Text (valueText) and value placeholder TMP_Text (valueTextPlaceholder) to supplied float (value).
        /// </summary>
        /// <param name="valueText"></param>
        /// <param name="valueTextPlaceholder"></param>
        /// <param name="value"></param>
        public static void SetSliderOptionText(TMP_Text valueText, TMP_Text valueTextPlaceholder, float value) {
            valueText.SetText($"{value}");
            valueTextPlaceholder.SetText($"{value}");
        }

        /// <summary>
        /// Sets state of cursor to supplied CursorLockMode (setCursorMode) and visibility bool (setVisibility).
        /// </summary>
        /// <param name="setCursorMode"></param>
        /// <param name="setVisibility"></param>
        public static void SetCursorState(CursorLockMode setCursorMode, bool setVisibility) {
            Cursor.lockState = setCursorMode;
            Cursor.visible   = setVisibility;
        }

        public static void SetTextPlaceholderColors(TMP_Text mainText, TMP_Text placeholderText, Color32 setColor) {
            if (mainText.color != setColor && placeholderText.color != setColor) {
                mainText.color        = setColor;
                placeholderText.color = setColor;
            }
        }

        /// <summary>
        /// Checks if supplied string (value) matches supplied regex string (matchString).
        /// </summary>
        /// <param name="value"></param>
        /// <param name="matchString"></param>
        /// <returns></returns>
        public static bool ReturnRegexMatch(string value, string matchString) {
            Regex regex = new Regex(matchString);
            return regex.Match(value).Success;
        }

        #region setting change overloads

        public static void RefSetSettingChange(ref bool changeReady, ref KeyCode setting, KeyCode setKeycode) {            setting = setKeycode;  changeReady = true; }
        public static void RefSetSettingChange(ref bool changeReady, ref SFXType setting, SFXType setSFX) {                setting = setSFX;      changeReady = true; }
        public static void RefSetSettingChange(ref bool changeReady, ref GamemodeType setting, GamemodeType setGamemode) { setting = setGamemode; changeReady = true; }
        public static void RefSetSettingChange(ref bool changeReady, ref TargetType setting, TargetType setTarget, ref int colorSetting, int setColor) { setting = setTarget; colorSetting = setColor; changeReady = true; }
        public static void RefSetSettingChange(ref bool changeReady, ref SkyboxType setting, SkyboxType setSkybox) {       setting = setSkybox;   changeReady = true; }
        public static void RefSetSettingChange(ref bool changeReady, ref int setting, int setInt) {                        setting = setInt;      changeReady = true; }
        public static void RefSetSettingChange(ref bool changeReady, ref float setting, float setFloat) {                  setting = setFloat;    changeReady = true; }
        public static void RefSetSettingChange(ref bool changeReady, ref string setting, string setString) {               setting = setString;   changeReady = true; }
        public static void RefSetSettingChange(ref bool changeReady, ref bool setting, bool setBool) {                     setting = setBool;     changeReady = true; }

        #endregion

        #region looping utils

        /// <summary>
        /// Class holding various looping methods for setting things on TMP_Text/Image/GameObject.
        /// </summary>
        public static class GameObjectLoops {
            /// <summary>
            /// Sets all supplied params TMP_Text elements (textElements) to supplied Colors32 (setColor).
            /// </summary>
            /// <param name="setColor"></param>
            /// <param name="textElements"></param>
            public static void ClearTMPTextColor(Color32 setColor, params TMP_Text[] textElements) {
                //foreach (TMP_Text text in textElements) { text.color = setColor; }
                for (int i = 0; i < textElements.Length; i++) { textElements[i].color = setColor; }
            }
            public static void ClearButtonBackgrounds(Color32 setColor, params TMP_Text[] textElements) {
                foreach (TMP_Text text in textElements) { text.transform.parent.gameObject.GetComponent<Image>().color = setColor; }
            }
            /// <summary>
            /// Sets all supplied params GameObjects transforms (transformObjects) to supplied Vector3 (setVector).
            /// </summary>
            /// <param name="setVector"></param>
            /// <param name="transformObjects"></param>
            public static void SetObjectsLocalScale(Vector3 setVector, params GameObject[] transformObjects) {
                foreach (GameObject objTransform in transformObjects) { objTransform.transform.localScale = setVector; }
            }
            /// <summary>
            /// Sets all supplied params Images gameobjects (imageObjects) to supplied bool (setActive).
            /// </summary>
            /// <param name="setActive"></param>
            /// <param name="imageObjects"></param>
            public static void ImagesSetActive(bool setActive, params Image[] imageObjects) {
                foreach (Image imageObject in imageObjects) { imageObject.transform.gameObject.SetActive(setActive); }
            }
            /// <summary>
            /// Calls ScrollRectExtension.ScrollToTop() with all supplied GameObjects (scrollViewObjects).
            /// </summary>
            /// <param name="scrollViewObjects"></param>
            public static void ResetScrollViews_Top(params GameObject[] scrollViewObjects) {
                foreach (GameObject scrollView in scrollViewObjects) { ScrollRectExtension.ScrollToTop(scrollView.GetComponent<ScrollRect>()); ; }
            }
            /// <summary>
            /// Calls ScrollRectExtension.ScrollToBottom() with all supplied GameObjects (scrollViewObjects).
            /// </summary>
            /// <param name="scrollViewObjects"></param>
            public static void ResetScrollViews_Bottom(params GameObject[] scrollViewObjects) {
                foreach (GameObject scrollView in scrollViewObjects) { ScrollRectExtension.ScrollToBottom(scrollView.GetComponent<ScrollRect>()); ; }
            }
        }

        /// <summary>
        /// Class holding various looping methods for setting things on VideoPlayer/VideoClip
        /// </summary>
        public static class VideoLoops {
            /// <summary>
            /// Sets supplied video players clips in VideoPlayer array (videoPlayers) from VideoClip array clips (setVideoClips). specialLimit is how many VideoPlayer and VideoClip.
            /// </summary>
            /// <param name="specialLimit"></param>
            /// <param name="setVideoClips"></param>
            /// <param name="videoPlayers"></param>
            public static void SetVideoPlayerClips(int specialLimit, VideoClip[] setVideoClips, params VideoPlayer[] videoPlayers) {
                for (int i = 0; i < specialLimit; i++) { videoPlayers[i].clip = setVideoClips[i]; }
            }
            /// <summary>
            /// Sets supplied video clips in VideoClip array (videoClips) from VideoClip array clips (setVideoClips). (specialLimit) is how many VideoClip.
            /// </summary>
            /// <param name="specialLimit"></param>
            /// <param name="setVideoClips"></param>
            /// <param name="videoClips"></param>
            public static void SetVideoClips(int specialLimit, VideoClip[] setVideoClips, params VideoClip[] videoClips) {
                for (int i = 0; i < specialLimit; i++) { videoClips[i] = setVideoClips[i]; }
            }
            /// <summary>
            /// Sets supplied video players aspect ratio in VideoPlayer array (videoPlayers) to supplied VideoAspectRatio (setRatio).
            /// </summary>
            /// <param name="setRatio"></param>
            /// <param name="videoPlayers"></param>
            public static void SetVideoPlayersAscpectRatio(VideoAspectRatio setRatio, params VideoPlayer[] videoPlayers) {
                foreach (VideoPlayer player in videoPlayers) { player.aspectRatio = setRatio; }
            }
            /// <summary>
            /// Plays all supplied video players in VideoPlayer array (videoPlayers).
            /// </summary>
            /// <param name="videoPlayers"></param>
            public static void PlayVideoPlayers(params VideoPlayer[] videoPlayers) {
                foreach (VideoPlayer player in videoPlayers) { player.Play(); }
            }
        }

        #endregion
    }
}