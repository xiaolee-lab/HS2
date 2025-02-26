using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Illusion.Component.UI;
using Illusion.Extensions;
using IllusionUtility.GetUtility;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000A25 RID: 2597
public class UI_ColorPresets : MonoBehaviour
{
	// Token: 0x140000B8 RID: 184
	// (add) Token: 0x06004D2E RID: 19758 RVA: 0x001D9F2C File Offset: 0x001D832C
	// (remove) Token: 0x06004D2F RID: 19759 RVA: 0x001D9F64 File Offset: 0x001D8364
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	public event Action<Color> updateColorAction;

	// Token: 0x17000E7D RID: 3709
	// (get) Token: 0x06004D30 RID: 19760 RVA: 0x001D9F9A File Offset: 0x001D839A
	// (set) Token: 0x06004D31 RID: 19761 RVA: 0x001D9FA2 File Offset: 0x001D83A2
	public Color color
	{
		get
		{
			return this._color;
		}
		set
		{
			this._color = value;
			this.SetColor(this._color);
		}
	}

	// Token: 0x06004D32 RID: 19762 RVA: 0x001D9FB8 File Offset: 0x001D83B8
	private void Awake()
	{
		if (null == this.objNew)
		{
			this.objNew = base.transform.FindLoop("New");
		}
		if (null != this.objNew)
		{
			Transform transform = this.objNew.transform.Find("imgColor");
			if (null != transform)
			{
				this.imgNew = transform.GetComponent<Image>();
			}
			this.trfParent = this.objNew.transform.parent;
		}
		if (null == this.objTemplate)
		{
			this.objTemplate = base.transform.FindLoop("TemplateColor");
		}
		this.lstInfo.Clear();
	}

	// Token: 0x06004D33 RID: 19763 RVA: 0x001DA074 File Offset: 0x001D8474
	private void Start()
	{
		this.saveDir = UserData.Path + "Custom/";
		this.LoadPresets();
		for (int i = 0; i < this.tglFile.Length; i++)
		{
			this.tglFile[i].isOn = false;
		}
		if (!this.tglFile.SafeProc(this.colorInfo.select, delegate(UI_ToggleEx _t)
		{
			_t.isOn = true;
		}))
		{
			this.colorInfo.select = Mathf.Clamp(this.colorInfo.select, 0, this.tglFile.Length - 1);
			this.tglFile[this.colorInfo.select].isOn = true;
		}
		this.SetPreset(false);
		if (null != this.objNew)
		{
			this.trfParent = this.objNew.transform.parent;
			Button component = this.objNew.GetComponent<Button>();
			if (null != component)
			{
				component.OnClickAsObservable().Subscribe(delegate(Unit _)
				{
					this.AddNewPreset(this.color, false);
					this.SavePresets();
				});
			}
			this.objNew.SetActiveIfDifferent(3 != this.colorInfo.select);
			if (77 <= this.lstInfo.Count)
			{
				this.objNew.SetActiveIfDifferent(false);
			}
		}
		for (int j = 0; j < this.tglFile.Length; j++)
		{
			int no = j;
			this.tglFile[j].onValueChanged.AddListener(delegate(bool isOn)
			{
				if (isOn)
				{
					this.colorInfo.select = no;
					this.SetPreset(false);
					this.SavePresets();
					this.objNew.SetActiveIfDifferent(3 != no);
					if (77 <= this.lstInfo.Count)
					{
						this.objNew.SetActiveIfDifferent(false);
					}
				}
			});
		}
	}

	// Token: 0x06004D34 RID: 19764 RVA: 0x001DA220 File Offset: 0x001D8620
	public int GetSelectIndex()
	{
		for (int i = 0; i < this.tglFile.Length; i++)
		{
			if (this.tglFile[i].isOn)
			{
				return i;
			}
		}
		return 0;
	}

	// Token: 0x06004D35 RID: 19765 RVA: 0x001DA25B File Offset: 0x001D865B
	public void SetColor(Color c)
	{
		if (null != this.objNew && null != this.imgNew)
		{
			this.imgNew.color = c;
		}
	}

