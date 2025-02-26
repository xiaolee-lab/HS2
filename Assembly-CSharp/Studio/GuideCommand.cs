using System;
using UnityEngine;

namespace Studio
{
	// Token: 0x020011FA RID: 4602
	public static class GuideCommand
	{
		// Token: 0x020011FB RID: 4603
		public class AddInfo
		{
			// Token: 0x04007943 RID: 31043
			public int dicKey;

			// Token: 0x04007944 RID: 31044
			public Vector3 value;
		}

		// Token: 0x020011FC RID: 4604
		public class EqualsInfo
		{
			// Token: 0x04007945 RID: 31045
			public int dicKey;

			// Token: 0x04007946 RID: 31046
			public Vector3 oldValue;

			// Token: 0x04007947 RID: 31047
			public Vector3 newValue;
		}

		// Token: 0x020011FD RID: 4605
		public class MoveAddCommand : ICommand
		{
			// Token: 0x06009706 RID: 38662 RVA: 0x003E7D80 File Offset: 0x003E6180
			public MoveAddCommand(GuideCommand.AddInfo[] _changeAmountInfo)
			{
				this.changeAmountInfo = _changeAmountInfo;
			}

			// Token: 0x06009707 RID: 38663 RVA: 0x003E7D90 File Offset: 0x003E6190
			public void Do()
			{
				for (int i = 0; i < this.changeAmountInfo.Length; i++)
				{
					ChangeAmount changeAmount = Studio.GetChangeAmount(this.changeAmountInfo[i].dicKey);
					if (changeAmount != null)
					{
						changeAmount.pos += this.changeAmountInfo[i].value;
					}
				}
			}

			// Token: 0x06009708 RID: 38664 RVA: 0x003E7DF2 File Offset: 0x003E61F2
			public void Redo()
			{
				this.Do();
			}

			// Token: 0x06009709 RID: 38665 RVA: 0x003E7DFC File Offset: 0x003E61FC
			public void Undo()
			{
				for (int i = 0; i < this.changeAmountInfo.Length; i++)
				{
					ChangeAmount changeAmount = Studio.GetChangeAmount(this.changeAmountInfo[i].dicKey);
					if (changeAmount != null)
					{
						changeAmount.pos -= this.changeAmountInfo[i].value;
					}
				}
			}

			// Token: 0x04007948 RID: 31048
			private GuideCommand.AddInfo[] changeAmountInfo;
		}

		// Token: 0x020011FE RID: 4606
		public class MoveEqualsCommand : ICommand
		{
			// Token: 0x0600970A RID: 38666 RVA: 0x003E7E5E File Offset: 0x003E625E
			public MoveEqualsCommand(GuideCommand.EqualsInfo[] _changeAmountInfo)
			{
				this.changeAmountInfo = _changeAmountInfo;
			}

			// Token: 0x0600970B RID: 38667 RVA: 0x003E7E70 File Offset: 0x003E6270
			public void Do()
			{
				for (int i = 0; i < this.changeAmountInfo.Length; i++)
				{
					ChangeAmount changeAmount = Studio.GetChangeAmount(this.changeAmountInfo[i].dicKey);
					if (changeAmount != null)
					{
						changeAmount.pos = this.changeAmountInfo[i].newValue;
					}
				}
			}

			// Token: 0x0600970C RID: 38668 RVA: 0x003E7EC7 File Offset: 0x003E62C7
			public void Redo()
			{
				this.Do();
			}

			// Token: 0x0600970D RID: 38669 RVA: 0x003E7ED0 File Offset: 0x003E62D0
			public void Undo()
			{
				for (int i = 0; i < this.changeAmountInfo.Length; i++)
				{
					ChangeAmount changeAmount = Studio.GetChangeAmount(this.changeAmountInfo[i].dicKey);
					if (changeAmount != null)
					{
						changeAmount.pos = this.changeAmountInfo[i].oldValue;
					}
				}
			}

			// Token: 0x04007949 RID: 31049
			private GuideCommand.EqualsInfo[] changeAmountInfo;
		}

		// Token: 0x020011FF RID: 4607
		public class RotationAddCommand : ICommand
		{
			// Token: 0x0600970E RID: 38670 RVA: 0x003E7F27 File Offset: 0x003E6327
			public RotationAddCommand(GuideCommand.AddInfo[] _changeAmountInfo)
			{
				this.changeAmountInfo = _changeAmountInfo;
			}

