using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace LFM.Submissions.Targets.LandRegistry
{
    /// <summary>
    /// Serialization and Deserialization tasks for an ojbect.
    /// </summary>
    public static class ObjectSerializer
    {
        /// <summary>
        /// Serializes specified object to an XML string.
        /// </summary>
        /// <param name="objectInstance">Object to serialize.</param>
        /// <returns>XML string.</returns>
        public static string XmlSerializeToString(this object objectInstance)
        {
            var serializer = new XmlSerializer(objectInstance.GetType());
            var sb = new StringBuilder();

            using (TextWriter writer = new StringWriter(sb))
            {
                serializer.Serialize(writer, objectInstance);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Deserializes an XML string to an object.
        /// </summary>
        /// <typeparam name="T">Type of object to deserialize.</typeparam>
        /// <param name="objectData">XML string to deserialize.</param>
        /// <returns>Object.</returns>
        public static T XmlDeserializeFromString<T>(string objectData)
        {
            return (T)XmlDeserializeFromString(objectData, typeof(T));
        }

        /// <summary>
        /// Deserializes an XML string to an object of the specified type.
        /// </summary>
        /// <param name="objectData">XML string to deserialize.</param>
        /// <param name="type">Type of object to deserialize.</param>
        /// <returns>Object.</returns>
        public static object XmlDeserializeFromString(string objectData, Type type)
        {
            var serializer = new XmlSerializer(type);
            object result;

            using (TextReader reader = new StringReader(objectData))
            {
                result = serializer.Deserialize(reader);
            }

            return result;
        }
    }
}
