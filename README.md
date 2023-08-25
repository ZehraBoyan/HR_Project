# İnsan Kaynakları Yönetim Paneli Projesi

Bu proje, şirketlerin insan kaynakları yönetimini kolaylaştırmak amacıyla geliştirilen bir Web API uygulaması içerir. Uygulama, ASP.NET Core ve en iyi uygulama yöntemlerini kullanarak tasarlanmıştır. İnsan kaynakları yöneticileri için gelişmiş bir panel sunmak üzere farklı katmanlardan oluşmaktadır.

## Özellikler

- Gelişmiş rol yönetimi: Proje, "admin" ve "kullanıcı" olmak üzere iki farklı rolü destekler. Kullanıcıların erişebileceği özellikler rol bazında kontrol edilir.
- Şirket personel bağları: Çalışanların ve şirketlerin ilişkilendirilmesini sağlayan kapsamlı bir sistem sunar.
- Katmanlı Mimari: Projede ayrı ayrı katmanlar (Service, Repository, Core, API, Web/UI) kullanılarak yüksek düzeyde modülerlik ve sürdürülebilirlik sağlanmıştır.

## Projeyi Çalıştırma

1. **Gereksinimler:**
   - .NET Core SDK
   - Veritabanı Sunucusu (örneğin, SQL Server)

2. **Ayarlar:**
   - `appsettings.json` dosyasında veritabanı bağlantı dizesini güncelleyin.

3. **Veritabanı İşlemleri:**
   - Konsolda, `dotnet ef database update` komutunu kullanarak veritabanını oluşturun ve güncelleyin.

4. **Proje Çalıştırma:**
   - Terminalde, proje klasörüne gidin.
   - `dotnet run` komutunu kullanarak uygulamayı başlatın.

5. **API Erişimi:**
   - Uygulama başarıyla başlatıldıktan sonra API'ye `https://localhost:5001` adresinden erişebilirsiniz.

## Katmanlar

- **Core:** Temel veri modelleri ve iş mantığı burada yer alır.
- **Repository:** Veritabanı ile iletişimi yönetir.
- **Service:** İş mantığının uygulandığı katmandır.
- **API:** API kontrolleri burada bulunur. İsteklerin işlenmesi ve cevapların dönülmesi sağlanır.
- **Web/UI:** Kullanıcı arayüzü katmanıdır. Bu projede sadece API üzerinden erişim sağlanmıştır.


