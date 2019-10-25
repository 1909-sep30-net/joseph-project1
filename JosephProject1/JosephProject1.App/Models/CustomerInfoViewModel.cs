namespace JosephProject1.App.Models
{
    public class CustomerInfoViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName { get => FirstName + " " + LastName; }
    }
}
