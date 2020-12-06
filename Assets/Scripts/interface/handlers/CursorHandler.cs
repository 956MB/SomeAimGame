using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//using System;
//using System.Linq;
//using System.Runtime.InteropServices;

using SomeAimGame.SFX;

public class CursorHandler : MonoBehaviour {
    public Texture2D defaultCursorTexture;
    public Texture2D hoverCursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    private static CursorHandler cursor;
    void Awake() { cursor = this; }

    //private void Start() {
    //    Cursor.SetCursor(defaultCursorTexture, hotSpot, cursorMode);
    //}

    public static void setDefaultCursorStatic() {
        //Cursor.SetCursor(cursor.defaultCursorTexture, cursor.hotSpot, cursor.cursorMode);
    }

    public static void setHoverCursorStatic() {
        //Cursor.SetCursor(cursor.hoverCursorTexture, cursor.hotSpot, cursor.cursorMode);
    }

    public void setDefaultCursor() {
        //Cursor.SetCursor(cursor.defaultCursorTexture, cursor.hotSpot, cursor.cursorMode);
    }

    public void setHoverCursor() {
        //Cursor.SetCursor(cursor.hoverCursorTexture, cursor.hotSpot, cursor.cursorMode);
        SFXManager.CheckPlayHover_Regular();
    }

    //public static void ChangeCursor(WindowsCursor cursor) {
    //    Debug.Log("chnageCursor called!!!!");
    //    SetCursor(LoadCursor(IntPtr.Zero, (int)cursor));
    //}

    //[DllImport("user32.dll", EntryPoint = "SetCursor")]
    //public static extern IntPtr SetCursor(IntPtr hCursor);

    //[DllImport("user32.dll", EntryPoint = "LoadCursor")]
    //public static extern IntPtr LoadCursor(IntPtr hInstance, int lpCursorName);
}
