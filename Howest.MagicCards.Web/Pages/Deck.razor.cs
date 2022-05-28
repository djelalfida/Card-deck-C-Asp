using Microsoft.AspNetCore.Components;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Howest.MagicCards.Web.Pages;

public partial class Deck
{
    private string message = string.Empty;

    private string title = "Card Deck Builder";

    private IEnumerable<CardReadDTO>? _cards = null;
    private readonly JsonSerializerOptions _jsonOptions;
    private HttpClient _httpClient;

    #region Services
    [Inject]
    public IHttpClientFactory? HttpClientFactory { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IMapper Mapper { get; set; }
    #endregion

    #region Parameters
    [Parameter]
    public string? pageNumber { get; set; }
    #endregion

    public Deck()
    {
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    protected override async Task OnInitializedAsync()
    {
        _httpClient = HttpClientFactory.CreateClient("CardsAPI");

        await ShowAllCards();
    }

    private async Task ShowAllCards()
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"cards?pagenumber={pageNumber ?? "1"}");

        string apiResponse = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            PagedResponse<IEnumerable<CardReadDTO>>? result = JsonSerializer.Deserialize<PagedResponse<IEnumerable<CardReadDTO>>>(apiResponse, _jsonOptions);

            _cards = result?.Data;
        }
        else
        {
            _cards = new List<CardReadDTO>();
        }            
    }
}
