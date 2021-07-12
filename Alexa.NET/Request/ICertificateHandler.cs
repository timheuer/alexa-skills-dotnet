using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Alexa.NET.Request
{
    public interface ICertificateHandler
    {
        Task<X509Certificate2> GetCertificate(Uri uri);
        void OnCertificateValidationFailed(X509Certificate2 certificate);
    }
}