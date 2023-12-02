namespace IgnoreMessages
{
    using CounterStrikeSharp.API.Core;

    public sealed class PluginConfig : BasePluginConfig
    {
        public bool PrintKeyNames { get; set; } = true;

        public HashSet<string> IgnoredMessages { get; set; } = new HashSet<string>();

        public WIN_LINUX<string> PrintToChatSignature { get; set; } = new WIN_LINUX<string>(string.Empty, string.Empty);
    }
}
