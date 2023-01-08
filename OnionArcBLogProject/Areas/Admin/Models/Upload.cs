using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace OnionArcBLogProject.WebUI.Areas.Admin.Models
{
    public class Upload
    {
        public static string ImageUpload(List<IFormFile> files, IHostingEnvironment _environment, out bool res) //IHostingEnvironment => sunucu üzerinde depolama
        {
            //resim yükleme işlemlerimizi gerç. geriye resim yolunu veya hata mesajını dönd. ayrıca dön. string. başarılı bilgisi vaya hata olduğunu anlamak için dıraşıya result fılatılacak.
            res = false;
            var uploads = Path.Combine(_environment.WebRootPath, "Uploads");
            foreach (var file in files)
            {
                if (file.ContentType.Contains("image"))
                {
                    if (file.Length <= 2097152)
                    {
                        string uniqueName = $"{Guid.NewGuid().ToString().Replace("-", "_").ToLower()}.{file.ContentType.Split('/')[1]}";
                        var filePath = Path.Combine(uploads, uniqueName);
                        using(var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                            res = true;
                            string newFilePath = filePath.Substring(filePath.IndexOf("Uploads\\"));
                            return newFilePath.Replace("\\", "/");
                        }
                    }
                    else
                    {
                        return($"Resim boyutu 2 Mb' tan büyük olamaz");
                    }
                }
                else
                {
                    return $"Lütfen sadece resim doyası yükleyiniz";
                }
            }
            return "Dosya bulunamadı lütfen en az bir resim seçiniz";
        }
    }
}
