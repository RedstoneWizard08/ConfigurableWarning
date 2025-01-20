using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ContentLibrary.API.Components;

/// <summary>
///     Component that listens for key presses.
/// </summary>
public class KeyListener : MonoBehaviour {
    /// <summary>
    ///     List of keys to listen for.
    /// </summary>
    public List<KeyCode> Keys = [];

    /// <summary>
    ///     Update is called once per frame.
    ///     This is a Unity builtin.
    /// </summary>
    public void Update() {
        foreach (var key in Keys.Where(Input.GetKeyDown)) OnKeyPressed?.Invoke(this, key);
    }

    /// <summary>
    ///     Event that is triggered when a key is pressed.
    /// </summary>
    public event EventHandler<KeyCode> OnKeyPressed;
}