using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Periotris.Common;

namespace Periotris.Customization.History
{
    [Serializable]
    [DataContract]
    public class History
    {
        private History()
        {
            PlayRecords = new List<TimeSpan>();
            FastestRecord = null;
        }

        [DataMember] public List<TimeSpan> PlayRecords { get; private set; }

        [DataMember] public TimeSpan? FastestRecord { get; private set; }

        /// <summary>
        ///     Add a new score to the record.
        /// </summary>
        /// <param name="newPlayTime">The new play time.</param>
        /// <returns>Whether the new score is the new record.</returns>
        public bool AddNewScore(TimeSpan newPlayTime)
        {
            PlayRecords.Add(newPlayTime);
            if (!FastestRecord.HasValue)
            {
                FastestRecord = newPlayTime;
                return true;
            }

            if (newPlayTime < FastestRecord)
            {
                FastestRecord = newPlayTime;
                return true;
            }

            return false;
        }

        public static void WriteToFile(History history)
        {
            var jsonSerializer = new JsonSerializer();

            using (var outStream =
                !File.Exists(TetrisConst.HistoryFilePath) ? File.Create(TetrisConst.HistoryFilePath) : File.OpenWrite(TetrisConst.HistoryFilePath))
            using (var sw = new StreamWriter(outStream))
            using (var writer = new JsonTextWriter(sw))
            {
                jsonSerializer.Serialize(writer, history);
            }
        }

        public static History ReadFromFile()
        {
            if (!File.Exists(TetrisConst.HistoryFilePath)) return new History();

            var jsonSerializer = new JsonSerializer();

            using (var inStream = File.OpenRead(TetrisConst.HistoryFilePath))
            using (var sr = new StreamReader(inStream))
            using (var reader = new JsonTextReader(sr))
            {
                return jsonSerializer.Deserialize<History>(reader);
            }
        }
    }
}