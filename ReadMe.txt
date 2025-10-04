
Перед запуском нужно накатить эти скрипты

создать миграцию
dotnet ef migrations add InitialCreate --project ApartmentMonitoring.Infrastructure --startup-project ApartmentMonitoring --context DataBaseContext

обновить базу
dotnet ef database update --project ApartmentMonitoring.Infrastructure --startup-project ApartmentMonitoring --context DataBaseContext

В appsettings.Development.json нужно добавить подключение к бд 
DefaultConnection это локальная бд на постгресе.
SupabaseDb это к supabase подключение.
___________________________________________
soft.reelly.Parser

Здесь утилитка на парсинг сайта. Что-бы запустить нужно в студии поставить "Set as Startup Project".
Она никуда не сохраняет (ни в бд ни в файл) 

AppartmentMonitoring.TelegramParser

Здесь утилита для парсинга сообщений групп Телеграма. Что-бы запустить нужно в студии поставить "Set as Startup Project".
Она никуда не сохраняет (ни в бд ни в файл). Нужно добавить номер телефона, api_hash ,_apiHash и id-шники групп. Сейчас там хардкод. Попробуй через ChatGPT
сделать гибкие настройки. Примернsй промт : Вот код (см. TelegramWorker.cs), хочу что-бы я мог вынести название групп, номер, api_hash ,_apiHash в конфиг файл.

Архитектура приложения 
 Всё начиналось по Clean Architecture https://medium.com/@mohanedzekry/clean-architecture-in-asp-net-core-web-api-d44e33893e1d
Базовая концепция. Это нужно для того что-бы всё лежало по разным слоям и файлам. Так любому будет легче разобратся, если он первый раз видит проект.
Работа с бд в одном месте, бизнес логика в другом и т.д.



ApartmentMonitoring.Entity

Здесь находятся сущности которые храняться в БД. Для Supabase и наши собственные

ApartmentMonitoring.Application

Здесь бизнес-логика. NotificationService, INotificationService и IHubNotifier тебе здесь только это нужно.

NotificationService.cs cсылается на работу с нашей бд, не с Supabese
Там вверху єтого класса есть репозитории 
		private readonly IApartmentRepository _apartmentRepository;
		private readonly IUserRepository _userRepository;
		private readonly INotificationRepository _notificationRepo;
		private readonly ISubscriptionRepository _subscriptionRepository;

	Это интерфейсы которые ссылаются  на реализацию классов, реализация сделана на НАШУ бд. Нужно сделать реализацию на Supabase (подключить SupabeseContext вместо DataBaseContext и т.д.)


ApartmentMonitoring.Infrastructure
Здесь находится взаимодействие с БД	(Supabese и собственной).

 Здесь начинает работу джоб по нотификациям. Он срабатівает каждую минуту и проверяет все новые листинги и сравнивает их с подписками юзеров.
BackgroundServices->NotificationJob 
HubNotifier.сs
NotificationHub.cs
UserConnectionTracker.cs

Эти классы отвечают за нотификации


ApartmentMonitoring

ConfigureServiceExtension.cs 
Program.cs
В этих классах регистрации интрфейсов через Dependency Injection
Репозиториев, остальной бизнес логики 



