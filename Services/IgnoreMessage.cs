namespace IgnoreMessages
{
    using CounterStrikeSharp.API.Core;
    using CounterStrikeSharp.API.Core.Plugin;
    using CounterStrikeSharp.API.Modules.Memory.DynamicFunctions;

    public class IgnoreMessage
    {
        private readonly ILogger<IgnoreMessage> Logger;

        private readonly PluginContext PluginContext;

        public required Plugin Plugin;

        public required MemoryFunctionVoid<nint, long, string, nint, long, nint, long> PrintToChat;

        public IgnoreMessage(ILogger<IgnoreMessage> logger, PluginContext pluginContext)
        {
            this.Logger = logger;
            this.PluginContext = pluginContext;
        }

        public void Initialize(bool hotReload)
        {
            this.Plugin = (this.PluginContext.Plugin as Plugin)!;

            this.PrintToChat = new(this.Plugin.Config.PrintToChatSignature.Get());
            this.PrintToChat.Hook(this.OnPrintToChat, HookMode.Pre);
        }

        private HookResult OnPrintToChat(DynamicHook hook)
        {
            string message = hook.GetParam<string>(2);

            if (this.Plugin.Config.IgnoredMessages.Contains(message))
                return HookResult.Stop;

            if (this.Plugin.Config.PrintKeyNames)
            {
                this.Logger.LogInformation("Current message key: \"{0}\"", message);
            }

            return HookResult.Continue;
        }

        public void Release(bool hotReload)
        {
            this.PrintToChat.Unhook(this.OnPrintToChat, HookMode.Pre);
        }
    }
}
