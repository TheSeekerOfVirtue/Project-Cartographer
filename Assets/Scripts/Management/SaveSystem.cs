///<summary>
/// Save System by The Seeker Of Virtue
/// Save or Load the Save File.
///<summary>

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Carto.Savedata
{
    public class SaveSystem : MonoBehaviour
    {
        //a static reference to the save data
        static SaveData dataRefGetter;
        public static SaveData dataRef
        {
            get
            {
                //get current save data when called
                if (dataRefGetter != null)
                    return dataRefGetter;
                dataRefGetter = LoadData();
                return dataRefGetter;
            }
            set
            {
                //save data when dataRef get's editted
                dataRefGetter = value;
                DoSave(GetPath(), dataRefGetter);
            }
        }
        ///<summary>
        /// The public Save Function. Overrides all data to file. 
        ///<summary>
        public static void Save(SaveData data)
        {
            string path = GetPath();
            //Save the data
            DoSave(path, data);
        }

        ///<summary>
        /// The public Load Function. Returns the Save Data
        ///<summary>
        public static SaveData LoadData()
        {
            string path = GetPath();
            //if does not exist, create one
            if (!File.Exists(path))
            {
                Debug.Log("Warning: Save File Does Not Exist. Creating New Data.");
                return DoSave(path, StartData());
            }
            //if does exist, load and return it
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveData data = binaryFormatter.Deserialize(stream) as SaveData;
            stream.Close();
            return data;
        }

        ///<summary>
        /// The actual save function. Overrides or create's data.
        ///<summary>
        static SaveData DoSave(string path, SaveData data)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Create);
            binaryFormatter.Serialize(stream, data);
            stream.Close();
            return data;
        }

        ///<summary>
        /// Returns the path to the save file
        ///<summary>
        static string GetPath()
        {
            return Application.persistentDataPath + @"/Carto.savedata";
        }

        public static SaveData StartData()
        {
            SaveData toReturn = new SaveData();
            toReturn.masterVolume = 1;
            toReturn.sfxVolume = 1;
            toReturn.musicVolume = 1;
            toReturn.voiceVolume = 1;
            return toReturn;
        }
    }
    [System.Serializable]
    public class SaveData
    {
        public float masterVolume;
        public float sfxVolume;
        public float musicVolume;
        public float voiceVolume;
    }
}