using System;
using UnityEngine;
using TMPro;

public class BetDisplay : MonoBehaviour {
    public Action<int> BetSubmitted;

    private TMP_InputField input;

    void Awake() {
        input = GetComponentInChildren<TMP_InputField>();
    }

    public void SubmitBet() {
        int betValue;
        if (Int32.TryParse(input.text, out betValue)) {
            Debug.Log("Bet Submitted (this may do nothing)");
            Debug.Log("Bet Value: " + betValue);
            BetSubmitted?.Invoke(betValue);
        }
    }

    void CreateInput() {
        GameObject inputObject = new GameObject("Input");
        inputObject.AddComponent<UnityEngine.UI.Image>().color = new Color32(33, 75, 38, 255);
        // Not Working
        //inputObject.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("TMP_InputFieldBackground");
        TMP_InputField inputField = inputObject.AddComponent<TMP_InputField>();
        RectTransform inputRect = inputObject.GetComponent<RectTransform>();

        GameObject textArea = new GameObject("Text Area");
        RectTransform textAreaRect = textArea.AddComponent<RectTransform>();
        textAreaRect.SetParent(inputRect);
        textAreaRect.offsetMin = new Vector2(10, 7);
        textAreaRect.offsetMax = new Vector2(-10, -6);

        GameObject placeholder = new GameObject("Placeholder");
        TextMeshProUGUI placeholderText = placeholder.AddComponent<TextMeshProUGUI>();
        // Not working
        //placeholderText.font = Resources.Load<TMP_FontAsset>("LiberationSans SDF");
        placeholderText.text = "Enter Bet...";
        placeholderText.fontStyle = TMPro.FontStyles.Bold | TMPro.FontStyles.Italic;
        placeholderText.fontSize = 14;
        placeholderText.textWrappingMode = TextWrappingModes.PreserveWhitespaceNoWrap;
        placeholderText.alignment = TextAlignmentOptions.MidlineLeft;

        inputField.placeholder = placeholderText;

        RectTransform placeholderRect = placeholder.GetComponent<RectTransform>();
        placeholderRect.SetParent(textAreaRect);
        placeholderRect.offsetMin = new Vector2(0, 0);
        placeholderRect.offsetMax = new Vector2(0, 0);

        inputField.textViewport = textAreaRect;

        GameObject text = new GameObject("Text");
        TextMeshProUGUI textComponent = text.AddComponent<TextMeshProUGUI>();
        // Not Working
        //textComponent.font = Resources.Load<TMP_FontAsset>("LiberationSans SDF");
        textComponent.fontSize = 14;
        textComponent.textWrappingMode = TextWrappingModes.PreserveWhitespaceNoWrap;
        textComponent.alignment = TextAlignmentOptions.MidlineLeft;

        inputField.textComponent = textComponent;

        RectTransform textRect = text.GetComponent<RectTransform>();
        textRect.SetParent(textAreaRect);
        textRect.offsetMin = new Vector2(0, 0);
        textRect.offsetMax = new Vector2(0, 0);

        inputObject.transform.SetParent(gameObject.transform);
        inputRect.anchoredPosition = new Vector2(-697, -444.5f);
        inputRect.sizeDelta = new Vector2(160, 30);
    }

    void CreateSubmit() {
        GameObject submitObject = new GameObject("Submit");
        submitObject.AddComponent<UnityEngine.UI.Image>().color = new Color32(128, 0, 0, 255);
        UnityEngine.UI.Button submitButton = submitObject.AddComponent<UnityEngine.UI.Button>();
        submitButton.onClick.AddListener(SubmitBet);
        RectTransform submitRect = submitObject.GetComponent<RectTransform>();

        GameObject submitText = new GameObject("Text (TMP)");
        TextMeshProUGUI submitTextComponent = submitText.AddComponent<TextMeshProUGUI>();
        // Not Working
        //submitTextComponent.font = Resources.Load<TMP_FontAsset>("LiberationSans SDF");
        submitTextComponent.text = "BET";
        submitTextComponent.fontStyle = TMPro.FontStyles.Bold;
        submitTextComponent.fontSize = 24;
        submitTextComponent.textWrappingMode = TextWrappingModes.PreserveWhitespaceNoWrap;
        submitTextComponent.alignment = TextAlignmentOptions.Center;

        RectTransform submitTextRect = submitText.GetComponent<RectTransform>();
        submitTextRect.SetParent(submitRect);
        submitTextRect.offsetMin = new Vector2(0, 0);
        submitTextRect.offsetMax = new Vector2(0, 0);

        submitObject.transform.SetParent(gameObject.transform);
        submitRect.anchoredPosition = new Vector2(-697, -479.9f);
        submitRect.sizeDelta = new Vector2(160, 30);
    }

    void Reset() {
        Debug.Log("Resetting Bet Display...");
        CreateInput();
        CreateSubmit();  
    }

    void Start() {

    }

    void Update() {

    }
}