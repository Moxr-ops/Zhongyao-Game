using System;
using DIALOGUE;

namespace COMMANDS
{
    public class CMD_DatabaseExtension_Tasks : CMD_DatabaseExtension
    {
        new public static void Extend(CommandDatabase database)
        {
            database.AddCommand("addtask", new Action<string[]>(AddTaskByName));
            database.AddCommand("destorytask", new Action<string[]>(DestoryTaskByName));
            database.AddCommand("finishtask", new Action<string[]>(AddTaskByName));
        }

        private static void AddTaskByName(string[] data)
        {
            string[] taskNames = data;

            foreach ( var taskName in taskNames )
            {
                TaskManager.Instance.AddTaskByName(taskName);
            }
        }

        private static  void DestoryTaskByName(string[] data)
        {
            string[] taskname = data;

            foreach (var taskName in taskname)
            {
                TaskManager.Instance.RemoveTaskByName(taskName);
            }

        }
    }
}