	// Token: 0x06004D36 RID: 19766 RVA: 0x001DA28C File Offset: 0x001D868C
	public void AddNewPreset(Color addColor, bool load = false)
	{
		GameObject addObj = UnityEngine.Object.Instantiate<GameObject>(this.objTemplate, this.trfParent);
		if (null != addObj)
		{
			int idx = this.GetSelectIndex();
			addObj.name = string.Format("PresetColor", Array.Empty<object>());
			addObj.transform.SetSiblingIndex(this.lstInfo.Count);
			this.objNew.transform.SetSiblingIndex(77);
			UI_ColorPresetsInfo cpi = addObj.GetComponent<UI_ColorPresetsInfo>();
			cpi.color = addColor;
			if (null != cpi.image)
			{
				cpi.image.color = addColor;
			}
			this.lstInfo.Add(cpi);
			MouseButtonCheck mouseButtonCheck = addObj.AddComponent<MouseButtonCheck>();
			mouseButtonCheck.isLeft = false;
			mouseButtonCheck.isCenter = false;
			mouseButtonCheck.onPointerUp.AddListener(delegate(PointerEventData data)
			{
				if (this.colorInfo.select != 3)
				{
					this.lstInfo.Remove(cpi);
					this.colorInfo.SetList(idx, (from info in this.lstInfo
					select info.color).ToList<Color>());
					this.SavePresets();
					UnityEngine.Object.Destroy(addObj);
					this.objNew.SetActiveIfDifferent(true);
				}
			});
			UI_OnMouseOverMessageEx component = addObj.GetComponent<UI_OnMouseOverMessageEx>();
			if (null != component)
			{
				component.showMsgNo = ((this.colorInfo.select != 3) ? 0 : 1);
			}
			MouseButtonCheck mouseButtonCheck2 = addObj.AddComponent<MouseButtonCheck>();
			mouseButtonCheck2.isRight = false;
			mouseButtonCheck2.isCenter = false;
			mouseButtonCheck2.onPointerUp.AddListener(delegate(PointerEventData data)
			{
				this.SetColor(cpi.color);
				this.color = cpi.color;
				if (this.updateColorAction != null)
				{
					this.updateColorAction(cpi.color);
				}
			});
			if (!load)
			{
				this.colorInfo.SetList(idx, (from info in this.lstInfo
				select info.color).ToList<Color>());
			}
			addObj.SetActiveIfDifferent(true);
			if (77 <= this.lstInfo.Count)
			{
				this.objNew.SetActiveIfDifferent(false);
			}
		}
	}

	// Token: 0x06004D37 RID: 19767 RVA: 0x001DA490 File Offset: 0x001D8890
	public void SetPreset(bool delete = false)
	{
		int count = this.lstInfo.Count;
		for (int i = count - 1; i >= 0; i--)
		{
			UnityEngine.Object.Destroy(this.lstInfo[i].gameObject);
		}
		this.lstInfo.Clear();
		int select = this.colorInfo.select;
		if (delete)
		{
			this.colorInfo.DeleteList(select);
		}
		List<Color> list = this.colorInfo.GetList(select);
		count = list.Count;
		for (int j = 0; j < count; j++)
		{
			this.AddNewPreset(list[j], true);
		}
	}

	// Token: 0x06004D38 RID: 19768 RVA: 0x001DA538 File Offset: 0x001D8938
	public void SavePresets()
	{
		string path = this.saveDir + "ColorPresets.json";
		if (!Directory.Exists(this.saveDir))
		{
			Directory.CreateDirectory(this.saveDir);
		}
		string contents = JsonUtility.ToJson(this.colorInfo);
		File.WriteAllText(path, contents);
	}

