using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

using SomeAimGame.Utilities;

namespace SomeAimGame.Targets {
    public class CustomTargetPanel : MonoBehaviour {
        public static bool customTargetPanelOpen;
        public GameObject customTargetPanelObject, newCustomtTargetPrefab, openCustomTargetPanelButton;
        public TMP_Text selectedTargetText;
        public GridLayoutGroup targetButtonsGridGroup;

        public static Object testTargetPrefab;
        public Material testTargetMaterial;
        public static GameObject openCustomTargetButtonInstantiated;
        public static GameObject newCreatedCustomTargetButton;
        //Texture2D thumbnailTexture2D;
        //Sprite thumbnailSprite;

        public static CustomTargetPanel customTargetPanel;
        private void Awake() { customTargetPanel = this; }

        private void Start() {
            SetCustomTargetPanelState(false);
        }

        /// <summary>
        /// Opens custom target color panel.
        /// </summary>
        public static void OpenCustomTargetPanel() {
            if (!customTargetPanelOpen) { SetCustomTargetPanelState(true); }

            EditCustomTarget.SetCustomTargetName($"CustomColor{CustomTargetColorUtil.GetCustomTargetsCount()}", true);
            // Load from asset preview(backup).
            //Texture2D thumbnailTexture2D = AssetPreview.GetAssetPreview(testTargetPrefab);
            //Sprite thumbnailSprite       = Sprite.Create(thumbnailTexture2D, new Rect(0.0f, 0.0f, thumbnailTexture2D.width, thumbnailTexture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
            //customTargetPanel.newCustomtTargetPrefab.transform.GetComponent<Image>().sprite = thumbnailSprite;

            // runtime preview generator method:
            //Texture2D thumbnailTexture2D = RuntimePreviewGenerator.GenerateModelPreview(customTargetPanel.testTargetTransoform, 82, 82);
            newCreatedCustomTargetButton = CreateCustomTargetButton(customTargetPanel.newCustomtTargetPrefab, 0, false);
            CustomTargetColorUtil.CreateRuntimeThumbnail(customTargetPanel.testTargetMaterial, newCreatedCustomTargetButton);
            newCreatedCustomTargetButton.GetComponent<CustomTargetButtonHover>().DisableHover();
            //Texture2D thumbnailTexture2D = RuntimePreviewGenerator.GenerateMaterialPreview(customTargetPanel.testTargetMaterial, PrimitiveType.Sphere, 82, 82);
            //Sprite thumbnailSprite       = Sprite.Create(thumbnailTexture2D, new Rect(0.0f, 0.0f, thumbnailTexture2D.width, thumbnailTexture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
            //newCreatedCustomTargetButton.transform.GetComponent<Image>().sprite = thumbnailSprite;
            SetNewCustomTargetButtonState(true);
        }
        /// <summary>
        /// Closes custom target color panel.
        /// </summary>
        public static void CloseCustomTargetPanel() {
            if (customTargetPanelOpen) { SetCustomTargetPanelState(false); }
        }

        /// <summary>
        /// Sets state of custom target color panel to supplied bool (state).
        /// </summary>
        /// <param name="state"></param>
        public static void SetCustomTargetPanelState(bool state) {
            customTargetPanelOpen = state;
            customTargetPanel.customTargetPanelObject.SetActive(state);
            //customTargetPanel.newCreatedCustomTargetButton.SetActive(state);
            Destroy(newCreatedCustomTargetButton);
            if (openCustomTargetButtonInstantiated != null) { Util.SetCanvasGroupState_DisableHover(openCustomTargetButtonInstantiated.GetComponent<CanvasGroup>(), !state); }
        }

        /// <summary>
        /// Loop and load all saved custom target colors and create new buttons for each.
        /// </summary>
        public static void LoadAllCustomTargets(int customTargetsLength) {
            for (int i = 0; i < customTargetsLength; i++) {
                CreateCustomTargetButton(customTargetPanel.newCustomtTargetPrefab, i, true);
            }
        }

        /// <summary>
        /// Creates new custom target button from supplied prefab (customTargetPrefab) and sets color index from supplied int (customColorIndex).
        /// </summary>
        /// <param name="customTargetPrefab"></param>
        /// <param name="customColorIndex"></param>
        /// <returns></returns>
        public static GameObject CreateCustomTargetButton(GameObject customTargetPrefab, int customColorIndex, bool setHoverEnabled) {
            GameObject customTargetObj = Instantiate(customTargetPrefab);
            customTargetObj.GetComponent<CustomTargetColor>().ColorIndex = customColorIndex;
            customTargetObj.name = $"TargetColor-{CustomTargetColorUtil.customColorNameStrings[customColorIndex]}";
            // TODO: set target image with correct color vvv
            CustomTargetColorUtil.CreateRuntimeThumbnail(customTargetPanel.testTargetMaterial, customTargetObj);
            if (setHoverEnabled) { customTargetObj.GetComponent<CustomTargetButtonHover>().EnableHover(); }

            Util.AppendToGridLayoutGroup(customTargetObj, customTargetPanel.targetButtonsGridGroup, true);

            return customTargetObj;
        }

        /// <summary>
        /// Creates 'add new custom target button' and appends to target colors grid group.
        /// </summary>
        public static void CreateAddNewCustomTargetButton() {
            openCustomTargetButtonInstantiated = Instantiate(customTargetPanel.openCustomTargetPanelButton);
            Util.AppendToGridLayoutGroup(openCustomTargetButtonInstantiated, customTargetPanel.targetButtonsGridGroup);
        }

        /// <summary>
        /// Sets selected target text in target colors group to supplied string (setText).
        /// </summary>
        /// <param name="setText"></param>
        public static void SetSelectedTargetText(string setText) { customTargetPanel.selectedTargetText.SetText($"//  {setText}"); }
        /// <summary>
        /// Resets selected target text in target colors group to saved string value in CosmeticsSaveSystem.
        /// </summary>
        public static void ResetSelectedTargetText() { customTargetPanel.selectedTargetText.SetText(CosmeticsSaveSystem.activeTargetColorText); }

        /// <summary>
        /// Sets currently created custom target button border state to supplied bool (disableHover).
        /// </summary>
        /// <param name="disableHover"></param>
        public static void SetNewCustomTargetButtonState(bool disableHover) {
            if (disableHover) {
                newCreatedCustomTargetButton.GetComponent<CustomTargetButtonHover>().DisableHover();
            } else {
                newCreatedCustomTargetButton.GetComponent<CustomTargetButtonHover>().EnableHover();
            }
        }
    }
}
