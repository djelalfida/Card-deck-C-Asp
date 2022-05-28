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
    private List<CardReadDTO> _selectedCards = new List<CardReadDTO>();

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

    private void SelectCard(CardReadDTO card)
    {
        if (_selectedCards.Contains(card))
        {
            _selectedCards.Remove(card);
        }
        else
        {
            _selectedCards.Add(card);
        }
    }

    // next page 
    private async Task ChangePage(int backOrNext)
    {
        pageNumber = (int.Parse(pageNumber ?? "1") + backOrNext).ToString();
        _cards = null;
        await ShowAllCards();
    }

    private async Task NextPage()
    {
        await ChangePage(1);
    }
    private async Task PreviousPage()
    {
        await ChangePage(-1);
    }

    //private async Task CreateDeck()
    //{
    //    if (_selectedCards.Count == 0)
    //    {
    //        message = "You need to select at least one card";
    //        return;
    //    }

    //    DeckCreateDTO deck = new DeckCreateDTO
    //    {
    //        Cards = _selectedCards.Select(c => c.Id).ToList()
    //    };

    //    HttpResponseMessage response = await _httpClient.PostAsync("decks", new StringContent(JsonSerializer.Serialize(deck), Encoding.UTF8, "application/json"));

    //    if (response.IsSuccessStatusCode)
    //    {
    //        message = "Deck created";
    //        NavigationManager.NavigateTo("/");
    //    }
    //    else
    //    {
    //        message = "Something went wrong";
    //    }
    //}

}
