using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Frontend.Helpers
{
    public class HelperXML
    {
        public Dictionary<string, List<string>> buildDictionary(string xml)
        {
            XDocument config = XDocument.Parse(xml);
            Dictionary<string, List<string>> ret = new Dictionary<string, List<string>>();

            IEnumerable<XNode> familyNodes = config.Element("PinOutInfo").Nodes();
            foreach (XElement familyElement in familyNodes)
            {
                IEnumerable<XNode> modelNodes = familyElement.Nodes();
                string family = familyElement.Attribute("name").Value;
                foreach (XElement modelElement in modelNodes)
                {
                    List<string> models = new List<string>();
                    models.AddRange(modelElement.Attribute("name").Value.Split(','));
                    if (ret.ContainsKey(family))
                    {
                        ret[family].AddRange(models);
                    }
                    else
                    {
                        ret[family] = models;
                    }
                }
            }
            return ret;
        }
    }
}
