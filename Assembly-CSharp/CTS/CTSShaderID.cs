using System;
using UnityEngine;

namespace CTS
{
	// Token: 0x02000697 RID: 1687
	public static class CTSShaderID
	{
		// Token: 0x060027EF RID: 10223 RVA: 0x000ECE60 File Offset: 0x000EB260
		static CTSShaderID()
		{
			for (int i = 1; i <= 16; i++)
			{
				CTSShaderID.Texture_X_Albedo_Index[i - 1] = Shader.PropertyToID(string.Format("_Texture_{0}_Albedo_Index", i));
				CTSShaderID.Texture_X_Normal_Index[i - 1] = Shader.PropertyToID(string.Format("_Texture_{0}_Normal_Index", i));
				CTSShaderID.Texture_X_H_AO_Index[i - 1] = Shader.PropertyToID(string.Format("_Texture_{0}_H_AO_Index", i));
				CTSShaderID.Texture_X_Tiling[i - 1] = Shader.PropertyToID(string.Format("_Texture_{0}_Tiling", i));
				CTSShaderID.Texture_X_Far_Multiplier[i - 1] = Shader.PropertyToID(string.Format("_Texture_{0}_Far_Multiplier", i));
				CTSShaderID.Texture_X_Perlin_Power[i - 1] = Shader.PropertyToID(string.Format("_Texture_{0}_Perlin_Power", i));
				CTSShaderID.Texture_X_Snow_Reduction[i - 1] = Shader.PropertyToID(string.Format("_Texture_{0}_Snow_Reduction", i));
				CTSShaderID.Texture_X_Geological_Power[i - 1] = Shader.PropertyToID(string.Format("_Texture_{0}_Geological_Power", i));
				CTSShaderID.Texture_X_Heightmap_Depth[i - 1] = Shader.PropertyToID(string.Format("_Texture_{0}_Heightmap_Depth", i));
				CTSShaderID.Texture_X_Height_Contrast[i - 1] = Shader.PropertyToID(string.Format("_Texture_{0}_Height_Contrast", i));
				CTSShaderID.Texture_X_Heightblend_Close[i - 1] = Shader.PropertyToID(string.Format("_Texture_{0}_Heightblend_Close", i));
				CTSShaderID.Texture_X_Heightblend_Far[i - 1] = Shader.PropertyToID(string.Format("_Texture_{0}_Heightblend_Far", i));
				CTSShaderID.Texture_X_Tesselation_Depth[i - 1] = Shader.PropertyToID(string.Format("_Texture_{0}_Tesselation_Depth", i));
				CTSShaderID.Texture_X_Heightmap_MinHeight[i - 1] = Shader.PropertyToID(string.Format("_Texture_{0}_Heightmap_MinHeight", i));
				CTSShaderID.Texture_X_Heightmap_MaxHeight[i - 1] = Shader.PropertyToID(string.Format("_Texture_{0}_Heightmap_MaxHeight", i));
				CTSShaderID.Texture_X_AO_Power[i - 1] = Shader.PropertyToID(string.Format("_Texture_{0}_AO_Power", i));
				CTSShaderID.Texture_X_Normal_Power[i - 1] = Shader.PropertyToID(string.Format("_Texture_{0}_Normal_Power", i));
				CTSShaderID.Texture_X_Triplanar[i - 1] = Shader.PropertyToID(string.Format("_Texture_{0}_Triplanar", i));
				CTSShaderID.Texture_X_Average[i - 1] = Shader.PropertyToID(string.Format("_Texture_{0}_Average", i));
				CTSShaderID.Texture_X_Color[i - 1] = Shader.PropertyToID(string.Format("_Texture_{0}_Color", i));
			}
		}

		// Token: 0x0400289C RID: 10396
		public static readonly int Texture_Array_Albedo = Shader.PropertyToID("_Texture_Array_Albedo");

		// Token: 0x0400289D RID: 10397
		public static readonly int Texture_Array_Normal = Shader.PropertyToID("_Texture_Array_Normal");

		// Token: 0x0400289E RID: 10398
		public static readonly int Texture_Splat_1 = Shader.PropertyToID("_Texture_Splat_1");

		// Token: 0x0400289F RID: 10399
		public static readonly int Texture_Splat_2 = Shader.PropertyToID("_Texture_Splat_2");

		// Token: 0x040028A0 RID: 10400
		public static readonly int Texture_Splat_3 = Shader.PropertyToID("_Texture_Splat_3");

