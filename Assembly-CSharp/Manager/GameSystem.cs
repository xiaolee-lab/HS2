using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;
using UploaderSystem;

namespace Manager
{
	// Token: 0x020008E2 RID: 2274
	public sealed class GameSystem : Singleton<GameSystem>
	{
		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06003CAA RID: 15530 RVA: 0x00162E48 File Offset: 0x00161248
		// (set) Token: 0x06003CAB RID: 15531 RVA: 0x00162E50 File Offset: 0x00161250
		public string EncryptedMacAddress { get; private set; }

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x06003CAC RID: 15532 RVA: 0x00162E59 File Offset: 0x00161259
		// (set) Token: 0x06003CAD RID: 15533 RVA: 0x00162E61 File Offset: 0x00161261
		public string UserUUID { get; private set; }

		// Token: 0x06003CAE RID: 15534 RVA: 0x00162E6A File Offset: 0x0016126A
		public void SetUserUUID(string uuid)
		{
			this.UserUUID = uuid;
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x06003CAF RID: 15535 RVA: 0x00162E73 File Offset: 0x00161273
		// (set) Token: 0x06003CB0 RID: 15536 RVA: 0x00162E7B File Offset: 0x0016127B
		public string UserPasswd { get; private set; }

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06003CB1 RID: 15537 RVA: 0x00162E84 File Offset: 0x00161284
		// (set) Token: 0x06003CB2 RID: 15538 RVA: 0x00162E8C File Offset: 0x0016128C
		public string HandleName { get; private set; }

		// Token: 0x06003CB3 RID: 15539 RVA: 0x00162E98 File Offset: 0x00161298
		public void SaveHandleName(string hn)
		{
			this.HandleName = hn;
			string path = UserData.Create("system/") + "hn.dat";
			byte[] bytes = Encoding.UTF8.GetBytes(this.HandleName);
			byte[] bytes2 = YS_Assist.EncryptAES(bytes, "illusion", "ai-syoujyo");
			File.WriteAllBytes(path, bytes2);
		}

		// Token: 0x06003CB4 RID: 15540 RVA: 0x00162EEC File Offset: 0x001612EC
		public void LoadHandleName()
		{
			string path = UserData.Create("system/") + "hn.dat";
			if (File.Exists(path))
			{
				try
				{
					byte[] srcData = File.ReadAllBytes(path);
					byte[] bytes = YS_Assist.DecryptAES(srcData, "illusion", "ai-syoujyo");
					this.HandleName = Encoding.UTF8.GetString(bytes);
				}
				catch (Exception)
				{
					this.HandleName = string.Empty;
				}
			}
			else
			{
				this.HandleName = string.Empty;
			}
		}

		// Token: 0x06003CB5 RID: 15541 RVA: 0x00162F78 File Offset: 0x00161378
		public void GenerateUserInfo(bool forceGenerate = false)
		{
			if (forceGenerate || !this.LoadIdAndPass())
			{
				this.UserUUID = YS_Assist.CreateUUID();
				this.UserPasswd = YS_Assist.GeneratePassword62(16);
				this.SaveIdAndPass();
			}
		}

		// Token: 0x06003CB6 RID: 15542 RVA: 0x00162FAC File Offset: 0x001613AC
		public void SaveIdAndPass()
		{
			string path = UserData.Create("system/") + "userinfo.dat";
			File.WriteAllLines(path, new string[]
			{
				this.UserInfoFileVersion.ToString(),
				this.UserUUID,
				this.UserPasswd
			});
		}

		// Token: 0x06003CB7 RID: 15543 RVA: 0x00162FFC File Offset: 0x001613FC
		public bool LoadIdAndPass()
		{
			try
			{
				string path = UserData.Create("system/") + "userinfo.dat";
				if (!File.Exists(path))
				{
					return false;
				}
				string[] array = File.ReadAllLines(path);
				if (array.IsNullOrEmpty<string>())
				{
					return false;
				}
				Version version = new Version(array[0]);
				if (array.Length != 3)
				{
					return false;
				}
				if (array[1].IsNullOrEmpty() || array[2].IsNullOrEmpty())
				{
					return false;
				}
				this.UserUUID = array[1];
				this.UserPasswd = array[2];
				return true;
			}
			catch (Exception)
			{
			}
			return false;
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06003CB8 RID: 15544 RVA: 0x001630B8 File Offset: 0x001614B8
		// (set) Token: 0x06003CB9 RID: 15545 RVA: 0x001630C0 File Offset: 0x001614C0
		public Version GameVersion { get; private set; } = new Version("1.0.0");

		// Token: 0x06003CBA RID: 15546 RVA: 0x001630CC File Offset: 0x001614CC
		public void SaveVersion()
		{
			string path = DefaultData.Create("system/") + "version.dat";
			File.WriteAllText(path, "1.2.2");
		}

		// Token: 0x06003CBB RID: 15547 RVA: 0x001630FC File Offset: 0x001614FC
		public void LoadVersion()
		{
			string path = DefaultData.Path + "system//version.dat";
			if (File.Exists(path))
			{
				string text = File.ReadAllText(path);
				if (!text.IsNullOrEmpty())
				{
					this.GameVersion = new Version(text);
				}
			}
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06003CBC RID: 15548 RVA: 0x00163142 File Offset: 0x00161542
		// (set) Token: 0x06003CBD RID: 15549 RVA: 0x0016314A File Offset: 0x0016154A
		public GameSystem.Language language { get; private set; }

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06003CBE RID: 15550 RVA: 0x00163153 File Offset: 0x00161553
		public int languageInt
		{
			get
			{
				return (int)this.language;
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06003CBF RID: 15551 RVA: 0x0016315B File Offset: 0x0016155B
		public string cultrureName
		{
			get
			{
				return this.cultureNames[this.languageInt];
			}
		}

		// Token: 0x06003CC0 RID: 15552 RVA: 0x0016316C File Offset: 0x0016156C
		private void LoadLanguage()
		{
			string text = UserData.Path + "setup.xml";
			if (!File.Exists(text))
			{
				this.language = GameSystem.Language.Japanese;
				return;
			}
			try
			{
				XElement xelement = XElement.Load(text);
				if (xelement != null)
				{
					IEnumerable<XElement> enumerable = xelement.Elements();
					foreach (XElement xelement2 in enumerable)
					{
						if (xelement2.Name.ToString() == "Language")
						{
							this.language = (GameSystem.Language)int.Parse(xelement2.Value);
							break;
						}
					}
				}
			}
			catch (XmlException)
			{
			}
		}

		// Token: 0x06003CC1 RID: 15553 RVA: 0x0016323C File Offset: 0x0016163C
		public void SaveNetworkSetting()
		{
			string path = UserData.Create("system/") + "netsave.dat";
			using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
				{
					binaryWriter.Write(this.NetSaveFileVersion.ToString());
					binaryWriter.Write(this.agreePolicy);
				}
			}
		}

		// Token: 0x06003CC2 RID: 15554 RVA: 0x001632CC File Offset: 0x001616CC
		public void LoadNetworkSetting()
		{
			string path = UserData.Path + "system//netsave.dat";
			if (File.Exists(path))
			{
				using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
				{
					using (BinaryReader binaryReader = new BinaryReader(fileStream))
					{
						Version version = new Version(binaryReader.ReadString());
						this.agreePolicy = binaryReader.ReadBoolean();
					}
				}
			}
		}

		// Token: 0x06003CC3 RID: 15555 RVA: 0x0016335C File Offset: 0x0016175C
		public bool SaveDownloadInfo()
		{
			string path = UserData.Create("system/") + "dli.sav";
			using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
				{
					HashSet<string> hsChara = this.downloadInfo.hsChara;
					int count = hsChara.Count;
					binaryWriter.Write(count);
					foreach (string value in hsChara)
					{
						binaryWriter.Write(value);
					}
					HashSet<string> hsHousing = this.downloadInfo.hsHousing;
					count = hsHousing.Count;
					binaryWriter.Write(count);
					foreach (string value2 in hsHousing)
					{
						binaryWriter.Write(value2);
					}
				}
			}
			return true;
		}

		// Token: 0x06003CC4 RID: 15556 RVA: 0x0016349C File Offset: 0x0016189C
		public bool LoadDownloadInfo()
		{
			string path = UserData.Path + "system/dli.sav";
			if (!File.Exists(path))
			{
				return false;
			}
			this.downloadInfo.hsChara.Clear();
			this.downloadInfo.hsHousing.Clear();
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				using (BinaryReader binaryReader = new BinaryReader(fileStream))
				{
					int num = binaryReader.ReadInt32();
					for (int i = 0; i < num; i++)
					{
						this.downloadInfo.hsChara.Add(binaryReader.ReadString());
					}
					num = binaryReader.ReadInt32();
					for (int j = 0; j < num; j++)
					{
						this.downloadInfo.hsHousing.Add(binaryReader.ReadString());
					}
				}
			}
			return true;
		}

		// Token: 0x06003CC5 RID: 15557 RVA: 0x001635A0 File Offset: 0x001619A0
		public void AddDownload(DataType type, string uuid)
		{
			if (type != DataType.Chara)
			{
				if (type != DataType.Housing)
				{
					return;
				}
				this.downloadInfo.hsHousing.Add(uuid);
			}
			else
			{
				this.downloadInfo.hsChara.Add(uuid);
			}
			this.SaveDownloadInfo();
		}

		// Token: 0x06003CC6 RID: 15558 RVA: 0x001635F5 File Offset: 0x001619F5
		public bool IsDownload(DataType type, string uuid)
		{
			if (type != DataType.Chara)
			{
				return type == DataType.Housing && this.downloadInfo.hsHousing.Contains(uuid);
			}
			return this.downloadInfo.hsChara.Contains(uuid);
		}

		// Token: 0x06003CC7 RID: 15559 RVA: 0x00163634 File Offset: 0x00161A34
		public bool SaveApplauseInfo()
		{
			string path = UserData.Create("system/") + "ali.sav";
			using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
				{
					HashSet<string> hsChara = this.applauseInfo.hsChara;
					int count = hsChara.Count;
					binaryWriter.Write(count);
					foreach (string value in hsChara)
					{
						binaryWriter.Write(value);
					}
					HashSet<string> hsHousing = this.applauseInfo.hsHousing;
					count = hsHousing.Count;
					binaryWriter.Write(count);
					foreach (string value2 in hsHousing)
					{
						binaryWriter.Write(value2);
					}
				}
			}
			return true;
		}

		// Token: 0x06003CC8 RID: 15560 RVA: 0x00163774 File Offset: 0x00161B74
		public bool LoadApplauseInfo()
		{
			string path = UserData.Path + "system/ali.sav";
			if (!File.Exists(path))
			{
				return false;
			}
			this.applauseInfo.hsChara.Clear();
			this.applauseInfo.hsHousing.Clear();
			using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				using (BinaryReader binaryReader = new BinaryReader(fileStream))
				{
					int num = binaryReader.ReadInt32();
					for (int i = 0; i < num; i++)
					{
						this.applauseInfo.hsChara.Add(binaryReader.ReadString());
					}
					num = binaryReader.ReadInt32();
					for (int j = 0; j < num; j++)
					{
						this.applauseInfo.hsHousing.Add(binaryReader.ReadString());
					}
				}
			}
			return true;
		}

