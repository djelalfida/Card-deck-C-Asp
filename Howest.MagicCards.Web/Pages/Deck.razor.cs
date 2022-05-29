using Howest.MagicCards.Shared.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Howest.MagicCards.Web.Pages;

public partial class Deck
{
    private const int MaxSelectedCards = 60;

    private string message = string.Empty;

    private string title = "Card Deck Builder";
    
    private IEnumerable<CardReadDTO>? _cards = null;
    private List<CardReadDTO> _selectedCards = new List<CardReadDTO>();

    private IEnumerable<DeckReadDTO>? _decks = null;
    private string? _deck = string.Empty;

    private IEnumerable<ArtistReadDTO>? _artists = null;
    private IEnumerable<RarityReadDTO>? _rarities = null;

    private string SelectedSorting { get; set; } = "asc";
    private string NameFilter { get; set; } = string.Empty;
    private string ArtistFilter { get; set; } = string.Empty;
    private string RarityFilter { get; set; } = string.Empty;

    private string? _deckName = string.Empty;

    private readonly JsonSerializerOptions _jsonOptions;
    private HttpClient _httpClient;
    private HttpClient _httpDeckApiClient;

    #region Services
    [Inject]
    public IHttpClientFactory? HttpClientFactory { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IMapper Mapper { get; set; }

    [Inject]
    public ProtectedLocalStorage storage { get; set; }
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
        _httpDeckApiClient = HttpClientFactory.CreateClient("DecksAPI");

        await ShowAllCards();
        await ShowAllArtists();
        await ShowAllRarities();
        await GetAllDecks();
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            List<CardReadDTO>? selectedCards = await ReadSelectedCards();
            if (selectedCards != null)
            {
                _selectedCards = selectedCards;
                StateHasChanged();
            }
        }
    }
 

    private async Task ShowAllCards()
    {
        string nameFilter = NameFilter != string.Empty ? $"&name={NameFilter}" : string.Empty;
        string artistFilter = ArtistFilter != string.Empty ? $"&artistid={ArtistFilter}" : string.Empty;
        string rarityFilter = RarityFilter != string.Empty ? $"&raritycode={RarityFilter}" : string.Empty;
        HttpResponseMessage response = await _httpClient.GetAsync($"cards?pagenumber={pageNumber ?? "1"}&sort={SelectedSorting}{nameFilter}{artistFilter}{rarityFilter}");

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

    private async Task ShowAllArtists()
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"artists");

        string apiResponse = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            IEnumerable<ArtistReadDTO>? result = JsonSerializer.Deserialize<IEnumerable<ArtistReadDTO>>(apiResponse, _jsonOptions);

            _artists = result;
        }
        else
        {
            _artists = new List<ArtistReadDTO>();
        }
    }
    
    private async Task ShowAllRarities()
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"cards/rarity");

        string apiResponse = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            IEnumerable<RarityReadDTO>? result = JsonSerializer.Deserialize<IEnumerable<RarityReadDTO>>(apiResponse, _jsonOptions);

            _rarities = result;
        }
        else
        {
            _rarities = new List<RarityReadDTO>();
        }
    }

    private async Task GetAllDecks()
    {
        HttpResponseMessage response = await _httpDeckApiClient.GetAsync("");

        string apiResponse = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            PagedResponse<IEnumerable<DeckReadDTO>>? result = JsonSerializer.Deserialize<PagedResponse<IEnumerable<DeckReadDTO>>>(apiResponse, _jsonOptions);

            _decks = result?.Data;
        }
        else
        {
            _decks = new List<DeckReadDTO>();
        }
    }

    private async Task DeleteDeck()
    {
        HttpResponseMessage response = await _httpDeckApiClient.DeleteAsync($"{_deck}");

        string apiResponse = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            message = $"Deck {_deck} has been deleted";
        }
        else
        {
            message = "Deck could not be deleted";
        }
    }

    private async Task SortCards(ChangeEventArgs e)
    {
        SelectedSorting = e.Value is not null ? e.Value.ToString() : "asc";
        _cards = null;
        await ShowAllCards();
        StateHasChanged();
    }

    private async Task FindCardByName(ChangeEventArgs e)
    {
        NameFilter = e.Value != null ? e.Value.ToString() : string.Empty;
        _cards = null;
        await ShowAllCards();
        
    }

    private async Task FindCardByArtist(ChangeEventArgs e)
    {
        ArtistFilter = e.Value != null ? e.Value.ToString() : string.Empty;
        _cards = null;
        await ShowAllCards();
    }

    private async Task FilterCardByRarity(ChangeEventArgs e)
    {
        RarityFilter = e.Value != null ? e.Value.ToString() : string.Empty;
        _cards = null;
        await ShowAllCards();
    }

    private async void SelectCard(CardReadDTO card)
    {
        if (_selectedCards.Count == MaxSelectedCards)
        {
            if (_selectedCards.Contains(card))
            {
                _selectedCards.Remove(card);
            }

            message = $"You can only select {MaxSelectedCards} cards";

        }
        else
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
        await SaveSelectedCards();
    }

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

    private async Task SaveSelectedCards()
    {
        await storage.SetAsync("selectedCards", _selectedCards);
    }
    
    private async Task<List<CardReadDTO>?> ReadSelectedCards()
    {
        ProtectedBrowserStorageResult<List<CardReadDTO>> storageResult = await storage.GetAsync<List<CardReadDTO>>("selectedCards");
        return storageResult.Success ? storageResult.Value : new List<CardReadDTO>();
    }

    private async Task CreateDeck()
    {

        if (_selectedCards.Count != MaxSelectedCards)
        {
            message = $"You must select {MaxSelectedCards} cards";
            return;
        }

        DeckWriteDTO deck = new DeckWriteDTO
        {
            Cards = _selectedCards.Select(c => c.Id).ToList(),
            Name = _deckName
        };

        HttpContent content =
            new StringContent(JsonSerializer.Serialize(deck), Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpDeckApiClient.PostAsync("create", content);      

        if (response.IsSuccessStatusCode)
        {
            message = "Deck created";
            NavigationManager.NavigateTo("/deck");
        }
        else
        {
            message = "Something went wrong";
        }
    }



}
