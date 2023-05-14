using Microsoft.AspNetCore.Hosting;

namespace PetShop.data.Services
{
    public class Storage : IStorageService
    {
        readonly IHostingEnvironment _hostingEnvironment;
        public Storage(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public string AbsolutePath => _hostingEnvironment.WebRootPath;
    }
}
