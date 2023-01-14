<Query Kind="Program">
  <NuGetReference>SixLabors.ImageSharp</NuGetReference>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>SixLabors.ImageSharp</Namespace>
  <Namespace>SixLabors.ImageSharp.Processing</Namespace>
  <Namespace>SixLabors.ImageSharp.Formats.Png</Namespace>
</Query>

void Main()
{
	var path = @"C:\Users\memoy\Desktop\临时图片\";
	MergeContactWayQrCodeHandler($"{path}qrcode.png", $"{path}logo.jpg");
}


public void MergeContactWayQrCodeHandler(string qrcodePath, string logoPath)
{
	Image qrCodeImage = Image.Load(qrcodePath);
	Image logoImage = Image.Load(logoPath);

	// 缩放图标
	logoImage.Mutate(x =>
	{
		//直接处理image对象
		x.Resize(90, 90);
	});

	// 合并图标
	qrCodeImage.Mutate(o =>
	{
		o.DrawImage(logoImage, new Point((qrCodeImage.Width / 2) - (logoImage.Width / 2), (qrCodeImage.Width / 2) - (logoImage.Width / 2)), 1);
	});

	using var mergeImageStream = new MemoryStream();
	qrCodeImage.Save(@"C:\aliyun-test\merge.png");
}
