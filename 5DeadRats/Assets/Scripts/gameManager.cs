using Unity.VisualScripting;
using UnityEngine.SocialPlatforms.Impl;

// The gameManager is used to pass variables between scenes.
// You can call it by doing this
// gameManager.function();
// You can add stuff but please do not change stuff you didn't add as things might go wrong


public static class gameManager
{
    // Stores the player count
    public static int Main_CurrentPlayers;

    /// <summary>
    /// Sets the player count
    /// </summary>
    /// <param name="playerCount"> The current number of players </param>
    public static void setPlayerCount(int playerCount)
    {
        Main_CurrentPlayers = playerCount;
    }

    /// <summary>
    /// Gives the current player count
    /// </summary>
    /// <returns>Player Count</returns>
    public static int getPlayerCount()
    {
        return Main_CurrentPlayers;
    }

}