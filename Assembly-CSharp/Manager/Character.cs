using System;
using System.Collections.Generic;
using System.Linq;
using AIChara;
using UnityEngine;

namespace Manager
{
	// Token: 0x02000812 RID: 2066
	public class Character : Singleton<Character>
	{
		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x06003467 RID: 13415 RVA: 0x00134B51 File Offset: 0x00132F51
		// (set) Token: 0x06003468 RID: 13416 RVA: 0x00134B59 File Offset: 0x00132F59
		public ChaListControl chaListCtrl { get; private set; }

		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x06003469 RID: 13417 RVA: 0x00134B62 File Offset: 0x00132F62
		// (set) Token: 0x0600346A RID: 13418 RVA: 0x00134B6A File Offset: 0x00132F6A
		public SortedDictionary<int, ChaControl> dictEntryChara { get; private set; }

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x0600346B RID: 13419 RVA: 0x00134B73 File Offset: 0x00132F73
		// (set) Token: 0x0600346C RID: 13420 RVA: 0x00134B7B File Offset: 0x00132F7B
		public bool enableCharaLoadGCClear { get; set; } = true;

		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x0600346D RID: 13421 RVA: 0x00134B84 File Offset: 0x00132F84
		// (set) Token: 0x0600346E RID: 13422 RVA: 0x00134B8C File Offset: 0x00132F8C
		public bool customLoadGCClear { get; set; }

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x0600346F RID: 13423 RVA: 0x00134B95 File Offset: 0x00132F95
		// (set) Token: 0x06003470 RID: 13424 RVA: 0x00134B9D File Offset: 0x00132F9D
		public bool isMod { get; set; }

		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x06003471 RID: 13425 RVA: 0x00134BA6 File Offset: 0x00132FA6
		// (set) Token: 0x06003472 RID: 13426 RVA: 0x00134BAE File Offset: 0x00132FAE
		private Material matHairDithering { get; set; }

		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x06003473 RID: 13427 RVA: 0x00134BB7 File Offset: 0x00132FB7
		// (set) Token: 0x06003474 RID: 13428 RVA: 0x00134BBF File Offset: 0x00132FBF
		public Shader shaderDithering { get; private set; }

		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06003475 RID: 13429 RVA: 0x00134BC8 File Offset: 0x00132FC8
		// (set) Token: 0x06003476 RID: 13430 RVA: 0x00134BD0 File Offset: 0x00132FD0
		private Material matHairCutout { get; set; }

		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06003477 RID: 13431 RVA: 0x00134BD9 File Offset: 0x00132FD9
		// (set) Token: 0x06003478 RID: 13432 RVA: 0x00134BE1 File Offset: 0x00132FE1
		public Shader shaderCutout { get; private set; }

		// Token: 0x06003479 RID: 13433 RVA: 0x00134BEA File Offset: 0x00132FEA
		public void BeginLoadAssetBundle()
		{
			this.lstLoadAssetBundleInfo.Clear();
			this.loadAssetBundle = true;
		}

		// Token: 0x0600347A RID: 13434 RVA: 0x00134C00 File Offset: 0x00133000
		public void AddLoadAssetBundle(string assetBundleName, string manifestName)
		{
			if (manifestName.IsNullOrEmpty())
			{
				manifestName = "abdata";
			}
			AssetBundleManifestData assetBundleManifestData = new AssetBundleManifestData();
			assetBundleManifestData.bundle = assetBundleName;
			assetBundleManifestData.manifest = manifestName;
			this.lstLoadAssetBundleInfo.Add(assetBundleManifestData);
		}

		// Token: 0x0600347B RID: 13435 RVA: 0x00134C40 File Offset: 0x00133040
		public void EndLoadAssetBundle(bool forceRemove = false)
		{
			foreach (AssetBundleData assetBundleData in this.lstLoadAssetBundleInfo)
			{
				AssetBundleManager.UnloadAssetBundle(assetBundleData.bundle, true, null, forceRemove);
			}
			Resources.UnloadUnusedAssets();
			GC.Collect();
			this.lstLoadAssetBundleInfo.Clear();
			this.loadAssetBundle = false;
		}

		// Token: 0x0600347C RID: 13436 RVA: 0x00134CC0 File Offset: 0x001330C0
		public ChaControl CreateChara(byte _sex, GameObject parent, int id, ChaFileControl _chaFile = null)
		{
			int num = 0;
			int num2 = 1;
			foreach (KeyValuePair<int, ChaControl> keyValuePair in this.dictEntryChara)
			{
				if (keyValuePair.Value.sex == _sex)
				{
					num2++;
				}
				if (num != keyValuePair.Key)
				{
					break;
				}
				num++;
			}
			string name = ((_sex != 0) ? "chaF_" : "chaM_") + num2.ToString("000");
			GameObject gameObject = new GameObject(name);
			if (parent)
			{
				gameObject.transform.SetParent(parent.transform, false);
			}
			ChaControl chaControl = gameObject.AddComponent<ChaControl>();
			if (chaControl)
			{
				chaControl.Initialize(_sex, gameObject, id, num, _chaFile);
			}
			this.dictEntryChara.Add(num, chaControl);
			return chaControl;
		}

