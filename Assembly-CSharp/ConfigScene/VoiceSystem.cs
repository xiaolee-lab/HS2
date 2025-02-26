using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Manager;
using UnityEngine;

namespace ConfigScene
{
	// Token: 0x0200085A RID: 2138
	public class VoiceSystem : BaseSystem
	{
		// Token: 0x06003671 RID: 13937 RVA: 0x00141AD8 File Offset: 0x0013FED8
		public VoiceSystem(string elementName, Dictionary<int, string> dic) : base(elementName)
		{
			this.PCM.ChangeEvent += delegate(SoundData sd)
			{
				float volume = sd.GetVolume();
				MixerVolume.Set(Manager.Voice.Mixer, MixerVolume.Names.PCMVolume, volume);
			};
			this.chara = dic.ToDictionary((KeyValuePair<int, string> v) => v.Key, (KeyValuePair<int, string> v) => new VoiceSystem.Voice(v.Value, new SoundData()));
			(from p in this.chara
			select new
			{
				sd = new
				{
					p.Key,
					p.Value.sound
				}
			}).ToList().ForEach(delegate(p)
			{
				p.sd.sound.ChangeEvent += delegate(SoundData sd)
				{
					float volume = sd.GetVolume();
					int key = p.sd.Key;
					Singleton<Manager.Voice>.Instance.GetPlayingList(key).ForEach(delegate(AudioSource playing)
					{
						playing.volume = volume;
					});
				};
			});
		}

		// Token: 0x06003672 RID: 13938 RVA: 0x00141BB4 File Offset: 0x0013FFB4
		public override void Init()
		{
			this.PCM.Mute = false;
			this.PCM.Volume = 100;
			foreach (KeyValuePair<int, VoiceSystem.Voice> keyValuePair in this.chara)
			{
				SoundData sound = keyValuePair.Value.sound;
				sound.Mute = false;
				sound.Volume = 100;
			}
		}

		// Token: 0x06003673 RID: 13939 RVA: 0x00141C40 File Offset: 0x00140040
		public override void Read(string rootName, XmlDocument xml)
		{
			try
			{
				string str = rootName + "/" + this.elementName + "/";
				XmlNodeList xmlNodeList = xml.SelectNodes(str + "PCM");
				if (xmlNodeList != null)
				{
					XmlElement xmlElement = xmlNodeList.Item(0) as XmlElement;
					if (xmlElement != null)
					{
						this.PCM.Parse(xmlElement.InnerText);
					}
				}
				foreach (KeyValuePair<int, VoiceSystem.Voice> keyValuePair in this.chara)
				{
					xmlNodeList = xml.SelectNodes(str + keyValuePair.Value.file);
					XmlElement xmlElement2 = xmlNodeList.Item(0) as XmlElement;
					if (xmlElement2 != null)
					{
						keyValuePair.Value.sound.Parse(xmlElement2.InnerText);
					}
				}
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06003674 RID: 13940 RVA: 0x00141D50 File Offset: 0x00140150
		public override void Write(XmlWriter writer)
		{
			writer.WriteStartElement(this.elementName);
			writer.WriteStartElement("PCM");
			writer.WriteValue(this.PCM);
			writer.WriteEndElement();
			foreach (KeyValuePair<int, VoiceSystem.Voice> keyValuePair in this.chara)
			{
				VoiceSystem.Voice value = keyValuePair.Value;
				writer.WriteStartElement(value.file);
				writer.WriteValue(value.sound);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		// Token: 0x040036EF RID: 14063
		public SoundData PCM = new SoundData();

		// Token: 0x040036F0 RID: 14064
		public Dictionary<int, VoiceSystem.Voice> chara;

		// Token: 0x0200085B RID: 2139
		public class Voice
		{
			// Token: 0x0600367A RID: 13946 RVA: 0x00141E9B File Offset: 0x0014029B
			public Voice(string file, SoundData sound)
			{
				this.file = file;
				this.sound = sound;
			}

			// Token: 0x170009C6 RID: 2502
			// (get) Token: 0x0600367B RID: 13947 RVA: 0x00141EB1 File Offset: 0x001402B1
			// (set) Token: 0x0600367C RID: 13948 RVA: 0x00141EB9 File Offset: 0x001402B9
			public string file { get; private set; }

			// Token: 0x170009C7 RID: 2503
			// (get) Token: 0x0600367D RID: 13949 RVA: 0x00141EC2 File Offset: 0x001402C2
			// (set) Token: 0x0600367E RID: 13950 RVA: 0x00141ECA File Offset: 0x001402CA
			public SoundData sound { get; private set; }
		}
	}
}
