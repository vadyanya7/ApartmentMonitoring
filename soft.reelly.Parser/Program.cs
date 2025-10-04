using System.Text.Json;


// Это для парсинга сайта. 
class Program
{
	static readonly HttpClient client = new HttpClient();
	static async Task Main()
	{
		string baseSearchUrl = "https://api.reelly.io/api:sk5LT7jx/projectsExternalSearch?page=";
		string detailUrl = "https://api.reelly.io/api:sk5LT7jx/projects/";

		List<ProjectDetail> projects = new List<ProjectDetail>();

		// Получаем первую страницу, чтобы узнать pageTotal
		var firstPageResponse = await GetAsync<ProjectSummaryResponse>(baseSearchUrl + "1");

		if (firstPageResponse == null)
		{
			Console.WriteLine("Не удалось загрузить первую страницу.");
			return;
		}

		int totalPages = firstPageResponse.pageTotal;

		for (int page = 1; page <= totalPages; page++)
		{
			var pageData = await GetAsync<ProjectSummaryResponse>(baseSearchUrl + page);
			if (pageData == null) continue;

			foreach (var item in pageData.items)
			{
				var project = await GetAsync<ProjectDetail>(detailUrl + item.id);
				if (project != null)
				{
					projects.Add(project);
					Console.WriteLine($"Добавлен проект: {project.id}");
				}
			}
			Console.WriteLine($"Page: {page}");
		}

		Console.WriteLine($"Загружено {projects.Count} проектов.");
	}

	static async Task<T?> GetAsync<T>(string url)
	{
		try
		{
			var response = await client.GetAsync(url);
			if (!response.IsSuccessStatusCode)
			{
				Console.WriteLine($"Ошибка {response.StatusCode} при загрузке {url}");
				return default;
			}

			var json = await response.Content.ReadAsStringAsync();
			var result = JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return result;
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Ошибка при запросе {url}: {ex.Message}");
			return default;
		}
	}
}
public class ProjectSummaryResponse
{
	public int itemsReceived { get; set; }
	public int curPage { get; set; }
	public int nextPage { get; set; }
	public int? prevPage { get; set; }
	public int offset { get; set; }
	public int itemsTotal { get; set; }
	public int pageTotal { get; set; }
	public List<ProjectDetail> items { get; set; }
}

public class StartingPrice
{
	public int? Price_from_AED { get; set; }
	public int? Price_to_AED { get; set; }
}

public class Cover
{
	public string url { get; set; }
}

public class ProjectDetail
{
	public int id { get; set; }
	public List<object> Developer { get; set; }
	public string Project_name { get; set; }
	public string Developers_name { get; set; }
	public string sale_status { get; set; }
	public int multilier_uae { get; set; }
	public string Status { get; set; }
	public string Completion_date { get; set; }
	public long? Completion_time { get; set; }
	public string Area_name { get; set; }
	public List<StartingPrice> Starting_price { get; set; }
	public int min_price { get; set; }
	public string project_availability_id { get; set; }
	public int units_in_sale { get; set; }
	public int max_commission { get; set; }
	public object? Priority { get; set; } // null может быть любым типом, если не знаешь — оставь object
	public Cover cover { get; set; }
}
