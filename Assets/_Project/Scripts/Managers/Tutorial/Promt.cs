
public class Promt
{
    public readonly PromtConfig Config;

    public Promt(PromtConfig config)
    {
        this.Config = config;
    }

    public void Start()
    {
        var handler = new PromtEnableHandler(this);

        handler.HandlePromt();
    }
}