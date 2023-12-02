# IgnoreMessages

This plugin can remove the messages from the chat. You can specify the message keys that should be ignored.

By default I've supplied the configuration with random keys but mostly related to cash messages and it has the `PrintKeyNames` enabled.

> [!NOTE]  
> Required CSSharp version: **84**

# Configuration

Message keys are found in: `../game/csgo/resource/csgo_english.txt`

> [!WARNING]  
> When adding new message keys to the configuration dont forget the `#` in the beginning.

```jsonc
// This configuration was automatically generated by CounterStrikeSharp for plugin 'IgnoreMessages', at 2023/12/02 08:40:11
{
  // if this is enabled, the plugin will print and log every message key name for you just in case you'd want to block it, just dont know the name of it. (should be disabled in production, only prints to console)
  "PrintKeyNames": true,
  "PrintToChatSignature": {
    // ....
  },
  "IgnoredMessages": [
    // each key must begin with '#'
    "#Player_Cash_Award_Bomb_Planted",
    "#Player_Cash_Award_Bomb_Defused",
    "#Player_Cash_Award_Killed_VIP",
    "#Player_Cash_Award_Killed_Enemy",
    "#Player_Cash_Award_Killed_Enemy_Generic",
    "#Player_Cash_Award_Rescued_Hostage",
    "#Player_Cash_Award_Interact_Hostage",
    "#Player_Cash_Award_Respawn",
    "#Player_Cash_Award_Get_Killed",
    "#Player_Cash_Award_Kill_Teammate",
    "#Player_Cash_Award_Damage_Hostage",
    "#Player_Cash_Award_Kill_Hostage",

    "#Player_Point_Award_Killed_Enemy",
    "#Player_Point_Award_Killed_Enemy_Plural",
    "#Player_Point_Award_Killed_Enemy_NoWeapon",
    "#Player_Point_Award_Killed_Enemy_NoWeapon_Plural",
    "#Player_Point_Award_Assist_Enemy",
    "#Player_Point_Award_Assist_Enemy_Plural",
    "#Player_Point_Award_Picked_Up_Dogtag",
    "#Player_Point_Award_Picked_Up_Dogtag_Plural",

    "#Player_Team_Award_Killed_Enemy",
    "#Player_Team_Award_Killed_Enemy_Plural",
    "#Player_Team_Award_Bonus_Weapon",
    "#Player_Team_Award_Bonus_Weapon_Plural",
    "#Player_Team_Award_Picked_Up_Dogtag",
    "#Player_Team_Award_Picked_Up_Dogtag_Plural",
    "#Player_Team_Award_Picked_Up_Dogtag_Friendly",

    "#Player_Cash_Award_ExplainSuicide_YouGotCash",
    "#Player_Cash_Award_ExplainSuicide_TeammateGotCash",
    "#Player_Cash_Award_ExplainSuicide_EnemyGotCash",
    "#Player_Cash_Award_ExplainSuicide_Spectators",

    "#Team_Cash_Award_T_Win_Bomb",
    "#Team_Cash_Award_Elim_Hostage",
    "#Team_Cash_Award_Elim_Bomb",
    "#Team_Cash_Award_Win_Time",
    "#Team_Cash_Award_Win_Defuse_Bomb",
    "#Team_Cash_Award_Win_Hostages_Rescue",
    "#Team_Cash_Award_Win_Hostage_Rescue",
    "#Team_Cash_Award_Loser_Bonus",
    "#Team_Cash_Award_Bonus_Shorthanded",
    "#Team_Cash_Award_Loser_Bonus_Neg",
    "#Team_Cash_Award_Loser_Zero",
    "#Team_Cash_Award_Rescued_Hostage",
    "#Team_Cash_Award_Hostage_Interaction",
    "#Team_Cash_Award_Hostage_Alive",
    "#Team_Cash_Award_Planted_Bomb_But_Defused",
    "#Team_Cash_Award_Survive_GuardianMode_Wave",
    "#Team_Cash_Award_CT_VIP_Escaped",
    "#Team_Cash_Award_T_VIP_Killed",
    "#Team_Cash_Award_no_income",
    "#Team_Cash_Award_no_income_suicide",
    "#Team_Cash_Award_Generic",
    "#Team_Cash_Award_Custom",

    "#Cstrike_TitlesTXT_Game_teammate_attack"
  ],
  // ..
}
```
