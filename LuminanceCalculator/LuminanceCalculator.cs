using System;
using UnityEngine;

public class LuminanceCalculator
{
    public float GetLuminanceValue(Texture texture)
    {
		Texture2D _texture = (Texture2D)texture;
		Color averageColor = avgColor(_texture);
		float lumaValue = getLum(averageColor.r, averageColor.g, averageColor.b);

		return lumaValue;
	}

	private Color avgColor(Texture2D tex)
	{
		Color[] cc = tex.GetPixels(0, 0, tex.width, tex.height, 0);

		float r = 0;
		float g = 0;
		float b = 0;

		int cnt = cc.Length;

		for (int i = 0; i < cc.Length; i++)
		{
			// Exclude White Color
			if (cc[i].r != 0 && cc[i].g != 0 && cc[i].b != 0)
			{
				r += cc[i].r;
				g += cc[i].g;
				b += cc[i].b;
			}
			else
			{
				cnt--;
			}
		}

		Color avgColor = new Color(r / cnt, g / cnt, b / cnt);

		return avgColor;
	}

	// Source : https://www.w3.org/WAI/GL/wiki/Relative_luminance
	private float getLum(float r, float g, float b, float alpha = 0f)
	{
		float _alpha = alpha;

		var RsRGB = ((r * 255 - (r * 255 * alpha)) / 255);
		var GsRGB = ((g * 255 - (g * 255 * alpha)) / 255);
		var BsRGB = ((b * 255 - (b * 255 * alpha)) / 255);

		var R = (RsRGB <= 0.03928) ? RsRGB / 12.92 : Math.Pow((RsRGB + 0.055) / 1.055, 2.4);
		var G = (GsRGB <= 0.03928) ? GsRGB / 12.92 : Math.Pow((GsRGB + 0.055) / 1.055, 2.4);
		var B = (BsRGB <= 0.03928) ? BsRGB / 12.92 : Math.Pow((BsRGB + 0.055) / 1.055, 2.4);

		// For the sRGB colorspace, the relative luminance of a color is defined as: 
		float L = (float)(0.2126f * R + 0.7152f * G + 0.0722f * B);

		return L;
	}
}
