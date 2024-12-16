using System;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class BetDisplay : MonoBehaviour {
    public Action<int> BetSubmitted;

    private TMP_InputField input;
    private TextMeshProUGUI display;
    private UnityEngine.UI.Button submit;
    private int currentBet = 0;

    void Awake() {
        input = GetComponentInChildren<TMP_InputField>();
    }

    // A Delete function that deletes each child, outside 
    // the scope of the gameObject iterative list
    void KillChildren() {
        // Create a temporary list to cage the children
        var children = new List<Transform>();
        foreach (Transform child in gameObject.transform) {
            // Add the child to the list
            children.Add(child);
        }

        // Now destroy each child in the cage
        foreach (Transform child in children) {
            Debug.Log($"Destroying: {child.name}");
            GameObject.DestroyImmediate(child.gameObject);
        }
    }

    public void SubmitBet() {
        int betValue;
        if (Int32.TryParse(input.text, out betValue)) {
            Debug.Log("Bet Submitted (this may do nothing)");
            Debug.Log("Bet Value: " + betValue);
            BetSubmitted?.Invoke(betValue);
        }
    }

    TMP_InputField CreateInput() {
        GameObject inputObject = new GameObject("Input");
        inputObject.AddComponent<UnityEngine.UI.Image>().color = new Color32(33, 75, 38, 255);
        // Not Working
        //inputObject.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("unity_builtin_extra/InputFieldBackground");
        TMP_InputField inputField = inputObject.AddComponent<TMP_InputField>();
        inputField.contentType = TMP_InputField.ContentType.IntegerNumber;

        RectTransform inputRect = inputObject.GetComponent<RectTransform>();

        GameObject textArea = new GameObject("Text Area");
        RectTransform textAreaRect = textArea.AddComponent<RectTransform>();
        textAreaRect.SetParent(inputRect);
        textAreaRect.anchorMin = new Vector2(0, 0);
        textAreaRect.anchorMax = new Vector2(1, 1);
        textAreaRect.pivot = new Vector2(0.5f, 0.5f);

        GameObject placeholder = new GameObject("Placeholder");
        TextMeshProUGUI placeholderText = placeholder.AddComponent<TextMeshProUGUI>();
        // Not working
        //placeholderText.font = Resources.Load<TMP_FontAsset>("LiberationSans SDF");
        placeholderText.text = "Enter Bet...";
        placeholderText.fontStyle = TMPro.FontStyles.Bold | TMPro.FontStyles.Italic;
        placeholderText.fontSize = 14;
        placeholderText.textWrappingMode = TextWrappingModes.NoWrap;
        placeholderText.alignment = TextAlignmentOptions.MidlineLeft;

        inputField.placeholder = placeholderText;

        RectTransform placeholderRect = placeholder.GetComponent<RectTransform>();
        placeholderRect.SetParent(textAreaRect);
        placeholderRect.offsetMin = new Vector2(0, 0);
        placeholderRect.offsetMax = new Vector2(0, 0);
        placeholderRect.anchorMin = new Vector2(0, 0);
        placeholderRect.anchorMax = new Vector2(1, 1);
        placeholderRect.pivot = new Vector2(0.5f, 0.5f);
        
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
        textRect.anchorMin = new Vector2(0, 0);
        textRect.anchorMax = new Vector2(1, 1);
        textRect.pivot = new Vector2(0.5f, 0.5f);

        inputObject.transform.SetParent(gameObject.transform);
        inputRect.sizeDelta = new Vector2(160, 30);

        textAreaRect.offsetMin = new Vector2(10, 7);
        textAreaRect.offsetMax = new Vector2(-10, -7);

        return(inputField);
    }

    UnityEngine.UI.Button CreateSubmit() {
        GameObject submitObject = new GameObject("Submit");
        submitObject.AddComponent<UnityEngine.UI.Image>().color = new Color32(128, 0, 0, 255);
        UnityEngine.UI.Button submitButton = submitObject.AddComponent<UnityEngine.UI.Button>();
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
        submitRect.sizeDelta = new Vector2(160, 30);

        return(submitButton);
    }


    TextMeshProUGUI CreateDisplay() {
        GameObject displayObject = new GameObject("Display");
        
        // Add the TMP Text (UI) component
        TextMeshProUGUI text = displayObject.AddComponent<TextMeshProUGUI>();
        text.text = "Current Bet:\n$";
        text.enableAutoSizing = true;
        text.fontStyle = FontStyles.Bold;
        text.alignment = TextAlignmentOptions.TopLeft;
        text.textWrappingMode |= TextWrappingModes.Normal;

        // Add the Rect Transform component
        RectTransform displayRect = displayObject.GetComponent<RectTransform>();
        
        displayObject.transform.SetParent(gameObject.transform);
        displayRect.sizeDelta = new Vector2(160, 50);

        return(text);
    }

    void Reset() {
        KillChildren();
        Debug.Log("Resetting Bet Display...");
        
        VerticalLayoutGroup layout = gameObject.AddComponent<VerticalLayoutGroup>() ?? gameObject.GetComponent<VerticalLayoutGroup>();
        layout.spacing = 10;
        layout.childAlignment = TextAnchor.UpperCenter;

        RectTransform rect = gameObject.GetComponent<RectTransform>();
        rect.localPosition = new Vector3(-697, -375, -5);
        rect.sizeDelta = new Vector2(0, 0);
        
        display = CreateDisplay();
        input = CreateInput();
        submit = CreateSubmit();
    }

    void Start() {
        Reset();
        
        submit.onClick.AddListener(() => { Debug.Log("Fuck This Shit");});
        submit.onClick.AddListener(SubmitBet);
    }

    void Update() {
        if (display) {
            display.text = "Current Bet:\n$" + currentBet;
        }
    }
}