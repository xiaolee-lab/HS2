using System;
using System.Collections.Generic;
using System.IO;
using AIChara;
using Manager;
using UnityEngine;

namespace Studio
{
	// Token: 0x020011E4 RID: 4580
	public class VoiceCtrl
	{
		// Token: 0x17001FEA RID: 8170
		// (get) Token: 0x06009683 RID: 38531 RVA: 0x003E1C74 File Offset: 0x003E0074
		// (set) Token: 0x06009684 RID: 38532 RVA: 0x003E1C7C File Offset: 0x003E007C
		public OCIChar ociChar { get; set; }

		// Token: 0x17001FEB RID: 8171
		// (get) Token: 0x06009685 RID: 38533 RVA: 0x003E1C85 File Offset: 0x003E0085
		// (set) Token: 0x06009686 RID: 38534 RVA: 0x003E1C8D File Offset: 0x003E008D
		public Transform transVoice { get; private set; }

		// Token: 0x17001FEC RID: 8172
		// (get) Token: 0x06009687 RID: 38535 RVA: 0x003E1C96 File Offset: 0x003E0096
		public bool isPlay
		{
			get
			{
				return Singleton<Voice>.IsInstance() && Singleton<Voice>.Instance.IsVoiceCheck(this.personality, this.transHead, true);
			}
		}

		// Token: 0x17001FED RID: 8173
		// (get) Token: 0x06009688 RID: 38536 RVA: 0x003E1CBF File Offset: 0x003E00BF
		private int personality
		{
			get
			{
				return (this.ociChar == null) ? 0 : this.ociChar.charInfo.fileParam.personality;
			}
		}

		// Token: 0x17001FEE RID: 8174
		// (get) Token: 0x06009689 RID: 38537 RVA: 0x003E1CE7 File Offset: 0x003E00E7
		private float pitch
		{
			get
			{
				return (this.ociChar == null) ? 1f : this.ociChar.charInfo.fileParam.voicePitch;
			}
		}

		// Token: 0x17001FEF RID: 8175
		// (get) Token: 0x0600968A RID: 38538 RVA: 0x003E1D14 File Offset: 0x003E0114
		private Transform transHead
		{
			get
			{
				if (this.m_TransformHead == null)
				{
					GameObject gameObject = (this.ociChar == null) ? null : this.ociChar.charInfo.GetReferenceInfo(ChaReference.RefObjKey.HeadParent);
					this.m_TransformHead = ((!(gameObject != null)) ? null : gameObject.transform);
				}
				return this.m_TransformHead;
			}
		}

		// Token: 0x0600968B RID: 38539 RVA: 0x003E1D7C File Offset: 0x003E017C
		public bool Play(int _idx)
		{
			if (!Singleton<Info>.IsInstance())
			{
				return false;
			}
			if (this.list.Count == 0)
			{
				return false;
			}
			if (!MathfEx.RangeEqualOn<int>(0, _idx, this.list.Count - 1))
			{
				this.index = -1;
				return false;
			}
			this.Stop();
			VoiceCtrl.VoiceInfo voiceInfo = this.list[_idx];
			Info.LoadCommonInfo loadInfo = this.GetLoadInfo(voiceInfo.group, voiceInfo.category, voiceInfo.no);
			if (loadInfo == null)
			{
				return false;
			}
			Voice instance = Singleton<Voice>.Instance;
			int personality = this.personality;
			string bundlePath = loadInfo.bundlePath;
			string fileName = loadInfo.fileName;
			float pitch = this.pitch;
			Transform transHead = this.transHead;
			this.transVoice = instance.Play(personality, bundlePath, fileName, pitch, 0f, 0f, true, transHead, Voice.Type.PCM, -1, true, true, false);
			if (this.transVoice == null)
			{
				return false;
			}
			this.index = _idx;
			this.voiceEndChecker = this.transVoice.gameObject.AddComponent<VoiceEndChecker>();
			VoiceEndChecker voiceEndChecker = this.voiceEndChecker;
			voiceEndChecker.onEndFunc = (VoiceEndChecker.OnEndFunc)Delegate.Combine(voiceEndChecker.onEndFunc, new VoiceEndChecker.OnEndFunc(this.NextVoicePlay));
			this.ociChar.SetVoice();
			return true;
		}

