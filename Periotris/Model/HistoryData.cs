using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Periotris.Model
{
    [Serializable]
    [DataContract]
    public class HistoryData
    {
        public static readonly string DataFilePath = "history.json";

        [DataMember]
        public List<TimeSpan> PlayRecords { get; private set; }

        [DataMember]
        public TimeSpan? FastestRecord { get; private set; }

        private HistoryData()
        {
            PlayRecords = new List<TimeSpan>();
            FastestRecord = null;
        }

        /// <summary>
        /// Add a new score to the record.
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

        public static void WriteToFile(HistoryData data)
        {
            Stream outStream;

            if (!File.Exists(DataFilePath))
            {
                outStream = File.Create(DataFilePath);
            }
            else
            {
                outStream = File.OpenWrite(DataFilePath);
            }

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(HistoryData));
            serializer.WriteObject(outStream, data);
            outStream.Dispose();
        }

        public static HistoryData ReadFromFile()
        {
            if (!File.Exists(DataFilePath))
            {
                return new HistoryData();
            }

            using (Stream inStream = File.OpenRead(DataFilePath))
            {
                return
                    new DataContractJsonSerializer(typeof(HistoryData))
                    .ReadObject(inStream)
                    as HistoryData;
            }
        }
    }
}
