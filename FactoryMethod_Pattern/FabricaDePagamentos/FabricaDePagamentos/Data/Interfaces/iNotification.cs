public interface iNotification
{
    string action { get; set; }
    string api_version { get; set; }
    iData data { get; set; }
    DateTime date_created { get; set; }
    long id { get; set; }
    bool live_mode { get; set; }
    string type { get; set; }
    long user_id { get; set; }
}

public interface iData
{
    public long id { get; set; }
}