		// Token: 0x06003CC9 RID: 15561 RVA: 0x00163878 File Offset: 0x00161C78
		public void AddApplause(DataType type, string uuid)
		{
			if (type != DataType.Chara)
			{
				if (type != DataType.Housing)
				{
					return;
				}
				this.applauseInfo.hsHousing.Add(uuid);
			}
			else
			{
				this.applauseInfo.hsChara.Add(uuid);
			}
			this.SaveApplauseInfo();
		}

		// Token: 0x06003CCA RID: 15562 RVA: 0x001638CD File Offset: 0x00161CCD
		public bool IsApplause(DataType type, string uuid)
		{
			if (type != DataType.Chara)
			{
				return type == DataType.Housing && this.applauseInfo.hsHousing.Contains(uuid);
			}
			return this.applauseInfo.hsChara.Contains(uuid);
		}

		// Token: 0x06003CCB RID: 15563 RVA: 0x0016390C File Offset: 0x00161D0C
		protected override void Awake()
		{
			if (!base.CheckInstance())
			{
				return;
			}
			string path = UserData.Path + "bg/";
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			path = UserData.Path + "cardframe/Back/";
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			path = UserData.Path + "cardframe/Front/";
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			this.EncryptedMacAddress = YS_Assist.CreateIrregularStringFromMacAddress();
			this.GenerateUserInfo(false);
			this.LoadHandleName();
			this.GameVersion = new Version(0, 0, 0);
			this.LoadVersion();
			this.LoadLanguage();
			this.LoadNetworkSetting();
			this.LoadDownloadInfo();
			this.LoadApplauseInfo();
		}

