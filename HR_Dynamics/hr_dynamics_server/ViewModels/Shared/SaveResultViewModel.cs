namespace hr_dynamics_server.ViewModels.Shared
{
    public class SaveResultViewModel<T> 
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public T? Result { get; set; }
    }
}
