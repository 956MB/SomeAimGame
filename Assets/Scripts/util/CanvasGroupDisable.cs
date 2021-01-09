using UnityEngine;

namespace SomeAimGame.Utilities {
    public class CanvasGroupDisable : MonoBehaviour {
        void Start() {
            Util.SetCanvasGroupState(gameObject.GetComponent<CanvasGroup>(), false);
        }
    }
}
