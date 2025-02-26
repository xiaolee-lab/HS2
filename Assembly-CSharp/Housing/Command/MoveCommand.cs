using System;
using System.Runtime.CompilerServices;
using Manager;
using UnityEngine;

namespace Housing.Command
{
	// Token: 0x0200089B RID: 2203
	public class MoveCommand : ICommand
	{
		// Token: 0x06003949 RID: 14665 RVA: 0x00151A70 File Offset: 0x0014FE70
		public MoveCommand(ObjectCtrl _objectCtrl, Vector3 _old)
		{
			this.infos = new MoveCommand.Info[]
			{
				new MoveCommand.Info(_objectCtrl, _old)
			};
		}

		// Token: 0x0600394A RID: 14666 RVA: 0x00151A8E File Offset: 0x0014FE8E
		public MoveCommand(MoveCommand.Info[] _infos)
		{
			this.infos = _infos;
		}

		// Token: 0x0600394B RID: 14667 RVA: 0x00151AA0 File Offset: 0x0014FEA0
		public void Do()
		{
			if (this.infos.IsNullOrEmpty<MoveCommand.Info>())
			{
				return;
			}
			foreach (MoveCommand.Info info in this.infos)
			{
				info.Do();
			}
			foreach (MoveCommand.Info info2 in this.infos)
			{
				Singleton<Housing>.Instance.CheckOverlap(info2.ObjectCtrl as OCItem);
			}
			Singleton<CraftScene>.Instance.UICtrl.ListUICtrl.RefreshList();
		}

		// Token: 0x0600394C RID: 14668 RVA: 0x00151B34 File Offset: 0x0014FF34
		public void Redo()
		{
			this.Do();
		}

		// Token: 0x0600394D RID: 14669 RVA: 0x00151B3C File Offset: 0x0014FF3C
		public void Undo()
		{
			if (this.infos.IsNullOrEmpty<MoveCommand.Info>())
			{
				return;
			}
			foreach (MoveCommand.Info info in this.infos)
			{
				info.Undo();
			}
			foreach (MoveCommand.Info info2 in this.infos)
			{
				Singleton<Housing>.Instance.CheckOverlap(info2.ObjectCtrl as OCItem);
			}
			Singleton<CraftScene>.Instance.UICtrl.ListUICtrl.RefreshList();
		}

		// Token: 0x040038FB RID: 14587
		private MoveCommand.Info[] infos;

		// Token: 0x0200089C RID: 2204
		public class Info
		{
			// Token: 0x0600394E RID: 14670 RVA: 0x00151BD0 File Offset: 0x0014FFD0
			public Info(ObjectCtrl _objectCtrl, Vector3 _old)
			{
				this.objectCtrl = _objectCtrl;
				this.newPos = this.objectCtrl.Position;
				this.oldPos = _old;
			}

			// Token: 0x17000A4F RID: 2639
			// (get) Token: 0x0600394F RID: 14671 RVA: 0x00151C0D File Offset: 0x0015000D
			public ObjectCtrl ObjectCtrl
			{
				[CompilerGenerated]
				get
				{
					return this.objectCtrl;
				}
			}

			// Token: 0x06003950 RID: 14672 RVA: 0x00151C15 File Offset: 0x00150015
			public void Do()
			{
				this.objectCtrl.Position = this.newPos;
			}

			// Token: 0x06003951 RID: 14673 RVA: 0x00151C28 File Offset: 0x00150028
			public void Undo()
			{
				this.objectCtrl.Position = this.oldPos;
			}

			// Token: 0x040038FC RID: 14588
			private ObjectCtrl objectCtrl;

			// Token: 0x040038FD RID: 14589
			private Vector3 newPos = Vector3.zero;

			// Token: 0x040038FE RID: 14590
			private Vector3 oldPos = Vector3.zero;
		}
	}
}
