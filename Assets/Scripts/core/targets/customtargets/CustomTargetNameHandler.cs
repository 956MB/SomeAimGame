using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SomeAimGame.Targets {
    public class CustomTargetNameHandler : MonoBehaviour {
        public TMP_InputField customTargetNameInput;

        public void SetCustomColorName(string inputString) {
            EditCustomTarget.SetCustomTargetName(inputString);

            //customTargetNameInput.Select();
            //string nameString = customTargetNameInput.text.ToString();

            //customTargetNameInput.text = $"\"{inputString}\"";
        }
    }
}
