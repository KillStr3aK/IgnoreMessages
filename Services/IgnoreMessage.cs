namespace IgnoreMessages
{
    using CounterStrikeSharp.API.Core;
    using CounterStrikeSharp.API.Core.Plugin;
    using CounterStrikeSharp.API.Modules.UserMessages;

    public enum TargetedMessages : int
    {
        UM_HudMsg = 110,
        UM_HudText = 111,

        UM_SayText = 117,
        UM_SayText2 = 118,
        UM_SayTextChannel = 119,

        UM_TextMsg = 124,

        CS_UM_HudText = 304,

        CS_UM_SayText = 305,
        CS_UM_SayText2 = 306,

        CS_UM_TextMsg = 307,

        CS_UM_HudMsg = 308,

        CS_UM_RadioText = 322,
        CS_UM_HintText = 323,
        CS_UM_KeyHintText = 324
    }

    public class IgnoreMessage
    {
        private readonly ILogger<IgnoreMessage> Logger;

        private readonly PluginContext PluginContext;

        public required Plugin Plugin;

        private readonly Array Messages = Enum.GetValues(typeof(TargetedMessages));

        public IgnoreMessage(ILogger<IgnoreMessage> logger, IPluginContext pluginContext)
        {
            this.Logger = logger;
            this.PluginContext = (pluginContext as PluginContext)!;
        }

        public void Initialize(bool hotReload)
        {
            this.Plugin = (this.PluginContext.Plugin as Plugin)!;

            foreach (TargetedMessages messageId in this.Messages)
            {
#if DEBUG
                this.Logger.LogInformation("Hooking UserMessage '{0}' ({1})", messageId, (int)messageId);
#endif

                // Process terminated.
                // A callback was made on a garbage collected delegate of type 'CounterStrikeSharp.API!CounterStrikeSharp.API.Core.FunctionReference+CallbackDelegate::Invoke'.
                this.Plugin.HookUserMessage((int)messageId, this.OnUserMessage, HookMode.Pre);
            }
        }

        private HookResult HandleMessage(string messageName, string locToken)
        {
            if (!locToken.StartsWith("#"))
            {
                return HookResult.Continue;
            }

            if (this.Plugin.Config.IgnoredMessages.Contains(locToken))
            {
                return HookResult.Stop;
            }

            if (this.Plugin.Config.PrintKeyNames)
            {
                this.Logger.LogInformation("Current message key: \"{0}\" ({1})", locToken.Replace(Environment.NewLine, string.Empty), messageName);
            }

            return HookResult.Continue;
        }

        private HookResult OnUserMessage(UserMessage message)
        {
#if DEBUG
            this.Logger.LogInformation("{0}", message.Name);
#endif

            TargetedMessages messageId = (TargetedMessages)message.Id;

            switch (messageId)
            {
                case TargetedMessages.UM_HudMsg:
                case TargetedMessages.UM_HudText:

                case TargetedMessages.CS_UM_HintText:
                    return this.SingleParam(message, "message");

                case TargetedMessages.UM_SayText:
                case TargetedMessages.UM_SayTextChannel:

                case TargetedMessages.CS_UM_HudText:
                case TargetedMessages.CS_UM_SayText:
                case TargetedMessages.CS_UM_HudMsg:
                    return this.SingleParam(message, "text");

                case TargetedMessages.UM_SayText2:

                case TargetedMessages.CS_UM_SayText2: // NEED REVISION: this one is only assumed yet
                    return this.SingleParam(message, "messagename");

                case TargetedMessages.UM_TextMsg:

                case TargetedMessages.CS_UM_TextMsg: // NEED REVISION: this one is only assumed yet
                    return this.MultipleParam(message, "param");

                case TargetedMessages.CS_UM_KeyHintText:
                    return this.MultipleParam(message, "messages");

                case TargetedMessages.CS_UM_RadioText:
                {
                    // this handles tokens like `#Game_radio_location`
                    if (this.SingleParam(message, "msg_name") != HookResult.Continue)
                    {
                        return HookResult.Stop;
                    }

                    // this handles tokens like `#Cstrike_TitlesTXT_Roger_that`
                    return this.MultipleParam(message, "params");
                }

                default:
                    break;
            }

            return HookResult.Continue;
        }

        private HookResult SingleParam(UserMessage message, string paramName)
        {
            return this.HandleMessage(message.Name, message.ReadString(paramName));
        }

        private HookResult MultipleParam(UserMessage message, string paramName)
        {
            int count = message.GetRepeatedFieldCount(paramName);

            for(int i = 0; i < count; i++)
            {
                HookResult result = this.HandleMessage(message.Name, message.ReadString(paramName, i));

                if (result != HookResult.Continue)
                {
                    return result;
                }
            }

            return HookResult.Continue;
        }

        public void Release(bool hotReload)
        {
            foreach (TargetedMessages messageId in this.Messages)
            {
                this.Plugin.UnhookUserMessage((int)messageId, this.OnUserMessage, HookMode.Pre);
            }
        }
    }
}