		// Token: 0x040028A1 RID: 10401
		public static readonly int Texture_Splat_4 = Shader.PropertyToID("_Texture_Splat_4");

		// Token: 0x040028A2 RID: 10402
		public static readonly int UV_Mix_Power = Shader.PropertyToID("_UV_Mix_Power");

		// Token: 0x040028A3 RID: 10403
		public static readonly int UV_Mix_Start_Distance = Shader.PropertyToID("_UV_Mix_Start_Distance");

		// Token: 0x040028A4 RID: 10404
		public static readonly int Perlin_Normal_Tiling_Close = Shader.PropertyToID("_Perlin_Normal_Tiling_Close");

		// Token: 0x040028A5 RID: 10405
		public static readonly int Perlin_Normal_Tiling_Far = Shader.PropertyToID("_Perlin_Normal_Tiling_Far");

		// Token: 0x040028A6 RID: 10406
		public static readonly int Perlin_Normal_Power = Shader.PropertyToID("_Perlin_Normal_Power");

		// Token: 0x040028A7 RID: 10407
		public static readonly int Perlin_Normal_Power_Close = Shader.PropertyToID("_Perlin_Normal_Power_Close");

		// Token: 0x040028A8 RID: 10408
		public static readonly int Terrain_Smoothness = Shader.PropertyToID("_Terrain_Smoothness");

		// Token: 0x040028A9 RID: 10409
		public static readonly int Terrain_Specular = Shader.PropertyToID("_Terrain_Specular");

		// Token: 0x040028AA RID: 10410
		public static readonly int TessValue = Shader.PropertyToID("_TessValue");

		// Token: 0x040028AB RID: 10411
		public static readonly int TessMin = Shader.PropertyToID("_TessMin");

		// Token: 0x040028AC RID: 10412
		public static readonly int TessMax = Shader.PropertyToID("_TessMax");

		// Token: 0x040028AD RID: 10413
		public static readonly int TessPhongStrength = Shader.PropertyToID("_TessPhongStrength");

		// Token: 0x040028AE RID: 10414
		public static readonly int TessDistance = Shader.PropertyToID("_TessDistance");

		// Token: 0x040028AF RID: 10415
		public static readonly int Ambient_Occlusion_Type = Shader.PropertyToID("_Ambient_Occlusion_Type");

		// Token: 0x040028B0 RID: 10416
		public static readonly int Remove_Vert_Height = Shader.PropertyToID("_Remove_Vert_Height");

		// Token: 0x040028B1 RID: 10417
		public static readonly int Texture_Additional_Masks = Shader.PropertyToID("_Texture_Additional_Masks");

		// Token: 0x040028B2 RID: 10418
		public static readonly int Use_AO = Shader.PropertyToID("_Use_AO");

		// Token: 0x040028B3 RID: 10419
		public static readonly int Use_AO_Texture = Shader.PropertyToID("_Use_AO_Texture");

		// Token: 0x040028B4 RID: 10420
		public static readonly int Ambient_Occlusion_Power = Shader.PropertyToID("_Ambient_Occlusion_Power");

		// Token: 0x040028B5 RID: 10421
		public static readonly int Texture_Perlin_Normal_Index = Shader.PropertyToID("_Texture_Perlin_Normal_Index");

		// Token: 0x040028B6 RID: 10422
		public static readonly int Global_Normalmap_Power = Shader.PropertyToID("_Global_Normalmap_Power");

		// Token: 0x040028B7 RID: 10423
		public static readonly int Global_Normal_Map = Shader.PropertyToID("_Global_Normal_Map");

		// Token: 0x040028B8 RID: 10424
		public static readonly int Global_Color_Map_Far_Power = Shader.PropertyToID("_Global_Color_Map_Far_Power");

		// Token: 0x040028B9 RID: 10425
		public static readonly int Global_Color_Map_Close_Power = Shader.PropertyToID("_Global_Color_Map_Close_Power");

		// Token: 0x040028BA RID: 10426
		public static readonly int Global_Color_Opacity_Power = Shader.PropertyToID("_Global_Color_Opacity_Power");

		// Token: 0x040028BB RID: 10427
		public static readonly int Global_Color_Map = Shader.PropertyToID("_Global_Color_Map");

		// Token: 0x040028BC RID: 10428
		public static readonly int Geological_Map_Offset_Close = Shader.PropertyToID("_Geological_Map_Offset_Close");

		// Token: 0x040028BD RID: 10429
		public static readonly int Geological_Map_Close_Power = Shader.PropertyToID("_Geological_Map_Close_Power");