		// Token: 0x0600347D RID: 13437 RVA: 0x00134DC8 File Offset: 0x001331C8
		public bool IsChara(ChaControl cha)
		{
			foreach (KeyValuePair<int, ChaControl> keyValuePair in this.dictEntryChara)
			{
				if (keyValuePair.Value == cha)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600347E RID: 13438 RVA: 0x00134E3C File Offset: 0x0013323C
		public bool DeleteChara(ChaControl cha, bool entryOnly = false)
		{
			foreach (KeyValuePair<int, ChaControl> keyValuePair in this.dictEntryChara)
			{
				if (keyValuePair.Value == cha)
				{
					if (!entryOnly)
					{
						keyValuePair.Value.gameObject.name = "Delete_Reserve";
						keyValuePair.Value.transform.SetParent(null);
						UnityEngine.Object.Destroy(keyValuePair.Value.gameObject);
					}
					this.dictEntryChara.Remove(keyValuePair.Key);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600347F RID: 13439 RVA: 0x00134F00 File Offset: 0x00133300
		public void DeleteCharaAll()
		{
			foreach (KeyValuePair<int, ChaControl> keyValuePair in this.dictEntryChara)
			{
				if (keyValuePair.Value)
				{
					keyValuePair.Value.gameObject.name = "Delete_Reserve";
					keyValuePair.Value.transform.SetParent(null);
					UnityEngine.Object.Destroy(keyValuePair.Value.gameObject);
				}
			}
			this.dictEntryChara.Clear();
		}

		// Token: 0x06003480 RID: 13440 RVA: 0x00134FAC File Offset: 0x001333AC
		public List<ChaControl> GetCharaList(byte _sex)
		{
			return (from c in this.dictEntryChara
			where c.Value.sex == _sex
			select c into x
			select x.Value).ToList<ChaControl>();
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x00135004 File Offset: 0x00133404
		public ChaControl GetChara(byte _sex, int _id)
		{
			try
			{
				return (from s in this.dictEntryChara
				where s.Value.sex == _sex
				select s).First((KeyValuePair<int, ChaControl> v) => v.Value.chaID == _id).Value;
			}
			catch (ArgumentNullException ex)
			{
			}
			catch (InvalidOperationException ex2)
			{
			}
			return null;
		}

		// Token: 0x06003482 RID: 13442 RVA: 0x00135088 File Offset: 0x00133488
		public ChaControl GetChara(int _id)
		{
			try
			{
				return this.dictEntryChara.First((KeyValuePair<int, ChaControl> v) => v.Value.chaID == _id).Value;
			}
			catch (ArgumentNullException ex)
			{
			}
			catch (InvalidOperationException ex2)
			{
			}
			return null;
		}

		// Token: 0x06003483 RID: 13443 RVA: 0x001350F4 File Offset: 0x001334F4
		public ChaControl GetCharaFromLoadNo(int _no)
		{
			try
			{
				return this.dictEntryChara.First((KeyValuePair<int, ChaControl> v) => v.Value.loadNo == _no).Value;
			}
			catch (ArgumentNullException ex)
			{
			}
			catch (InvalidOperationException ex2)
			{
			}
			return null;
		}

		// Token: 0x06003484 RID: 13444 RVA: 0x00135160 File Offset: 0x00133560
		public static void ChangeRootParent(ChaControl cha, Transform trfNewParent)
		{
			if (null != cha)
			{
				cha.transform.SetParent(trfNewParent, false);
			}
		}

		// Token: 0x06003485 RID: 13445 RVA: 0x0013517C File Offset: 0x0013357C
		public string GetCharaTypeName(int no)
		{
			if (!Singleton<Voice>.IsInstance())
			{
				return "不明";
			}
			VoiceInfo.Param param;
			if (!Singleton<Voice>.Instance.voiceInfoDic.TryGetValue(no, out param))
			{
				return "不明";
			}
			return param.Personality;
		}

		// Token: 0x06003486 RID: 13446 RVA: 0x001351BC File Offset: 0x001335BC
		protected new void Awake()
		{
			if (!base.CheckInstance())
			{
				return;
			}
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			this.chaListCtrl = new ChaListControl();
			this.dictEntryChara = new SortedDictionary<int, ChaControl>();
			this.matHairDithering = CommonLib.LoadAsset<Material>("chara/hair_shader_mat.unity3d", "hair_dithering", false, "abdata");
			this.matHairCutout = CommonLib.LoadAsset<Material>("chara/hair_shader_mat.unity3d", "hair_cutout", false, "abdata");
			this.shaderDithering = this.matHairDithering.shader;
			this.shaderCutout = this.matHairCutout.shader;
			this.chaListCtrl.LoadListInfoAll();
		}

		// Token: 0x06003487 RID: 13447 RVA: 0x0013525C File Offset: 0x0013365C
		protected void Update()
		{
			if (!base.CheckInstance())
			{
				return;
			}
			foreach (KeyValuePair<int, ChaControl> keyValuePair in this.dictEntryChara)
			{
				keyValuePair.Value.UpdateForce();
			}
		}

		// Token: 0x06003488 RID: 13448 RVA: 0x001352CC File Offset: 0x001336CC
		protected void LateUpdate()
		{
			foreach (ChaControl chaControl in this.dictEntryChara.Values)
			{
				chaControl.LateUpdateForce();
			}
		}

		// Token: 0x06003489 RID: 13449 RVA: 0x0013532C File Offset: 0x0013372C
		protected override void OnDestroy()
		{
			this.chaListCtrl.SaveItemID();
		}

		// Token: 0x04003530 RID: 13616
		public List<AssetBundleData> lstLoadAssetBundleInfo = new List<AssetBundleData>();

		// Token: 0x04003531 RID: 13617
		public bool loadAssetBundle;
	}
}
