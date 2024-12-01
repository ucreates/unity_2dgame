using System.Collections.Generic;

namespace Editor.Build
{
    public class CommandEditorBuilder : BaseEditorBuilder
    {
        public CommandEditorBuilder()
        {
            commandElementList = new List<string>();
        }

        public List<string> commandElementList { get; }

        public string Build()
        {
            var command = string.Empty;
            foreach (var commandElement in commandElementList) command += commandElement + " ";
            return command.Trim();
        }
    }
}