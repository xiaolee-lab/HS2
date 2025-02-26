using System;
using Manager;
using UnityEngine;

namespace Housing.Command
{
	// Token: 0x0200089D RID: 2205
	public class RotationCommand : ICommand
	{
		// Token: 0x06003952 RID: 14674 RVA: 0x00151C3B File Offset: 0x0015003B
		public RotationCommand(ObjectCtrl _objectCtrl, Vector3 _old)
		{
			this.objectCtrl = _objectCtrl;
			this.newRot = this.objectCtrl.LocalEulerAngles;
			this.oldRot = _old;
		}

		// Token: 0x06003953 RID: 14675 RVA: 0x00151C78 File Offset: 0x00150078
		public void Do()
		{
			this.objectCtrl.LocalEulerAngles = this.newRot;
			Singleton<Housing>.Instance.CheckOverlap(this.objectCtrl as OCItem);
			Singleton<CraftScene>.Instance.UICtrl.ListUICtrl.RefreshList();
		}

		// Token: 0x06003954 RID: 14676 RVA: 0x00151CB5 File Offset: 0x001500B5
		public void Redo()
		{
			this.Do();
		}

		// Token: 0x06003955 RID: 14677 RVA: 0x00151CBD File Offset: 0x001500BD
		public void Undo()
		{
			this.objectCtrl.LocalEulerAngles = this.oldRot;
			Singleton<Housing>.Instance.CheckOverlap(this.objectCtrl as OCItem);
			Singleton<CraftScene>.Instance.UICtrl.ListUICtrl.RefreshList();
		}

		// Token: 0x040038FF RID: 14591
		private ObjectCtrl objectCtrl;

		// Token: 0x04003900 RID: 14592
		private Vector3 newRot = Vector3.zero;

		// Token: 0x04003901 RID: 14593
		private Vector3 oldRot = Vector3.zero;
	}
}
