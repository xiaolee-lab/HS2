using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Studio
{
	// Token: 0x0200132D RID: 4909
	public class ObjectCtrl : Singleton<ObjectCtrl>
	{
		// Token: 0x17002274 RID: 8820
		// (get) Token: 0x0600A43C RID: 42044 RVA: 0x00430FF5 File Offset: 0x0042F3F5
		// (set) Token: 0x0600A43D RID: 42045 RVA: 0x00431002 File Offset: 0x0042F402
		public bool active
		{
			get
			{
				return base.gameObject.activeSelf;
			}
			set
			{
				base.gameObject.SetActive(value);
			}
		}

		// Token: 0x0600A43E RID: 42046 RVA: 0x00431010 File Offset: 0x0042F410
		private void OnBeginDragTrans()
		{
			Dictionary<int, ChangeAmount> selectObjectDictionary = Singleton<GuideObjectManager>.Instance.selectObjectDictionary;
			this.dicOld = selectObjectDictionary.ToDictionary((KeyValuePair<int, ChangeAmount> v) => v.Key, (KeyValuePair<int, ChangeAmount> v) => v.Value.pos);
		}

		// Token: 0x0600A43F RID: 42047 RVA: 0x00431070 File Offset: 0x0042F470
		private void OnEndDragTrans()
		{
			GuideCommand.EqualsInfo[] changeAmountInfo = (from v in Singleton<GuideObjectManager>.Instance.selectObjectDictionary
			select new GuideCommand.EqualsInfo
			{
				dicKey = v.Key,
				oldValue = this.dicOld[v.Key],
				newValue = v.Value.pos
			}).ToArray<GuideCommand.EqualsInfo>();
			Singleton<UndoRedoManager>.Instance.Push(new GuideCommand.MoveEqualsCommand(changeAmountInfo));
		}

		// Token: 0x0600A440 RID: 42048 RVA: 0x004310B0 File Offset: 0x0042F4B0
		private void OnDragTransXZ()
		{
			Vector3 vector = new Vector3(Input.GetAxis("Mouse X"), 0f, Input.GetAxis("Mouse Y"));
			vector *= this.moveRate;
			vector = Camera.main.transform.TransformVector(vector);
			vector.y = 0f;
			foreach (GuideObject guideObject in from v in Singleton<GuideObjectManager>.Instance.selectObjects
			where v.enablePos
			select v)
			{
				guideObject.MoveLocal(vector);
			}
		}

		// Token: 0x0600A441 RID: 42049 RVA: 0x0043117C File Offset: 0x0042F57C
		private void OnDragTransY()
		{
			Vector3 vector = new Vector3(0f, Input.GetAxis("Mouse Y"), 0f);
			vector *= this.moveRate;
			vector = Camera.main.transform.TransformVector(vector);
			vector.x = 0f;
			vector.z = 0f;
			foreach (GuideObject guideObject in from v in Singleton<GuideObjectManager>.Instance.selectObjects
			where v.enablePos
			select v)
			{
				guideObject.MoveLocal(vector);
			}
		}

		// Token: 0x0600A442 RID: 42050 RVA: 0x0043124C File Offset: 0x0042F64C
		private void OnBeginDragRot()
		{
			Dictionary<int, ChangeAmount> selectObjectDictionary = Singleton<GuideObjectManager>.Instance.selectObjectDictionary;
			this.dicOld = selectObjectDictionary.ToDictionary((KeyValuePair<int, ChangeAmount> v) => v.Key, (KeyValuePair<int, ChangeAmount> v) => v.Value.rot);
		}

		// Token: 0x0600A443 RID: 42051 RVA: 0x004312AC File Offset: 0x0042F6AC
		private void OnEndDragRot()
		{
			GuideCommand.EqualsInfo[] changeAmountInfo = (from v in Singleton<GuideObjectManager>.Instance.selectObjectDictionary
			select new GuideCommand.EqualsInfo
			{
				dicKey = v.Key,
				oldValue = this.dicOld[v.Key],
				newValue = v.Value.rot
			}).ToArray<GuideCommand.EqualsInfo>();
			Singleton<UndoRedoManager>.Instance.Push(new GuideCommand.RotationEqualsCommand(changeAmountInfo));
		}

		// Token: 0x0600A444 RID: 42052 RVA: 0x004312EC File Offset: 0x0042F6EC
		private void OnDragRotX()
		{
			float axis = Input.GetAxis("Mouse Y");
			Vector3 right = this.transformBase.right;
			foreach (GuideObject guideObject in from v in Singleton<GuideObjectManager>.Instance.selectObjects
			where v.enableRot
			select v)
			{
				guideObject.Rotation(right, axis);
			}
		}

		// Token: 0x0600A445 RID: 42053 RVA: 0x00431384 File Offset: 0x0042F784
		private void OnDragRotY()
		{
			float angle = Input.GetAxis("Mouse X") * -1f;
			Vector3 up = this.transformBase.up;
			foreach (GuideObject guideObject in from v in Singleton<GuideObjectManager>.Instance.selectObjects
			where v.enableRot
			select v)
			{
				guideObject.Rotation(up, angle);
			}
		}

		// Token: 0x0600A446 RID: 42054 RVA: 0x00431424 File Offset: 0x0042F824
		private void OnDragRotZ()
		{
			float angle = Input.GetAxis("Mouse X") * -1f;
			Vector3 forward = this.transformBase.forward;
			foreach (GuideObject guideObject in from v in Singleton<GuideObjectManager>.Instance.selectObjects
			where v.enableRot
			select v)
			{
				guideObject.Rotation(forward, angle);
			}
		}

		// Token: 0x0600A447 RID: 42055 RVA: 0x004314C4 File Offset: 0x0042F8C4
		protected override void Awake()
		{
			if (!base.CheckInstance())
			{
				return;
			}
			MapDragButton mapDragButton = this.mapDragButton[0];
			mapDragButton.onBeginDragFunc = (Action)Delegate.Combine(mapDragButton.onBeginDragFunc, new Action(this.OnBeginDragTrans));
			MapDragButton mapDragButton2 = this.mapDragButton[0];
			mapDragButton2.onDragFunc = (Action)Delegate.Combine(mapDragButton2.onDragFunc, new Action(this.OnDragTransXZ));
			MapDragButton mapDragButton3 = this.mapDragButton[0];
			mapDragButton3.onEndDragFunc = (Action)Delegate.Combine(mapDragButton3.onEndDragFunc, new Action(this.OnEndDragTrans));
			MapDragButton mapDragButton4 = this.mapDragButton[1];
			mapDragButton4.onBeginDragFunc = (Action)Delegate.Combine(mapDragButton4.onBeginDragFunc, new Action(this.OnBeginDragTrans));
			MapDragButton mapDragButton5 = this.mapDragButton[1];
			mapDragButton5.onDragFunc = (Action)Delegate.Combine(mapDragButton5.onDragFunc, new Action(this.OnDragTransY));
			MapDragButton mapDragButton6 = this.mapDragButton[1];
			mapDragButton6.onEndDragFunc = (Action)Delegate.Combine(mapDragButton6.onEndDragFunc, new Action(this.OnEndDragTrans));
			for (int i = 0; i < 3; i++)
			{
				MapDragButton mapDragButton7 = this.mapDragButton[2 + i];
				mapDragButton7.onBeginDragFunc = (Action)Delegate.Combine(mapDragButton7.onBeginDragFunc, new Action(this.OnBeginDragRot));
				MapDragButton mapDragButton8 = this.mapDragButton[2 + i];
				mapDragButton8.onEndDragFunc = (Action)Delegate.Combine(mapDragButton8.onEndDragFunc, new Action(this.OnEndDragRot));
			}
			MapDragButton mapDragButton9 = this.mapDragButton[2];
			mapDragButton9.onDragFunc = (Action)Delegate.Combine(mapDragButton9.onDragFunc, new Action(this.OnDragRotX));
			MapDragButton mapDragButton10 = this.mapDragButton[3];
			mapDragButton10.onDragFunc = (Action)Delegate.Combine(mapDragButton10.onDragFunc, new Action(this.OnDragRotY));
			MapDragButton mapDragButton11 = this.mapDragButton[4];
			mapDragButton11.onDragFunc = (Action)Delegate.Combine(mapDragButton11.onDragFunc, new Action(this.OnDragRotZ));
		}

		// Token: 0x04008166 RID: 33126
		[SerializeField]
		private MapDragButton[] mapDragButton;

		// Token: 0x04008167 RID: 33127
		[SerializeField]
		private Transform transformBase;

		// Token: 0x04008168 RID: 33128
		[SerializeField]
		private float moveRate = 0.005f;

		// Token: 0x04008169 RID: 33129
		private Dictionary<int, Vector3> dicOld;
	}
}
