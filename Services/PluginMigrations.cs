namespace IgnoreMessages
{
    public class PluginMigrations : Dictionary<int, Dictionary<int, string>>
    {
        public PluginMigrations()
        {
            this.AddMigration(1, 2, "You have to remove the 'PrintToChatSignature' key from your configuration, and change the 'Version' to 2. (The latest example configuration is always available at: https://github.com/KillStr3aK/IgnoreMessages/blob/master/public/addons/counterstrikesharp/configs/plugins/IgnoreMessages/IgnoreMessages.json)");
        }

        public bool HasInstruction(int fromVersion, int toVersion)
        {
            return base.ContainsKey(fromVersion) && base[fromVersion].ContainsKey(toVersion);
        }

        public string GetInstruction(int fromVersion, int toVersion)
        {
            return base[fromVersion][toVersion];
        }

        public void AddMigration(int fromVersion, int toVersion, string instruction)
        {
            base.Add(fromVersion, new()
            {
                [toVersion] = instruction
            });
        }
    }
}
