using System;
using System.Collections;
using System.Collections.Generic;
using DIALOGUE;
using UnityEngine;

namespace COMMANDS
{
    public class CMD_DatabaseExtension_Tasks : CMD_DatabaseExtension
    {
        private static string[] PARAM_ENABLE => new string[] { "e", "enable" };
        private static string[] PARAM_IMMEDIATE => new string[] { "i", "immediate" };
        private static string[] PARAM_SPEED => new string[] { "spd", "speed" };
        private static string[] PARAM_SMOOTH => new string[] { "sm", "smooth" };
        private static string PARAM_XPOS => "x";
        private static string PARAM_YPOS => "y";

        new public static void Extend(CommandDatabase database)
        {
            database.AddCommand("addtask", new Action<string[]>(AddTaskByName));
        }

        private static void AddTaskByName(string[] data)
        {
            string[] taskNames = data;

            foreach ( var taskName in taskNames )
            {
                TaskManager.Instance.AddTaskByName(taskName);
            }
        }

        private static  void RemoveTaskByName(string[] data)
        {

        }
    }
}