		// Token: 0x0600968C RID: 38540 RVA: 0x003E1EB0 File Offset: 0x003E02B0
		public void Stop()
		{
			if (this.voiceEndChecker != null)
			{
				this.voiceEndChecker.onEndFunc = null;
			}
			if (this.transVoice != null)
			{
				Singleton<Voice>.Instance.Stop(this.personality, this.transHead);
			}
			this.transVoice = null;
			this.ociChar.SetVoice();
		}

		// Token: 0x0600968D RID: 38541 RVA: 0x003E1F14 File Offset: 0x003E0314
		public void Save(BinaryWriter _writer, Version _version)
		{
			int count = this.list.Count;
			_writer.Write(count);
			for (int i = 0; i < count; i++)
			{
				VoiceCtrl.VoiceInfo voiceInfo = this.list[i];
				_writer.Write(voiceInfo.group);
				_writer.Write(voiceInfo.category);
				_writer.Write(voiceInfo.no);
			}
			_writer.Write((int)this.repeat);
		}

		// Token: 0x0600968E RID: 38542 RVA: 0x003E1F84 File Offset: 0x003E0384
		public void Load(BinaryReader _reader, Version _version)
		{
			int num = _reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				int group = _reader.ReadInt32();
				int category = _reader.ReadInt32();
				int no = _reader.ReadInt32();
				if (this.GetLoadInfo(group, category, no) != null)
				{
					this.list.Add(new VoiceCtrl.VoiceInfo(group, category, no));
				}
			}
			this.repeat = (VoiceCtrl.Repeat)_reader.ReadInt32();
		}