		// Token: 0x06003CCC RID: 15564 RVA: 0x001639DC File Offset: 0x00161DDC
		private void OnApplicationQuit()
		{
		}

		// Token: 0x04003ADD RID: 15069
		public const string BGDir = "bg/";

		// Token: 0x04003ADE RID: 15070
		public const string CardFDir = "cardframe/Front/";

		// Token: 0x04003ADF RID: 15071
		public const string CardBDir = "cardframe/Back/";

		// Token: 0x04003AE0 RID: 15072
		public const string SystemDir = "system/";

		// Token: 0x04003AE1 RID: 15073
		public const string VersionFile = "version.dat";

		// Token: 0x04003AE2 RID: 15074
		public const string SetupFile = "setup.xml";

		// Token: 0x04003AE3 RID: 15075
		public const string HNFile = "hn.dat";

		// Token: 0x04003AE4 RID: 15076
		public const string UserInfoFile = "userinfo.dat";

		// Token: 0x04003AE5 RID: 15077
		public const string NetSaveFile = "netsave.dat";

		// Token: 0x04003AE6 RID: 15078
		public const string DownloadInfoFile = "dli.sav";

		// Token: 0x04003AE7 RID: 15079
		public const string ApplauseInfoFile = "ali.sav";

		// Token: 0x04003AE8 RID: 15080
		public const int ProductNo = 100;

