using TL;
using WTelegram;

// Это утилитка для парсинга телеграмма. 
class TelegramGroupParser
{
	private static Client _client;

	// Настройки  к доступам. Нужно подставить свои.
	// ToDO внести в отдельный файл
	
	//-----------------------------------
	private static string _apiHash = "";
	private static int _apiId = 24108788;
	private static string _phoneNumber = "";

	private static HashSet<long> _targetChatIds = new HashSet<long>
	{
		//ids of chats
		1541139194,
		4863004505
	};

	//-----------------------------------
	static async Task Main(string[] args)
	{
		Console.WriteLine("Telegram Group Message Parser with WTelegramClient");
		Console.WriteLine("=================================================\n");

		_client = new Client(Config);

		// подписываемся на обновления
		_client.OnUpdates += Client_Update;

		Console.WriteLine("Подключаемся к Telegram...");
		var user = await _client.LoginUserIfNeeded();
		Console.WriteLine($"Вы вошли как {user.username ?? user.first_name + " " + user.last_name} ({user.id})");

		// Загружаем список чатов, чтобы проверить доступность
		var dialogs = await _client.Messages_GetAllDialogs();
		foreach (var chatId in _targetChatIds)
		{
			if (dialogs.chats.TryGetValue(chatId, out var chat))
			{
				Console.WriteLine($"Мониторим чат: {chat.Title} (ID: {chatId})");
			}
			else
			{
				Console.WriteLine($"Чат {chatId} не найден или нет доступа");
			}
		}

		Console.WriteLine("\nОжидание новых сообщений... (Нажмите Enter для выхода)");
		Console.ReadLine(); // держим программу открытой, пока пользователь не нажмет Enter
	}

	private static async Task Client_Update(IObject update)
	{
		if (update is UpdatesBase updates)
		{
			foreach (var upd in updates.UpdateList)
			{
				if (upd is UpdateNewMessage { message: Message msg })
				{
					if (_targetChatIds.Contains(msg.Peer.ID))
					{
						Console.WriteLine("\nНовое сообщение в целевом чате:");
						await PrintMessage(msg);
					}
				}
			}
		}
	}

	private static async Task PrintMessage(Message msg)
	{
		Console.WriteLine($"[{msg.date.ToLocalTime():HH:mm:ss}]");

		if (!string.IsNullOrEmpty(msg.message))
			Console.WriteLine($"Текст: {msg.message}");

		switch (msg.media)
		{
			case MessageMediaPhoto { photo: Photo photo }:
				await DownloadPhoto(photo,msg.Date);
				Console.WriteLine("Медиа: фото");
				break;
			case MessageMediaDocument doc:
				Console.WriteLine($"Медиа: документ ({doc.document.GetType().Name})");
				break;
			case MessageMediaWebPage wp:
				Console.WriteLine($"Ссылка: {wp.webpage.Url}");
				break;
		}

		if (msg.reply_markup is ReplyInlineMarkup)
			Console.WriteLine("Содержит inline-кнопки");
	}

	private static async Task DownloadPhoto(Photo photo, DateTime date)
	{
		try
		{
			// Выбираем фото с максимальным разрешением
			var photoSize = photo.sizes
				.OfType<PhotoSize>()
				.OrderByDescending(ps => ps.w)
				.FirstOrDefault();

			if (photoSize == null) return;

			// Получаем информацию о файле
			var fileLocation = new InputPhotoFileLocation
			{
				id = photo.id,
				access_hash = photo.access_hash,
				file_reference = photo.file_reference,
				thumb_size = photoSize.type
			};

			// Создаем уникальное имя файла
			var fileName = $"{date:yyyyMMdd_HHmmss}_photo_{photo.id}.jpg";
			var filePath = Path.Combine("A:\\eaes", fileName);

			// Скачиваем файл
			using var fileStream = File.Create(filePath);
			await _client.DownloadFileAsync(fileLocation, fileStream);

			Console.WriteLine($"Фото сохранено: {filePath}");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Ошибка при загрузке фото: {ex.Message}");
		}
	}
	static string Config(string what)
	{
		return what switch
		{
			"api_id" => _apiId.ToString(),
			"api_hash" => _apiHash,
			"phone_number" => _phoneNumber,
			"session_pathname" => "session.dat",
			_ => null
		};
	}
}
