using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace LibrarySystem.Utilities
{
    public class Serializer
    {
        public static void serialize(string filepath, Object objects)
        {

            using (Stream stream = System.IO.File.Open(filepath, FileMode.Create))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream, objects);
            }

        }
    }
}