using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Illusion.Elements.Xml
{
	// Token: 0x02001074 RID: 4212
	public class Control
	{
		// Token: 0x06008D36 RID: 36150 RVA: 0x003B0908 File Offset: 0x003AED08
		public Control(string savePath, string saveName, string rootName, params Data[] datas)
		{
			this.savePath = savePath;
			this.saveName = saveName;
			this.rootName = rootName;
			this.datas = datas;
			this.Init();
		}

		// Token: 0x17001EDB RID: 7899
		// (get) Token: 0x06008D37 RID: 36151 RVA: 0x003B0933 File Offset: 0x003AED33
		public Data[] Datas
		{
			get
			{
				return this.datas;
			}
		}

		// Token: 0x17001EDC RID: 7900
		public Data this[int index]
		{
			get
			{
				return this.datas[index];
			}
		}

		// Token: 0x06008D39 RID: 36153 RVA: 0x003B0948 File Offset: 0x003AED48
		public void Init()
		{
			foreach (Data data in this.datas)
			{
				data.Init();
			}
		}

		// Token: 0x06008D3A RID: 36154 RVA: 0x003B097C File Offset: 0x003AED7C
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
				foreach (Data data in this.datas)
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

		// Token: 0x06008D3B RID: 36155 RVA: 0x003B0A30 File Offset: 0x003AEE30
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
					foreach (Data data in this.datas)
					{
						data.Read(this.rootName, xmlDocument);
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x040072C6 RID: 29382
		private readonly string savePath;

		// Token: 0x040072C7 RID: 29383
		private readonly string saveName;

		// Token: 0x040072C8 RID: 29384
		private readonly string rootName;

		// Token: 0x040072C9 RID: 29385
		private Data[] datas;
	}
}
