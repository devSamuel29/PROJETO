using Microsoft.Extensions.Caching.Memory;

public class Jwt
{
    private readonly IMemoryCache _memoryCache;

    public Jwt(IMemoryCache memoryCache) => _memoryCache = memoryCache;
}
