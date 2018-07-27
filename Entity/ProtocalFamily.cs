using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace WindowMake.Entity
{
    public class ProtocalFamily
    {
        /// <summary>
        /// 协议名称，CG,CMS,EC...
        /// </summary>
        [XmlAttribute(DataType = "string")]
        public string Name { get; set; }

        public string Note { get; set; }

        /// <summary>
        /// 支持的设备类型
        /// </summary>
        [XmlArrayItem(ElementName = "EquType")]
        public Collection<string> SupportedEquipTypes
        {
            get; set;
        }

        /// <summary>
        /// 不同厂家的具体协议
        /// </summary>
        [XmlAttribute(DataType = "string")]
        public Collection<string> Vendors
        {
            get;
            set;
        }
    }
}
