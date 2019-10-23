/*********
* 
*   from:https://www.cnblogs.com/artech/p/new-config-system-09.html
*   
*********/
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Xml;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace ConfigurationDemo2
{
    public class ExtendXmlConfigurationProvider : XmlConfigurationProvider
    { 
        public ExtendXmlConfigurationProvider(XmlConfigurationSource source) : base(source)
        { }

        public override void Load(Stream stream)
        {
            //加载源文件并创建一个XmlDocument
            XmlDocument sourceDoc = new XmlDocument();
            sourceDoc.Load(stream);

            //添加索引
            AddIndexes(sourceDoc.DocumentElement);

            //根据添加的索引创建一个新的XmlDocument
            XmlDocument newDoc = new XmlDocument();
            XmlElement documentElement = newDoc.CreateElement(sourceDoc.DocumentElement.Name);
            newDoc.AppendChild(documentElement);

            foreach (XmlElement element in sourceDoc.DocumentElement.ChildNodes)
            {
                Rebuild(element, documentElement, name => newDoc.CreateElement(name));
            }

            //根据新的XmlDocument初始化配置字典
            using (Stream newStream = new MemoryStream())
            {
                using (XmlWriter writer = XmlWriter.Create(newStream))
                {
                    newDoc.WriteTo(writer);
                }
                newStream.Position = 0;
                base.Load(newStream);
            }
        }

        private void AddIndexes(XmlElement element)
        {
            if (element.ChildNodes.OfType<XmlElement>().Count() > 1)
            {
                if (element.ChildNodes.OfType<XmlElement>().GroupBy(it => it.Name).Count() == 1)
                {
                    int index = 0;
                    foreach (XmlElement subElement in element.ChildNodes)
                    {
                        subElement.SetAttribute("append_index", (index++).ToString());
                        AddIndexes(subElement);
                    }
                }
            }
        }

        private void Rebuild(XmlElement source, XmlElement destParent, Func<string, XmlElement> creator)
        {
            string index = source.GetAttribute("append_index");
            string elementName = string.IsNullOrEmpty(index) ? source.Name : $"{source.Name}_index_{index}";
            XmlElement element = creator(elementName);
            destParent.AppendChild(element);
            foreach (XmlAttribute attribute in source.Attributes)
            {
                if (attribute.Name != "append_index")
                {
                    element.SetAttribute(attribute.Name, attribute.Value);
                }
            }

            foreach (XmlElement subElement in source.ChildNodes)
            {
                Rebuild(subElement, element, creator);
            }
        }
    }

    public class ExtendXmlConfigurationSource : XmlConfigurationSource
    {
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            base.FileProvider = base.FileProvider ?? builder.GetFileProvider();

            return new ExtendXmlConfigurationProvider(this);
        }
    }

    public class Options
    {
        public Profile[] Profiles { get; set; }
    }

    class ExtendXmlConfigurationDemo
    {
        public void Execute()
        {
            IConfiguration config = new ConfigurationBuilder()
                .Add(new ExtendXmlConfigurationSource() { Path = "profileCollection.xml" })
                .Build();

            Dictionary<string, Profile> profiles = new ServiceCollection()
                .AddOptions()
                .Configure<Dictionary<string, Profile>>(config)
                .BuildServiceProvider()
                .GetService<IOptions<Dictionary<string, Profile>>>()
                .Value;

            foreach (var profile in profiles.Values)
            {
                Console.WriteLine(profile.Gender);
                Console.WriteLine(profile.Age);
                Console.WriteLine(profile.ContactInfo.EmailAddress);
                Console.WriteLine(profile.ContactInfo.PhoneNo);
            }
        }
    }
}
