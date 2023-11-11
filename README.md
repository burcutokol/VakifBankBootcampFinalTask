
# Vakıfbank Full Stack Bootcamp Final Task
- Bu proje, bir sipariş yönetim sistemini tasarlamak için geliştirilen bir uygulamayı ifade etmektedir. Sistem, iki ana kullanıcı rolü olan Admin ve Bayi'yi desteklemektedir. Bayi, sistemdeki ürünleri görüntüleyebilir, sipariş oluşturabilir ve kendi sipariş geçmişini görebilir. Kredi kartı, EFT, havale veya açık hesap gibi ödeme seçenekleri arasından tercih yapabilir. Açık hesap ödemelerinde bayilerin limit kontrolü yapılır. Bayilerin belirli bir kar marjına sahip olmaları ve admin tarafından tanımlanan fiyatlar üzerinden ürünleri görmeleri sağlanır.

- Sipariş işlemlerinde stok kontrolü yapılır ve stoklar otomatik olarak güncellenir. Siparişler, admin onayından geçer ve fatura bilgileri oluşturulur. Şirket ve bayi arasında iletişim amacıyla bir mesajlaşma altyapısı sağlanır. Sistem, JWT token kullanarak kullanıcı yetkilendirmesi sağlar.



## Kullanılan Teknolojiler

**Veritabanı** :
MSSQL

**Yetkilendirme**:
JWT Token

**Veritabanı Erişim Katmanı**:
Entity Framework (EF)

**Veritabanı İşlemleri Yönetimi**:
Repository ve Unit of Work Deseni

**API Dokümantasyon**:
Postman ve Swagger

  
## Ekler
Projeye ait Postman dokümantasyonuna ulaşabilirsiniz.

[![Run In Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/27941015-6ab0ba54-9888-43ea-ad08-099dc82471a8?action=collection%2Ffork&source=rip_markdown&collection-url=entityId%3D27941015-6ab0ba54-9888-43ea-ad08-099dc82471a8%26entityType%3Dcollection%26workspaceId%3D23dcce72-fd9b-47e3-a114-bf70acd77dcf)
