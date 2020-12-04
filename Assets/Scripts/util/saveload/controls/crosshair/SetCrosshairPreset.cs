using UnityEngine;
using UnityEngine.EventSystems;

using SomeAimGame.Utilities;

public class SetCrosshairPreset : MonoBehaviour, IPointerClickHandler {
    public void OnPointerClick(PointerEventData pointerEventData) {
        string clickedPresetName = pointerEventData.pointerCurrentRaycast.gameObject.name;

        CrosshairPresets.SetPresetCrosshair(clickedPresetName);

        NotificationHandler.ShowTimedNotification_Translated("presetcrosshairset", "", InterfaceColors.notificationColorGreen);
    }
}
