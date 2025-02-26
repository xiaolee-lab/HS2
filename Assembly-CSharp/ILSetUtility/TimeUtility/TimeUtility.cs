using System;
using UnityEngine;

namespace ILSetUtility.TimeUtility
{
	// Token: 0x0200084B RID: 2123
	public class TimeUtility : MonoBehaviour
	{
		// Token: 0x06003634 RID: 13876 RVA: 0x0013FF68 File Offset: 0x0013E368
		private void Awake()
		{
			this.fps = 0f;
			this.time_scale = 1f;
			this.deltaTime = 0f;
			this.frame_cnt = 0U;
			this.time_cnt = 0f;
			this.mode_mem = false;
			this.memTime = 0f;
		}

		// Token: 0x06003635 RID: 13877 RVA: 0x0013FFBC File Offset: 0x0013E3BC
		private void Start()
		{
			this.style = new GUIStyle();
			this.style.fontSize = 20;
			this.styleState = new GUIStyleState();
			this.styleState.textColor = new Color(1f, 1f, 1f);
			this.style.normal = this.styleState;
			base.enabled = false;
		}

		// Token: 0x06003636 RID: 13878 RVA: 0x00140024 File Offset: 0x0013E424
		private void Update()
		{
			if (!Input.GetKey(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.Delete))
			{
			}
			this.deltaTime = Time.deltaTime * this.time_scale;
			if (this.mode_mem)
			{
				this.memTime += Time.deltaTime;
			}
			this.time_cnt += Time.deltaTime;
			this.frame_cnt += 1U;
			if (1f <= this.time_cnt)
			{
				this.fps = this.frame_cnt / this.time_cnt;
				this.frame_cnt = 0U;
				this.time_cnt = 0f;
			}
		}

		// Token: 0x06003637 RID: 13879 RVA: 0x001400D4 File Offset: 0x0013E4D4
		private void OnGUI()
		{
			GUILayout.BeginVertical("box", Array.Empty<GUILayoutOption>());
			GUILayout.Label("FPS:" + this.fps.ToString("000.0"), this.style, Array.Empty<GUILayoutOption>());
			GUILayout.EndVertical();
		}

		// Token: 0x06003638 RID: 13880 RVA: 0x00140124 File Offset: 0x0013E524
		public void SetTimeScale(float value)
		{
			this.time_scale = value;
		}

		// Token: 0x06003639 RID: 13881 RVA: 0x0014012D File Offset: 0x0013E52D
		public float GetTimeScale()
		{
			return this.time_scale;
		}

		// Token: 0x0600363A RID: 13882 RVA: 0x00140135 File Offset: 0x0013E535
		public float GetFps()
		{
			return this.fps;
		}

		// Token: 0x0600363B RID: 13883 RVA: 0x0014013D File Offset: 0x0013E53D
		public float GetTime()
		{
			return this.deltaTime;
		}

		// Token: 0x0600363C RID: 13884 RVA: 0x00140145 File Offset: 0x0013E545
		public void ChangeMemoryFlags(bool flags)
		{
			this.mode_mem = flags;
		}

		// Token: 0x0600363D RID: 13885 RVA: 0x0014014E File Offset: 0x0013E54E
		public float GetMemoryTime()
		{
			return this.memTime;
		}

		// Token: 0x04003685 RID: 13957
		private float fps;

		// Token: 0x04003686 RID: 13958
		[Range(0f, 50f)]
		public float time_scale = 1f;

		// Token: 0x04003687 RID: 13959
		private float deltaTime;

		// Token: 0x04003688 RID: 13960
		private uint frame_cnt;

		// Token: 0x04003689 RID: 13961
		private float time_cnt;

		// Token: 0x0400368A RID: 13962
		public bool mode_mem;

		// Token: 0x0400368B RID: 13963
		private float memTime;

		// Token: 0x0400368C RID: 13964
		private GUIStyle style;

		// Token: 0x0400368D RID: 13965
		private GUIStyleState styleState;

		// Token: 0x0400368E RID: 13966
		public bool ForceDrawFPS;
	}
}
