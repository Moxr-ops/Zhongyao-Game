using COMMANDS;

namespace DIALOGUE
{
    public abstract class CMD_DatabaseExtension
    {
        public static void Extend(CommandDatabase database) { }
        public static CommandParameters ConvertDataToParameters(string[] data) => new CommandParameters(data);
    }
}
