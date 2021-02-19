using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using SomeAimGame.Utilities;
using SomeAimGame.SFX;

namespace SomeAimGame.Targets {
    public class CustomTargetButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        public GameObject borderObj;
        private bool hoverEnabled;

        //private static CustomTargetButtonHover customHover;
        //void Awake() { customHover = this; }

        //private void Start() {
        //    DisableHover();
        //}

        public void OnPointerEnter(PointerEventData pointerEventData) {
            if (hoverEnabled) {
                borderObj.SetActive(true);

                string customTargetName = CustomTargetColorUtil.GetCustomTargetName(pointerEventData.pointerCurrentRaycast.gameObject.GetComponent<CustomTargetColor>().ColorIndex);
                CustomTargetPanel.SetSelectedTargetText($"{customTargetName} :: {pointerEventData.pointerCurrentRaycast.gameObject.GetComponent<CustomTargetColor>().ColorIndex}");
            
                SFXManager.CheckPlayHover_Regular();
            }
        }

        public void OnPointerExit(PointerEventData pointerEventData) {
            if (hoverEnabled) {
                borderObj.SetActive(false);

                CustomTargetPanel.ResetSelectedTargetText();
            }
        }

        public void EnableHover() {
            hoverEnabled = true;
            borderObj.SetActive(false);
        }
        public void DisableHover() {
            hoverEnabled = false;
            borderObj.SetActive(true);
        }
    }
}
