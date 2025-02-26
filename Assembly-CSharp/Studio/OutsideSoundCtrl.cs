using System;
using System.IO;
using Manager;
using UnityEngine;

namespace Studio
{
	// Token: 0x020012A9 RID: 4777
	public class OutsideSoundCtrl
	{
		// Token: 0x170021C1 RID: 8641
		// (get) Token: 0x06009DF1 RID: 40433 RVA: 0x004070DF File Offset: 0x004054DF
		// (set) Token: 0x06009DF2 RID: 40434 RVA: 0x004070E7 File Offset: 0x004054E7
		public BGMCtrl.Repeat repeat
		{
			get
			{
				return this.m_Repeat;
			}
			set
			{
				if (Utility.SetStruct<BGMCtrl.Repeat>(ref this.m_Repeat, value) && this.audioSource)
				{
					this.audioSource.loop = (this.repeat == BGMCtrl.Repeat.All);
				}
			}
		}

		// Token: 0x170021C2 RID: 8642
		// (get) Token: 0x06009DF3 RID: 40435 RVA: 0x0040711E File Offset: 0x0040551E
		// (set) Token: 0x06009DF4 RID: 40436 RVA: 0x00407126 File Offset: 0x00405526
		public string fileName
		{
			get
			{
				return this.m_FileName;
			}
			set
			{
				if (this.m_FileName != value)
				{
					this.Play(value);
				}
			}
		}

		// Token: 0x170021C3 RID: 8643
		// (get) Token: 0x06009DF5 RID: 40437 RVA: 0x00407140 File Offset: 0x00405540
		// (set) Token: 0x06009DF6 RID: 40438 RVA: 0x00407148 File Offset: 0x00405548
		public bool play
		{
			get
			{
				return this.m_Play;
			}
			set
			{
				if (Utility.SetStruct<bool>(ref this.m_Play, value))
				{
					if (this.m_Play)
					{
						this.Play();
					}
					else
					{
						this.Stop();
					}
				}
			}
		}

		// Token: 0x06009DF7 RID: 40439 RVA: 0x00407177 File Offset: 0x00405577
		public void Play()
		{
			this.m_Play = true;
			this.Play(this.m_FileName);
		}

		// Token: 0x06009DF8 RID: 40440 RVA: 0x0040718C File Offset: 0x0040558C
		public void Play(string _file)
		{
			this.m_FileName = _file;
			if (!this.m_Play)
			{
				return;
			}
			string path = UserData.Create("audio") + _file;
			if (!File.Exists(path))
			{
				return;
			}
			if (Singleton<Studio>.Instance.bgmCtrl.play)
			{
				Singleton<Studio>.Instance.bgmCtrl.Stop();
			}
			Singleton<Sound>.Instance.StopBGM(0f);
			string empty = string.Empty;
			AudioClip audioClip = ExternalAudioClip.Load(path, 0L, null, ref empty);
			if (audioClip == null)
			{
				return;
			}
			Singleton<Sound>.Instance.Play(Sound.Type.BGM, audioClip, 0f);
		}

		// Token: 0x06009DF9 RID: 40441 RVA: 0x0040722C File Offset: 0x0040562C
		public void Stop()
		{
			this.m_Play = false;
			Singleton<Sound>.Instance.Stop(Sound.Type.BGM);
		}

		// Token: 0x06009DFA RID: 40442 RVA: 0x00407240 File Offset: 0x00405640
		public void Save(BinaryWriter _writer, Version _version)
		{
			_writer.Write((int)this.m_Repeat);
			_writer.Write(this.m_FileName);
			_writer.Write(this.m_Play);
		}

		// Token: 0x06009DFB RID: 40443 RVA: 0x00407266 File Offset: 0x00405666
		public void Load(BinaryReader _reader, Version _version)
		{
			this.m_Repeat = (BGMCtrl.Repeat)_reader.ReadInt32();
			this.m_FileName = _reader.ReadString();
			this.m_Play = _reader.ReadBoolean();
		}

		// Token: 0x04007DA2 RID: 32162
		public const string dataPath = "audio";

		// Token: 0x04007DA3 RID: 32163
		private BGMCtrl.Repeat m_Repeat = BGMCtrl.Repeat.All;

		// Token: 0x04007DA4 RID: 32164
		private string m_FileName = string.Empty;

		// Token: 0x04007DA5 RID: 32165
		private bool m_Play;

		// Token: 0x04007DA6 RID: 32166
		private AudioSource audioSource;
	}
}
