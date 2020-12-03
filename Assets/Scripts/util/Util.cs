using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

namespace SomeAimGame.Utilities {
    public class Util : MonoBehaviour {
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

        /// Looping utils ///

        public static class GameObjectLoops {
            /// <summary>
            /// Sets all supplied params TMP_Text elements (textElements) to supplied Colors32 (setColor).
            /// </summary>
            /// <param name="setColor"></param>
            /// <param name="textElements"></param>
            public static void Util_ClearTMPTextColor(Color32 setColor, params TMP_Text[] textElements) {
                foreach (TMP_Text text in textElements) {
                    text.color = setColor;
                }
            }
            /// <summary>
            /// Sets all supplied params GameObjects transforms (transformObjects) to supplied Vector3 (setVector).
            /// </summary>
            /// <param name="setVector"></param>
            /// <param name="transformObjects"></param>
            public static void Util_SetObjectsLocalScale(Vector3 setVector, params GameObject[] transformObjects) {
                foreach (GameObject objTransform in transformObjects) {
                    objTransform.transform.localScale = setVector;
                }
            }
            /// <summary>
            /// Sets all supplied params Images gameobjects (imageObjects) to supplied bool (setActive).
            /// </summary>
            /// <param name="setActive"></param>
            /// <param name="imageObjects"></param>
            public static void Util_ImagesSetActive(bool setActive, params Image[] imageObjects) {
                foreach (Image imageObject in imageObjects) {
                    imageObject.transform.gameObject.SetActive(setActive);
                }
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
                for (int i = 0; i < specialLimit; i++) {
                    videoPlayers[i].clip = setVideoClips[i];
                }
            }
            /// <summary>
            /// Sets supplied video clips in VideoClip array (videoClips) from VideoClip array clips (setVideoClips). (specialLimit) is how many VideoClip.
            /// </summary>
            /// <param name="specialLimit"></param>
            /// <param name="setVideoClips"></param>
            /// <param name="videoClips"></param>
            public static void Util_SetVideoClips(int specialLimit, VideoClip[] setVideoClips, params VideoClip[] videoClips) {
                for (int i = 0; i < specialLimit; i++) {
                    videoClips[i] = setVideoClips[i];
                }
            }
            /// <summary>
            /// Sets supplied video players aspect ratio in VideoPlayer array (videoPlayers) to supplied VideoAspectRatio (setRatio).
            /// </summary>
            /// <param name="setRatio"></param>
            /// <param name="videoPlayers"></param>
            public static void Util_SetVideoPlayersAscpectRatio(VideoAspectRatio setRatio, params VideoPlayer[] videoPlayers) {
                foreach (VideoPlayer player in videoPlayers) {
                    player.aspectRatio = setRatio;
                }
            }
            /// <summary>
            /// Plays all supplied video players in VideoPlayer array (videoPlayers).
            /// </summary>
            /// <param name="videoPlayers"></param>
            public static void Util_PlayVideoPlayers(params VideoPlayer[] videoPlayers) {
                foreach (VideoPlayer player in videoPlayers) {
                    player.Play();
                }
            }

        }
    }
}