	// Token: 0x06004D39 RID: 19769 RVA: 0x001DA588 File Offset: 0x001D8988
	public void LoadPresets()
	{
		string path = this.saveDir + "ColorPresets.json";
		if (!File.Exists(path))
		{
			TextAsset textAsset = CommonLib.LoadAsset<TextAsset>("custom/colorsample.unity3d", "ColorPresets", false, string.Empty);
			if (null != textAsset)
			{
				this.colorInfo = JsonUtility.FromJson<UI_ColorPresets.ColorInfo>(textAsset.text);
				AssetBundleManager.UnloadAssetBundle("custom/colorsample.unity3d", true, null, false);
				this.SavePresets();
			}
			return;
		}
		string json = File.ReadAllText(path);
		this.colorInfo = JsonUtility.FromJson<UI_ColorPresets.ColorInfo>(json);
		if (this.colorInfo.lstColorSample.Count == 0)
		{
			TextAsset textAsset2 = CommonLib.LoadAsset<TextAsset>("custom/colorsample.unity3d", "ColorPresets", false, string.Empty);
			if (null != textAsset2)
			{
				UI_ColorPresets.ColorInfo colorInfo = JsonUtility.FromJson<UI_ColorPresets.ColorInfo>(textAsset2.text);
				AssetBundleManager.UnloadAssetBundle("custom/colorsample.unity3d", true, null, false);
				this.colorInfo.lstColorSample = new List<Color>(colorInfo.lstColorSample);
				this.SavePresets();
			}
		}
	}

	// Token: 0x04004695 RID: 18069
	private const string colorPresetsFile = "ColorPresets.json";

	// Token: 0x04004696 RID: 18070
	private const string sampleAssetBundle = "custom/colorsample.unity3d";

	// Token: 0x04004697 RID: 18071
	private const string sampleAsset = "ColorPresets";

	// Token: 0x04004698 RID: 18072
	private UI_ColorPresets.ColorInfo colorInfo = new UI_ColorPresets.ColorInfo();

	// Token: 0x04004699 RID: 18073
	private string saveDir = string.Empty;

	// Token: 0x0400469A RID: 18074
	private const int presetMax = 77;

	// Token: 0x0400469C RID: 18076
	[SerializeField]
	private UI_ToggleEx[] tglFile;

	// Token: 0x0400469D RID: 18077
	[SerializeField]
	private GameObject objTemplate;

	// Token: 0x0400469E RID: 18078
	[SerializeField]
	private GameObject objNew;

	// Token: 0x0400469F RID: 18079
	private Image imgNew;

	// Token: 0x040046A0 RID: 18080
	private Transform trfParent;

	// Token: 0x040046A1 RID: 18081
	private List<UI_ColorPresetsInfo> lstInfo = new List<UI_ColorPresetsInfo>();

	// Token: 0x040046A2 RID: 18082
	private Color _color = Color.white;

	// Token: 0x02000A26 RID: 2598
	public class ColorInfo
	{
		// Token: 0x06004D3E RID: 19774 RVA: 0x001DA6DC File Offset: 0x001D8ADC
		public void SetList(int idx, List<Color> lst)
		{
			switch (idx)
			{
			case 0:
				this.lstColor01 = lst;
				break;
			case 1:
				this.lstColor02 = lst;
				break;
			case 2:
				this.lstColor03 = lst;
				break;
			case 3:
				this.lstColorSample = lst;
				break;
			}
		}

		// Token: 0x06004D3F RID: 19775 RVA: 0x001DA739 File Offset: 0x001D8B39
		public List<Color> GetList(int idx)
		{
			switch (idx)
			{
			case 0:
				return this.lstColor01;
			case 1:
				return this.lstColor02;
			case 2:
				return this.lstColor03;
			case 3:
				return this.lstColorSample;
			default:
				return null;
			}
		}

		// Token: 0x06004D40 RID: 19776 RVA: 0x001DA774 File Offset: 0x001D8B74
		public void DeleteList(int idx)
		{
			switch (idx)
			{
			case 0:
				this.lstColor01.Clear();
				break;
			case 1:
				this.lstColor02.Clear();
				break;
			case 2:
				this.lstColor03.Clear();
				break;
			case 3:
				this.lstColorSample.Clear();
				break;
			}
		}

		// Token: 0x040046A5 RID: 18085
		public int select = 3;

		// Token: 0x040046A6 RID: 18086
		public List<Color> lstColor01 = new List<Color>();

		// Token: 0x040046A7 RID: 18087
		public List<Color> lstColor02 = new List<Color>();

		// Token: 0x040046A8 RID: 18088
		public List<Color> lstColor03 = new List<Color>();

		// Token: 0x040046A9 RID: 18089
		public List<Color> lstColorSample = new List<Color>();
	}
}
