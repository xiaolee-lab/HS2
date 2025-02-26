using System;
using System.Collections.Generic;
using System.IO;
using MessagePack;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001234 RID: 4660
	public class SceneInfo
	{
		// Token: 0x06009956 RID: 39254 RVA: 0x003F1DB4 File Offset: 0x003F01B4
		public SceneInfo()
		{
			this.dicObject = new Dictionary<int, ObjectInfo>();
			this.cameraData = new CameraControl.CameraData[10];
			for (int i = 0; i < this.cameraData.Length; i++)
			{
				this.cameraData[i] = new CameraControl.CameraData();
			}
			this.hashIndex = new HashSet<int>();
			ChangeAmount ca = this.mapInfo.ca;
			ca.onChangePos = (Action)Delegate.Combine(ca.onChangePos, new Action(Singleton<MapCtrl>.Instance.Reflect));
			ChangeAmount ca2 = this.mapInfo.ca;
			ca2.onChangeRot = (Action)Delegate.Combine(ca2.onChangeRot, new Action(Singleton<MapCtrl>.Instance.Reflect));
			this.Init();
		}

		// Token: 0x170020AA RID: 8362
		// (get) Token: 0x06009957 RID: 39255 RVA: 0x003F1EE7 File Offset: 0x003F02E7
		public Version version
		{
			get
			{
				return this.m_Version;
			}
		}

		// Token: 0x170020AB RID: 8363
		// (get) Token: 0x06009958 RID: 39256 RVA: 0x003F1EEF File Offset: 0x003F02EF
		// (set) Token: 0x06009959 RID: 39257 RVA: 0x003F1EF7 File Offset: 0x003F02F7
		public Dictionary<int, ObjectInfo> dicImport { get; private set; }

		// Token: 0x170020AC RID: 8364
		// (get) Token: 0x0600995A RID: 39258 RVA: 0x003F1F00 File Offset: 0x003F0300
		// (set) Token: 0x0600995B RID: 39259 RVA: 0x003F1F08 File Offset: 0x003F0308
		public Dictionary<int, int> dicChangeKey { get; private set; }

		// Token: 0x170020AD RID: 8365
		// (get) Token: 0x0600995C RID: 39260 RVA: 0x003F1F11 File Offset: 0x003F0311
		public bool isLightCheck
		{
			get
			{
				return this.lightCount < 2;
			}
		}

		// Token: 0x170020AE RID: 8366
		// (get) Token: 0x0600995D RID: 39261 RVA: 0x003F1F1C File Offset: 0x003F031C
		public bool isLightLimitOver
		{
			get
			{
				return this.lightCount > 2;
			}
		}

		// Token: 0x170020AF RID: 8367
		// (get) Token: 0x0600995E RID: 39262 RVA: 0x003F1F27 File Offset: 0x003F0327
		// (set) Token: 0x0600995F RID: 39263 RVA: 0x003F1F2F File Offset: 0x003F032F
		public Version dataVersion { get; set; }

		// Token: 0x06009960 RID: 39264 RVA: 0x003F1F38 File Offset: 0x003F0338
		public void Init()
		{
			this.dicObject.Clear();
			this.mapInfo.no = -1;
			this.mapInfo.ca.Reset();
			this.mapInfo.option = true;
			this.mapInfo.light = true;
			this.cgLookupTexture = ScreenEffectDefine.ColorGradingLookupTexture;
			this.cgBlend = ScreenEffectDefine.ColorGradingBlend;
			this.cgSaturation = ScreenEffectDefine.ColorGradingSaturation;
			this.cgBrightness = ScreenEffectDefine.ColorGradingBrightness;
			this.cgContrast = ScreenEffectDefine.ColorGradingContrast;
			this.enableAmbientOcclusion = ScreenEffectDefine.AmbientOcclusion;
			this.aoIntensity = ScreenEffectDefine.AmbientOcclusionIntensity;
			this.aoThicknessModeifier = ScreenEffectDefine.AmbientOcclusionThicknessModeifier;
			this.aoColor = ScreenEffectDefine.AmbientOcclusionColor;
			this.enableBloom = ScreenEffectDefine.Bloom;
			this.bloomIntensity = ScreenEffectDefine.BloomIntensity;
			this.bloomThreshold = ScreenEffectDefine.BloomThreshold;
			this.bloomSoftKnee = ScreenEffectDefine.BloomSoftKnee;
			this.bloomClamp = ScreenEffectDefine.BloomClamp;
			this.bloomDiffusion = ScreenEffectDefine.BloomDiffusion;
			this.bloomColor = ScreenEffectDefine.BloomColor;
			this.enableDepth = ScreenEffectDefine.DepthOfField;
			this.depthForcus = ScreenEffectDefine.DepthOfFieldForcus;
			this.depthFocalSize = ScreenEffectDefine.DepthOfFieldFocalSize;
			this.depthAperture = ScreenEffectDefine.DepthOfFieldAperture;
			this.enableVignette = ScreenEffectDefine.Vignette;
			this.vignetteIntensity = ScreenEffectDefine.VignetteIntensity;
			this.enableSSR = ScreenEffectDefine.ScreenSpaceReflections;
			this.enableReflectionProbe = ScreenEffectDefine.ReflectionProbe;
			this.reflectionProbeCubemap = ScreenEffectDefine.ReflectionProbeCubemap;
			this.reflectionProbeIntensity = ScreenEffectDefine.ReflectionProbeIntensity;
			this.enableFog = ScreenEffectDefine.Fog;
			this.fogExcludeFarPixels = ScreenEffectDefine.FogExcludeFarPixels;
			this.fogHeight = ScreenEffectDefine.FogHeight;
			this.fogHeightDensity = ScreenEffectDefine.FogHeightDensity;
			this.fogColor = ScreenEffectDefine.FogColor;
			this.fogDensity = ScreenEffectDefine.FogDensity;
			this.enableSunShafts = ScreenEffectDefine.SunShaft;
			this.sunCaster = ScreenEffectDefine.SunShaftCaster;
			this.sunThresholdColor = ScreenEffectDefine.SunShaftThresholdColor;
			this.sunColor = ScreenEffectDefine.SunShaftShaftsColor;
			this.sunDistanceFallOff = ScreenEffectDefine.SunShaftDistanceFallOff;
			this.sunBlurSize = ScreenEffectDefine.SunShaftBlurSize;
			this.sunIntensity = ScreenEffectDefine.SunShaftIntensity;
			this.enableShadow = true;
			this.environmentLightingSkyColor = ScreenEffectDefine.EnvironmentLightingSkyColor;
			this.environmentLightingEquatorColor = ScreenEffectDefine.EnvironmentLightingEquatorColor;
			this.environmentLightingGroundColor = ScreenEffectDefine.EnvironmentLightingGroundColor;
			this.skyInfo.Enable = false;
			this.skyInfo.Pattern = 0;
			this.cameraSaveData = null;
			this.cameraData = new CameraControl.CameraData[10];
			if (Singleton<Studio>.IsInstance())
			{
				for (int i = 0; i < 10; i++)
				{
					this.cameraData[i] = Singleton<Studio>.Instance.cameraCtrl.ExportResetData();
				}
			}
			this.charaLight.Init();
			this.mapLight.Init();
			this.bgmCtrl.play = false;
			this.bgmCtrl.repeat = BGMCtrl.Repeat.All;
			this.bgmCtrl.no = 0;
			this.envCtrl.play = false;
			this.envCtrl.repeat = BGMCtrl.Repeat.All;
			this.envCtrl.no = 0;
			this.outsideSoundCtrl.play = false;
			this.outsideSoundCtrl.repeat = BGMCtrl.Repeat.All;
			this.outsideSoundCtrl.fileName = string.Empty;
			this.background = string.Empty;
			this.frame = string.Empty;
			this.hashIndex.Clear();
			this.lightCount = 0;
			this.dataVersion = this.m_Version;
		}

		// Token: 0x06009961 RID: 39265 RVA: 0x003F2274 File Offset: 0x003F0674
		public int GetNewIndex()
		{
			int num = 0;
			while (MathfEx.RangeEqualOn<int>(0, num, 2147483647))
			{
				if (!this.hashIndex.Contains(num))
				{
					this.hashIndex.Add(num);
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x06009962 RID: 39266 RVA: 0x003F22C0 File Offset: 0x003F06C0
		public int CheckNewIndex()
		{
			int num = -1;
			while (MathfEx.RangeEqualOn<int>(0, num, 2147483647))
			{
				if (!this.hashIndex.Contains(num))
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x06009963 RID: 39267 RVA: 0x003F22FD File Offset: 0x003F06FD
		public bool SetNewIndex(int _index)
		{
			return this.hashIndex.Add(_index);
		}

		// Token: 0x06009964 RID: 39268 RVA: 0x003F230B File Offset: 0x003F070B
		public bool DeleteIndex(int _index)
		{
			return this.hashIndex.Remove(_index);
		}

		// Token: 0x06009965 RID: 39269 RVA: 0x003F231C File Offset: 0x003F071C
		public bool Save(string _path)
		{
			using (FileStream fileStream = new FileStream(_path, FileMode.Create, FileAccess.Write))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
				{
					byte[] buffer = Singleton<Studio>.Instance.gameScreenShot.CreatePngScreen(320, 180, false, false);
					binaryWriter.Write(buffer);
					binaryWriter.Write(this.m_Version.ToString());
					this.Save(binaryWriter, this.dicObject);
					binaryWriter.Write(this.mapInfo.no);
					this.mapInfo.ca.Save(binaryWriter);
					binaryWriter.Write(this.mapInfo.option);
					binaryWriter.Write(this.mapInfo.light);
					binaryWriter.Write(this.cgLookupTexture);
					binaryWriter.Write(this.cgBlend);
					binaryWriter.Write(this.cgSaturation);
					binaryWriter.Write(this.cgBrightness);
					binaryWriter.Write(this.cgContrast);
					binaryWriter.Write(this.enableAmbientOcclusion);
					binaryWriter.Write(this.aoIntensity);
					binaryWriter.Write(this.aoThicknessModeifier);
					binaryWriter.Write(JsonUtility.ToJson(this.aoColor));
					binaryWriter.Write(this.enableBloom);
					binaryWriter.Write(this.bloomIntensity);
					binaryWriter.Write(this.bloomThreshold);
					binaryWriter.Write(this.bloomSoftKnee);
					binaryWriter.Write(this.bloomClamp);
					binaryWriter.Write(this.bloomDiffusion);
					binaryWriter.Write(JsonUtility.ToJson(this.bloomColor));
					binaryWriter.Write(this.enableDepth);
					binaryWriter.Write(this.depthForcus);
					binaryWriter.Write(this.depthFocalSize);
					binaryWriter.Write(this.depthAperture);
					binaryWriter.Write(this.enableVignette);
					binaryWriter.Write(this.vignetteIntensity);
					binaryWriter.Write(this.enableSSR);
					binaryWriter.Write(this.enableReflectionProbe);
					binaryWriter.Write(this.reflectionProbeCubemap);
					binaryWriter.Write(this.reflectionProbeIntensity);
					binaryWriter.Write(this.enableFog);
					binaryWriter.Write(this.fogExcludeFarPixels);
					binaryWriter.Write(this.fogHeight);
					binaryWriter.Write(this.fogHeightDensity);
					binaryWriter.Write(JsonUtility.ToJson(this.fogColor));
					binaryWriter.Write(this.fogDensity);
					binaryWriter.Write(this.enableSunShafts);
					binaryWriter.Write(this.sunCaster);
					binaryWriter.Write(JsonUtility.ToJson(this.sunThresholdColor));
					binaryWriter.Write(JsonUtility.ToJson(this.sunColor));
					binaryWriter.Write(this.sunDistanceFallOff);
					binaryWriter.Write(this.sunBlurSize);
					binaryWriter.Write(this.sunIntensity);
					binaryWriter.Write(this.enableShadow);
					binaryWriter.Write(JsonUtility.ToJson(this.environmentLightingSkyColor));
					binaryWriter.Write(JsonUtility.ToJson(this.environmentLightingEquatorColor));
					binaryWriter.Write(JsonUtility.ToJson(this.environmentLightingGroundColor));
					byte[] array = MessagePackSerializer.Serialize<SceneInfo.SkyInfo>(this.skyInfo);
					binaryWriter.Write(array.Length);
					binaryWriter.Write(array);
					this.cameraSaveData.Save(binaryWriter);
					for (int i = 0; i < 10; i++)
					{
						this.cameraData[i].Save(binaryWriter);
					}
					this.charaLight.Save(binaryWriter, this.m_Version);
					this.mapLight.Save(binaryWriter, this.m_Version);
					this.bgmCtrl.Save(binaryWriter, this.m_Version);
					this.envCtrl.Save(binaryWriter, this.m_Version);
					this.outsideSoundCtrl.Save(binaryWriter, this.m_Version);
					binaryWriter.Write(this.background);
					binaryWriter.Write(this.frame);
					binaryWriter.Write("【StudioNEOV2】");
				}
			}
			return true;
		}

		// Token: 0x06009966 RID: 39270 RVA: 0x003F2738 File Offset: 0x003F0B38
		public void Save(BinaryWriter _writer, Dictionary<int, ObjectInfo> _dicObject)
		{
			int count = _dicObject.Count;
			_writer.Write(count);
			foreach (KeyValuePair<int, ObjectInfo> keyValuePair in _dicObject)
			{
				_writer.Write(keyValuePair.Key);
				keyValuePair.Value.Save(_writer, this.m_Version);
			}
		}

		// Token: 0x06009967 RID: 39271 RVA: 0x003F27B8 File Offset: 0x003F0BB8
		public bool Load(string _path)
		{
			Version version;
			return this.Load(_path, out version);
		}

		// Token: 0x06009968 RID: 39272 RVA: 0x003F27D0 File Offset: 0x003F0BD0
		public bool Load(string _path, out Version _dataVersion)
		{
			using (FileStream fileStream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				using (BinaryReader binaryReader = new BinaryReader(fileStream))
				{
					PngFile.SkipPng(binaryReader);
					this.dataVersion = new Version(binaryReader.ReadString());
					int num = binaryReader.ReadInt32();
					for (int i = 0; i < num; i++)
					{
						int num2 = binaryReader.ReadInt32();
						int num3 = binaryReader.ReadInt32();
						ObjectInfo objectInfo = null;
						switch (num3)
						{
						case 0:
							objectInfo = new OICharInfo(null, -1);
							break;
						case 1:
							objectInfo = new OIItemInfo(-1, -1, -1, -1);
							break;
						case 2:
							objectInfo = new OILightInfo(-1, -1);
							break;
						case 3:
							objectInfo = new OIFolderInfo(-1);
							break;
						case 4:
							objectInfo = new OIRouteInfo(-1);
							break;
						case 5:
							objectInfo = new OICameraInfo(-1);
							break;
						}
						objectInfo.Load(binaryReader, this.dataVersion, false, true);
						this.dicObject.Add(num2, objectInfo);
						this.hashIndex.Add(num2);
					}
					this.mapInfo.no = binaryReader.ReadInt32();
					this.mapInfo.ca.Load(binaryReader);
					this.mapInfo.option = binaryReader.ReadBoolean();
					if (this.dataVersion.CompareTo(new Version(1, 1, 0)) >= 0)
					{
						this.mapInfo.light = binaryReader.ReadBoolean();
					}
					this.cgLookupTexture = binaryReader.ReadInt32();
					this.cgBlend = binaryReader.ReadSingle();
					this.cgSaturation = binaryReader.ReadInt32();
					this.cgBrightness = binaryReader.ReadInt32();
					this.cgContrast = binaryReader.ReadInt32();
					this.enableAmbientOcclusion = binaryReader.ReadBoolean();
					this.aoIntensity = binaryReader.ReadSingle();
					this.aoThicknessModeifier = binaryReader.ReadSingle();
					this.aoColor = JsonUtility.FromJson<Color>(binaryReader.ReadString());
					this.enableBloom = binaryReader.ReadBoolean();
					this.bloomIntensity = binaryReader.ReadSingle();
					this.bloomThreshold = binaryReader.ReadSingle();
					this.bloomSoftKnee = binaryReader.ReadSingle();
					this.bloomClamp = binaryReader.ReadBoolean();
					this.bloomDiffusion = binaryReader.ReadSingle();
					this.bloomColor = JsonUtility.FromJson<Color>(binaryReader.ReadString());
					this.enableDepth = binaryReader.ReadBoolean();
					this.depthForcus = binaryReader.ReadInt32();
					this.depthFocalSize = binaryReader.ReadSingle();
					this.depthAperture = binaryReader.ReadSingle();
					this.enableVignette = binaryReader.ReadBoolean();
					if (this.dataVersion.CompareTo(new Version(1, 1, 1)) >= 0)
					{
						this.vignetteIntensity = binaryReader.ReadSingle();
					}
					this.enableSSR = binaryReader.ReadBoolean();
					this.enableReflectionProbe = binaryReader.ReadBoolean();
					this.reflectionProbeCubemap = binaryReader.ReadInt32();
					this.reflectionProbeIntensity = binaryReader.ReadSingle();
					this.enableFog = binaryReader.ReadBoolean();
					this.fogExcludeFarPixels = binaryReader.ReadBoolean();
					this.fogHeight = binaryReader.ReadSingle();
					this.fogHeightDensity = binaryReader.ReadSingle();
					this.fogColor = JsonUtility.FromJson<Color>(binaryReader.ReadString());
					this.fogDensity = binaryReader.ReadSingle();
					this.enableSunShafts = binaryReader.ReadBoolean();
					this.sunCaster = binaryReader.ReadInt32();
					this.sunThresholdColor = JsonUtility.FromJson<Color>(binaryReader.ReadString());
					this.sunColor = JsonUtility.FromJson<Color>(binaryReader.ReadString());
					this.sunDistanceFallOff = binaryReader.ReadSingle();
					this.sunBlurSize = binaryReader.ReadSingle();
					this.sunIntensity = binaryReader.ReadSingle();
					this.enableShadow = binaryReader.ReadBoolean();
					this.environmentLightingSkyColor = JsonUtility.FromJson<Color>(binaryReader.ReadString());
					this.environmentLightingEquatorColor = JsonUtility.FromJson<Color>(binaryReader.ReadString());
					this.environmentLightingGroundColor = JsonUtility.FromJson<Color>(binaryReader.ReadString());
					if (this.dataVersion.CompareTo(new Version(1, 1, 0)) >= 0)
					{
						this.skyInfo = MessagePackSerializer.Deserialize<SceneInfo.SkyInfo>(binaryReader.ReadBytes(binaryReader.ReadInt32()));
					}
					if (this.cameraSaveData == null)
					{
						this.cameraSaveData = new CameraControl.CameraData();
					}
					this.cameraSaveData.Load(binaryReader);
					for (int j = 0; j < 10; j++)
					{
						CameraControl.CameraData cameraData = new CameraControl.CameraData();
						cameraData.Load(binaryReader);
						this.cameraData[j] = cameraData;
					}
					this.charaLight.Load(binaryReader, this.dataVersion);
					this.mapLight.Load(binaryReader, this.dataVersion);
					this.bgmCtrl.Load(binaryReader, this.dataVersion);
					this.envCtrl.Load(binaryReader, this.dataVersion);
					this.outsideSoundCtrl.Load(binaryReader, this.dataVersion);
					this.background = binaryReader.ReadString();
					this.frame = binaryReader.ReadString();
					_dataVersion = this.dataVersion;
				}
			}
			return true;
		}

		// Token: 0x06009969 RID: 39273 RVA: 0x003F2CD4 File Offset: 0x003F10D4
		public bool Import(string _path)
		{
			using (FileStream fileStream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				using (BinaryReader binaryReader = new BinaryReader(fileStream))
				{
					PngFile.SkipPng(binaryReader);
					Version version = new Version(binaryReader.ReadString());
					this.Import(binaryReader, version);
				}
			}
			return true;
		}

		// Token: 0x0600996A RID: 39274 RVA: 0x003F2D50 File Offset: 0x003F1150
		public void Import(BinaryReader _reader, Version _version)
		{
			this.dicImport = new Dictionary<int, ObjectInfo>();
			this.dicChangeKey = new Dictionary<int, int>();
			int num = _reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				int value = _reader.ReadInt32();
				int num2 = _reader.ReadInt32();
				ObjectInfo objectInfo = null;
				switch (num2)
				{
				case 0:
					objectInfo = new OICharInfo(null, Studio.GetNewIndex());
					break;
				case 1:
					objectInfo = new OIItemInfo(-1, -1, -1, Studio.GetNewIndex());
					break;
				case 2:
					objectInfo = new OILightInfo(-1, Studio.GetNewIndex());
					break;
				case 3:
					objectInfo = new OIFolderInfo(Studio.GetNewIndex());
					break;
				case 4:
					objectInfo = new OIRouteInfo(Studio.GetNewIndex());
					break;
				case 5:
					objectInfo = new OICameraInfo(Studio.GetNewIndex());
					break;
				}
				objectInfo.Load(_reader, _version, true, true);
				this.dicObject.Add(objectInfo.dicKey, objectInfo);
				this.dicImport.Add(objectInfo.dicKey, objectInfo);
				this.dicChangeKey.Add(objectInfo.dicKey, value);
			}
		}

		// Token: 0x04007A60 RID: 31328
		private readonly Version m_Version = new Version(1, 1, 1);

		// Token: 0x04007A61 RID: 31329
		public Dictionary<int, ObjectInfo> dicObject;

		// Token: 0x04007A62 RID: 31330
		public SceneInfo.MapInfo mapInfo = new SceneInfo.MapInfo();

		// Token: 0x04007A63 RID: 31331
		public int cgLookupTexture;

		// Token: 0x04007A64 RID: 31332
		public float cgBlend;

		// Token: 0x04007A65 RID: 31333
		public int cgSaturation;

		// Token: 0x04007A66 RID: 31334
		public int cgBrightness;

		// Token: 0x04007A67 RID: 31335
		public int cgContrast;

		// Token: 0x04007A68 RID: 31336
		public bool enableAmbientOcclusion;

		// Token: 0x04007A69 RID: 31337
		public float aoIntensity;

		// Token: 0x04007A6A RID: 31338
		public float aoThicknessModeifier;

		// Token: 0x04007A6B RID: 31339
		public Color aoColor;

		// Token: 0x04007A6C RID: 31340
		public bool enableBloom;

		// Token: 0x04007A6D RID: 31341
		public float bloomIntensity;

		// Token: 0x04007A6E RID: 31342
		public float bloomThreshold;

		// Token: 0x04007A6F RID: 31343
		public float bloomSoftKnee;

		// Token: 0x04007A70 RID: 31344
		public bool bloomClamp;

		// Token: 0x04007A71 RID: 31345
		public float bloomDiffusion;

		// Token: 0x04007A72 RID: 31346
		public Color bloomColor;

		// Token: 0x04007A73 RID: 31347
		public bool enableDepth;

		// Token: 0x04007A74 RID: 31348
		public int depthForcus;

		// Token: 0x04007A75 RID: 31349
		public float depthFocalSize;

		// Token: 0x04007A76 RID: 31350
		public float depthAperture;

		// Token: 0x04007A77 RID: 31351
		public bool enableVignette;

		// Token: 0x04007A78 RID: 31352
		public float vignetteIntensity;

		// Token: 0x04007A79 RID: 31353
		public bool enableSSR;

		// Token: 0x04007A7A RID: 31354
		public bool enableReflectionProbe;

		// Token: 0x04007A7B RID: 31355
		public int reflectionProbeCubemap;

		// Token: 0x04007A7C RID: 31356
		public float reflectionProbeIntensity;

		// Token: 0x04007A7D RID: 31357
		public bool enableFog;

		// Token: 0x04007A7E RID: 31358
		public bool fogExcludeFarPixels;

		// Token: 0x04007A7F RID: 31359
		public float fogHeight;

		// Token: 0x04007A80 RID: 31360
		public float fogHeightDensity;

		// Token: 0x04007A81 RID: 31361
		public Color fogColor;

		// Token: 0x04007A82 RID: 31362
		public float fogDensity;

		// Token: 0x04007A83 RID: 31363
		public bool enableSunShafts;

		// Token: 0x04007A84 RID: 31364
		public int sunCaster;

		// Token: 0x04007A85 RID: 31365
		public Color sunThresholdColor;

		// Token: 0x04007A86 RID: 31366
		public Color sunColor;

		// Token: 0x04007A87 RID: 31367
		public float sunDistanceFallOff;

		// Token: 0x04007A88 RID: 31368
		public float sunBlurSize;

		// Token: 0x04007A89 RID: 31369
		public float sunIntensity;

		// Token: 0x04007A8A RID: 31370
		public bool enableShadow;

		// Token: 0x04007A8B RID: 31371
		public Color environmentLightingSkyColor;

		// Token: 0x04007A8C RID: 31372
		public Color environmentLightingEquatorColor;

		// Token: 0x04007A8D RID: 31373
		public Color environmentLightingGroundColor;

		// Token: 0x04007A8E RID: 31374
		public SceneInfo.SkyInfo skyInfo = new SceneInfo.SkyInfo();

		// Token: 0x04007A8F RID: 31375
		public CameraControl.CameraData cameraSaveData;

		// Token: 0x04007A90 RID: 31376
		public CameraControl.CameraData[] cameraData;

		// Token: 0x04007A91 RID: 31377
		public CameraLightCtrl.LightInfo charaLight = new CameraLightCtrl.LightInfo();

		// Token: 0x04007A92 RID: 31378
		public CameraLightCtrl.MapLightInfo mapLight = new CameraLightCtrl.MapLightInfo();

		// Token: 0x04007A93 RID: 31379
		public BGMCtrl bgmCtrl = new BGMCtrl();

		// Token: 0x04007A94 RID: 31380
		public ENVCtrl envCtrl = new ENVCtrl();

		// Token: 0x04007A95 RID: 31381
		public OutsideSoundCtrl outsideSoundCtrl = new OutsideSoundCtrl();

		// Token: 0x04007A96 RID: 31382
		public string background = string.Empty;

		// Token: 0x04007A97 RID: 31383
		public string frame = string.Empty;

		// Token: 0x04007A98 RID: 31384
		private HashSet<int> hashIndex;

		// Token: 0x04007A9B RID: 31387
		private int lightCount;

		// Token: 0x02001235 RID: 4661
		public class MapInfo
		{
			// Token: 0x04007A9D RID: 31389
			public int no = -1;

			// Token: 0x04007A9E RID: 31390
			public ChangeAmount ca = new ChangeAmount();

			// Token: 0x04007A9F RID: 31391
			public bool option = true;

			// Token: 0x04007AA0 RID: 31392
			public bool light = true;
		}

		// Token: 0x02001236 RID: 4662
		[MessagePackObject(true)]
		public class SkyInfo
		{
			// Token: 0x0600996C RID: 39276 RVA: 0x003F2E9E File Offset: 0x003F129E
			public SkyInfo()
			{
				this.Enable = false;
				this.Pattern = 0;
			}

			// Token: 0x170020B0 RID: 8368
			// (get) Token: 0x0600996D RID: 39277 RVA: 0x003F2EB4 File Offset: 0x003F12B4
			// (set) Token: 0x0600996E RID: 39278 RVA: 0x003F2EBC File Offset: 0x003F12BC
			public bool Enable { get; set; }

			// Token: 0x170020B1 RID: 8369
			// (get) Token: 0x0600996F RID: 39279 RVA: 0x003F2EC5 File Offset: 0x003F12C5
			// (set) Token: 0x06009970 RID: 39280 RVA: 0x003F2ECD File Offset: 0x003F12CD
			public int Pattern { get; set; }
		}
	}
}
