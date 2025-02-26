using System;
using System.Collections.Generic;
using Correct.Process;
using UniRx;
using UnityEngine;

namespace Correct
{
	// Token: 0x02000B35 RID: 2869
	public abstract class BaseCorrect : MonoBehaviour
	{
		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x0600541B RID: 21531 RVA: 0x00251EA0 File Offset: 0x002502A0
		// (set) Token: 0x0600541C RID: 21532 RVA: 0x00251EA8 File Offset: 0x002502A8
		public List<BaseCorrect.Info> list
		{
			get
			{
				return this._list;
			}
			set
			{
				this._list = value;
			}
		}

		// Token: 0x0600541D RID: 21533 RVA: 0x00251EB4 File Offset: 0x002502B4
		public List<string> GetFrameNames(string[] FrameNames)
		{
			List<string> list = new List<string>();
			foreach (string item in FrameNames)
			{
				list.Add(item);
			}
			return list;
		}

		// Token: 0x17000F19 RID: 3865
		// (set) Token: 0x0600541E RID: 21534 RVA: 0x00251EEC File Offset: 0x002502EC
		public bool isEnabled
		{
			set
			{
				foreach (BaseCorrect.Info info in this.list)
				{
					info.enabled = value;
				}
			}
		}

		// Token: 0x04004F00 RID: 20224
		[SerializeField]
		private List<BaseCorrect.Info> _list = new List<BaseCorrect.Info>();

		// Token: 0x02000B36 RID: 2870
		[Serializable]
		public class Info
		{
			// Token: 0x0600541F RID: 21535 RVA: 0x00251F48 File Offset: 0x00250348
			public Info(Component component)
			{
				this.component = component;
				this.name = component.name;
			}

			// Token: 0x17000F1A RID: 3866
			// (get) Token: 0x06005420 RID: 21536 RVA: 0x00251F63 File Offset: 0x00250363
			public BaseProcess process
			{
				get
				{
					if (this._process == null)
					{
						this._process = this.CreateProcess();
					}
					return this._process;
				}
			}

			// Token: 0x06005421 RID: 21537 RVA: 0x00251F88 File Offset: 0x00250388
			public void ReSetup()
			{
				this._process = this.CreateProcess();
			}

			// Token: 0x17000F1B RID: 3867
			// (get) Token: 0x06005422 RID: 21538 RVA: 0x00251F96 File Offset: 0x00250396
			public BaseData data
			{
				get
				{
					if (this._data == null)
					{
						this._data = this.process.data;
					}
					return this._data;
				}
			}

			// Token: 0x17000F1C RID: 3868
			// (get) Token: 0x06005423 RID: 21539 RVA: 0x00251FC0 File Offset: 0x002503C0
			// (set) Token: 0x06005424 RID: 21540 RVA: 0x00251FCD File Offset: 0x002503CD
			public bool enabled
			{
				get
				{
					return this.process.enabled;
				}
				set
				{
					this.process.enabled = value;
				}
			}

			// Token: 0x06005425 RID: 21541 RVA: 0x00251FDC File Offset: 0x002503DC
			private BaseProcess CreateProcess()
			{
				BaseProcess[] components = this.component.GetComponents<BaseProcess>();
				for (int i = 0; i < components.Length; i++)
				{
					BaseProcess delete2 = components[i];
					BaseProcess delete = delete2;
					Observable.NextFrame(FrameCountType.Update).Subscribe(delegate(Unit _)
					{
						UnityEngine.Object.DestroyImmediate(delete);
					});
				}
				BaseCorrect.Info.ProcOrderType procOrderType = this.type;
				if (procOrderType == BaseCorrect.Info.ProcOrderType.First)
				{
					return this.component.gameObject.AddComponent<IKBeforeOfDankonProcess>();
				}
				if (procOrderType == BaseCorrect.Info.ProcOrderType.Second)
				{
					return this.component.gameObject.AddComponent<IKBeforeProcess>();
				}
				if (procOrderType != BaseCorrect.Info.ProcOrderType.Last)
				{
					return null;
				}
				return this.component.gameObject.AddComponent<IKAfterProcess>();
			}

			// Token: 0x17000F1D RID: 3869
			// (get) Token: 0x06005426 RID: 21542 RVA: 0x00252096 File Offset: 0x00250496
			// (set) Token: 0x06005427 RID: 21543 RVA: 0x002520A3 File Offset: 0x002504A3
			public Transform bone
			{
				get
				{
					return this.data.bone;
				}
				set
				{
					this.data.bone = value;
				}
			}

			// Token: 0x17000F1E RID: 3870
			// (get) Token: 0x06005428 RID: 21544 RVA: 0x002520B1 File Offset: 0x002504B1
			// (set) Token: 0x06005429 RID: 21545 RVA: 0x002520BE File Offset: 0x002504BE
			public Vector3 pos
			{
				get
				{
					return this.data.pos;
				}
				set
				{
					this.data.pos = value;
				}
			}

			// Token: 0x17000F1F RID: 3871
			// (get) Token: 0x0600542A RID: 21546 RVA: 0x002520CC File Offset: 0x002504CC
			// (set) Token: 0x0600542B RID: 21547 RVA: 0x002520D9 File Offset: 0x002504D9
			public Quaternion rot
			{
				get
				{
					return this.data.rot;
				}
				set
				{
					this.data.rot = value;
				}
			}

			// Token: 0x17000F20 RID: 3872
			// (get) Token: 0x0600542C RID: 21548 RVA: 0x002520E7 File Offset: 0x002504E7
			// (set) Token: 0x0600542D RID: 21549 RVA: 0x002520F4 File Offset: 0x002504F4
			public Vector3 ang
			{
				get
				{
					return this.data.ang;
				}
				set
				{
					this.data.ang = value;
				}
			}

			// Token: 0x04004F01 RID: 20225
			[SerializeField]
			public string name;

			// Token: 0x04004F02 RID: 20226
			public BaseCorrect.Info.ProcOrderType type;

			// Token: 0x04004F03 RID: 20227
			[SerializeField]
			private Component component;

			// Token: 0x04004F04 RID: 20228
			[SerializeField]
			private BaseProcess _process;

			// Token: 0x04004F05 RID: 20229
			private BaseData _data;

			// Token: 0x02000B37 RID: 2871
			public enum ProcOrderType
			{
				// Token: 0x04004F07 RID: 20231
				First,
				// Token: 0x04004F08 RID: 20232
				Second,
				// Token: 0x04004F09 RID: 20233
				Last
			}
		}
	}
}
