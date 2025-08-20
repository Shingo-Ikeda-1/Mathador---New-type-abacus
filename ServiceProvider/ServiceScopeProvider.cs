using UnityEngine;

public enum ServiceContext
{
    Development,
    Production
}

[DefaultExecutionOrder(-1)]
public class ServiceScopeProvider : MonoBehaviour
{
    [SerializeField] ServiceContext context;

    public IServiceScope Scope { get; internal set; }
    public static ServiceScopeProvider Instance { get; internal set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
            return;
        }

        DontDestroyOnLoad(gameObject); // Keep this instance across scenes
        Instance = this;

        if (context == ServiceContext.Development)
        {
            Scope = new DevelopmentServiceScope();
        }
        else
        {
            Scope = new ProductionServiceScope();
        }
    }
}

