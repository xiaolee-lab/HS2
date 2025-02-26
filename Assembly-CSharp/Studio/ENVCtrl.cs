using System;
using System.IO;
using Manager;
using UnityEngine;

namespace Studio
{
	// Token: 0x020012A8 RID: 4776
	public class ENVCtrl
	{
		// Token: 0x170021BE RID: 8638
		// (get) Token: 0x06009DE5 RID: 40421 RVA: 0x00406F0D File Offset: 0x0040530D
		// (set) Token: 0x06009DE6 RID: 40422 RVA: 0x00406F15 File Offset: 0x00405315
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

		// Token: 0x170021BF RID: 8639
		// (get) Token: 0x06009DE7 RID: 40423 RVA: 0x00406F4C File Offset: 0x0040534C
		// (set) Token: 0x06009DE8 RID: 40424 RVA: 0x00406F54 File Offset: 0x00405354
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

		// Token: 0x170021C0 RID: 8640
		// (get) Token: 0x06009DE9 RID: 40425 RVA: 0x00406F69 File Offset: 0x00405369
		// (set) Token: 0x06009DEA RID: 40426 RVA: 0x00406F71 File Offset: 0x00405371
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

		// Token: 0x06009DEB RID: 40427 RVA: 0x00406FA0 File Offset: 0x004053A0
		public void Play()
		{
			this.m_Play = true;
			this.Play(this.m_No);
		}

		// Token: 0x06009DEC RID: 40428 RVA: 0x00406FB8 File Offset: 0x004053B8
		public void Play(int _no)
		{
			this.m_No = _no;
			if (!this.m_Play)
			{
				return;
			}
			Info.LoadCommonInfo loadCommonInfo = null;
			if (!Singleton<Info>.Instance.dicENVLoadInfo.TryGetValue(this.m_No, out loadCommonInfo))
			{
				return;
			}
			Singleton<Sound>.Instance.Stop(Sound.Type.ENV);
			Transform transform = Singleton<Sound>.Instance.Play(Sound.Type.ENV, loadCommonInfo.bundlePath, loadCommonInfo.fileName, 0f, 0f, true, true, -1, true);
			if (transform == null)
			{
				return;
			}
			this.audioSource = transform.GetComponent<AudioSource>();
			this.audioSource.loop = (this.repeat == BGMCtrl.Repeat.All);
			this.audioSource.spatialBlend = 0f;
		}

		// Token: 0x06009DED RID: 40429 RVA: 0x00407065 File Offset: 0x00405465
		public void Stop()
		{
			this.m_Play = false;
			Singleton<Sound>.Instance.Stop(Sound.Type.ENV);
		}

		// Token: 0x06009DEE RID: 40430 RVA: 0x00407079 File Offset: 0x00405479
		public void Save(BinaryWriter _writer, Version _version)
		{
			_writer.Write((int)this.m_Repeat);
			_writer.Write(this.m_No);
			_writer.Write(this.m_Play);
		}

		// Token: 0x06009DEF RID: 40431 RVA: 0x0040709F File Offset: 0x0040549F
		public void Load(BinaryReader _reader, Version _version)
		{
			this.m_Repeat = (BGMCtrl.Repeat)_reader.ReadInt32();
			this.m_No = _reader.ReadInt32();
			this.m_Play = _reader.ReadBoolean();
		}

		// Token: 0x04007D9E RID: 32158
		private BGMCtrl.Repeat m_Repeat = BGMCtrl.Repeat.All;

		// Token: 0x04007D9F RID: 32159
		private int m_No;

		// Token: 0x04007DA0 RID: 32160
		private bool m_Play;

		// Token: 0x04007DA1 RID: 32161
		private AudioSource audioSource;
	}
}