		// Token: 0x040028BE RID: 10430
		public static readonly int Geological_Tiling_Close = Shader.PropertyToID("_Geological_Tiling_Close");

		// Token: 0x040028BF RID: 10431
		public static readonly int Geological_Map_Offset_Far = Shader.PropertyToID("_Geological_Map_Offset_Far");

		// Token: 0x040028C0 RID: 10432
		public static readonly int Geological_Map_Far_Power = Shader.PropertyToID("_Geological_Map_Far_Power");

		// Token: 0x040028C1 RID: 10433
		public static readonly int Geological_Tiling_Far = Shader.PropertyToID("_Geological_Tiling_Far");

		// Token: 0x040028C2 RID: 10434
		public static readonly int Texture_Geological_Map = Shader.PropertyToID("_Texture_Geological_Map");

		// Token: 0x040028C3 RID: 10435
		public static readonly int Texture_Snow_Index = Shader.PropertyToID("_Texture_Snow_Index");

		// Token: 0x040028C4 RID: 10436
		public static readonly int Texture_Snow_Normal_Index = Shader.PropertyToID("_Texture_Snow_Normal_Index");

		// Token: 0x040028C5 RID: 10437
		public static readonly int Texture_Snow_H_AO_Index = Shader.PropertyToID("_Texture_Snow_H_AO_Index");

		// Token: 0x040028C6 RID: 10438
		public static readonly int Snow_Amount = Shader.PropertyToID("_Snow_Amount");

		// Token: 0x040028C7 RID: 10439
		public static readonly int Snow_Maximum_Angle = Shader.PropertyToID("_Snow_Maximum_Angle");

		// Token: 0x040028C8 RID: 10440
		public static readonly int Snow_Maximum_Angle_Hardness = Shader.PropertyToID("_Snow_Maximum_Angle_Hardness");

		// Token: 0x040028C9 RID: 10441
		public static readonly int Snow_Min_Height = Shader.PropertyToID("_Snow_Min_Height");

		// Token: 0x040028CA RID: 10442
		public static readonly int Snow_Min_Height_Blending = Shader.PropertyToID("_Snow_Min_Height_Blending");

		// Token: 0x040028CB RID: 10443
		public static readonly int Snow_Noise_Power = Shader.PropertyToID("_Snow_Noise_Power");

		// Token: 0x040028CC RID: 10444
		public static readonly int Snow_Noise_Tiling = Shader.PropertyToID("_Snow_Noise_Tiling");

		// Token: 0x040028CD RID: 10445
		public static readonly int Snow_Normal_Scale = Shader.PropertyToID("_Snow_Normal_Scale");

		// Token: 0x040028CE RID: 10446
		public static readonly int Snow_Perlin_Power = Shader.PropertyToID("_Snow_Perlin_Power");

		// Token: 0x040028CF RID: 10447
		public static readonly int Snow_Tiling = Shader.PropertyToID("_Snow_Tiling");

		// Token: 0x040028D0 RID: 10448
		public static readonly int Snow_Tiling_Far_Multiplier = Shader.PropertyToID("_Snow_Tiling_Far_Multiplier");

		// Token: 0x040028D1 RID: 10449
		public static readonly int Snow_Brightness = Shader.PropertyToID("_Snow_Brightness");

		// Token: 0x040028D2 RID: 10450
		public static readonly int Snow_Blend_Normal = Shader.PropertyToID("_Snow_Blend_Normal");

		// Token: 0x040028D3 RID: 10451
		public static readonly int Snow_Smoothness = Shader.PropertyToID("_Snow_Smoothness");

		// Token: 0x040028D4 RID: 10452
		public static readonly int Snow_Specular = Shader.PropertyToID("_Snow_Specular");

		// Token: 0x040028D5 RID: 10453
		public static readonly int Snow_Heightblend_Close = Shader.PropertyToID("_Snow_Heightblend_Close");

		// Token: 0x040028D6 RID: 10454
		public static readonly int Snow_Heightblend_Far = Shader.PropertyToID("_Snow_Heightblend_Far");

		// Token: 0x040028D7 RID: 10455
		public static readonly int Snow_Height_Contrast = Shader.PropertyToID("_Snow_Height_Contrast");

		// Token: 0x040028D8 RID: 10456
		public static readonly int Snow_Heightmap_Depth = Shader.PropertyToID("_Snow_Heightmap_Depth");

