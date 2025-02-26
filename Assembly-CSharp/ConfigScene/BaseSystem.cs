using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using Illusion.Elements.Xml;
using UnityEngine;

namespace ConfigScene
{
	// Token: 0x02000854 RID: 2132
	public abstract class BaseSystem : Data
	{
		// Token: 0x06003656 RID: 13910 RVA: 0x00140799 File Offset: 0x0013EB99
		public BaseSystem(string elementName) : base(elementName)
		{
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06003657 RID: 13911 RVA: 0x001407A2 File Offset: 0x0013EBA2
		public FieldInfo[] FieldInfos
		{
			get
			{
				return base.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
			}
		}

		// Token: 0x06003658 RID: 13912 RVA: 0x001407B4 File Offset: 0x0013EBB4
		public override void Read(string rootName, XmlDocument xml)
		{
			string str = rootName + "/" + this.elementName + "/";
			foreach (FieldInfo fieldInfo in this.FieldInfos)
			{
				XmlNodeList xmlNodeList = xml.SelectNodes(str + fieldInfo.Name);
				if (xmlNodeList != null)
				{
					XmlElement xmlElement = xmlNodeList.Item(0) as XmlElement;
					if (xmlElement != null)
					{
						fieldInfo.SetValue(this, BaseSystem.Cast(xmlElement.InnerText, fieldInfo.FieldType));
					}
				}
			}
		}

		// Token: 0x06003659 RID: 13913 RVA: 0x00140850 File Offset: 0x0013EC50
		public override void Write(XmlWriter writer)
		{
			writer.WriteStartElement(this.elementName);
			foreach (FieldInfo fieldInfo in this.FieldInfos)
			{
				writer.WriteStartElement(fieldInfo.Name);
				writer.WriteValue(BaseSystem.ConvertString(fieldInfo.GetValue(this)));
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		// Token: 0x0600365A RID: 13914 RVA: 0x001408B4 File Offset: 0x0013ECB4
		public static object Cast(string str, Type type)
		{
			if (type == typeof(Color))
			{
				string[] array = str.Split(new char[]
				{
					','
				});
				if (array.Length == 4)
				{
					int num = 0;
					return new Color(float.Parse(array[num++]), float.Parse(array[num++]), float.Parse(array[num++]), float.Parse(array[num++]));
				}
				return Color.white;
			}
			else
			{
				if (type.IsArray)
				{
					string[] array2 = str.Split(new char[]
					{
						','
					});
					Type elementType = type.GetElementType();
					Array array3 = Array.CreateInstance(elementType, array2.Length);
					foreach (var <>__AnonType in array2.Select((string v, int i) => new
					{
						v,
						i
					}))
					{
						array3.SetValue(Convert.ChangeType(<>__AnonType.v, elementType), <>__AnonType.i);
					}
					return array3;
				}
				return Convert.ChangeType(str, type);
			}
		}

		// Token: 0x0600365B RID: 13915 RVA: 0x001409F8 File Offset: 0x0013EDF8
		public static string ConvertString(object o)
		{
			if (o is Color)
			{
				Color color = (Color)o;
				return string.Format("{0},{1},{2},{3}", new object[]
				{
					color.r,
					color.g,
					color.b,
					color.a
				});
			}
			if (o.GetType().IsArray)
			{
				Array array = (Array)o;
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < array.Length; i++)
				{
					stringBuilder.Append(array.GetValue(i));
					if (i + 1 < array.Length)
					{
						stringBuilder.Append(",");
					}
				}
				return stringBuilder.ToString();
			}
			return o.ToString();
		}
	}
}
