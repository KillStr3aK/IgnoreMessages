namespace IgnoreMessages
{
    using CounterStrikeSharp.API.Core;

    public sealed class PluginConfig : BasePluginConfig
    {
        public bool PrintKeyNames { get; set; } = true;

        public HashSet<string> IgnoredMessages { get; set; } = new HashSet<string>();

        public override int Version { get; set; } = 2;
    }
}
