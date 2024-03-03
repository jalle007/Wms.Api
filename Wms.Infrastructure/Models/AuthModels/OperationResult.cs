using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wms.Infrastructure.Models.AuthModels
{
    public record OperationResult
    {
        public bool Success { get; set; } = true;
        public string Description { get; set; } = default!;
        public string Message { get; set; } = default!;
        public string AccessToken { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
    }
}
