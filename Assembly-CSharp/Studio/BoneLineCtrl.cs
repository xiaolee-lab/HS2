using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Studio
{
	// Token: 0x020011D0 RID: 4560
	public class BoneLineCtrl : MonoBehaviour
	{
		// Token: 0x060095A8 RID: 38312 RVA: 0x003DD540 File Offset: 0x003DB940
		private void Draw(OCIChar _oCIChar)
		{
			if (!_oCIChar.charInfo.visibleAll)
			{
				return;
			}
			if (!_oCIChar.oiCharInfo.enableFK)
			{
				return;
			}
			List<OCIChar.BoneInfo> listBones = _oCIChar.listBones;
			if (_oCIChar.oiCharInfo.activeFK[0])
			{
				this.DrawLine(listBones, 100, 102, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 100, 104, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 106, 108, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 114, 116, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 200, 201, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 201, 290, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 290, 291, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 202, 204, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 204, 206, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 208, 210, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 210, 212, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 214, 216, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 216, 218, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 292, 293, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 293, 294, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 294, 295, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 296, 297, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 297, 298, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 298, 299, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 220, 221, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 221, 222, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 222, 223, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 223, 224, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 225, 227, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 227, 229, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 229, 231, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 231, 233, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 233, 235, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 237, 239, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 239, 241, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 243, 245, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 245, 247, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 249, 251, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 251, 253, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 255, 256, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 256, 257, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 257, 258, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 258, 259, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 260, 262, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 262, 264, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 264, 266, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 266, 268, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 268, 270, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 272, 274, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 274, 276, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 278, 280, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 280, 282, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 284, 286, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 286, 288, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 350, 352, Studio.optionSystem.colorFKHair);
				this.DrawLine(listBones, 354, 356, Studio.optionSystem.colorFKHair);
			}
			if (_oCIChar.oiCharInfo.activeFK[1])
			{
				this.Draw(listBones, 1, 1, Studio.optionSystem.colorFKNeck);
			}
			if (_oCIChar.oiCharInfo.activeFK[2])
			{
				this.Draw(listBones, 53, 4, Studio.optionSystem.colorFKBreast);
				this.Draw(listBones, 59, 4, Studio.optionSystem.colorFKBreast);
			}
			if (_oCIChar.oiCharInfo.activeFK[3])
			{
				this.Draw(listBones, 3, 2, Studio.optionSystem.colorFKBody);
				this.DrawLine(listBones, 5, 6, Studio.optionSystem.colorFKBody);
				this.Draw(listBones, 6, 3, Studio.optionSystem.colorFKBody);
				this.DrawLine(listBones, 5, 10, Studio.optionSystem.colorFKBody);
				this.Draw(listBones, 10, 3, Studio.optionSystem.colorFKBody);
				this.DrawLine(listBones, 3, 14, Studio.optionSystem.colorFKBody);
				this.Draw(listBones, 14, 3, Studio.optionSystem.colorFKBody);
				this.DrawLine(listBones, 3, 18, Studio.optionSystem.colorFKBody);
				this.Draw(listBones, 18, 3, Studio.optionSystem.colorFKBody);
				this.DrawLine(listBones, 65, 66, Studio.optionSystem.colorFKBody);
			}
			if (_oCIChar.oiCharInfo.activeFK[4])
			{
				this.Draw(listBones, 22, 2, Studio.optionSystem.colorFKRightHand);
				this.Draw(listBones, 25, 2, Studio.optionSystem.colorFKRightHand);
				this.Draw(listBones, 28, 2, Studio.optionSystem.colorFKRightHand);
				this.Draw(listBones, 31, 2, Studio.optionSystem.colorFKRightHand);
				this.Draw(listBones, 34, 2, Studio.optionSystem.colorFKRightHand);
			}
			if (_oCIChar.oiCharInfo.activeFK[5])
			{
				this.Draw(listBones, 37, 2, Studio.optionSystem.colorFKLeftHand);
				this.Draw(listBones, 40, 2, Studio.optionSystem.colorFKLeftHand);
				this.Draw(listBones, 43, 2, Studio.optionSystem.colorFKLeftHand);
				this.Draw(listBones, 46, 2, Studio.optionSystem.colorFKLeftHand);
				this.Draw(listBones, 49, 2, Studio.optionSystem.colorFKLeftHand);
			}
			if (_oCIChar.oiCharInfo.activeFK[6])
			{
				int num = 400;
				this.Draw(listBones, num, 5, Studio.optionSystem.colorFKSkirt);
				num += 6;
				this.Draw(listBones, num, 5, Studio.optionSystem.colorFKSkirt);
				num += 6;
				this.Draw(listBones, num, 5, Studio.optionSystem.colorFKSkirt);
				num += 6;
				this.Draw(listBones, num, 5, Studio.optionSystem.colorFKSkirt);
				num += 6;
				this.Draw(listBones, num, 5, Studio.optionSystem.colorFKSkirt);
				num += 6;
				this.Draw(listBones, num, 5, Studio.optionSystem.colorFKSkirt);
				num += 6;
				this.Draw(listBones, num, 5, Studio.optionSystem.colorFKSkirt);
				num += 6;
				this.Draw(listBones, num, 5, Studio.optionSystem.colorFKSkirt);
			}
		}

		// Token: 0x060095A9 RID: 38313 RVA: 0x003DDDC0 File Offset: 0x003DC1C0
		private void Draw(List<OCIChar.BoneInfo> _bones, int _start, int _num, Color _color)
		{
			for (int i = 0; i < _num; i++)
			{
				this.DrawLine(_bones, _start + i, _start + i + 1, _color);
			}
		}

		// Token: 0x060095AA RID: 38314 RVA: 0x003DDDF0 File Offset: 0x003DC1F0
		private void DrawLine(List<OCIChar.BoneInfo> _bones, int _start, int _end, Color _color)
		{
			OCIChar.BoneInfo boneInfo = _bones.Find((OCIChar.BoneInfo v) => v.boneID == _start);
			if (boneInfo == null || !boneInfo.boneWeight)
			{
				return;
			}
			OCIChar.BoneInfo boneInfo2 = _bones.Find((OCIChar.BoneInfo v) => v.boneID == _end);
			if (boneInfo2 == null || !boneInfo2.boneWeight)
			{
				return;
			}
			this.DrawLine(boneInfo.posision, boneInfo2.posision, _color);
		}

		// Token: 0x060095AB RID: 38315 RVA: 0x003DDE6F File Offset: 0x003DC26F
		private void DrawLine(Vector3 _s, Vector3 _e, Color _color)
		{
			GL.Color(_color);
			GL.Vertex(_s);
			GL.Vertex(_e);
		}

		// Token: 0x060095AC RID: 38316 RVA: 0x003DDE84 File Offset: 0x003DC284
		private void OnPostRender()
		{
			if (Studio.optionSystem == null)
			{
				return;
			}
			if (!Studio.optionSystem.lineFK)
			{
				return;
			}
			IEnumerable<OCIChar> enumerable = from v in Studio.GetSelectObjectCtrl()
			where v.kind == 0
			select v as OCIChar;
			if (enumerable == null || enumerable.Count<OCIChar>() == 0)
			{
				return;
			}
			this.material.SetPass(0);
			GL.PushMatrix();
			GL.Begin(1);
			foreach (OCIChar oCIChar in enumerable)
			{
				this.Draw(oCIChar);
			}
			GL.End();
			GL.PopMatrix();
		}

		// Token: 0x04007884 RID: 30852
		[SerializeField]
		private Material material;
	}
}
