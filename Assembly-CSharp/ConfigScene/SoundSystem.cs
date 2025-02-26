using System;
using System.Reflection;
using System.Xml;
using Manager;

namespace ConfigScene
{
	// Token: 0x02000859 RID: 2137
	public class SoundSystem : BaseSystem
	{
		// Token: 0x06003667 RID: 13927 RVA: 0x0014177C File Offset: 0x0013FB7C
		public SoundSystem(string elementName) : base(elementName)
		{
			this.Master.ChangeEvent += delegate(SoundData sd)
			{
				MixerVolume.Set(Sound.Mixer, MixerVolume.Names.MasterVolume, sd.GetVolume());
			};
			this.BGM.ChangeEvent += delegate(SoundData sd)
			{
				MixerVolume.Set(Sound.Mixer, MixerVolume.Names.BGMVolume, sd.GetVolume());
			};
			this.ENV.ChangeEvent += delegate(SoundData sd)
			{
				MixerVolume.Set(Sound.Mixer, MixerVolume.Names.ENVVolume, sd.GetVolume());
			};
			this.SystemSE.ChangeEvent += delegate(SoundData sd)
			{
				MixerVolume.Set(Sound.Mixer, MixerVolume.Names.SystemSEVolume, sd.GetVolume());
			};
			this.GameSE.ChangeEvent += delegate(SoundData sd)
			{
				MixerVolume.Set(Sound.Mixer, MixerVolume.Names.GameSEVolume, sd.GetVolume());
			};
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06003668 RID: 13928 RVA: 0x00141890 File Offset: 0x0013FC90
		public SoundData[] Sounds
		{
			get
			{
				SoundData[] result;
				if ((result = this._sounds) == null)
				{
					result = (this._sounds = new SoundData[]
					{
						this.Master,
						this.BGM,
						this.ENV,
						this.SystemSE,
						this.GameSE
					});
				}
				return result;
			}
		}

		// Token: 0x06003669 RID: 13929 RVA: 0x001418E8 File Offset: 0x0013FCE8
		public override void Init()
		{
			foreach (SoundData soundData in this.Sounds)
			{
				soundData.Mute = false;
			}
			this.Master.Volume = 100;
			this.BGM.Volume = 40;
			this.ENV.Volume = 80;
			this.SystemSE.Volume = 50;
			this.GameSE.Volume = 70;
		}

		// Token: 0x0600366A RID: 13930 RVA: 0x0014195C File Offset: 0x0013FD5C
		public override void Read(string rootName, XmlDocument xml)
		{
			try
			{
				FieldInfo[] fieldInfos = base.FieldInfos;
				for (int i = 0; i < fieldInfos.Length; i++)
				{
					string xpath = string.Concat(new string[]
					{
						rootName,
						"/",
						this.elementName,
						"/",
						fieldInfos[i].Name
					});
					XmlNodeList xmlNodeList = xml.SelectNodes(xpath);
					if (xmlNodeList != null)
					{
						XmlElement xmlElement = xmlNodeList.Item(0) as XmlElement;
						if (xmlElement != null)
						{
							this.Sounds[i].Parse(xmlElement.InnerText);
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x0600366B RID: 13931 RVA: 0x00141A18 File Offset: 0x0013FE18
		public override void Write(XmlWriter writer)
		{
			FieldInfo[] fieldInfos = base.FieldInfos;
			writer.WriteStartElement(this.elementName);
			for (int i = 0; i < fieldInfos.Length; i++)
			{
				writer.WriteStartElement(fieldInfos[i].Name);
				writer.WriteValue(this.Sounds[i]);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		// Token: 0x040036E4 RID: 14052
		private SoundData[] _sounds;

		// Token: 0x040036E5 RID: 14053
		public SoundData Master = new SoundData();

		// Token: 0x040036E6 RID: 14054
		public SoundData BGM = new SoundData();

		// Token: 0x040036E7 RID: 14055
		public SoundData ENV = new SoundData();

		// Token: 0x040036E8 RID: 14056
		public SoundData SystemSE = new SoundData();

		// Token: 0x040036E9 RID: 14057
		public SoundData GameSE = new SoundData();
	}
}
