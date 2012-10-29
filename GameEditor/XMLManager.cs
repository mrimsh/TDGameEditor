using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace GameEditor
{

    class XMLManager
    {

        private string storagePath;

        #region Singleton
        private static XMLManager instance = new XMLManager();

        public static XMLManager Instance
        {
            get
            {
                return instance;
            }
        }


        private XMLManager()
        { }
        #endregion

        public void SetStoragePath(string storagePath)
        {
            this.storagePath = storagePath;
        }

        public object Load(string fileName, Type type)
        {
            object retObj = null;
            var serializer = new XmlSerializer(type);
            FileStream stream;

            if (!File.Exists(storagePath + fileName))
            {
                Save(fileName, Activator.CreateInstance(type), type);
            }

            try
            {
                stream = new FileStream(storagePath + fileName, FileMode.Open);
                using (stream)
                {
                    retObj = serializer.Deserialize(stream);
                }
                stream.Close();
            }
            catch (FileNotFoundException)
            {
            }

            return retObj;
        }

        /// <summary>
        /// Writes the serialized object into XML file..
        /// </summary>
        /// <param name='fileName'>
        /// XML file name, to store serialized object.
        /// </param>
        /// <param name='data'>
        /// Object for serialization.
        /// </param>
        /// <param name='type'>
        /// Type of object.
        /// </param>
        public void Save(string fileName, object data, Type type)
        {
            if (!Directory.Exists(storagePath))
            {
                Directory.CreateDirectory(storagePath);
            }
            var serializer = new XmlSerializer(type);
            var stream = new FileStream(storagePath + fileName, FileMode.Create);
            serializer.Serialize(stream, data);
            stream.Close();
        }
    }
}
