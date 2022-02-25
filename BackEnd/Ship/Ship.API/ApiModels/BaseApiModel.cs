namespace Ship.API.ApiModels
{
    public class BaseApiModel
    {
        public int Id { get; set; }
        public bool IsDeleted { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime? ModifiedOn { get; private set; }
    }
}
