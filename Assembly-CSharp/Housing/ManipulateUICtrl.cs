using System;
using Housing.Command;
using Illusion.Extensions;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Housing
{
	// Token: 0x020008AB RID: 2219
	[Serializable]
	public class ManipulateUICtrl : UIDerived
	{
		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x060039CB RID: 14795 RVA: 0x00153AF2 File Offset: 0x00151EF2
		// (set) Token: 0x060039CC RID: 14796 RVA: 0x00153AFF File Offset: 0x00151EFF
		public bool Visible
		{
			get
			{
				return this.visibleReactive.Value;
			}
			set
			{
				this.visibleReactive.Value = value;
			}
		}

		// Token: 0x060039CD RID: 14797 RVA: 0x00153B10 File Offset: 0x00151F10
		public override void Init(UICtrl _uiCtrl, bool _tutorial)
		{
			base.Init(_uiCtrl, _tutorial);
			this.graphics = new Graphic[]
			{
				this.buttonRotL.image,
				this.buttonRotR.image
			};
			this.buttonRotL.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.Rotation(90f);
			});
			this.buttonRotR.OnClickAsObservable().Subscribe(delegate(Unit _)
			{
				this.Rotation(-90f);
			});
			this.visibleReactive.Subscribe(delegate(bool _b)
			{
				this.canvasGroup.Enable(_b, false);
				foreach (Graphic graphic in this.graphics)
				{
					graphic.raycastTarget = _b;
				}
			});
		}

		// Token: 0x060039CE RID: 14798 RVA: 0x00153B9F File Offset: 0x00151F9F
		public override void UpdateUI()
		{
		}

		// Token: 0x060039CF RID: 14799 RVA: 0x00153BA4 File Offset: 0x00151FA4
		private void Rotation(float _value)
		{
			ObjectCtrl selectObject = Singleton<Selection>.Instance.SelectObject;
			Vector3 localEulerAngles = selectObject.LocalEulerAngles;
			Vector3 localEulerAngles2 = selectObject.LocalEulerAngles;
			for (int i = 0; i < 3; i++)
			{
				localEulerAngles2.y = (localEulerAngles2.y + _value) % 360f;
				selectObject.LocalEulerAngles = localEulerAngles2;
				if (Singleton<GuideManager>.Instance.CheckRot(selectObject))
				{
					Singleton<UndoRedoManager>.Instance.Push(new RotationCommand(selectObject, localEulerAngles));
					Singleton<Housing>.Instance.CheckOverlap(selectObject as OCItem);
					Singleton<CraftScene>.Instance.UICtrl.ListUICtrl.RefreshList();
					return;
				}
			}
			selectObject.LocalEulerAngles = localEulerAngles;
		}

		// Token: 0x0400394C RID: 14668
		[SerializeField]
		private CanvasGroup canvasGroup;

		// Token: 0x0400394D RID: 14669
		[SerializeField]
		private Button buttonRotL;

		// Token: 0x0400394E RID: 14670
		[SerializeField]
		private Button buttonRotR;

		// Token: 0x0400394F RID: 14671
		private BoolReactiveProperty visibleReactive = new BoolReactiveProperty(false);

		// Token: 0x04003950 RID: 14672
		private Graphic[] graphics;
	}
}
