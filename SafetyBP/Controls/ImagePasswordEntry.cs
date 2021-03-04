﻿using SafetyBP.Effects;

namespace SafetyBP.Controls
{
    public class ImagePasswordEntry : ImageEntry
	{
		public ImagePasswordEntry(): base()
		{
			IsPassword = true;
			this.Effects.Add(new ShowHidePassEffect());
		}

		/*public static readonly BindableProperty ImageShowPasswordProperty =
			BindableProperty.Create(nameof(ImageShowPassword), typeof(string), typeof(ImagePasswordEntry), string.Empty);

		public static readonly BindableProperty ImageHidePasswordProperty =
			BindableProperty.Create(nameof(ImageHidePassword), typeof(string), typeof(ImagePasswordEntry), string.Empty);

		public string ImageShowPassword
		{
			get { return (string)GetValue(ImageShowPasswordProperty); }
			set { SetValue(ImageShowPasswordProperty, value); }
		}

		public string ImageHidePassword
		{
			get { return (string)GetValue(ImageHidePasswordProperty); }
			set { SetValue(ImageHidePasswordProperty, value); }
		}*/

		
	}
}