		// Token: 0x04003AE9 RID: 15081
		public const string CharaFileMark = "【AIS_Chara】";

		// Token: 0x04003AEA RID: 15082
		public const string ClothesFileMark = "【AIS_Clothes】";

		// Token: 0x04003AEB RID: 15083
		public const string HousingFileMark = "【AIS_Housing】";

		// Token: 0x04003AEC RID: 15084
		public const string StudioFileMark = "【AIS_Studio】";

		// Token: 0x04003AED RID: 15085
		public readonly Version UserInfoFileVersion = new Version(0, 0, 0);

		// Token: 0x04003AEE RID: 15086
		public readonly Version NetSaveFileVersion = new Version(0, 0, 0);

		// Token: 0x04003AF3 RID: 15091
		public const string GameSystemVersion = "1.2.2";

		// Token: 0x04003AF6 RID: 15094
		public readonly string[] cultureNames = new string[]
		{
			"ja-JP"
		};

		// Token: 0x04003AF7 RID: 15095
		public string networkSceneName = string.Empty;

		// Token: 0x04003AF8 RID: 15096
		public bool agreePolicy;

		// Token: 0x04003AF9 RID: 15097
		public GameSystem.DownloadInfo downloadInfo = new GameSystem.DownloadInfo();

		// Token: 0x04003AFA RID: 15098
		public GameSystem.ApplauseInfo applauseInfo = new GameSystem.ApplauseInfo();

		// Token: 0x020008E3 RID: 2275
		public enum Language
		{
			// Token: 0x04003AFC RID: 15100
			Japanese
		}

		// Token: 0x020008E4 RID: 2276
		public class CultureScope : IDisposable
		{
			// Token: 0x06003CCD RID: 15565 RVA: 0x001639E0 File Offset: 0x00161DE0
			public CultureScope()
			{
				string cultrureName = Singleton<GameSystem>.Instance.cultrureName;
				this.culture = Thread.CurrentThread.CurrentCulture;
				Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(cultrureName);
			}

			// Token: 0x06003CCE RID: 15566 RVA: 0x00163A1E File Offset: 0x00161E1E
			void IDisposable.Dispose()
			{
				Thread.CurrentThread.CurrentCulture = this.culture;
			}

			// Token: 0x04003AFD RID: 15101
			private CultureInfo culture;
		}

		// Token: 0x020008E5 RID: 2277
		public class DownloadInfo
		{
			// Token: 0x04003AFE RID: 15102
			public HashSet<string> hsChara = new HashSet<string>();

			// Token: 0x04003AFF RID: 15103
			public HashSet<string> hsHousing = new HashSet<string>();
		}

		// Token: 0x020008E6 RID: 2278
		public class ApplauseInfo
		{
			// Token: 0x04003B00 RID: 15104
			public HashSet<string> hsChara = new HashSet<string>();

			// Token: 0x04003B01 RID: 15105
			public HashSet<string> hsHousing = new HashSet<string>();
		}
	}
}
