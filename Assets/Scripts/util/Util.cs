﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

namespace SomeAimGame.Utilities {
    public class Util : MonoBehaviour {
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
        /// Copies supplied string (copyString) to clipboard.
        /// </summary>
        /// <param name="copyString"></param>
        public static void CopyToClipboard(string copyString) {
            GUIUtility.systemCopyBuffer = copyString;
        }

        /// <summary>
        /// Sets supplied CanvasGroup (setCanvasGroup) state (enabled/disabled) from supplied bool (isEnabled).
        /// </summary>
        /// <param name="setCanvasGroup"></param>
        /// <param name="isEnabled"></param>
        public static void CanvasGroupState(CanvasGroup setCanvasGroup, bool isEnabled) {
            setCanvasGroup.alpha          = isEnabled ? 1f : 0.35f;
            setCanvasGroup.interactable   = isEnabled;
            setCanvasGroup.blocksRaycasts = isEnabled;
        }

        // Looping utils //

        /// <summary>
        /// Class holding various looping methods for setting things on TMP_Text/Image/GameObject.
        /// </summary>
        public static class GameObjectLoops {
            /// <summary>
            /// Sets all supplied params TMP_Text elements (textElements) to supplied Colors32 (setColor).
            /// </summary>
            /// <param name="setColor"></param>
            /// <param name="textElements"></param>
            public static void Util_ClearTMPTextColor(Color32 setColor, params TMP_Text[] textElements) {
                foreach (TMP_Text text in textElements) { text.color = setColor; }
            }
            /// <summary>
            /// Sets all supplied params GameObjects transforms (transformObjects) to supplied Vector3 (setVector).
            /// </summary>
            /// <param name="setVector"></param>
            /// <param name="transformObjects"></param>
            public static void Util_SetObjectsLocalScale(Vector3 setVector, params GameObject[] transformObjects) {
                foreach (GameObject objTransform in transformObjects) { objTransform.transform.localScale = setVector; }
            }
            /// <summary>
            /// Sets all supplied params Images gameobjects (imageObjects) to supplied bool (setActive).
            /// </summary>
            /// <param name="setActive"></param>
            /// <param name="imageObjects"></param>
            public static void Util_ImagesSetActive(bool setActive, params Image[] imageObjects) {
                foreach (Image imageObject in imageObjects) { imageObject.transform.gameObject.SetActive(setActive); }
            }
            /// <summary>
            /// Calls ScrollRectExtension.ScrollToTop() with all supplied GameObjects (scrollViewObjects).
            /// </summary>
            /// <param name="scrollViewObjects"></param>
            public static void Util_ResetScrollViews_Top(params GameObject[] scrollViewObjects) {
                foreach (GameObject scrollView in scrollViewObjects) { ScrollRectExtension.ScrollToTop(scrollView.GetComponent<ScrollRect>()); ; }
            }
            /// <summary>
            /// Calls ScrollRectExtension.ScrollToBottom() with all supplied GameObjects (scrollViewObjects).
            /// </summary>
            /// <param name="scrollViewObjects"></param>
            public static void Util_ResetScrollViews_Bottom(params GameObject[] scrollViewObjects) {
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
            public static void Util_SetVideoPlayerClips(int specialLimit, VideoClip[] setVideoClips, params VideoPlayer[] videoPlayers) {
                for (int i = 0; i < specialLimit; i++) { videoPlayers[i].clip = setVideoClips[i]; }
            }
            /// <summary>
            /// Sets supplied video clips in VideoClip array (videoClips) from VideoClip array clips (setVideoClips). (specialLimit) is how many VideoClip.
            /// </summary>
            /// <param name="specialLimit"></param>
            /// <param name="setVideoClips"></param>
            /// <param name="videoClips"></param>
            public static void Util_SetVideoClips(int specialLimit, VideoClip[] setVideoClips, params VideoClip[] videoClips) {
                for (int i = 0; i < specialLimit; i++) { videoClips[i] = setVideoClips[i]; }
            }
            /// <summary>
            /// Sets supplied video players aspect ratio in VideoPlayer array (videoPlayers) to supplied VideoAspectRatio (setRatio).
            /// </summary>
            /// <param name="setRatio"></param>
            /// <param name="videoPlayers"></param>
            public static void Util_SetVideoPlayersAscpectRatio(VideoAspectRatio setRatio, params VideoPlayer[] videoPlayers) {
                foreach (VideoPlayer player in videoPlayers) { player.aspectRatio = setRatio; }
            }
            /// <summary>
            /// Plays all supplied video players in VideoPlayer array (videoPlayers).
            /// </summary>
            /// <param name="videoPlayers"></param>
            public static void Util_PlayVideoPlayers(params VideoPlayer[] videoPlayers) {
                foreach (VideoPlayer player in videoPlayers) { player.Play(); }
            }
        }
    }
}