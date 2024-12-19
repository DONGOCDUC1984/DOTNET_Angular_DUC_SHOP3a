using Newtonsoft.Json;
using StackExchange.Redis;

namespace DOTNET_Angular_DUC_SHOP3a.Redis
{
    public class RedisCacheService : IRedisCacheService
    {
        private IDatabase _db;
        public RedisCacheService()
        {
            ConfigureRedis();
        }
        private void ConfigureRedis()
        {
            var redis = ConnectionMultiplexer.Connect("localhost:6379");
            _db = redis.GetDatabase();
        }
        public T GetData<T>(string key)
        {
            var value = _db.StringGet(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            return default;
        }
        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
            var isSet = _db.StringSet(key, JsonConvert.SerializeObject(value), expiryTime);
            return isSet;
        }
        public object RemoveData(string key)
        {
            bool _isKeyExist = _db.KeyExists(key);
            if (_isKeyExist == true)
            {
                return _db.KeyDelete(key);
            }
            return false;
        }

    }
}
