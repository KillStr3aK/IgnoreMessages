namespace IgnoreMessages
{
    using CounterStrikeSharp.API.Core;

    public sealed partial class Plugin : BasePlugin
    {
        public override string ModuleName => "Ignore Messages";

        public override string ModuleDescription => "Ignore messages that has been sent by the game.";

        public override string ModuleAuthor => "Nexd @ Eternar (https://eternar.dev)";

        public override string ModuleVersion => "1.0.1 " +
#if RELEASE
            "(release)";
#else
            "(debug)";
#endif
    }
}
