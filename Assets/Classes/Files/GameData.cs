using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Classes.Files
{
    /// <summary>
    /// Save data in object form
    /// </summary>
    [System.Serializable]
    public class GameData
    {
        /// <summary>
        /// If we reached the ending
        /// </summary>
        public bool GotEnding = false;

        /// <summary>
        /// If we reached the true ending
        /// </summary>
        public bool GotTrueEnding = false;

        /// <summary>
        /// If we beat the game on Nightmare
        /// </summary>
        public bool GotNightmareEnding = false;

        /// <summary>
        /// Health of Yoshi in this playthrough
        /// </summary>
        public int Health = 100;

        /// <summary>
        /// Difficulty of this playthrough
        /// </summary>
        public Difficulty Difficulty = Difficulty.NORMAL;

        /// <summary>
        /// What level we're on right now
        /// </summary>
        public string Level = "MushroomKingdom";

        /// <summary>
        /// Are we doing a playthrough
        /// </summary>
        public bool AreWeDoingPlaythrough = false;

        public GameData(bool areWeDoingPlaythrough,
                        bool gotEnding,
                        bool gotTrueEnding,
                        bool gotNightmareEnding,
                        int health,
                        Difficulty difficulty,
                        string level)
        {
            this.AreWeDoingPlaythrough = areWeDoingPlaythrough;
            this.GotEnding = gotEnding;
            this.GotTrueEnding = gotTrueEnding;
            this.GotNightmareEnding = gotNightmareEnding;
            this.Health = health;
            this.Difficulty = difficulty;
            this.Level = level;
        }
    }
}
