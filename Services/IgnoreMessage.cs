namespace IgnoreMessages
{
    using CounterStrikeSharp.API.Core;
    using CounterStrikeSharp.API.Core.Plugin;
    using CounterStrikeSharp.API.Modules.Memory;
    using CounterStrikeSharp.API.Modules.Memory.DynamicFunctions;

    public class IgnoreMessage
    {
        private readonly ILogger<IgnoreMessage> Logger;

        private readonly PluginContext PluginContext;

        public required Plugin Plugin;

        public IgnoreMessage(ILogger<IgnoreMessage> logger, PluginContext pluginContext)
        {
            this.Logger = logger;
            this.PluginContext = pluginContext;
        }

        public void Initialize(bool hotReload)
        {
            this.Plugin = (this.PluginContext.Plugin as Plugin)!;

            VirtualFunctions.ClientPrintFunc.Hook(this.OnPrintToChat, HookMode.Pre);
            VirtualFunctions.ClientPrintAllFunc.Hook(this.OnPrintToChatAll, HookMode.Pre);
        }

        private HookResult InternalHandler(string message)
        {
            if (message.StartsWith("#"))
            {
                if (this.Plugin.Config.IgnoredMessages.Contains(message))
                {
                    return HookResult.Stop;
                }

                if (this.Plugin.Config.PrintKeyNames)
                {
                    this.Logger.LogInformation("Current message key: \"{0}\"", message.Replace(Environment.NewLine, string.Empty));
                }
            }

            return HookResult.Continue;
        }

        private HookResult OnPrintToChat(DynamicHook hook)
        {
            return this.InternalHandler(hook.GetParam<string>(2));
        }

        private HookResult OnPrintToChatAll(DynamicHook hook)
        {
            return this.InternalHandler(hook.GetParam<string>(1));
        }

        public void Release(bool hotReload)
        {
            VirtualFunctions.ClientPrintFunc.Unhook(this.OnPrintToChat, HookMode.Pre);
            VirtualFunctions.ClientPrintAllFunc.Unhook(this.OnPrintToChatAll, HookMode.Pre);
        }
    }
}
