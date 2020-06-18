using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Alexa.NET.Request
{
    public class CachedCertificateHandler : ICertificateHandler
    {
        public ConcurrentDictionary<Uri, X509Certificate2> _cache = new ConcurrentDictionary<Uri, X509Certificate2>();
        public async Task<X509Certificate2> GetCertificate(Uri uri)
        {
            if(_cache.TryGetValue(uri, out var cert))
            {
                return cert;
            }
            cert = await RequestVerification.GetCertificate(uri);
            return _cache.AddOrUpdate(uri, cert, (u,c) => cert);
        }

        public Task OnCertificateValidationFailed(X509Certificate2 certificate)
        {
            var pair = _cache.FirstOrDefault(kvp => kvp.Value == certificate);
            if (pair.Key != default)
            {
                _cache.TryRemove(pair.Key, out _);
            }

            return Task.CompletedTask;
        }
    }
}