using System;
using AIChara;
using Manager;

namespace Studio
{
	// Token: 0x02001215 RID: 4629
	public class OCICharFemale : OCIChar
	{
		// Token: 0x17002039 RID: 8249
		// (get) Token: 0x060097FB RID: 38907 RVA: 0x003EBA5D File Offset: 0x003E9E5D
		public ChaControl female
		{
			get
			{
				return this.charInfo;
			}
		}

		// Token: 0x060097FC RID: 38908 RVA: 0x003EBA65 File Offset: 0x003E9E65
		public override void OnDelete()
		{
			base.OnDelete();
			Singleton<Character>.Instance.DeleteChara(this.female, false);
		}

		// Token: 0x060097FD RID: 38909 RVA: 0x003EBA7F File Offset: 0x003E9E7F
		public override void SetSiruFlags(ChaFileDefine.SiruParts _parts, byte _state)
		{
			base.SetSiruFlags(_parts, _state);
			this.female.SetSiruFlag(_parts, _state);
		}

		// Token: 0x060097FE RID: 38910 RVA: 0x003EBA96 File Offset: 0x003E9E96
		public override byte GetSiruFlags(ChaFileDefine.SiruParts _parts)
		{
			return this.female.GetSiruFlag(_parts);
		}

		// Token: 0x060097FF RID: 38911 RVA: 0x003EBAA4 File Offset: 0x003E9EA4
		public override void SetNipStand(float _value)
		{
			base.SetNipStand(_value);
			base.oiCharInfo.nipple = _value;
			this.female.ChangeNipRate(_value);
		}

		// Token: 0x06009800 RID: 38912 RVA: 0x003EBAC8 File Offset: 0x003E9EC8
		public override void ChangeChara(string _path)
		{
			base.ChangeChara(_path);
			this.female.UpdateBustSoftnessAndGravity();
			this.optionItemCtrl.ReCounterScale();
			this.optionItemCtrl.height = base.animeOptionParam1;
			base.animeOptionParam1 = base.animeOptionParam1;
			base.animeOptionParam2 = base.animeOptionParam2;
		}

		// Token: 0x06009801 RID: 38913 RVA: 0x003EBB1B File Offset: 0x003E9F1B
		public override void SetClothesStateAll(int _state)
		{
			base.SetClothesStateAll(_state);
			this.female.SetClothesStateAll((byte)_state);
		}

		// Token: 0x06009802 RID: 38914 RVA: 0x003EBB34 File Offset: 0x003E9F34
		public override void LoadClothesFile(string _path)
		{
			base.LoadClothesFile(_path);
			this.female.UpdateBustSoftnessAndGravity();
			bool active = base.oiCharInfo.activeFK[6];
			base.ActiveFK(OIBoneInfo.BoneGroup.Skirt, false, base.oiCharInfo.enableFK);
			this.fkCtrl.ResetUsedBone(this);
			this.skirtDynamic = AddObjectFemale.GetSkirtDynamic(this.charInfo.objClothes);
			base.ActiveFK(OIBoneInfo.BoneGroup.Skirt, active, base.oiCharInfo.enableFK);
		}
	}
}
