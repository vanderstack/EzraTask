namespace EzraTask.Api.Services;

#if DEBUG
public interface IResettableService
{
    void ResetStateForTests();
}
#endif