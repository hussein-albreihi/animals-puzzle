using UnityEngine;
using System.Collections.Generic;

public static class GlobalVariables
{
    public static class Tags {
        public const string WALL = "Wall";
        public const string PLAYER = "Player";
        public const string PICKUP = "Pickup";
    }

    public static bool m_canPlayAd = false;
    public static bool m_playLongAd = false;
}
