using UnityEditor.VersionControl;
using UnityEngine;
using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.XR;
using Unity.VisualScripting;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ActionDisplay : MonoBehaviour {
    // Events that can be listened for by the Hand display manager
    public event Action Hit;
    public event Action Stand;
    public event Action Split;
    public event Action Insurance;
    public event Action DoubleDown;

    private const int GeneralSpacing = 20;
    private const int GeneralPadding = 10; 
    private const int FontSize = 24;

    private readonly (int width, int height, int spacing) primaryButton = (120, 60, 20);
    private readonly (int width, int height, int spacing) secondaryButton = (110, 55, 20);
    private readonly (int width, int height, int spacing) thirdButton = (120, 60, 0);
    private const int SpacerSize = 10;

    private Button HitButton;
    private Button StandButton;
    private Button SplitButton;
    private Button InsuranceButton;
    private Button DoubleDownButton;
    
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

    // Toggle a button from On to Off, or Off to On
    public void Toggle(String button) {
        switch(button) {
            case("Hit"):
                HitButton.enabled = !HitButton.enabled;
                break;
            case("Stand"):
                StandButton.enabled = !StandButton.enabled;
                break;
            case("Split"):
                SplitButton.enabled = !SplitButton.enabled;
                break;
            case("Double Down"):
                DoubleDownButton.enabled = !DoubleDownButton.enabled;
                break;
            case("Insurance"):
                InsuranceButton.enabled = !InsuranceButton.enabled;
                break;
        }
    }

    // Create a layout object
    private GameObject CreateLayout(string name, int spacing, bool isHorizontal) {
        GameObject obj = new GameObject(name);
        if (isHorizontal) {
            var layout = obj.AddComponent<HorizontalLayoutGroup>();
            layout.spacing = spacing;
            layout.childAlignment = TextAnchor.MiddleCenter;
        }
        else {
            var layout = obj.AddComponent<VerticalLayoutGroup>();
            layout.spacing = spacing;
            layout.childAlignment = TextAnchor.MiddleCenter;
        }
        return obj;
    }

    // Create a GameObject with a button
    private GameObject CreateButton(string name, (int width, int height) dimensions, Color32 color) {
        GameObject obj = new GameObject(name);
        // Add the image, and set the color
        obj.AddComponent<Image>().color = color;
        // Add the button
        obj.AddComponent<Button>();

        // Set the dimensions
        obj.GetComponent<RectTransform>().sizeDelta = new Vector2(dimensions.width, dimensions.height);

        // Add the text
        GameObject objText = new GameObject("Text (TMP)");
        var text = objText.AddComponent<TextMeshProUGUI>();
        text.fontSize = FontSize;
        text.text = name;
        text.alignment = TextAlignmentOptions.Center;

        // Set the width/height boundaries for the text
        objText.transform.SetParent(obj.transform);
        objText.GetComponent<RectTransform>().sizeDelta = new Vector2(dimensions.width, dimensions.height);

        return obj;
    }

    // Calculate & set the proper width and height
    private void AutoSize(GameObject obj) {
        // The dimensions of the layout container, with children
        float width = 0;
        float height = 0;

        // The layout type
        String type = obj.GetComponent<HorizontalLayoutGroup>() ? "Horizontal" : "Vertical";

        foreach (Transform child in obj.transform) {
            float childWidth = child.gameObject.GetComponent<RectTransform>().sizeDelta.x;
            float childHeight = child.gameObject.GetComponent<RectTransform>().sizeDelta.y;

            // Calculate the total width
            if (type == "Horizontal") {
                width += childWidth;
            }
            // Calculate the maximum width
            else if (childWidth > width) {
                width = childWidth;
            }

            // Calculate the total height
            if (type == "Vertical") {
                height += childHeight;
            }
            // Calculate the maximum height
            else if (childHeight > height) {
                height = childHeight;
            }
        }

        // Factor in the spacing dimensions
        if (type == "Horizontal") {
            width += obj.GetComponent<HorizontalLayoutGroup>().spacing * (obj.transform.childCount - 1);
        }
        else {
            height += obj.GetComponent<VerticalLayoutGroup>().spacing * (obj.transform.childCount - 1);
        }

        // Factor in any padding
        LayoutGroup layout = (LayoutGroup) obj.GetComponent<HorizontalLayoutGroup>() ?? obj.GetComponent<VerticalLayoutGroup>();
        width += layout.padding.left + layout.padding.right;
        height += layout.padding.top + layout.padding.bottom;

        Debug.Log("Height: " + height);

        // Resize the GameObject
        obj.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
    }

    void Reset() {
        KillChildren();
        
        // The Primary Buttons
        GameObject primary = CreateLayout("Primary", primaryButton.spacing, true);
        var hit = CreateButton("Hit", (primaryButton.width, primaryButton.height), new Color32(92, 92, 92, 255));
        hit.transform.SetParent(primary.transform);
        HitButton = hit.GetComponent<Button>();

        var stand = CreateButton("Stand", (primaryButton.width, primaryButton.height), new Color32(92, 92, 92, 255));
        stand.transform.SetParent(primary.transform);
        StandButton = stand.GetComponent<Button>();

        // The Secondary Buttons
        GameObject secondary = CreateLayout("Secondary", secondaryButton.spacing, false);
        var split = CreateButton("Split", (secondaryButton.width, secondaryButton.height), new Color32(92, 92, 92, 255));
        split.transform.SetParent(secondary.transform);
        SplitButton = split.GetComponent<Button>();

        var double_down = CreateButton("Double Down", (secondaryButton.width, secondaryButton.height), new Color32(92, 92, 92, 255));
        double_down.transform.SetParent(secondary.transform);
        DoubleDownButton = double_down.GetComponent<Button>();
        
        // The Third Buttons
        GameObject third = CreateLayout("Third Priority", thirdButton.spacing, false);
        var insurance = CreateButton("Insurance", (thirdButton.width, thirdButton.height), new Color32(92, 92, 92, 255));
        insurance.transform.SetParent(third.transform);
        InsuranceButton = insurance.GetComponent<Button>();

        GameObject spacer = new GameObject("Spacer");
        spacer.AddComponent<RectTransform>().sizeDelta = new Vector2(SpacerSize, 0);
        
        // Add the primary panel to the GameObject, then resize it
        primary.transform.SetParent(gameObject.transform);
        AutoSize(primary);
        // Add the secondary panel to the GameObject, then resize it
        secondary.transform.SetParent(gameObject.transform);
        AutoSize(secondary);
        // Add the spacer to the GameObject
        spacer.transform.SetParent(gameObject.transform);
        // Add the third panel to the GameObject, then resize it
        third.transform.SetParent(gameObject.transform);
        AutoSize(third);

        // Get/Add a Horizontal layout to (this) ActionDisplay, and set the spacing
        var layout = gameObject.AddComponent<HorizontalLayoutGroup>() ?? gameObject.GetComponent<HorizontalLayoutGroup>();
        layout.spacing = GeneralSpacing;
        layout.padding = new RectOffset(GeneralPadding, GeneralPadding, GeneralPadding, GeneralPadding);
        layout.childAlignment = TextAnchor.MiddleCenter;


        // Resize the ActionDisplay
        AutoSize(gameObject);
        
        // Set the position of the ActionDisplay
        // Get the RectTransform of (this) ActionDisplay object
        RectTransform rect = gameObject.GetComponent<RectTransform>();
        // Set the position of the ActionDisplay
        rect.anchorMin = new Vector2(0.5f, 0);
        rect.anchorMax = new Vector2(0.5f, 0);
        rect.pivot = new Vector2(0.5f, 0.5f);

        // WEIRD: For whatever reason, when Y is set to 90, it displays as 630
        // Instead, when set to -450, it displays as 90.
        // 90 is the desired Y value for Action Display
        rect.localPosition = new Vector3(0, -450, 0);
    }   

    void Awake() {
        // Reset the ActionsDisplay
        Reset();

        // Attach the event listeners to the buttons
        HitButton.onClick.AddListener(() => { Hit.Invoke(); });
        StandButton.onClick.AddListener(() => { Stand.Invoke(); });
        SplitButton.onClick.AddListener(() => { Split.Invoke(); });
        DoubleDownButton.onClick.AddListener(() => { DoubleDown.Invoke(); });
        InsuranceButton.onClick.AddListener(() => { Insurance.Invoke(); });
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
