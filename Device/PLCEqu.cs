using System.Xml;

namespace WindowMake.Device
{
    public class PLCEqu : MyObject
    {
        public PLCPropert plc_pro = new PLCPropert();

        public PLCEqu()
        {
            equtype = MyObject.ObjectType.UnKnow;
            picName = "\\Pic\\unkown.png";
        }
        
        public virtual void WaitControl(out bool bComplete, out bool bResult)
        {
            bComplete = true;
            bResult = false;
        }

        public override XmlElement SaveObject(XmlDocument doc)
        {
            XmlElement xmlElement = base.SaveObject(doc);
            XmlElement xmlElement2 = doc.CreateElement("Device");
            XmlElement xmlElement3 = doc.CreateElement("YX");
            XmlElement xmlElement4 = doc.CreateElement("YK");
            XmlElement xmlElement5 = doc.CreateElement("YC");
            for (int i = 0; i < 8; i++)
            {
                
            }
            xmlElement2.AppendChild(xmlElement3);
            xmlElement2.AppendChild(xmlElement4);
            xmlElement2.AppendChild(xmlElement5);
            xmlElement.AppendChild(xmlElement2);
            return xmlElement;
        }

        public override void OpenObject(XmlNode node)
        {
            base.OpenObject(node);
            XmlNode firstChild = node.FirstChild;
            if (firstChild != null && firstChild.Name == "Device")
            {
               
            }
        }
    }
}
