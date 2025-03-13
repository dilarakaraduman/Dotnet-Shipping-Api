# Dotnet Shipping Api
● Müşterinin siparişi esnasında girilen desi bilgisine göre otomatik olarak
kargo firmasını seçen .NET API projesi geliştirilmesi hedeflenmektedir.
● Müşteri sipariş oluşturduğu esnada daha önce kargo firmalarına
tanımlayan veriler üzerinden siparişin desi miktarına göre kargo ücretinin
hesaplanması sağlanmalı, ilgili veriler sipariş tablosuna kayıt edilmelidir.
● Projeye ait gereklilik ve detaylar diğer başlıklar içerisinde verilmiştir.

Kullanılacak Tablo Bilgileri

● Kullanılacak tüm kolonların veri girişi zorunlu tutulmalıdır.
● Proje geliştirme akışına göre bu tablolara ek (genel yapıyı bozmayacak
şekilde) yardımcı tablolar eklenebilir ve kullanılabilir.

Geliştirilmesi Beklenen Servis ve İşlemler
Siparişler (Orders)
Siparişleri Listele
Veritabanında kayıtlı olan tüm kayıtlar listelenmelidir.
Sipariş Ekle
Gerekli parametreler doldurularak, veritabanına kayıt eklenmelidir.
1. Sipariş desisi herhangi bir kargo firmasının MinDesi-MaxDesi aralığına
giriyor ise;
a. O kargo firmasının fiyat değerleri alınmalı ve siparişin kargo ücreti
olarak kayıt edilmelidir. Bulunan kargo firmaları arasından en düşük
ücrete sahip 1 adet kargo firması seçilecektir.

2. Sipariş desisi herhangi bir kargo firmasının MinDesi-MaxDesi aralığına
girmiyor ise;
a. CarrierConfigurations tablosunda bulunan veriler içerisinden,
sipariş desi değerine en yakın olan kargo firmasının verileri gerekli
sorgular ile getirilir.
b. Getirilen kayıt/kayıtların fiyat ve +1 desi fiyatı bilgisi alınır.
c. Siparişe ait desi miktarı ile A maddesinde elde edilen kargo
firmasına ait en yakın desi miktarı arasındaki fark bulunur.
d. C maddesinde bulunan fark değeri ile +1 desi fiyatı çarpılarak kargo
fiyatı ile toplanır. Bu sayede kargo firmasının standart değeri +
aralığı aşan her bir desi için gerekli fiyatlandırma sağlanır.
e. D maddesinde elde edilen değer, veritabanına siparişin kargo ücreti
kabul edilerek veritabanına kayıt edilir.

İlgili mantıksal işlemlerin netleştirilebilmesi adına aşağıdaki örnek incelenebilir.
ÖRNEK:
Sipariş Desisi: 13
Kargo Min-Max Desi Aralıkları: 1-10
İlgili Desi Aralığındaki Kargo Fiyatı: 32
Kargo +1 Desi Fiyatı: 4
Hesaplanan Son Kargo Fiyatı (Sipariş tablosuna gidecek olan değer): 32 + (4 *
(13-10) ) = 44₺
Sipariş Sil
Gerekli parametreler ile veritabanındaki kayıt silinmelidir.

Kargo Firmaları (Carriers)
Kargo Firmalarını Listele
Tüm kayıtlar listelenmelidir.
Kargo Firması Ekle
Gerekli parametreler doldurularak, veritabanına kayıt eklenmelidir.
Kargo Firması Güncelle
Gerekli parametreler ile veritabanındaki kayıt güncellenmelidir.
Kargo Firması Sil
Gerekli parametreler ile veritabanındaki kayıt silinmelidir.
Kargo Firma Konfigürasyonları (CarrierConfigurations)
Kargo Firma Konfigürasyonlarını Listele
Tüm kayıtlar listelenmelidir.
Kargo Firması Konfigürasyonu Ekle
Gerekli parametreler doldurularak, veritabanına kayıt eklenmelidir.
Kargo Firması Konfigürasyonu Güncelle
Gerekli parametreler ile veritabanındaki kayıt güncellenmelidir.
Kargo Firması Konfigürasyonu Sil
Gerekli parametreler ile veritabanındaki kayıt silinmelidir.

Dikkat Edilmesi Gereken Hususlar
★ Yapılan tüm istekler sonucunda yapılan işleme ait bir string response
olarak dönülmelidir.
★ Örnek: “14 ID’li kayıt silindi”, “Sipariş eklendi”,” Kargo bilgileri güncellendi”
vs.

Kullanılacak Teknolojiler
(*) ile belirtilen teknoloji, mimari ve araçların kullanılması zorunludur.
● .NET Core 6 API (*)
● MSSQL (*)
● Swagger UI (*)
● Entity Framework - Code First Approach (*)
● Repository Design Pattern (*)
● N-Tier Architecture (*)
● CQRS Design Pattern - Mediatr (Tercihen)
● Onion Architecture (Tercihen)
● Veritabanı yönetimi için visual studio eklentileri dışında ekstra bir
veritabanı yönetim aracı kullanınız. (SSMS (SQL Management Studio),
Dbeaver, pgAdmin vs.)
