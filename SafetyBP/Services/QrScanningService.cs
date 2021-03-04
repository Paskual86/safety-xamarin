﻿using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing.Mobile;

[assembly: Dependency(typeof(SafetyBP.Services.QrScanningService))]
namespace SafetyBP.Services
{
    public class QrScanningService : IQrScanningService
    {
        public async Task<string> ScanAsync()
        {
            var optionsCustom = new MobileBarcodeScanningOptions();

            var scanner = new MobileBarcodeScanner()
            {
                TopText = "Scan the QR Code",
                BottomText = "Please Wait",
            };

            var scanResult = await scanner.Scan(optionsCustom);
            return scanResult.Text;
        }
    }
}
