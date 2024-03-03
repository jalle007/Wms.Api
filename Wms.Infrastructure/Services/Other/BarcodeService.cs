using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Wms.Infrastructure.Services.Other
{
    public interface IBarcodeService
    {
        Task<string> CreateBarcode(string text);
    }

    public class BarcodeService : IBarcodeService
    {
        private readonly string _ipAddress = "10.3.14.42"; // find right IP
        private readonly int _port = 9100;

        public BarcodeService()
        {
            
        }

        public async Task<string> CreateBarcode(string text)
        {
            var zplString = $"^XA^FO50,50^A0N50,50^FD{text}^FS^XZ";
            string response = null;

            try
            {
                using var client = new TcpClient(_ipAddress, _port);
                using var writer = new StreamWriter(client.GetStream());
                using var reader = new StreamReader(client.GetStream());

                await writer.WriteAsync(zplString);
                await writer.FlushAsync();

                // Read the response from the printer if available
                response = await reader.ReadToEndAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return response;
        }
    }

}
