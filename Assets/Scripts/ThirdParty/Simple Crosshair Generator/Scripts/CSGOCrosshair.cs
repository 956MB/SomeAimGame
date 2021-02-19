using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class CSGOCrosshair : MonoBehaviour {
    private static string DICTIONARY        = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefhijkmnopqrstuvwxyz23456789";
    private static int    DICTIONARY_LENGTH = DICTIONARY.Length;
    private static string SHARECODE_PATTERN = @"(CSGO)(-?[\w]{5}){5}$";

    private static CSGOCrosshair csgoCrosshair;
    void Awake() { csgoCrosshair = this; }

    /// <summary>
    /// Checks if supplied crosshair string (crosshairCode) contains "CSGO-", making it possibly a CSGO crossahair Share Code.
    /// </summary>
    /// <param name="crosshairCode"></param>
    /// <returns></returns>
    public static bool CheckIfCSGOCrosshair(string crosshairCode) {
        if (crosshairCode.Contains("CSGO-")) {  return true; }
        return false;
    }

    /// <summary>
    /// Checks if supplied CSGO Share Code string (crosshairCode) fits format of "CSGO-XXXXX-XXXXX-XXXXX-XXXXX-XXXXX". Returns true/false.
    /// </summary>
    /// <param name="crosshairCode"></param>
    /// <returns></returns>
    public static bool ValidateCSGOCrosshair(string crosshairCode) {
        Regex regex = new Regex(SHARECODE_PATTERN);

        if (regex.Match(crosshairCode).Success) { return true; }
        return false;
    }

    /// <summary>
    /// Decodes supplied CSGO crosshair Share Code (crosshairCode) and returns final Dictionary containing matching keys/values for crosshair.
    /// </summary>
    /// <param name="crosshairCode"></param>
    /// <returns></returns>
    public static Dictionary<string, double> DecodeCSGOCrosshair(string crosshairCode) {
        // CSGO shareCode current: CSGO-qVc9Z-4HOXX-wFENS-HEDLx-v8QBD
        Dictionary<string, double> decodeResults = new Dictionary<string, double>();
        //List<int> bytes = new List<int>();
        //IDictionary<string, double> decoded = new Dictionary<string, double>();

        string repShareCode = crosshairCode.Replace("CSGO", "").Replace("-", "");
        char[] chars = repShareCode.ToCharArray();
        Array.Reverse(chars);
        BigInteger big = new BigInteger(0);

        for (int i = 0; i < chars.Length; i++) {
            big = big * DICTIONARY_LENGTH + DICTIONARY.IndexOf(chars[i]);
        }

        //bytes    = csgoCrosshair.bigNumberToByteArray(bytes, big);
        //decoded  = csgoCrosshair.parseBytes(decoded, bytes);
        //decode = csgoCrosshair.parseBytes(listInt);
        //return csgoCrosshair.parseBytes(csgoCrosshair.bigNumberToByteArray(big));
        return parseBytes(bigNumberToByteArray(big), decodeResults);
    }

    /// <summary>
    /// Returns byte array from supplied BigInteger (big).
    /// </summary>
    /// <param name="big"></param>
    /// <returns></returns>
    private static List<int> bigNumberToByteArray(BigInteger big) {
        string str = big.ToString("x").PadLeft(36, '0');
        Debug.Log($"str: {str}");
        List<int> bytes = new List<int>();

        try {
            for (int i = 0; i < str.Length; i += 2) {
                string subStr = str.Substring(i, 2);
                bytes.Add(Convert.ToInt16(subStr, 16));
            }
        } catch (ArgumentOutOfRangeException) {
            bytes.Clear();
            str = str.Substring(1);
            for (int i = 0; i < str.Length; i += 2) {
                string subStr = str.Substring(i, 2);
                bytes.Add(Convert.ToInt16(subStr, 16));
            }
        }

        return bytes;
    }

    /// <summary>
    /// Parses supplied bytes list (bytes) and returns supplied Dictionary (decodeResults) containing all crosshair keys/values.
    /// </summary>
    /// <param name="bytes"></param>
    /// <param name="decodeResults"></param>
    /// <returns></returns>
    private static Dictionary<string, double> parseBytes(List<int> bytes, Dictionary<string, double> decodeResults) {
        //IDictionary<string, double> decodedResults = new Dictionary<string, double>();
        //Console.WriteLine($"gap: {bytes.ElementAt(2)}");
        //Console.WriteLine($"gapsbyte: {bytes.ElementAt(2)}");
        //Console.WriteLine($"fixedGap: {bytes.ElementAt(8)}");

        //var gapBytes    = new sbyte[bytes.ElementAt(2)]; //wrong1
        var fixGapBytes = new sbyte[bytes.ElementAt(9)]; //wrong2
        //var _sbytes = bytes.SelectMany(BitConverter.GetBytes).ToArray();

        //Console.WriteLine($"gapbyte0: {gapBytes[0]}");
        try {
            decodeResults.Add("cl_crosshairgap", Convert.ToSByte(bytes.ElementAt(2)) / 10.0); //wrong1
        } catch (OverflowException) {
            decodeResults.Add("cl_crosshairgap", Convert.ToSByte(bytes.ElementAt(2) / 10.0) / 10.0); //wrong1
        }
        decodeResults.Add("cl_crosshair_outlinethickness", (bytes.ElementAt(3) & 7) / 2.0); // SAG round: Math.Round((bytes.ElementAt(3) & 7) / 2.0)
        decodeResults.Add("cl_crosshaircolor_r", bytes.ElementAt(4));
        decodeResults.Add("cl_crosshaircolor_g", bytes.ElementAt(5));
        decodeResults.Add("cl_crosshaircolor_b", bytes.ElementAt(6));
        decodeResults.Add("cl_crosshairalpha", bytes.ElementAt(7));
        decodeResults.Add("cl_fixedcrosshairgap", fixGapBytes[0] / 10.0); //wrong2
        decodeResults.Add("cl_crosshair_drawoutline", (bytes.ElementAt(10) & 8) == 0 ? 0 : 1);
        // outline color for imported csgo crosshair is only black
        decodeResults.Add("cl_crosshaircolor_r_outline", 0);
        decodeResults.Add("cl_crosshaircolor_g_outline", 0);
        decodeResults.Add("cl_crosshaircolor_b_outline", 0);
        decodeResults.Add("cl_crosshairalpha_outline", 255);
        decodeResults.Add("cl_crosshairthickness", (bytes.ElementAt(12) & 0x3F) / 10.0); // SAG round: Math.Round((bytes.ElementAt(12) & 0x3F) / 10.0)
        decodeResults.Add("cl_crosshairdot", (bytes.ElementAt(13) & 0x10) == 0 ? 0 : 1);
        decodeResults.Add("cl_crosshair_t", (bytes.ElementAt(13) & 0x80) == 0 ? 0 : 1);
        decodeResults.Add("cl_crosshairsize", (((bytes.ElementAt(15) & 0x1f) << 8) + bytes.ElementAt(14)) / 10.0); // SAG round: Math.Round(((bytes.ElementAt(15) & 0x1f) << 8) + bytes.ElementAt(14)) / 10.0)

        return decodeResults;
    }
}