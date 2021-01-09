using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class I18nTextTranslator : MonoBehaviour {
    public string TextId;

    void Start() {
        TMP_Text text = GetComponent<TMP_Text>();

        if (text != null) {
            if (TextId == "ISOCode") {
                text.SetText(I18n.GetLanguage());
            } else {
                text.SetText(I18n.Fields[TextId]);
            }
        }
    }

    /// <summary>
    /// Returns the appropriate translated text from supplied string id (textId).
    /// </summary>
    /// <param name="textId"></param>
    /// <returns></returns>
    public static string SetTranslatedText(string textId) {
        try {
            return I18n.Fields[textId];
        } catch (KeyNotFoundException) {
            //Debug.LogError($"KNFE: KEY '{textId}' NOT FOUND;");
            return $"{textId}";
        }
    }
}