namespace MultiWindowExample.Models
{
    public class ActiveWindow
    {
        public string Name { get; set; }
        public string NavigationServiceName { get; set; }
        public int Id { get; set; }
        public ViewLifetimeControl viewControl { get; set; }
    }
}
