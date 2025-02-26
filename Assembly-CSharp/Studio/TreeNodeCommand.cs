using System;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001208 RID: 4616
	public static class TreeNodeCommand
	{
		// Token: 0x02001209 RID: 4617
		public class MoveCopyInfo
		{
			// Token: 0x0600972A RID: 38698 RVA: 0x003E82AC File Offset: 0x003E66AC
			public MoveCopyInfo(int _dicKey, ChangeAmount _old, ChangeAmount _new)
			{
				this.dicKey = _dicKey;
				this.oldValue = new Vector3[]
				{
					_old.pos,
					_old.rot
				};
				this.newValue = new Vector3[]
				{
					_new.pos,
					_new.rot
				};
			}

			// Token: 0x04007954 RID: 31060
			public int dicKey;

			// Token: 0x04007955 RID: 31061
			public Vector3[] oldValue = new Vector3[]
			{
				Vector3.zero,
				Vector3.zero
			};

			// Token: 0x04007956 RID: 31062
			public Vector3[] newValue = new Vector3[]
			{
				Vector3.zero,
				Vector3.zero
			};
		}

		// Token: 0x0200120A RID: 4618
		public class MoveCopyCommand : ICommand
		{
			// Token: 0x0600972B RID: 38699 RVA: 0x003E8382 File Offset: 0x003E6782
			public MoveCopyCommand(TreeNodeCommand.MoveCopyInfo[] _changeAmountInfo)
			{
				this.changeAmountInfo = _changeAmountInfo;
			}

			// Token: 0x0600972C RID: 38700 RVA: 0x003E8394 File Offset: 0x003E6794
			public void Do()
			{
				for (int i = 0; i < this.changeAmountInfo.Length; i++)
				{
					ChangeAmount changeAmount = Studio.GetChangeAmount(this.changeAmountInfo[i].dicKey);
					if (changeAmount != null)
					{
						changeAmount.pos = this.changeAmountInfo[i].newValue[0];
						changeAmount.rot = this.changeAmountInfo[i].newValue[1];
					}
				}
			}

			// Token: 0x0600972D RID: 38701 RVA: 0x003E8414 File Offset: 0x003E6814
			public void Redo()
			{
				this.Do();
			}

			// Token: 0x0600972E RID: 38702 RVA: 0x003E841C File Offset: 0x003E681C
			public void Undo()
			{
				for (int i = 0; i < this.changeAmountInfo.Length; i++)
				{
					ChangeAmount changeAmount = Studio.GetChangeAmount(this.changeAmountInfo[i].dicKey);
					if (changeAmount != null)
					{
						changeAmount.pos = this.changeAmountInfo[i].oldValue[0];
						changeAmount.rot = this.changeAmountInfo[i].oldValue[1];
					}
				}
			}

			// Token: 0x04007957 RID: 31063
			private TreeNodeCommand.MoveCopyInfo[] changeAmountInfo;
		}
	}
}
