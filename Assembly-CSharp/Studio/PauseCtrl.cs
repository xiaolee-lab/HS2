using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Studio
{
	// Token: 0x020011E0 RID: 4576
	public class PauseCtrl
	{
		// Token: 0x0600966C RID: 38508 RVA: 0x003E1020 File Offset: 0x003DF420
		public static void Save(OCIChar _ociChar, string _name)
		{
			string path = UserData.Create("studio/pose") + Utility.GetCurrentTime() + ".dat";
			PauseCtrl.FileInfo fileInfo = new PauseCtrl.FileInfo(_ociChar);
			using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
				{
					binaryWriter.Write("【pose】");
					binaryWriter.Write(101);
					binaryWriter.Write(_ociChar.oiCharInfo.sex);
					binaryWriter.Write(_name);
					fileInfo.Save(binaryWriter);
				}
			}
		}

		// Token: 0x0600966D RID: 38509 RVA: 0x003E10D0 File Offset: 0x003DF4D0
		public static bool Load(OCIChar _ociChar, string _path)
		{
			PauseCtrl.FileInfo fileInfo = new PauseCtrl.FileInfo(null);
			using (FileStream fileStream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				using (BinaryReader binaryReader = new BinaryReader(fileStream))
				{
					if (string.Compare(binaryReader.ReadString(), "【pose】") != 0)
					{
						return false;
					}
					int ver = binaryReader.ReadInt32();
					binaryReader.ReadInt32();
					binaryReader.ReadString();
					fileInfo.Load(binaryReader, ver);
				}
			}
			fileInfo.Apply(_ociChar);
			return true;
		}

		// Token: 0x0600966E RID: 38510 RVA: 0x003E117C File Offset: 0x003DF57C
		public static bool CheckIdentifyingCode(string _path, int _sex)
		{
			using (FileStream fileStream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				using (BinaryReader binaryReader = new BinaryReader(fileStream))
				{
					if (string.Compare(binaryReader.ReadString(), "【pose】") != 0)
					{
						return false;
					}
					binaryReader.ReadInt32();
					if (_sex != binaryReader.ReadInt32())
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600966F RID: 38511 RVA: 0x003E1214 File Offset: 0x003DF614
		public static string LoadName(string _path)
		{
			string result;
			using (FileStream fileStream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				using (BinaryReader binaryReader = new BinaryReader(fileStream))
				{
					if (string.Compare(binaryReader.ReadString(), "【pose】") != 0)
					{
						result = string.Empty;
					}
					else
					{
						binaryReader.ReadInt32();
						binaryReader.ReadInt32();
						result = binaryReader.ReadString();
					}
				}
			}
			return result;
		}

		// Token: 0x040078E4 RID: 30948
		public const string savePath = "studio/pose";

		// Token: 0x040078E5 RID: 30949
		public const string saveExtension = ".dat";

		// Token: 0x040078E6 RID: 30950
		public const string saveIdentifyingCode = "【pose】";

		// Token: 0x040078E7 RID: 30951
		public const int saveVersion = 101;

		// Token: 0x020011E1 RID: 4577
		public class FileInfo
		{
			// Token: 0x06009670 RID: 38512 RVA: 0x003E12A4 File Offset: 0x003DF6A4
			public FileInfo(OCIChar _char = null)
			{
				bool[] array = new bool[7];
				array[1] = true;
				array[3] = true;
				this.activeFK = array;
				this.dicFK = new Dictionary<int, ChangeAmount>();
				this.expression = new bool[]
				{
					true,
					true,
					true,
					true
				};
				base..ctor();
				if (_char == null)
				{
					return;
				}
				this.group = _char.oiCharInfo.animeInfo.group;
				this.category = _char.oiCharInfo.animeInfo.category;
				this.no = _char.oiCharInfo.animeInfo.no;
				this.normalizedTime = _char.charAnimeCtrl.normalizedTime;
				this.enableIK = _char.oiCharInfo.enableIK;
				for (int i = 0; i < this.activeIK.Length; i++)
				{
					this.activeIK[i] = _char.oiCharInfo.activeIK[i];
				}
				foreach (KeyValuePair<int, OIIKTargetInfo> keyValuePair in _char.oiCharInfo.ikTarget)
				{
					this.dicIK.Add(keyValuePair.Key, keyValuePair.Value.changeAmount.Clone());
				}
				this.enableFK = _char.oiCharInfo.enableFK;
				for (int j = 0; j < this.activeFK.Length; j++)
				{
					this.activeFK[j] = _char.oiCharInfo.activeFK[j];
				}
				OIBoneInfo.BoneGroup fkGroup = (OIBoneInfo.BoneGroup)895;
				foreach (KeyValuePair<int, OIBoneInfo> keyValuePair2 in from b in _char.oiCharInfo.bones
				where (fkGroup & b.Value.@group) != (OIBoneInfo.BoneGroup)0
				select b)
				{
					this.dicFK.Add(keyValuePair2.Key, keyValuePair2.Value.changeAmount.Clone());
				}
				for (int k = 0; k < this.expression.Length; k++)
				{
					this.expression[k] = _char.oiCharInfo.expression[k];
				}
			}

			// Token: 0x06009671 RID: 38513 RVA: 0x003E1538 File Offset: 0x003DF938
			public void Apply(OCIChar _char)
			{
				_char.LoadAnime(this.group, this.category, this.no, this.normalizedTime);
				for (int i = 0; i < this.activeIK.Length; i++)
				{
					_char.ActiveIK((OIBoneInfo.BoneGroup)(1 << i), this.activeIK[i], false);
				}
				_char.ActiveKinematicMode(OICharInfo.KinematicMode.IK, this.enableIK, true);
				foreach (KeyValuePair<int, ChangeAmount> keyValuePair in this.dicIK)
				{
					_char.oiCharInfo.ikTarget[keyValuePair.Key].changeAmount.Copy(keyValuePair.Value, true, true, true);
				}
				for (int j = 0; j < this.activeFK.Length; j++)
				{
					_char.ActiveFK(FKCtrl.parts[j], this.activeFK[j], false);
				}
				_char.ActiveKinematicMode(OICharInfo.KinematicMode.FK, this.enableFK, true);
				foreach (KeyValuePair<int, ChangeAmount> keyValuePair2 in this.dicFK)
				{
					_char.oiCharInfo.bones[keyValuePair2.Key].changeAmount.Copy(keyValuePair2.Value, true, true, true);
				}
				for (int k = 0; k < this.expression.Length; k++)
				{
					_char.EnableExpressionCategory(k, this.expression[k]);
				}
			}

			// Token: 0x06009672 RID: 38514 RVA: 0x003E16F0 File Offset: 0x003DFAF0
			public void Save(BinaryWriter _writer)
			{
				_writer.Write(this.group);
				_writer.Write(this.category);
				_writer.Write(this.no);
				_writer.Write(this.normalizedTime);
				_writer.Write(this.enableIK);
				for (int i = 0; i < this.activeIK.Length; i++)
				{
					_writer.Write(this.activeIK[i]);
				}
				_writer.Write(this.dicIK.Count);
				foreach (KeyValuePair<int, ChangeAmount> keyValuePair in this.dicIK)
				{
					_writer.Write(keyValuePair.Key);
					keyValuePair.Value.Save(_writer);
				}
				_writer.Write(this.enableFK);
				for (int j = 0; j < this.activeFK.Length; j++)
				{
					_writer.Write(this.activeFK[j]);
				}
				_writer.Write(this.dicFK.Count);
				foreach (KeyValuePair<int, ChangeAmount> keyValuePair2 in this.dicFK)
				{
					_writer.Write(keyValuePair2.Key);
					keyValuePair2.Value.Save(_writer);
				}
				for (int k = 0; k < this.expression.Length; k++)
				{
					_writer.Write(this.expression[k]);
				}
			}

			// Token: 0x06009673 RID: 38515 RVA: 0x003E18A4 File Offset: 0x003DFCA4
			public void Load(BinaryReader _reader, int _ver)
			{
				this.group = _reader.ReadInt32();
				this.category = _reader.ReadInt32();
				this.no = _reader.ReadInt32();
				if (_ver >= 101)
				{
					this.normalizedTime = _reader.ReadSingle();
				}
				this.enableIK = _reader.ReadBoolean();
				for (int i = 0; i < this.activeIK.Length; i++)
				{
					this.activeIK[i] = _reader.ReadBoolean();
				}
				int num = _reader.ReadInt32();
				for (int j = 0; j < num; j++)
				{
					int key = _reader.ReadInt32();
					ChangeAmount changeAmount = new ChangeAmount();
					changeAmount.Load(_reader);
					try
					{
						this.dicIK.Add(key, changeAmount);
					}
					catch (Exception)
					{
					}
				}
				this.enableFK = _reader.ReadBoolean();
				for (int k = 0; k < this.activeFK.Length; k++)
				{
					this.activeFK[k] = _reader.ReadBoolean();
				}
				num = _reader.ReadInt32();
				for (int l = 0; l < num; l++)
				{
					int key2 = _reader.ReadInt32();
					ChangeAmount changeAmount2 = new ChangeAmount();
					changeAmount2.Load(_reader);
					this.dicFK.Add(key2, changeAmount2);
				}
				for (int m = 0; m < this.expression.Length; m++)
				{
					this.expression[m] = _reader.ReadBoolean();
				}
			}

			// Token: 0x040078E8 RID: 30952
			public int group = -1;

			// Token: 0x040078E9 RID: 30953
			public int category = -1;

			// Token: 0x040078EA RID: 30954
			public int no = -1;

			// Token: 0x040078EB RID: 30955
			public float normalizedTime;

			// Token: 0x040078EC RID: 30956
			public bool enableIK;

			// Token: 0x040078ED RID: 30957
			public bool[] activeIK = new bool[]
			{
				true,
				true,
				true,
				true,
				true
			};

			// Token: 0x040078EE RID: 30958
			public Dictionary<int, ChangeAmount> dicIK = new Dictionary<int, ChangeAmount>();

			// Token: 0x040078EF RID: 30959
			public bool enableFK;

			// Token: 0x040078F0 RID: 30960
			public bool[] activeFK;

			// Token: 0x040078F1 RID: 30961
			public Dictionary<int, ChangeAmount> dicFK;

			// Token: 0x040078F2 RID: 30962
			public bool[] expression;
		}
	}
}
