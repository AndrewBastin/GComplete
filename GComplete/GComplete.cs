using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GComplete {
    public class GComplete {

        const string GOOGLE_SUGGEST_URL = "http://google.com/complete/search?output=toolbar&q=";

        public static string[] GetSuggestions(string input) {
            // Getting the XML response
            string xmlData = null;
            using (WebClient client = new WebClient()) {
                xmlData = client.DownloadString(GOOGLE_SUGGEST_URL + input);
            }

            // Parsing the response
            if (xmlData != null) {
                using (XmlReader reader = XmlReader.Create(new StringReader(xmlData))) {

                    List<string> response = new List<string>();

                    while (reader.Read()) {
                        switch (reader.NodeType) {
                            case XmlNodeType.Element:
                                if (reader.Name == "suggestion") {
                                    response.Add(reader.GetAttribute("data"));
                                }
                                break;
                        }
                    }
                    return response.ToArray();
                }
            }
            return new String[0];
        }

    }
}
