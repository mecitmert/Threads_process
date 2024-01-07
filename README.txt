# Threaded Sayı Analizi Projesi

Bu proje, 1'den 1.000.000'e kadar olan sayıları çift, tek ve asal olmak üzere üç kategoriye ayırarak analiz eden bir çoklu-thread uygulamasını içermektedir. Projenin temel amacı, işlemi hızlandırmak ve sayıları eşit parçalara bölmek için birden fazla thread kullanmaktır.

## Projenin Genel Yapısı

- `SonucKutusu` sınıfı, çift, tek ve asal sayıları saklamak için üç ayrı `ArrayList` içerir.
- `SayiTur` enum'u, sayı türlerini belirtir: Çift, Tek ve Asal.
- `Program` sınıfı, ana uygulama kodunu içerir. Bu sınıf, sayıları belirli aralıklarda analiz eden ve sonuçları `SonucKutusu`'na ekleyen birden fazla thread içerir.
- `BelirliAraliktaSayilariBul` fonksiyonu, belirli bir aralıktaki sayıları analiz eder ve sonuçları `SonucKutusu`'na ekler.
- `SayilariGoster` fonksiyonu, kullanıcıdan hangi türdeki sayıları görmek istediğini sorar ve sonuçları ekrana yazdırır.
- `CiftMi` ve `AsalMi` fonksiyonları, bir sayının çift ve asal olup olmadığını kontrol eder.

## Çalışma Prensibi

1. `Main` fonksiyonu, 1'den 1.000.000'e kadar olan sayıları içeren `orijinalListe`'yi oluşturur.
2. Sayıları dört eşit parçaya bölmek için `altListeBoyutu` belirlenir.
3. Sonsuz bir döngü başlar ve her döngüde dört farklı thread oluşturularak belirli aralıktaki sayıları analiz etmeleri için görevlendirilir.
4. Her bir thread, belirli aralıktaki sayıları analiz eder ve sonuçları `SonucKutusu`'na ekler.
5. `SayilariGoster` fonksiyonu, kullanıcıdan hangi türdeki sayıları görmek istediğini sorar ve sonuçları ekrana yazdırır.
6. Her döngü sonunda bir saniye bekleme yapılır.

## Thread Yönetimi

- Her thread, belirli bir aralıktaki sayıları analiz eder ve sonuçları `SonucKutusu`'na ekler.
- Thread'ler arasındaki eşzamanlılık, `Join` metodu kullanılarak sağlanır. Her thread birbirini bekler ve sırayla çalışır.
- `lock` anahtar kelimesi, paylaşılan kaynaklara (örneğin, `simdikiThreadNumarasi` değişkenine) eş zamanlı erişimi kontrol etmek için kullanılır.

