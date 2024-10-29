

namespace Services.Dto
{
    //üyelik gerektirmeyen clientler(api) için
    /*
     * Client Credentials Flow'da uygulama kendi kimlik bilgilerini kullanarak
     * her zaman yeni bir access token alabileceği için refresh token'a ihtiyaç kalmaz.
     * Bu akış, arka plan servisleri veya API tüketen sunucular arasında yaygın olarak kullanılır.
     * (hava durumu,borsa , yemek tarifi apileri gibi)
     * client bilgileri appsettingste veya fazla ise db de tutulur access token her exp olduğunda direkt olarak dbye gidip alır
     */
    public class ClientTokenDto
    {
        public string AccessToken { get; set; } = default!;
        public DateTime AccessTokenExpiration { get; set; }
    }
}