		// Token: 0x0600968F RID: 38543 RVA: 0x003E1FF4 File Offset: 0x003E03F4
		public void SaveList(string _name)
		{
			string path = UserData.Create("studio/voicelist") + Utility.GetCurrentTime() + ".dat";
			using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
				{
					binaryWriter.Write("【voice】");
					binaryWriter.Write(_name);
					int count = this.list.Count;
					binaryWriter.Write(count);
					for (int i = 0; i < count; i++)
					{
						VoiceCtrl.VoiceInfo voiceInfo = this.list[i];
						binaryWriter.Write(voiceInfo.group);
						binaryWriter.Write(voiceInfo.category);
						binaryWriter.Write(voiceInfo.no);
					}
				}
			}
		}

		// Token: 0x06009690 RID: 38544 RVA: 0x003E20DC File Offset: 0x003E04DC
		public bool LoadList(string _path, bool _import = false)
		{
			if (!_import)
			{
				this.list.Clear();
			}
			using (FileStream fileStream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				using (BinaryReader binaryReader = new BinaryReader(fileStream))
				{
					if (string.Compare(binaryReader.ReadString(), "【voice】") != 0)
					{
						return false;
					}
					binaryReader.ReadString();
					int num = binaryReader.ReadInt32();
					for (int i = 0; i < num; i++)
					{
						int group = binaryReader.ReadInt32();
						int category = binaryReader.ReadInt32();
						int no = binaryReader.ReadInt32();
						if (this.GetLoadInfo(group, category, no) != null)
						{
							this.list.Add(new VoiceCtrl.VoiceInfo(group, category, no));
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06009691 RID: 38545 RVA: 0x003E21D4 File Offset: 0x003E05D4
		public static string LoadListName(string _path)
		{
			string result = string.Empty;
			using (FileStream fileStream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				using (BinaryReader binaryReader = new BinaryReader(fileStream))
				{
					if (string.Compare(binaryReader.ReadString(), "【voice】") != 0)
					{
						return string.Empty;
					}
					result = binaryReader.ReadString();
				}
			}
			return result;
		}

		// Token: 0x06009692 RID: 38546 RVA: 0x003E2264 File Offset: 0x003E0664
		public static bool CheckIdentifyingCode(string _path)
		{
			bool result = true;
			using (FileStream fileStream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				using (BinaryReader binaryReader = new BinaryReader(fileStream))
				{
					if (string.Compare(binaryReader.ReadString(), "【voice】") != 0)
					{
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x06009693 RID: 38547 RVA: 0x003E22DC File Offset: 0x003E06DC
		private Info.LoadCommonInfo GetLoadInfo(int _group, int _category, int _no)
		{
			Dictionary<int, Dictionary<int, Info.LoadCommonInfo>> dictionary = null;
			if (!Singleton<Info>.Instance.dicVoiceLoadInfo.TryGetValue(_group, out dictionary))
			{
				return null;
			}
			Dictionary<int, Info.LoadCommonInfo> dictionary2 = null;
			if (!dictionary.TryGetValue(_category, out dictionary2))
			{
				return null;
			}
			Info.LoadCommonInfo result = null;
			if (!dictionary2.TryGetValue(_no, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06009694 RID: 38548 RVA: 0x003E232C File Offset: 0x003E072C
		private void NextVoicePlay()
		{
			this.transVoice = null;
			VoiceCtrl.Repeat repeat = this.repeat;
			if (repeat != VoiceCtrl.Repeat.None)
			{
				if (repeat != VoiceCtrl.Repeat.All)
				{
					if (repeat == VoiceCtrl.Repeat.Select)
					{
						this.Play(this.index);
					}
				}
				else if (this.list.Count != 0)
				{
					this.index = (this.index + 1) % this.list.Count;
					this.Play(this.index);
				}
			}
			else
			{
				this.index++;
				this.Play(this.index);
			}
		}

		// Token: 0x040078FB RID: 30971
		public const string savePath = "studio/voicelist";

		// Token: 0x040078FC RID: 30972
		public const string saveExtension = ".dat";

		// Token: 0x040078FD RID: 30973
		public const string saveIdentifyingCode = "【voice】";

		// Token: 0x040078FE RID: 30974
		public List<VoiceCtrl.VoiceInfo> list = new List<VoiceCtrl.VoiceInfo>();

		// Token: 0x040078FF RID: 30975
		public VoiceCtrl.Repeat repeat;

		// Token: 0x04007900 RID: 30976
		public int index = -1;

		// Token: 0x04007903 RID: 30979
		private Transform m_TransformHead;

		// Token: 0x04007904 RID: 30980
		private VoiceEndChecker voiceEndChecker;

		// Token: 0x020011E5 RID: 4581
		public class VoiceInfo
		{
			// Token: 0x06009695 RID: 38549 RVA: 0x003E23D3 File Offset: 0x003E07D3
			public VoiceInfo(int _group, int _category, int _no)
			{
				this.group = _group;
				this.category = _category;
				this.no = _no;
			}

			// Token: 0x17001FF0 RID: 8176
			// (get) Token: 0x06009696 RID: 38550 RVA: 0x003E23F0 File Offset: 0x003E07F0
			// (set) Token: 0x06009697 RID: 38551 RVA: 0x003E23F8 File Offset: 0x003E07F8
			public int group { get; private set; }

			// Token: 0x17001FF1 RID: 8177
			// (get) Token: 0x06009698 RID: 38552 RVA: 0x003E2401 File Offset: 0x003E0801
			// (set) Token: 0x06009699 RID: 38553 RVA: 0x003E2409 File Offset: 0x003E0809
			public int category { get; private set; }

			// Token: 0x17001FF2 RID: 8178
			// (get) Token: 0x0600969A RID: 38554 RVA: 0x003E2412 File Offset: 0x003E0812
			// (set) Token: 0x0600969B RID: 38555 RVA: 0x003E241A File Offset: 0x003E081A
			public int no { get; private set; }
		}

		// Token: 0x020011E6 RID: 4582
		public enum Repeat
		{
			// Token: 0x04007909 RID: 30985
			None,
			// Token: 0x0400790A RID: 30986
			All,
			// Token: 0x0400790B RID: 30987
			Select
		}
	}
}