			// Token: 0x0600970F RID: 38671 RVA: 0x003E7F38 File Offset: 0x003E6338
			public void Do()
			{
				for (int i = 0; i < this.changeAmountInfo.Length; i++)
				{
					ChangeAmount changeAmount = Studio.GetChangeAmount(this.changeAmountInfo[i].dicKey);
					if (changeAmount != null)
					{
						changeAmount.rot += this.changeAmountInfo[i].value;
					}
				}
			}

			// Token: 0x06009710 RID: 38672 RVA: 0x003E7F9A File Offset: 0x003E639A
			public void Redo()
			{
				this.Do();
			}

			// Token: 0x06009711 RID: 38673 RVA: 0x003E7FA4 File Offset: 0x003E63A4
			public void Undo()
			{
				for (int i = 0; i < this.changeAmountInfo.Length; i++)
				{
					ChangeAmount changeAmount = Studio.GetChangeAmount(this.changeAmountInfo[i].dicKey);
					if (changeAmount != null)
					{
						changeAmount.rot = this.changeAmountInfo[i].value;
					}
				}
			}

			// Token: 0x0400794A RID: 31050
			private GuideCommand.AddInfo[] changeAmountInfo;
		}

		// Token: 0x02001200 RID: 4608
		public class RotationEqualsCommand : ICommand
		{
			// Token: 0x06009712 RID: 38674 RVA: 0x003E7FFB File Offset: 0x003E63FB
			public RotationEqualsCommand(GuideCommand.EqualsInfo[] _changeAmountInfo)
			{
				this.changeAmountInfo = _changeAmountInfo;
			}

			// Token: 0x06009713 RID: 38675 RVA: 0x003E800C File Offset: 0x003E640C
			public void Do()
			{
				for (int i = 0; i < this.changeAmountInfo.Length; i++)
				{
					ChangeAmount changeAmount = Studio.GetChangeAmount(this.changeAmountInfo[i].dicKey);
					if (changeAmount != null)
					{
						changeAmount.rot = this.changeAmountInfo[i].newValue;
					}
				}
			}

			// Token: 0x06009714 RID: 38676 RVA: 0x003E8063 File Offset: 0x003E6463
			public void Redo()
			{
				this.Do();
			}

			// Token: 0x06009715 RID: 38677 RVA: 0x003E806C File Offset: 0x003E646C
			public void Undo()
			{
				for (int i = 0; i < this.changeAmountInfo.Length; i++)
				{
					ChangeAmount changeAmount = Studio.GetChangeAmount(this.changeAmountInfo[i].dicKey);
					if (changeAmount != null)
					{
						changeAmount.rot = this.changeAmountInfo[i].oldValue;
					}
				}
			}

			// Token: 0x0400794B RID: 31051
			private GuideCommand.EqualsInfo[] changeAmountInfo;
		}

		// Token: 0x02001201 RID: 4609
		public class ScaleEqualsCommand : ICommand
		{
			// Token: 0x06009716 RID: 38678 RVA: 0x003E80C3 File Offset: 0x003E64C3
			public ScaleEqualsCommand(GuideCommand.EqualsInfo[] _changeAmountInfo)
			{
				this.changeAmountInfo = _changeAmountInfo;
			}

			// Token: 0x06009717 RID: 38679 RVA: 0x003E80D4 File Offset: 0x003E64D4
			public void Do()
			{
				for (int i = 0; i < this.changeAmountInfo.Length; i++)
				{
					ChangeAmount changeAmount = Studio.GetChangeAmount(this.changeAmountInfo[i].dicKey);
					if (changeAmount != null)
					{
						changeAmount.scale = this.changeAmountInfo[i].newValue;
					}
				}
			}

			// Token: 0x06009718 RID: 38680 RVA: 0x003E812B File Offset: 0x003E652B
			public void Redo()
			{
				this.Do();
			}

			// Token: 0x06009719 RID: 38681 RVA: 0x003E8134 File Offset: 0x003E6534
			public void Undo()
			{
				for (int i = 0; i < this.changeAmountInfo.Length; i++)
				{
					ChangeAmount changeAmount = Studio.GetChangeAmount(this.changeAmountInfo[i].dicKey);
					if (changeAmount != null)
					{
						changeAmount.scale = this.changeAmountInfo[i].oldValue;
					}
				}
			}

			// Token: 0x0400794C RID: 31052
			private GuideCommand.EqualsInfo[] changeAmountInfo;
		}
	}
}
