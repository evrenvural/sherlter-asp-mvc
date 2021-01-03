# Wep Programlama Sahiplendirme Sitesi
## Numara: G181210373
## Ad Soyad: Evren VURAL
## Ders Grubu 1 / C
## Github: https://github.com/evrenvural/sherlter-asp-mvc
## Youtube: https://www.youtube.com/watch?v=Ux5rccYjd20

Sahiplendirme Web Sitesi, Barınaklardaki köpeklerin listelendiği ve onları sahiplenmenizi sağlayan bir web sitesidir. Ana sayfada listelenen köpeklere tıklayıp detay sayfasına gidebilir ve onlara sahiplenme isteği gönderebilirsiniz. Bu istekler, admin sayfasında request tablosuna düşer ve admin, bu sayfada kimin isteği attığını ve ne zaman attığını görebilir. Admin eğer isteği kabul ederse sahiplenme gerçekleşir. Red ederse sahiplenme gerçekleşmez. Bir user birden fazla köpeğe istekte bulunabilir.

Ödevde tabloları sql bir veritabanı olan mssql’de tuttum. Bu veritabanına ise ORM framework’uü olan Entity Framework ile ulaştım. Köpek, User (Identity User’dan kalıtım alır.) ve Dog olmak üzere 3 ayrı Entity sınıfı oluşturdum. Köpek sınıfı içinde User field’ı bulunur. Buradaki amaç eğer sahiplenme gerçekleşmişse hangi kullanıcı tarafından sahiplenildiği bilgisini tutmaktır. Request tablosunda ise hem dog sınıfının objesi hem de user sınıfının bir objesi tutulur. Buradaki amaç ise köpek ile user arasındaki bağlantıyı sağlayıp sahiplenilmeyi kontrol etmektir.

Ödevde mümkün olduğunca boş sınıf bırakmamaya çalışarak Base abstract classlar oluşturdum. Bu class’ları oluşturmamdaki amaç ise ortak özellikleri tek bir yerden kontrol etmek istememdi.
Ön tarafta ise genel olarak bootstrap tema yapısını kullandım. Köpeklerin listelendiği ana sayfada ise CSS’in grid özelliğinden faydalandım.
Ödeve ait ekran görüntüleri aşağıdadır.
