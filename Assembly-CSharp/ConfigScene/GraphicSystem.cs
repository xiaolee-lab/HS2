using System;
using System.Xml;
using UnityEngine;

namespace ConfigScene
{
	// Token: 0x02000857 RID: 2135
	public class GraphicSystem : BaseSystem
	{
		// Token: 0x06003661 RID: 13921 RVA: 0x00140BEC File Offset: 0x0013EFEC
		public GraphicSystem(string elementName) : base(elementName)
		{
		}

		// Token: 0x06003662 RID: 13922 RVA: 0x00140C70 File Offset: 0x0013F070
		public override void Init()
		{
			this.SelfShadow = true;
			this.Bloom = true;
			this.SSAO = true;
			this.SSR = true;
			this.DepthOfField = true;
			this.Atmospheric = true;
			this.Vignette = true;
			this.Rain = true;
			this.CharaGraphicQuality = 0;
			this.MapGraphicQuality = 0;
			this.FaceLight = false;
			this.AmbientLight = true;
			this.Shield = true;
			this.SimpleBody = false;
			this.SilhouetteColor = Color.blue;
			this.BackColor = Color.black;
			this.MaxCharaNum = 4;
			for (int i = 0; i < 4; i++)
			{
				this.CharasEntry[i] = true;
			}
		}

