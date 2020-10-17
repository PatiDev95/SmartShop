namespace WebApplication3.DTO
{
    public class GroupDetail
    {
        public GroupDetail(string name, int numberOfRepetitions)
        {
            Name = name;
            NumberOfRepetitions = numberOfRepetitions;
        }

        public string Name { get; set; }
        public int NumberOfRepetitions { get; set; }
    }
}
