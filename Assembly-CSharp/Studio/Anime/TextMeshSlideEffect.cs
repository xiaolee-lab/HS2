using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Studio.Anime
{
	// Token: 0x020012E9 RID: 4841
	public class TextMeshSlideEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
	{
		// Token: 0x1700220C RID: 8716
		// (get) Token: 0x0600A195 RID: 41365 RVA: 0x00424FD0 File Offset: 0x004233D0
		private float ParentWidth
		{
			[CompilerGenerated]
			get
			{
				return (!(this.transBase != null)) ? 0f : this.transBase.sizeDelta.x;
			}
		}

		// Token: 0x1700220D RID: 8717
		// (get) Token: 0x0600A196 RID: 41366 RVA: 0x0042500B File Offset: 0x0042340B
		private float PreferredWidth
		{
			[CompilerGenerated]
			get
			{
				return (!(this.textMesh != null)) ? 0f : this.textMesh.preferredWidth;
			}
		}

		// Token: 0x0600A197 RID: 41367 RVA: 0x00425033 File Offset: 0x00423433
		public void Stop()
		{
			if (!this.isPlay)
			{
				return;
			}
			this.isPlay = false;
			this.textMesh.margin = Vector4.zero;
			this.textMesh.overflowMode = TextOverflowModes.Ellipsis;
		}

		// Token: 0x0600A198 RID: 41368 RVA: 0x00425064 File Offset: 0x00423464
		public void OnChangedText()
		{
			if (this.sildeDisposable != null)
			{
				this.sildeDisposable.Dispose();
				this.sildeDisposable = null;
			}
			this.textMesh.margin = Vector4.zero;
			this.textMesh.alignment = TextAlignmentOptions.Center;
			this.textMesh.overflowMode = TextOverflowModes.Overflow;
			this.textMesh.enableWordWrapping = false;
			this.CheckText();
		}

		// Token: 0x0600A199 RID: 41369 RVA: 0x004250CC File Offset: 0x004234CC
		[DebuggerStepThrough]
		private void CheckText()
		{
			TextMeshSlideEffect.<CheckText>c__async0 <CheckText>c__async;
			<CheckText>c__async.$this = this;
			<CheckText>c__async.$builder = AsyncVoidMethodBuilder.Create();
			<CheckText>c__async.$builder.Start<TextMeshSlideEffect.<CheckText>c__async0>(ref <CheckText>c__async);
		}

		// Token: 0x0600A19A RID: 41370 RVA: 0x004250FB File Offset: 0x004234FB
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (!this.isMove)
			{
				return;
			}
			this.isPlay = true;
			this.textMesh.overflowMode = TextOverflowModes.Overflow;
		}

		// Token: 0x0600A19B RID: 41371 RVA: 0x0042511C File Offset: 0x0042351C
		public void OnPointerExit(PointerEventData eventData)
		{
			if (!this.isMove)
			{
				return;
			}
			this.isPlay = false;
			this.textMesh.margin = Vector4.zero;
			this.textMesh.overflowMode = TextOverflowModes.Ellipsis;
		}

		// Token: 0x04007FB8 RID: 32696
		[SerializeField]
		private RectTransform transBase;

		// Token: 0x04007FB9 RID: 32697
		[SerializeField]
		private TextMeshProUGUI textMesh;

		// Token: 0x04007FBA RID: 32698
		public float speed = 50f;

		// Token: 0x04007FBB RID: 32699
		private SingleAssignmentDisposable sildeDisposable;

		// Token: 0x04007FBC RID: 32700
		private bool isPlay;

		// Token: 0x04007FBD RID: 32701
		private bool isMove;
	}
}
