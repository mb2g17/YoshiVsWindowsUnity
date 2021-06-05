using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Classes.Files
{
    /// <summary>
    /// Allows saving and loading game data
    /// </summary>
    public class FileManager
    {
        /// <summary>
        /// Saves data to a file number
        /// </summary>
        /// <param name="fileNo">The number the file is</param>
        /// <param name="data">The data to save</param>
        public static void SaveFile(int fileNo, GameData data)
        {
            string destination = Application.persistentDataPath + "/file" + fileNo + ".dat";
            FileStream file;

            if (File.Exists(destination)) file = File.OpenWrite(destination);
            else file = File.Create(destination);

            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, data);
            file.Close();
        }

        /// <summary>
        /// Loads data from file number and returns it
        /// </summary>
        /// <param name="fileNo">The file number to load from</param>
        /// <returns>The loaded data</returns>
        public static GameData LoadFile(int fileNo)
        {
            string destination = Application.persistentDataPath + "/file" + fileNo + ".dat";
            FileStream file;

            if (File.Exists(destination)) file = File.OpenRead(destination);
            else
            {
                // Create a new file if one doesn't exist
                return CreateFile(fileNo);
            }

            BinaryFormatter bf = new BinaryFormatter();
            GameData data = (GameData)bf.Deserialize(file);
            file.Close();
            return data;
        }

        /// <summary>
        /// Creates a new file
        /// </summary>
        /// <param name="fileNo">The file number of the new file</param>
        /// <returns>The GameData of the new file we just made</returns>
        private static GameData CreateFile(int fileNo)
        {
            // Creates data, saves it then returns it
            GameData gameData = new GameData(false, false, false, false, 100, Difficulty.NORMAL, "MushroomKingdom");
            SaveFile(fileNo, gameData);
            return gameData;
        }

        /// <summary>
        /// Creates a fully completed file
        /// </summary>
        /// <param name="fileNo">The file number of the new file</param>
        /// <returns>The GameData of the new file we just made</returns>
        private static GameData Create100PercentFile(int fileNo)
        {
            // Creates data, saves it then returns it
            GameData gameData = new GameData(true, true, true, true, 100, Difficulty.NIGHTMARE, "BillGatesBoss");
            SaveFile(fileNo, gameData);
            return gameData;
        }
    }
}
