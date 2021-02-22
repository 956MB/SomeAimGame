//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;

using SomeAimGame.Utilities;

namespace SomeAimGame.Console {
    public class ConsoleUtil : MonoBehaviour {
        public static Color caretEnableColor  = new Color(255f, 255f, 255f, 255f);
        public static Color caretDisableColor = new Color(0f, 0f, 0f, 0f);

        private static ConsoleUtil consoleUtil;
        void Awake() { consoleUtil = this; }

        /// <summary>
        /// Splits supplied command string (fullCommandString) and returns CommandTrip<ConsoleErrorType, string, string> containing errortype, key, and value.
        /// </summary>
        /// <param name="fullCommandString"></param>
        /// <returns></returns>
        public static CommandTrip<CommandReturnType, string, string> SplitConsoleCommandString(string fullCommandString) {
            CommandTrip<CommandReturnType, string, string> returnConsoleTrip = new CommandTrip<CommandReturnType, string, string>();

            string[] splitCommand = fullCommandString.Split(' ');
            string commandKey     = "";
            string commandValue   = "";

            if (splitCommand.Length >= 1) {
                commandKey = splitCommand[0];
                if (splitCommand.Length >= 2) { commandValue = splitCommand[1]; }

                returnConsoleTrip.Key   = commandKey;
                returnConsoleTrip.Value = commandValue;
                
                switch (splitCommand.Length) {
                    case 1:  returnConsoleTrip.Type = CommandReturnType.PRINT_VALUE;   return returnConsoleTrip;
                    case 2:  returnConsoleTrip.Type = CommandReturnType.VALID_COMMAND; return returnConsoleTrip;
                    default: returnConsoleTrip.Type = CommandReturnType.TOO_MANY_ARGS; return returnConsoleTrip;
                }
            } else {
                returnConsoleTrip.Type = CommandReturnType.NULL_COMMAND;
                return returnConsoleTrip;
            }
        }

        #region regex checks

        public static bool CheckValidCommandKey(string commandKey) {
            return true;
        }
        /// <summary>
        /// Checks if supplied command value matches int Regex ("[-]?[0-9]+")
        /// </summary>
        /// <param name="commandValue"></param>
        /// <returns></returns>
        public static bool CheckValidCommandValue_Int(string commandValue) {
            return Util.ReturnRegexMatch(commandValue, "[-]?[0-9]+");
        }
        /// <summary>
        /// Checks if supplied command value matches float Regex ("[-]?([0-9]*[.])+[0-9]+").
        /// </summary>
        /// <param name="commandValue"></param>
        /// <returns></returns>
        public static bool CheckValidCommandValue_Float(string commandValue) {
            return Util.ReturnRegexMatch(commandValue, "[-]?([0-9]*[.])+[0-9]+");
        }
        /// <summary>
        /// Checks if supplied command value matches bool Regex ("[0-1]{1}").
        /// </summary>
        /// <param name="commandValue"></param>
        /// <returns></returns>
        public static bool CheckValidCommandValue_Bool(string commandValue) {
            return Util.ReturnRegexMatch(commandValue, "[0-1]{1}");
        }
        /// <summary>
        /// Checks if supplied command value matches crosshair Regex (@"^(\d){33}$").
        /// </summary>
        /// <param name="commandValue"></param>
        /// <returns></returns>
        public static bool CheckValidCommandValue_Crosshair(string commandValue) {
            return Util.ReturnRegexMatch(commandValue, @"^(\d){33}$");
        }
        /// <summary>
        /// Checks if supplied command value matches csgo crosshair Regex (@"(CSGO)(-?[\w]{5}){5}$").
        /// </summary>
        /// <param name="commandValue"></param>
        /// <returns></returns>
        public static bool CheckValidCommandValue_CrosshairCSGO(string commandValue) {
            return Util.ReturnRegexMatch(commandValue, @"(CSGO)(-?[\w]{5}){5}$");
        }

        #endregion

        #region errors

        //public static void ThrowTooFewArgumentsError(string commandKey) {
        //    // "TOO FEW ARGUMENTS SUPPLIED FOR COMMAND `{commandKey}` :: Command format: `{commandKey} 0`"
        //}

        public static void ThrowTooManyArgumentsError(string commandKey, string commandValue) {
            // "TOO MANY ARGUMENTS SUPPLIED FOR COMMAND`{commandKey}` :: Command format: `{commandKey} 0`"
            Debug.Log("ThrowTooManyArgumentsError");
        }
        public static void ThrowInvalidCommandKeyError(string commandKey, string commandValue) {
            // "SUPPLIED COMMAND `{}` DOES NOT EXIST :: Type `help` to see command list"
        }
        public static void ThrowInvalidCommandValueError(string commandKey, string commandValue) {
            // "SUPPLIED VALUE `{}` FOR COMMAND `{}` IS INVALID :: Command format: `{commandKey} 0`"
        }
        public static void ThrowInvalidCommandValueKeybindError(string commandKey, string commandValue) {
            // "SUPPLIED KEYBIND STRING `{}` FOR COMMAND `{}` IS INVALID :: Type `ke_keybind_list` to see keybinds list"
        }


        #endregion

        public static void SetCaretState(TMP_InputField inputField, ref bool caretBool, bool setState) {
            inputField.caretColor = setState ? caretDisableColor : caretEnableColor;
            caretBool = setState;
        }
    }
}