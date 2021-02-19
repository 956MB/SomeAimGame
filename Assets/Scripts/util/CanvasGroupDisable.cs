using UnityEngine;

namespace SomeAimGame.Utilities {
    public class CanvasGroupDisable : MonoBehaviour {
        void Start() {
            Util.SetCanvasGroupState_DisableHover(gameObject.GetComponent<CanvasGroup>(), false);
        }
    }
}
