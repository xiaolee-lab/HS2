using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Utility.Xml
{
	// Token: 0x020011AE RID: 4526
	public class Control
	{
		// Token: 0x060094AE RID: 38062 RVA: 0x003D4FF8 File Offset: 0x003D33F8
		public Control(string savePath, string saveName, string rootName, List<Data> dataList)
		{
			this.savePath = savePath;
			this.saveName = saveName;
			this.rootName = rootName;
			this.dataList = dataList;
			this.Init();
		}

		// Token: 0x17001F8D RID: 8077
		// (get) Token: 0x060094AF RID: 38063 RVA: 0x003D502E File Offset: 0x003D342E
		public List<Data> DataList
		{
			get
			{
				return this.dataList;
			}
		}

		// Token: 0x17001F8E RID: 8078
		public Data this[int index]
		{
			get
			{
				return this.dataList[index];
			}
		}

		// Token: 0x060094B1 RID: 38065 RVA: 0x003D5044 File Offset: 0x003D3444
		public void Init()
		{
			foreach (Data data in this.dataList)
			{
				data.Init();
			}
		}

		// Token: 0x060094B2 RID: 38066 RVA: 0x003D50A0 File Offset: 0x003D34A0
		public void Write()
		{
			string outputFileName = UserData.Create(this.savePath) + this.saveName;
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			xmlWriterSettings.Indent = true;
			xmlWriterSettings.Encoding = Encoding.UTF8;
			XmlWriter xmlWriter = null;
			try
			{
				xmlWriter = XmlWriter.Create(outputFileName, xmlWriterSettings);
				xmlWriter.WriteStartDocument();
				xmlWriter.WriteStartElement(this.rootName);
				foreach (Data data in this.dataList)
				{
					data.Write(xmlWriter);
				}
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndDocument();
			}
			finally
			{
				if (xmlWriter != null)
				{
					xmlWriter.Close();
				}
			}
		}

		// Token: 0x060094B3 RID: 38067 RVA: 0x003D5174 File Offset: 0x003D3574
		public void Read()
		{
			XmlDocument xmlDocument = new XmlDocument();
			try
			{
				string text = string.Concat(new object[]
				{
					UserData.Path,
					this.savePath,
					'/',
					this.saveName
				});
				if (File.Exists(text))
				{
					xmlDocument.Load(text);
					foreach (Data data in this.dataList)
					{
						data.Read(this.rootName, xmlDocument);
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x04007794 RID: 30612
		private readonly string savePath;

		// Token: 0x04007795 RID: 30613
		private readonly string saveName;

		// Token: 0x04007796 RID: 30614
		private readonly string rootName;

		// Token: 0x04007797 RID: 30615
		private List<Data> dataList = new List<Data>();
	}
}