		// Token: 0x040028D9 RID: 10457
		public static readonly int Snow_Heightmap_MinHeight = Shader.PropertyToID("_Snow_Heightmap_MinHeight");

		// Token: 0x040028DA RID: 10458
		public static readonly int Snow_Heightmap_MaxHeight = Shader.PropertyToID("_Snow_Heightmap_MaxHeight");

		// Token: 0x040028DB RID: 10459
		public static readonly int Snow_Ambient_Occlusion_Power = Shader.PropertyToID("_Snow_Ambient_Occlusion_Power");

		// Token: 0x040028DC RID: 10460
		public static readonly int Snow_Tesselation_Depth = Shader.PropertyToID("_Snow_Tesselation_Depth");

		// Token: 0x040028DD RID: 10461
		public static readonly int Snow_Color = Shader.PropertyToID("_Snow_Color");

		// Token: 0x040028DE RID: 10462
		public static readonly int Texture_Snow_Average = Shader.PropertyToID("_Texture_Snow_Average");

		// Token: 0x040028DF RID: 10463
		public static readonly int Texture_Glitter = Shader.PropertyToID("_Texture_Glitter");

		// Token: 0x040028E0 RID: 10464
		public static readonly int Glitter_Color_Power = Shader.PropertyToID("_Gliter_Color_Power");

		// Token: 0x040028E1 RID: 10465
		public static readonly int Glitter_Noise_Threshold = Shader.PropertyToID("_Glitter_Noise_Treshold");

		// Token: 0x040028E2 RID: 10466
		public static readonly int Glitter_Specular = Shader.PropertyToID("_Glitter_Specular");

		// Token: 0x040028E3 RID: 10467
		public static readonly int Glitter_Smoothness = Shader.PropertyToID("_Glitter_Smoothness");

		// Token: 0x040028E4 RID: 10468
		public static readonly int Glitter_Refreshing_Speed = Shader.PropertyToID("_Glitter_Refreshing_Speed");

		// Token: 0x040028E5 RID: 10469
		public static readonly int Glitter_Tiling = Shader.PropertyToID("_Glitter_Tiling");

		// Token: 0x040028E6 RID: 10470
		public static readonly int[] Texture_X_Albedo_Index = new int[16];

		// Token: 0x040028E7 RID: 10471
		public static readonly int[] Texture_X_Normal_Index = new int[16];

		// Token: 0x040028E8 RID: 10472
		public static readonly int[] Texture_X_H_AO_Index = new int[16];

		// Token: 0x040028E9 RID: 10473
		public static readonly int[] Texture_X_Tiling = new int[16];

		// Token: 0x040028EA RID: 10474
		public static readonly int[] Texture_X_Far_Multiplier = new int[16];

		// Token: 0x040028EB RID: 10475
		public static readonly int[] Texture_X_Perlin_Power = new int[16];

		// Token: 0x040028EC RID: 10476
		public static readonly int[] Texture_X_Snow_Reduction = new int[16];

		// Token: 0x040028ED RID: 10477
		public static readonly int[] Texture_X_Geological_Power = new int[16];

		// Token: 0x040028EE RID: 10478
		public static readonly int[] Texture_X_Heightmap_Depth = new int[16];

		// Token: 0x040028EF RID: 10479
		public static readonly int[] Texture_X_Height_Contrast = new int[16];

		// Token: 0x040028F0 RID: 10480
		public static readonly int[] Texture_X_Heightblend_Close = new int[16];

		// Token: 0x040028F1 RID: 10481
		public static readonly int[] Texture_X_Heightblend_Far = new int[16];

		// Token: 0x040028F2 RID: 10482
		public static readonly int[] Texture_X_Tesselation_Depth = new int[16];

		// Token: 0x040028F3 RID: 10483
		public static readonly int[] Texture_X_Heightmap_MinHeight = new int[16];

		// Token: 0x040028F4 RID: 10484
		public static readonly int[] Texture_X_Heightmap_MaxHeight = new int[16];

		// Token: 0x040028F5 RID: 10485
		public static readonly int[] Texture_X_AO_Power = new int[16];

		// Token: 0x040028F6 RID: 10486
		public static readonly int[] Texture_X_Normal_Power = new int[16];

		// Token: 0x040028F7 RID: 10487
		public static readonly int[] Texture_X_Triplanar = new int[16];

		// Token: 0x040028F8 RID: 10488
		public static readonly int[] Texture_X_Average = new int[16];

		// Token: 0x040028F9 RID: 10489
		public static readonly int[] Texture_X_Color = new int[16];
	}
}
