// -----------------------------------------------------------------------
// <copyright file="SettingsButton.cs" company="ContentSettings">
// Copyright (c) ContentSettings. All rights reserved.
// Licensed under the GPL-3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace ContentSettings.API.Settings.UI;

using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// A component representing a button in a settings menu, providing hover and selection states.
/// </summary>
public class SettingsButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    /// <summary>
    /// The text mesh pro component of the tab, displaying the tab name.
    /// </summary>
    [SerializeField]
    private TextMeshProUGUI? textMeshPro;

    /// <summary>
    /// The image component of the tab, displaying the tab icon.
    /// </summary>
    [SerializeField]
    private Image? image;

    /// <summary>
    /// Gets or sets the display name of the button.
    /// </summary>
    public string Name {
        get => textMeshPro?.text ?? string.Empty;
        set {
            if (textMeshPro != null) {
                textMeshPro.text = value;
            }
        }
    }

    /// <summary>
    /// Gets a value indicating whether the tab is hovered over.
    /// </summary>
    [UsedImplicitly]
    public bool IsHovered { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the tab is selected.
    /// </summary>
    [UsedImplicitly]
    public bool IsSelected { get; private set; }

    /// <summary>
    /// Called when the tab is hovered over.
    /// </summary>
    /// <param name="eventData">The pointer event data.</param>
    public void OnPointerEnter(PointerEventData eventData) {
        IsHovered = true;
        OnHover();
    }

    /// <summary>
    /// Called when the tab is no longer hovered over.
    /// </summary>
    /// <param name="eventData">The pointer event data.</param>
    public void OnPointerExit(PointerEventData eventData) {
        IsHovered = false;
        OnUnhover();
    }

    /// <summary>
    /// Select the tab.
    /// </summary>
    /// <remarks>See <see cref="OnSelected"/> for the event triggered by this.</remarks>
    public void Select() {
        IsSelected = true;
        OnSelected();
    }

    /// <summary>
    /// Deselect the tab.
    /// </summary>
    /// <remarks>See <see cref="OnDeselected"/> for the event triggered by this.</remarks>
    public void Deselect() {
        IsSelected = false;
        OnDeselected();
    }

    /// <summary>
    /// Called when the tab is selected.
    /// </summary>
    public virtual void OnSelected() {
        SetColorActive();
    }

    /// <summary>
    /// Called when the tab is deselected.
    /// </summary>
    public virtual void OnDeselected() {
        SetColorInactive();
    }

    /// <summary>
    /// Called when the tab is hovered over.
    /// </summary>
    public virtual void OnHover() {
        SetColorActive();
    }

    /// <summary>
    /// Called when the tab is no longer hovered over.
    /// </summary>
    public virtual void OnUnhover() {
        SetColorInactive();
    }

    /// <summary>
    /// Awake is called by Unity when the script instance is being loaded.
    /// </summary>
    protected virtual void Awake() {
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        image = GetComponentInChildren<Image>();
    }

    private void SetColorActive() {
        if (image is not null) {
            image.color = new(0.992f, 0.839f, 0.000f, 0.600f);
        }

        if (textMeshPro is not null) {
            textMeshPro.color = new(0.0f, 0.0f, 0.0f, 1.000f);
        }
    }

    private void SetColorInactive() {
        if (image is not null && !IsHovered && !IsSelected) {
            image.color = new(0.122f, 0.122f, 0.122f, 0.482f);
        }

        if (textMeshPro is not null && !IsHovered && !IsSelected) {
            textMeshPro.color = new(1.0f, 1.0f, 1.0f, 1.000f);
        }
    }
}
