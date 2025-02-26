using System;
using AIProject;
using Illusion.Extensions;
using Manager;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace CharaCustom
{
	// Token: 0x020009F9 RID: 2553
	public class CvsC_ClothesInput : MonoBehaviour
	{
		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x06004B6F RID: 19311 RVA: 0x001CD2B6 File Offset: 0x001CB6B6
		private CustomBase customBase
		{
			get
			{
				return Singleton<CustomBase>.Instance;
			}
		}

		// Token: 0x06004B70 RID: 19312 RVA: 0x001CD2C0 File Offset: 0x001CB6C0
		public void SetupInputCoordinateNameWindow(string name)
		{
			this.customBase.customCtrl.showInputCoordinate = true;
			this.inpName.text = name;
			if (null != this.textNameDummy)
			{
				this.textNameDummy.text = name;
			}
			this.inpName.ActivateInputField();
		}

		// Token: 0x06004B71 RID: 19313 RVA: 0x001CD314 File Offset: 0x001CB714
		private void Start()
		{
			this.customBase.lstInputField.Add(this.inpName);
			if (null != this.inpName)
			{
				this.inpName.OnEndEditAsObservable().Subscribe(delegate(string buf)
				{
					if (null != this.textNameDummy)
					{
						this.textNameDummy.text = this.inpName.text;
					}
				});
				if (null != this.objNameDummy)
				{
					this.objNameDummy.UpdateAsObservable().Subscribe(delegate(Unit _)
					{
						bool isFocused = this.inpName.isFocused;
						if (this.objNameDummy.activeSelf == isFocused)
						{
							this.objNameDummy.SetActiveIfDifferent(!isFocused);
						}
					});
				}
				if (null != this.btnBack)
				{
					this.btnBack.OnClickAsObservable().Subscribe(delegate(Unit _)
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Cancel);
						this.customBase.customCtrl.showInputCoordinate = false;
					});
				}
				if (null != this.btnEntry)
				{
					this.btnEntry.UpdateAsObservable().Subscribe(delegate(Unit _)
					{
						bool interactable = !this.inpName.text.IsNullOrEmpty();
						this.btnEntry.interactable = interactable;
					});
					this.btnEntry.OnClickAsObservable().Subscribe(delegate(Unit _)
					{
						Singleton<Manager.Resources>.Instance.SoundPack.Play(SoundPack.SystemSE.Save);
						this.customBase.customCtrl.showInputCoordinate = false;
						if (this.actEntry != null)
						{
							this.actEntry(this.inpName.text);
						}
					});
				}
			}
		}

		// Token: 0x04004560 RID: 17760
		[SerializeField]
		private InputField inpName;

		// Token: 0x04004561 RID: 17761
		[SerializeField]
		private GameObject objNameDummy;

		// Token: 0x04004562 RID: 17762
		[SerializeField]
		private Text textNameDummy;

		// Token: 0x04004563 RID: 17763
		[SerializeField]
		private Button btnEntry;

		// Token: 0x04004564 RID: 17764
		[SerializeField]
		private Button btnBack;

		// Token: 0x04004565 RID: 17765
		public Action<string> actEntry;
	}
}
