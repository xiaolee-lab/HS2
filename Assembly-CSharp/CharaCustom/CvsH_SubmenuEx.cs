using System;
using System.Linq;
using AIChara;
using Illusion.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x020009F7 RID: 2551
	public class CvsH_SubmenuEx : MonoBehaviour
	{
		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x06004B58 RID: 19288 RVA: 0x001CC767 File Offset: 0x001CAB67
		protected CustomBase customBase
		{
			get
			{
				return Singleton<CustomBase>.Instance;
			}
		}

		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x06004B59 RID: 19289 RVA: 0x001CC76E File Offset: 0x001CAB6E
		protected ChaControl chaCtrl
		{
			get
			{
				return this.customBase.chaCtrl;
			}
		}

		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x06004B5A RID: 19290 RVA: 0x001CC77B File Offset: 0x001CAB7B
		protected ChaFileHair hair
		{
			get
			{
				return this.chaCtrl.fileHair;
			}
		}

		// Token: 0x06004B5B RID: 19291 RVA: 0x001CC788 File Offset: 0x001CAB88
		public void UpdateCustomUI()
		{
			this.tglShaderType[this.hair.shaderType].SetIsOnWithoutCallback(true);
			this.tglShaderType[(this.hair.shaderType + 1) % 2].SetIsOnWithoutCallback(false);
		}

		// Token: 0x06004B5C RID: 19292 RVA: 0x001CC7C0 File Offset: 0x001CABC0
		private void Start()
		{
			this.customBase.actUpdateCvsHair += this.UpdateCustomUI;
			if (this.tglShaderType.Any<Toggle>())
			{
				this.tglShaderType.Select((Toggle p, int idx) => new
				{
					toggle = p,
					index = (byte)idx
				}).ToList().ForEach(delegate(p)
				{
					(from isOn in p.toggle.onValueChanged.AsObservable<bool>()
					where isOn
					select isOn).Subscribe(delegate(bool isOn)
					{
						this.hair.shaderType = (int)p.index;
						this.chaCtrl.ChangeSettingHairShader();
						this.chaCtrl.ChangeSettingHairTypeAccessoryShaderAll();
					});
				});
			}
		}

		// Token: 0x04004555 RID: 17749
		[SerializeField]
		private Toggle[] tglShaderType;
	}
}
