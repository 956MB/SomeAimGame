using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

using SomeAimGame.Utilities;

namespace SomeAimGame.Console {
    public class ConsoleUtil : MonoBehaviour {

        private static ConsoleUtil consoleUtil;
        void Awake() { consoleUtil = this; }

        public static CommandTrip<ConsoleErrorType, string, string> SplitConsoleCommandString(string fullCommandString) {
            CommandTrip<ConsoleErrorType, string, string> returnConsoleTrip = new CommandTrip<ConsoleErrorType, string, string>();

            string[] splitCommand = fullCommandString.Split(' ');
            string commandKey     = "";
            string commandValue   = "";

            if (splitCommand.Length >= 1) {
                commandKey = splitCommand[0];
                if (splitCommand.Length >= 2) { commandValue = splitCommand[1]; }

                returnConsoleTrip.Key   = commandKey;
                returnConsoleTrip.Value = commandValue;
                
                switch (splitCommand.Length) {
                    case 1:  returnConsoleTrip.Type = ConsoleErrorType.PRINT_VALUE;   return returnConsoleTrip;
                    case 2:  returnConsoleTrip.Type = ConsoleErrorType.VALID_COMMAND; return returnConsoleTrip;
                    default: returnConsoleTrip.Type = ConsoleErrorType.TOO_MANY_ARGS; return returnConsoleTrip;
                }
            } else {
                returnConsoleTrip.Type = ConsoleErrorType.NULL_COMMAND;
                return returnConsoleTrip;
            }
        }

        public static bool CheckValidCommandKey(string commandKey) {
            return true;
        }
        public static bool CheckValidCommandValue_Int(string commandValue) {
            return Util.ReturnRegexMatch(commandValue, "[-]?[0-9]+");
        }
        public static bool CheckValidCommandValue_Float(string commandValue) {
            return Util.ReturnRegexMatch(commandValue, "[-]?([0-9]*[.])+[0-9]+");
        }
        public static bool CheckValidCommandValue_Bool(string commandValue) {
            return Util.ReturnRegexMatch(commandValue, "[0-1]{1}");
        }


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


        #endregion
    }
}