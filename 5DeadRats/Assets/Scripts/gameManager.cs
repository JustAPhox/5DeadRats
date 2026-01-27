using Unity.VisualScripting;
using UnityEngine.SocialPlatforms.Impl;

// The gameManager is used to pass variables between scenes.
// You can call it by doing this
// gameManager.function();
// You can add stuff but please do not change stuff you didn't add as things might go wrong


public static class gameManager
{
    // Stores the current volume settings
    public static int Main_CurrentPlayers;

    /// <summary>
    /// Changes the affinity of the wanted character
    /// </summary>
    /// <param name="affinitySlot">The number for the character</param>
    /// <param name="newAffinity">The new affinity</param>
    public static void sePlayerCount(int playerCount)
    {
        Main_CurrentPlayers = playerCount;
    }

    /// <summary>
    /// Gets the affinity for the choosen character
    /// </summary>
    /// <returns></returns>
    public static int getPlayerCount()
    {
        return Main_CurrentPlayers;
    }

}