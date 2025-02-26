using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace PlaceholderSoftware.WetStuff.Demos.Demo_Assets
{
	// Token: 0x020004CD RID: 1229
	[RequireComponent(typeof(WetStuff))]
	public class Wipe : MonoBehaviour
	{
		// Token: 0x060016B3 RID: 5811 RVA: 0x0008B9E4 File Offset: 0x00089DE4
		private void Start()
		{
			Shader shader = Shader.Find("Demo/ExcludeWetness");
			this._material = new Material(shader);
			this._mesh = Wipe.CreateFullscreenQuad();
			this._decals = base.GetComponent<WetStuff>();
			this._decals.AfterDecalRender += this.RecordCommandBuffer;
		}

		// Token: 0x060016B4 RID: 5812 RVA: 0x0008BA38 File Offset: 0x00089E38
		private static Mesh CreateFullscreenQuad()
		{
			Mesh mesh = new Mesh
			{
				vertices = new Vector3[]
				{
					new Vector3(-1f, -1f, 0f),
					new Vector3(-1f, 1f, 0f),
					new Vector3(1f, 1f, 0f),
					new Vector3(1f, -1f, 0f)
				},
				uv = new Vector2[]
				{
					new Vector2(0f, 1f),
					new Vector2(0f, 0f),
					new Vector2(1f, 0f),
					new Vector2(1f, 1f)
				}
			};
			mesh.SetIndices(new int[]
			{
				0,
				1,
				2,
				2,
				3,
				0
			}, MeshTopology.Triangles, 0);
			return mesh;
		}

		// Token: 0x060016B5 RID: 5813 RVA: 0x0008BB6B File Offset: 0x00089F6B
		private void OnDestroy()
		{
			if (this._decals != null)
			{
				this._decals.AfterDecalRender -= this.RecordCommandBuffer;
			}
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x0008BB98 File Offset: 0x00089F98
		private void RecordCommandBuffer(CommandBuffer cmd)
		{
			float x = Mathf.Lerp(-2f, 2f, this.Progress);
			cmd.SetRenderTarget(BuiltinRenderTextureType.None, BuiltinRenderTextureType.CameraTarget);
			cmd.DrawMesh(this._mesh, Matrix4x4.Translate(new Vector3(x, 0f, 0f)), this._material);
		}

		// Token: 0x060016B7 RID: 5815 RVA: 0x0008BBF4 File Offset: 0x00089FF4
		public void OnGUI()
		{
			float width = GUILayoutUtility.GetRect(float.MaxValue, 1f).width;
			Rect position = new Rect(170f, 20f, width - 190f, 30f);
			this.Progress = GUI.HorizontalSlider(position, this.Progress, 0f, 0.5f);
			Rect position2 = new Rect(20f, 15f, 160f, 30f);
			GUI.Label(position2, "Remove Wetness Effect:");
		}

		// Token: 0x04001978 RID: 6520
		private Mesh _mesh;

		// Token: 0x04001979 RID: 6521
		private Material _material;

		// Token: 0x0400197A RID: 6522
		private WetStuff _decals;

		// Token: 0x0400197B RID: 6523
		[Range(0f, 1f)]
		public float Progress;
	}
}
