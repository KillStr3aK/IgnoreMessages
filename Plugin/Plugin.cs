namespace IgnoreMessages
{
    using CounterStrikeSharp.API.Core;
    using CounterStrikeSharp.API.Core.Attributes;

    [MinimumApiVersion(84)]
    public sealed partial class Plugin : BasePlugin, IPluginConfig<PluginConfig>
    {
        public required PluginConfig Config { get; set; } = new PluginConfig();

        private readonly IgnoreMessage IgnoreMessage;

        public Plugin(IgnoreMessage ignoreMessage)
        {
            this.IgnoreMessage = ignoreMessage;
        }

        public void OnConfigParsed(PluginConfig config)
        {
            if (config.Version < this.Config.Version)
            {
                base.Logger.LogWarning("Configuration version mismatch (Expected: {0} | Current: {1})", this.Config.Version, config.Version);
            }

            this.Config = config;
        }

        public override void Load(bool hotReload)
        {
            this.IgnoreMessage.Initialize(hotReload);
        }

        public override void Unload(bool hotReload)
        {
            this.IgnoreMessage.Release(hotReload);
        }
    }
}
