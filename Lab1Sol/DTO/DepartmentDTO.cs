namespace Lab1Sol.DTO
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manger { get; set; }
        public string Location { get; set; }
        public List<string> StdsName { get; set; } = new List<string>();
    }
}
