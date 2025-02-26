using System;
using System.IO;
using Manager;
using UnityEngine;

namespace Studio
{
	// Token: 0x020012A6 RID: 4774
	public class BGMCtrl
	{
		// Token: 0x170021BA RID: 8634
		// (get) Token: 0x06009DD6 RID: 40406 RVA: 0x00406CD9 File Offset: 0x004050D9
		// (set) Token: 0x06009DD7 RID: 40407 RVA: 0x00406CE1 File Offset: 0x004050E1
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

		// Token: 0x170021BB RID: 8635
		// (get) Token: 0x06009DD8 RID: 40408 RVA: 0x00406D18 File Offset: 0x00405118
		// (set) Token: 0x06009DD9 RID: 40409 RVA: 0x00406D20 File Offset: 0x00405120
		public int no
		{
			get
			{
				return this.m_No;
			}
			set
			{
				if (this.m_No != value)
				{
					this.Play(value);
				}
			}
		}

		// Token: 0x170021BC RID: 8636
		// (get) Token: 0x06009DDA RID: 40410 RVA: 0x00406D35 File Offset: 0x00405135
		// (set) Token: 0x06009DDB RID: 40411 RVA: 0x00406D3D File Offset: 0x0040513D
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

		// Token: 0x170021BD RID: 8637
		// (get) Token: 0x06009DDC RID: 40412 RVA: 0x00406D6C File Offset: 0x0040516C
		// (set) Token: 0x06009DDD RID: 40413 RVA: 0x00406D74 File Offset: 0x00405174
		public bool isPause { get; private set; }

		// Token: 0x06009DDE RID: 40414 RVA: 0x00406D7D File Offset: 0x0040517D
		public void Play()
		{
			if (this.isPause)
			{
				this.isPause = false;
				Singleton<Sound>.Instance.PlayBGM(0f);
			}
			else
			{
				this.m_Play = true;
				this.Play(this.m_No);
			}
		}

		// Token: 0x06009DDF RID: 40415 RVA: 0x00406DB8 File Offset: 0x004051B8
		public void Play(int _no)
		{
			this.m_No = _no;
			if (!this.m_Play)
			{
				return;
			}
			Info.LoadCommonInfo loadCommonInfo = null;
			if (!Singleton<Info>.Instance.dicBGMLoadInfo.TryGetValue(this.m_No, out loadCommonInfo))
			{
				return;
			}
			if (Singleton<Studio>.Instance.outsideSoundCtrl.play)
			{
				Singleton<Studio>.Instance.outsideSoundCtrl.Stop();
			}
			Transform transform = Singleton<Sound>.Instance.Play(Sound.Type.BGM, loadCommonInfo.bundlePath, loadCommonInfo.fileName, 0f, 0f, true, false, -1, true);
			if (transform == null)
			{
				return;
			}
			this.audioSource = transform.GetComponent<AudioSource>();
			this.audioSource.loop = (this.repeat == BGMCtrl.Repeat.All);
			this.isPause = false;
		}

		// Token: 0x06009DE0 RID: 40416 RVA: 0x00406E74 File Offset: 0x00405274
		public void Stop()
		{
			this.m_Play = false;
			this.isPause = false;
			Singleton<Sound>.Instance.StopBGM(0f);
		}

		// Token: 0x06009DE1 RID: 40417 RVA: 0x00406E93 File Offset: 0x00405293
		public void Pause()
		{
			if (!this.m_Play)
			{
				return;
			}
			this.isPause = true;
			Singleton<Sound>.Instance.PauseBGM();
		}

		// Token: 0x06009DE2 RID: 40418 RVA: 0x00406EB2 File Offset: 0x004052B2
		public void Save(BinaryWriter _writer, Version _version)
		{
			_writer.Write((int)this.m_Repeat);
			_writer.Write(this.m_No);
			_writer.Write(this.m_Play);
		}

		// Token: 0x06009DE3 RID: 40419 RVA: 0x00406ED8 File Offset: 0x004052D8
		public void Load(BinaryReader _reader, Version _version)
		{
			this.m_Repeat = (BGMCtrl.Repeat)_reader.ReadInt32();
			this.m_No = _reader.ReadInt32();
			this.m_Play = _reader.ReadBoolean();
		}

		// Token: 0x04007D96 RID: 32150
		private BGMCtrl.Repeat m_Repeat = BGMCtrl.Repeat.All;

		// Token: 0x04007D97 RID: 32151
		private int m_No;

		// Token: 0x04007D98 RID: 32152
		private bool m_Play;

		// Token: 0x04007D9A RID: 32154
		private AudioSource audioSource;

		// Token: 0x020012A7 RID: 4775
		public enum Repeat
		{
			// Token: 0x04007D9C RID: 32156
			None,
			// Token: 0x04007D9D RID: 32157
			All
		}
	}
}