		// Token: 0x06003663 RID: 13923 RVA: 0x00140D18 File Offset: 0x0013F118
		public override void Read(string rootName, XmlDocument xml)
		{
			try
			{
				string text = rootName + "/" + this.elementName + "/";
				XmlNodeList xmlNodeList = xml.SelectNodes(text + "SelfShadow");
				if (xmlNodeList != null)
				{
					XmlElement xmlElement = xmlNodeList.Item(0) as XmlElement;
					if (xmlElement != null)
					{
						this.SelfShadow = (bool)BaseSystem.Cast(xmlElement.InnerText, this.SelfShadow.GetType());
					}
				}
				xmlNodeList = xml.SelectNodes(text + "Bloom");
				if (xmlNodeList != null)
				{
					XmlElement xmlElement2 = xmlNodeList.Item(0) as XmlElement;
					if (xmlElement2 != null)
					{
						this.Bloom = (bool)BaseSystem.Cast(xmlElement2.InnerText, this.Bloom.GetType());
					}
				}
				xmlNodeList = xml.SelectNodes(text + "SSAO");
				if (xmlNodeList != null)
				{
					XmlElement xmlElement3 = xmlNodeList.Item(0) as XmlElement;
					if (xmlElement3 != null)
					{
						this.SSAO = (bool)BaseSystem.Cast(xmlElement3.InnerText, this.SSAO.GetType());
					}
				}
				xmlNodeList = xml.SelectNodes(text + "SSR");
				if (xmlNodeList != null)
				{
					XmlElement xmlElement4 = xmlNodeList.Item(0) as XmlElement;
					if (xmlElement4 != null)
					{
						this.SSR = (bool)BaseSystem.Cast(xmlElement4.InnerText, this.SSR.GetType());
					}
				}
				xmlNodeList = xml.SelectNodes(text + "DepthOfField");
				if (xmlNodeList != null)
				{
					XmlElement xmlElement5 = xmlNodeList.Item(0) as XmlElement;
					if (xmlElement5 != null)
					{
						this.DepthOfField = (bool)BaseSystem.Cast(xmlElement5.InnerText, this.DepthOfField.GetType());
					}
				}
				xmlNodeList = xml.SelectNodes(text + "Atmospheric");
				if (xmlNodeList != null)
				{
					XmlElement xmlElement6 = xmlNodeList.Item(0) as XmlElement;
					if (xmlElement6 != null)
					{
						this.Atmospheric = (bool)BaseSystem.Cast(xmlElement6.InnerText, this.Atmospheric.GetType());
					}
				}
				xmlNodeList = xml.SelectNodes(text + "Vignette");
				if (xmlNodeList != null)
				{
					XmlElement xmlElement7 = xmlNodeList.Item(0) as XmlElement;
					if (xmlElement7 != null)
					{
						this.Vignette = (bool)BaseSystem.Cast(xmlElement7.InnerText, this.Vignette.GetType());
					}
				}
				xmlNodeList = xml.SelectNodes(text + "Rain");
				if (xmlNodeList != null)
				{
					XmlElement xmlElement8 = xmlNodeList.Item(0) as XmlElement;
					if (xmlElement8 != null)
					{
						this.Rain = (bool)BaseSystem.Cast(xmlElement8.InnerText, this.Rain.GetType());
					}
				}
				xmlNodeList = xml.SelectNodes(text + "CharaGraphicQuality");
				if (xmlNodeList != null)
				{
					XmlElement xmlElement9 = xmlNodeList.Item(0) as XmlElement;
					if (xmlElement9 != null)
					{
						this.CharaGraphicQuality = (byte)BaseSystem.Cast(xmlElement9.InnerText, this.CharaGraphicQuality.GetType());
					}
				}
				xmlNodeList = xml.SelectNodes(text + "MapGraphicQuality");
				if (xmlNodeList != null)
				{
					XmlElement xmlElement10 = xmlNodeList.Item(0) as XmlElement;
					if (xmlElement10 != null)
					{
						this.MapGraphicQuality = (byte)BaseSystem.Cast(xmlElement10.InnerText, this.MapGraphicQuality.GetType());
					}
				}
				xmlNodeList = xml.SelectNodes(text + "FaceLight");
				if (xmlNodeList != null)
				{
					XmlElement xmlElement11 = xmlNodeList.Item(0) as XmlElement;
					if (xmlElement11 != null)
					{
						this.FaceLight = (bool)BaseSystem.Cast(xmlElement11.InnerText, this.FaceLight.GetType());
					}
				}
				xmlNodeList = xml.SelectNodes(text + "AmbientLight");
				if (xmlNodeList != null)
				{
					XmlElement xmlElement12 = xmlNodeList.Item(0) as XmlElement;
					if (xmlElement12 != null)
					{
						this.AmbientLight = (bool)BaseSystem.Cast(xmlElement12.InnerText, this.AmbientLight.GetType());
					}
				}
				xmlNodeList = xml.SelectNodes(text + "Shield");
				if (xmlNodeList != null)
				{
					XmlElement xmlElement13 = xmlNodeList.Item(0) as XmlElement;
					if (xmlElement13 != null)
					{
						this.Shield = (bool)BaseSystem.Cast(xmlElement13.InnerText, this.Shield.GetType());
					}
				}
				xmlNodeList = xml.SelectNodes(text + "SimpleBody");
				if (xmlNodeList != null)
				{
					XmlElement xmlElement14 = xmlNodeList.Item(0) as XmlElement;
					if (xmlElement14 != null)
					{
						this.SimpleBody = (bool)BaseSystem.Cast(xmlElement14.InnerText, this.SimpleBody.GetType());
					}
				}
				xmlNodeList = xml.SelectNodes(text + "SilhouetteColor");
				if (xmlNodeList != null)
				{
					XmlElement xmlElement15 = xmlNodeList.Item(0) as XmlElement;
					if (xmlElement15 != null)
					{
						this.SilhouetteColor = (Color)BaseSystem.Cast(xmlElement15.InnerText, this.SilhouetteColor.GetType());
					}
				}
				xmlNodeList = xml.SelectNodes(text + "BackColor");
				if (xmlNodeList != null)
				{
					XmlElement xmlElement16 = xmlNodeList.Item(0) as XmlElement;
					if (xmlElement16 != null)
					{
						this.BackColor = (Color)BaseSystem.Cast(xmlElement16.InnerText, this.BackColor.GetType());
					}
				}
				xmlNodeList = xml.SelectNodes(text + "MaxCharaNum");
				if (xmlNodeList != null)
				{
					XmlElement xmlElement17 = xmlNodeList.Item(0) as XmlElement;
					if (xmlElement17 != null)
					{
						this.MaxCharaNum = (int)BaseSystem.Cast(xmlElement17.InnerText, this.MaxCharaNum.GetType());
					}
				}
				for (int i = 0; i < 4; i++)
				{
					xmlNodeList = xml.SelectNodes(text + "CharasEntry" + i);
					if (xmlNodeList != null)
					{
						XmlElement xmlElement18 = xmlNodeList.Item(0) as XmlElement;
						if (xmlElement18 != null)
						{
							this.CharasEntry[i] = (bool)BaseSystem.Cast(xmlElement18.InnerText, this.CharasEntry[i].GetType());
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06003664 RID: 13924 RVA: 0x00141388 File Offset: 0x0013F788
		public override void Write(XmlWriter writer)
		{
			writer.WriteStartElement(this.elementName);
			writer.WriteStartElement("SelfShadow");
			writer.WriteValue(BaseSystem.ConvertString(this.SelfShadow));
			writer.WriteEndElement();
			writer.WriteStartElement("Bloom");
			writer.WriteValue(BaseSystem.ConvertString(this.Bloom));
			writer.WriteEndElement();
			writer.WriteStartElement("SSAO");
			writer.WriteValue(BaseSystem.ConvertString(this.SSAO));
			writer.WriteEndElement();
			writer.WriteStartElement("SSR");
			writer.WriteValue(BaseSystem.ConvertString(this.SSR));
			writer.WriteEndElement();
			writer.WriteStartElement("DepthOfField");
			writer.WriteValue(BaseSystem.ConvertString(this.DepthOfField));
			writer.WriteEndElement();
			writer.WriteStartElement("Atmospheric");
			writer.WriteValue(BaseSystem.ConvertString(this.Atmospheric));
			writer.WriteEndElement();
			writer.WriteStartElement("Vignette");
			writer.WriteValue(BaseSystem.ConvertString(this.Vignette));
			writer.WriteEndElement();
			writer.WriteStartElement("Rain");
			writer.WriteValue(BaseSystem.ConvertString(this.Rain));
			writer.WriteEndElement();
			writer.WriteStartElement("CharaGraphicQuality");
			writer.WriteValue(BaseSystem.ConvertString(this.CharaGraphicQuality));
			writer.WriteEndElement();
			writer.WriteStartElement("MapGraphicQuality");
			writer.WriteValue(BaseSystem.ConvertString(this.MapGraphicQuality));
			writer.WriteEndElement();
			writer.WriteStartElement("FaceLight");
			writer.WriteValue(BaseSystem.ConvertString(this.FaceLight));
			writer.WriteEndElement();
			writer.WriteStartElement("AmbientLight");
			writer.WriteValue(BaseSystem.ConvertString(this.AmbientLight));
			writer.WriteEndElement();
			writer.WriteStartElement("Shield");
			writer.WriteValue(BaseSystem.ConvertString(this.Shield));
			writer.WriteEndElement();
			writer.WriteStartElement("SimpleBody");
			writer.WriteValue(BaseSystem.ConvertString(this.SimpleBody));
			writer.WriteEndElement();
			writer.WriteStartElement("SilhouetteColor");
			writer.WriteValue(BaseSystem.ConvertString(this.SilhouetteColor));
			writer.WriteEndElement();
			writer.WriteStartElement("BackColor");
			writer.WriteValue(BaseSystem.ConvertString(this.BackColor));
			writer.WriteEndElement();
			writer.WriteStartElement("MaxCharaNum");
			writer.WriteValue(BaseSystem.ConvertString(this.MaxCharaNum));
			writer.WriteEndElement();
			for (int i = 0; i < 4; i++)
			{
				string localName = "CharasEntry" + i;
				writer.WriteStartElement(localName);
				writer.WriteValue(BaseSystem.ConvertString(this.CharasEntry[i]));
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		// Token: 0x040036BD RID: 14013
		public const int MAX_CHARA_NUM = 4;

		// Token: 0x040036BE RID: 14014
		public const byte CHARA_GRAPHIC_QUALITY = 0;

		// Token: 0x040036BF RID: 14015
		public const byte MAP_GRAPHIC_QUALITY = 0;

		// Token: 0x040036C0 RID: 14016
		public bool SelfShadow = true;

		// Token: 0x040036C1 RID: 14017
		public bool Bloom = true;

		// Token: 0x040036C2 RID: 14018
		public bool SSAO = true;

		// Token: 0x040036C3 RID: 14019
		public bool SSR = true;

		// Token: 0x040036C4 RID: 14020
		public bool DepthOfField = true;

		// Token: 0x040036C5 RID: 14021
		public bool Atmospheric = true;

		// Token: 0x040036C6 RID: 14022
		public bool Vignette = true;

		// Token: 0x040036C7 RID: 14023
		public bool Rain = true;

		// Token: 0x040036C8 RID: 14024
		public byte CharaGraphicQuality;

		// Token: 0x040036C9 RID: 14025
		public byte MapGraphicQuality;

		// Token: 0x040036CA RID: 14026
		public bool FaceLight;

		// Token: 0x040036CB RID: 14027
		public bool AmbientLight = true;

		// Token: 0x040036CC RID: 14028
		public bool Shield = true;

		// Token: 0x040036CD RID: 14029
		public bool SimpleBody;

		// Token: 0x040036CE RID: 14030
		public Color SilhouetteColor = Color.blue;

		// Token: 0x040036CF RID: 14031
		public Color BackColor = Color.black;

		// Token: 0x040036D0 RID: 14032
		public int MaxCharaNum = 4;

		// Token: 0x040036D1 RID: 14033
		public bool[] CharasEntry = new bool[4];
	}
}
