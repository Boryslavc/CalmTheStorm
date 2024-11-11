public interface IPromtHandler
{
    public void HandlePromt();

    public void SetNexthandler(IPromtHandler nexthandler);

    public void HandleNext();
}
