using UnityEngine;

/// <summary>
/// Class containing preset albedo, emission and light for all target color options.
/// </summary>
public class TargetColors {
    // Red
    public static Color RedAlbedo() { return new Color(0f / 255f, 0f / 255f, 0f / 255f, 255f / 255f); }
    public static Color RedEmission() { return new Color(162f / 255f, 0f / 255f, 0f / 255f, 255f / 255f); }
    public static Color RedLight() { return new Color(255f / 255f, 0f / 255f, 11f / 255f, 255f / 255f); }
    // Orange
    public static Color OrangeAlbedo() { return new Color(255f / 255f, 125f / 255f, 0f / 255f, 255f / 255f); }
    public static Color OrangeEmission() { return new Color(255f / 255f, 87f / 255f, 0f / 255f, 255f / 255f); }
    public static Color OrangeLight() { return new Color(255f / 255f, 64f / 255f, 0f / 255f, 255f / 255f); }
    // Yellow
    public static Color YellowAlbedo() { return new Color(179f / 255f, 136f / 255f, 0f / 255f, 255f / 255f); }
    public static Color YellowEmission() { return new Color(255f / 255f, 166f / 255f, 0f / 255f, 255f / 255f); }
    public static Color YellowLight() { return new Color(255f / 255f, 205f / 255f, 0f / 255f, 255f / 255f); }
    // Green
    public static Color GreenAlbedo() { return new Color(40f / 255f, 82f / 255f, 0f / 255f, 255f / 255f); }
    public static Color GreenEmission() { return new Color(0f / 255f, 183f / 255f, 2f / 255f, 255f / 255f); }
    public static Color GreenLight() { return new Color(59f / 255f, 255f / 255f, 0f / 255f, 255f / 255f); }
    // Blue
    public static Color BlueAlbedo() { return new Color(0f / 255f, 73f / 255f, 255f / 255f, 255f / 255f); }
    public static Color BlueEmission() { return new Color(0f / 255f, 70f / 255f, 255f / 255f, 255f / 255f); }
    public static Color BlueLight() { return new Color(0f / 255f, 47f / 255f, 255f / 255f, 255f / 255f); }
    // Purple
    public static Color PurpleAlbedo() { return new Color(113f / 255f, 0f / 255f, 255f / 255f, 255f / 255f); }
    public static Color PurpleEmission() { return new Color(74f / 255f, 0f / 255f, 255f / 255f, 255f / 255f); }
    public static Color PurpleLight() { return new Color(114f / 255f, 0f / 255f, 255f / 255f, 255f / 255f); }
    // Pink
    public static Color PinkAlbedo() { return new Color(208f / 255f, 0f / 255f, 255f / 255f, 255f / 255f); }
    public static Color PinkEmission() { return new Color(148f / 255f, 0f / 255f, 255f / 255f, 255f / 255f); }
    public static Color PinkLight() { return new Color(198f / 255f, 0f / 255f, 255f / 255f, 255f / 255f); }
    // White
    public static Color WhiteAlbedo() { return new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f); }
    public static Color WhiteEmission() { return new Color(255f / 255f, 255f / 255f, 255f / 255f, 255f / 255f); }
    public static Color WhiteLight() { return new Color(231f / 255f, 231f / 255f, 231f / 255f, 255f / 255f); }
}
