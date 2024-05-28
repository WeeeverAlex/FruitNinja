using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    // IDs do jogo para Android e iOS, definidos no Unity Dashboard
    [SerializeField] private string androidGameId = "5627609";
    [SerializeField] private string iosGameId = "5627608";
    // IDs das unidades de anúncio para Android e iOS
    [SerializeField] private string adUnitIdAndroid = "Rewarded_Android";
    [SerializeField] private string adUnitIdIos = "Rewarded_iOS";
    // Modo de teste para desenvolvimento
    [SerializeField] private bool testMode = true;

    private static AdsManager instance;
    private string gameId;
    private string adUnitId;
    private bool isAdLoaded = false;

    void Awake()
    {
        // Singleton pattern para garantir que apenas uma instância do AdsManager exista
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAds();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Inicializa o Unity Ads com os IDs de jogo apropriados para a plataforma
    public void InitializeAds()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            gameId = iosGameId;
            adUnitId = adUnitIdIos;
        }
        else
        {
            gameId = androidGameId;
            adUnitId = adUnitIdAndroid;
        }
        Advertisement.Initialize(gameId, testMode, this);
        Debug.Log("Initializing Ads with gameId: " + gameId);
    }

    // Carrega um anúncio
    public void LoadAd()
    {
        Debug.Log("Loading Ad...");
        Advertisement.Load(adUnitId, this);
    }

    // Mostra um anúncio se estiver carregado, caso contrário, tenta carregar novamente
    public void ShowAd()
    {
        if (isAdLoaded)
        {
            Debug.Log("Showing Ad...");
            Advertisement.Show(adUnitId, this);
        }
        else
        {
            Debug.LogWarning("Ad not ready yet.");
            LoadAd();
        }
    }

    // Callback chamado quando a inicialização do Unity Ads é concluída
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        LoadAd();
    }

    // Callback chamado quando a inicialização do Unity Ads falha
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    // Callback chamado quando um anúncio é carregado com sucesso
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad loaded: " + adUnitId);
        isAdLoaded = true;
    }

    // Callback chamado quando o carregamento de um anúncio falha
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        isAdLoaded = false;
    }

    // Callback chamado quando a exibição de um anúncio falha
    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    // Callback chamado quando a exibição de um anúncio é concluída
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(this.adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // Adicione aqui a lógica para recompensar o jogador
            LoadAd(); // Carrega o próximo anúncio
        }
    }
}