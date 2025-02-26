using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200133D RID: 4925
public class RouteNode : MonoBehaviour
{
	// Token: 0x1700228A RID: 8842
	// (get) Token: 0x0600A4E9 RID: 42217 RVA: 0x00433F76 File Offset: 0x00432376
	// (set) Token: 0x0600A4EA RID: 42218 RVA: 0x00433F83 File Offset: 0x00432383
	public string text
	{
		get
		{
			return this.textName.text;
		}
		set
		{
			this.textName.text = value;
		}
	}

	// Token: 0x1700228B RID: 8843
	// (set) Token: 0x0600A4EB RID: 42219 RVA: 0x00433F94 File Offset: 0x00432394
	public RouteNode.State state
	{
		set
		{
			if (value != RouteNode.State.Stop)
			{
				if (value != RouteNode.State.Play)
				{
					if (value == RouteNode.State.End)
					{
						this.buttonPlay.image.color = Color.red;
						this.buttonPlay.image.sprite = this.spritePlay[1];
					}
				}
				else
				{
					this.buttonPlay.image.color = Color.green;
					this.buttonPlay.image.sprite = this.spritePlay[1];
				}
			}
			else
			{
				this.buttonPlay.image.color = Color.white;
				this.buttonPlay.image.sprite = this.spritePlay[0];
			}
		}
	}

	// Token: 0x040081D3 RID: 33235
	public Button buttonSelect;

	// Token: 0x040081D4 RID: 33236
	[SerializeField]
	private Text textName;

	// Token: 0x040081D5 RID: 33237
	public Button buttonPlay;

	// Token: 0x040081D6 RID: 33238
	public Sprite[] spritePlay;

	// Token: 0x0200133E RID: 4926
	public enum State
	{
		// Token: 0x040081D8 RID: 33240
		Stop,
		// Token: 0x040081D9 RID: 33241
		Play,
		// Token: 0x040081DA RID: 33242
		End
	}
}
