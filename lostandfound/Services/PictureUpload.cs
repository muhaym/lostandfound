using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace lostandfound
{
	public class PictureUpload
	{
		async public static Task<RootObject> lostPost(Stream filestream)
		{
			try
			{
				if (filestream != null)
				{
					StreamContent stream = new StreamContent(filestream);
					MultipartFormDataContent content = new MultipartFormDataContent();
					stream.Headers.Add("Content-Type", "image/jpeg");
					content.Add(stream, "imageFile", new Guid().ToString() + ".jpg");
					var result = await CommonService.HttpPostOperation("foundPost", content);
					return result;
				}
				return new RootObject() { status = 666, message = "File Stream can't be empty" };
			}
			catch (Exception e)
			{
				return new RootObject() { status = 666, message = e.ToString() };
			}
		}

		async public static Task<RootObject> foundPost(Stream filestream)
		{
			try
			{
				if (filestream != null)
				{
					StreamContent stream = new StreamContent(filestream);
					MultipartFormDataContent content = new MultipartFormDataContent();
					stream.Headers.Add("Content-Type", "image/jpeg");
					content.Add(stream, "imageFile", new Guid().ToString() + ".jpg");
					var result = await CommonService.HttpPostOperation("lostPost", content);
					return result;
				}
				return new RootObject() { status = 666, message = "File Stream can't be empty" };
			}
			catch (Exception e)
			{
				return new RootObject() { status = 666, message = e.ToString() };
			}
		}

		async public static Task<RootObject> claimItem(string id)
		{
			try
			{
				if (!string.IsNullOrWhiteSpace(id))
				{
					MultipartFormDataContent content = new MultipartFormDataContent();
					content.Add(new StringContent(id), "id");
					var result = await CommonService.HttpPostOperation("claimItem", content);
					return result;
				}
				return new RootObject() { status = 666, message = "ImageId or Score can't be empty" };
			}
			catch (Exception e)
			{
				return new RootObject() { status = 666, message = e.ToString() };
			}
		}
	}
}
