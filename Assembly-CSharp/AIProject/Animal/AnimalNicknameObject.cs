using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000B88 RID: 2952
	public class AnimalNicknameObject : MonoBehaviour
	{
		// Token: 0x17001033 RID: 4147
		// (get) Token: 0x060057D1 RID: 22481 RVA: 0x0025DD5E File Offset: 0x0025C15E
		// (set) Token: 0x060057D2 RID: 22482 RVA: 0x0025DD66 File Offset: 0x0025C166
		public INicknameObject Obj { get; private set; }

		// Token: 0x060057D3 RID: 22483 RVA: 0x0025DD70 File Offset: 0x0025C170
		private void Awake()
		{
			if (this.Obj == null)
			{
				this.Obj = ((this._animal ?? base.GetComponent<AnimalBase>()) as INicknameObject);
			}
			if (this.Obj == null)
			{
				return;
			}
			this._outputter = ((!Singleton<MapUIContainer>.IsInstance()) ? null : MapUIContainer.NicknameUI);
			if (this._outputter == null)
			{
				UnityEngine.Object.DestroyImmediate(this);
				return;
			}
			this.Obj.NicknameEnabled = true;
			this._outputter.AddElement(this.Obj);
			(from _ in this.OnEnableAsObservable()
			where this.Obj != null
			select _).Subscribe(delegate(Unit _)
			{
				this.Obj.NicknameEnabled = true;
			});
			(from _ in this.OnDisableAsObservable()
			where this.Obj != null
			select _).Subscribe(delegate(Unit _)
			{
				this.Obj.NicknameEnabled = false;
			});
			(from _ in this.OnDestroyAsObservable()
			where this.Obj != null
			select _).Subscribe(delegate(Unit _)
			{
				if (this._outputter != null)
				{
					this._outputter.RemoveElement(this.Obj);
				}
			});
		}

		// Token: 0x060057D4 RID: 22484 RVA: 0x0025DE7D File Offset: 0x0025C27D
		private void LateUpdate()
		{
		}

		// Token: 0x040050BF RID: 20671
		[SerializeField]
		private AnimalBase _animal;

		// Token: 0x040050C1 RID: 20673
		private AnimalNicknameOutput _outputter;
	}
}
