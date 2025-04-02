namespace IgnoreMessages
{
    using CounterStrikeSharp.API;
    using CounterStrikeSharp.API.Core;
    using CounterStrikeSharp.API.Core.Plugin;
    using CounterStrikeSharp.API.Modules.Memory;
    using CounterStrikeSharp.API.Modules.Memory.DynamicFunctions;
    using CounterStrikeSharp.API.Modules.UserMessages;
    using CounterStrikeSharp.API.Modules.Utils;

    public class IgnoreMessage
    {
        private readonly ILogger<IgnoreMessage> Logger;

        private readonly PluginContext PluginContext;

        public required Plugin Plugin;

        public IgnoreMessage(ILogger<IgnoreMessage> logger, IPluginContext pluginContext)
        {
            this.Logger = logger;
            this.PluginContext = (pluginContext as PluginContext)!;
        }

        public void Initialize(bool hotReload)
        {
            this.Plugin = (this.PluginContext.Plugin as Plugin)!;

            this.Plugin.HookUserMessage(124, OnMessagePrint, HookMode.Pre);
            this.Plugin.HookUserMessage(323, OnHudMessage, HookMode.Pre);
        }

        private HookResult OnHudMessage(UserMessage message)
        {
            var msg = message.ReadString("message");

            if (msg.StartsWith("#"))
            {
                if (this.Plugin.Config.IgnoredMessages.Contains(msg))
                {
                    return HookResult.Stop;
                }

                if (this.Plugin.Config.PrintKeyNames)
                {
                    this.Logger.LogInformation("Current message key: \"{0}\"", msg.Replace(Environment.NewLine, string.Empty));
                }
            }

            return HookResult.Continue;
        }

        private HookResult OnMessagePrint(UserMessage message)
        {
            var count = message.GetRepeatedFieldCount("param");

            for(int i = 0; i < count; i++)
            {
                var msg = message.ReadString("param", i);

                if (msg.StartsWith("#"))
                {
                    if (this.Plugin.Config.IgnoredMessages.Contains(msg))
                    {
                        return HookResult.Stop;
                    }

                    if (this.Plugin.Config.PrintKeyNames)
                    {
                        this.Logger.LogInformation("Current message key: \"{0}\"", msg.Replace(Environment.NewLine, string.Empty));
                    }
                }
            }

            return HookResult.Continue;
        }

        public void Release(bool hotReload)
        {
            this.Plugin.UnhookUserMessage(124, OnMessagePrint, HookMode.Pre);
            this.Plugin.UnhookUserMessage(323, OnHudMessage, HookMode.Pre);
        }
    }
}
