using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pustok.Business.Services.Abstractions
{
    public interface ICloudinaryService
    {
        Task<string> FileUploadAsync(IFormFile image);
        Task<bool> FileDeleteAsync(string publicId);
    }
}
