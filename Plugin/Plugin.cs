namespace IgnoreMessages
{
    using CounterStrikeSharp.API.Core;
    using CounterStrikeSharp.API.Core.Attributes;

    [MinimumApiVersion(120)]
    public sealed partial class Plugin : BasePlugin, IPluginConfig<PluginConfig>
    {
        public required PluginConfig Config { get; set; } = new PluginConfig();

        private readonly IgnoreMessage IgnoreMessage;

        private readonly PluginMigrations Migrations;

        public Plugin(PluginMigrations migrations, IgnoreMessage ignoreMessage)
        {
            this.IgnoreMessage = ignoreMessage;
            this.Migrations = migrations;
        }

        public void OnConfigParsed(PluginConfig config)
        {
            if (config.Version < this.Config.Version)
            {
                base.Logger.LogWarning("Configuration version mismatch (Expected: {0} | Current: {1})", this.Config.Version, config.Version);

                if (this.Migrations.HasInstruction(config.Version, this.Config.Version))
                {
                    base.Logger.LogWarning("Instruction for migrating your config file: {0}", this.Migrations.GetInstruction(config.Version, this.Config.Version));
                } else
                {
                    base.Logger.LogWarning("No migrating instruction available");
                }
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
